import { Component, OnInit, Injector, ReflectiveInjector, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { IDataSourceService } from './index';
import { Observable, throwError } from 'rxjs';
import { ValidationRule, FormValidationService } from './index';
import { IViewFormService } from './index';
import { ICreateFormService } from './index';
import { IUpdateFormService } from './index';
import { NotificationService } from '../dialog/notification.service';


/**
* DataSource format, which receive from server
*/
export interface DataSource {
  value: any;
  name: any;
}
export class FormMode {
  public static VIEW: number = 2;
  public static CREATE: number = 4;
  public static EDIT: number = 8;
  public static ALL: number = 2 | 4 | 8;
}
export class FormLink {
  listPageUrl: string;
  viewFormUrl: string;
}
export class FormEvent {
  onAfterInitFormData: Function = function () { };
  onBeforeInitFormData: Function = function () { };
}
export class FormDataSourceMap {
  public name: string;
  public source?: string | IDataSourceService | Function;
  public onAfterGetData?: Function;
  public formMode: number = null;

}
export class FormDataSourceMapper {
  private _maps: Array<FormDataSourceMap> = new Array<FormDataSourceMap>();

  public add(name: string, source: string | Function | IDataSourceService,
    onAfterGetData?: Function,
    formMode?: number): FormDataSourceMap {
    return this.addForMode(FormMode.ALL, name, source, onAfterGetData);
  }
  public addForMode(
    formMode: number,
    name: string,
    source: string | Function | IDataSourceService,
    onAfterGetData?: Function): FormDataSourceMap {
    let map = new FormDataSourceMap();
    map.name = name;
    map.source = source;
    map.onAfterGetData = onAfterGetData;
    map.formMode = formMode;
    this._maps.push(map);
    return map;
  }


  public get(name: string): FormDataSourceMap {
    return this._maps.find(p => p.name == name);
  }

  public getMap(): Array<FormDataSourceMap> {
    return this._maps;
  }
}

export class FormConfiguration {
  public events: FormEvent = new FormEvent();
  public dataSourceMapper: FormDataSourceMapper = new FormDataSourceMapper();

}

export class FormError {
  public message: string;
  public error: any;

  constructor(message: string, error?: any) {
    this.message = message;
    this.error = error;
  }
}
export abstract class FormBaseComponent {

  @ViewChild("mainForm") mainFormElement: FormGroup;
  protected _formMode: string;      // origin formMode as string: create, view,edit
  protected _formModeCode: number;  // formMode as number: 2,4,8
  protected _defaultFormData: object;
  public formId: number;
  public isViewFormMode: boolean;
  public isCreateFormMode: boolean;
  public isUpdateFormMode: boolean;

  public formName: string;
  public formTitle: string;
  public formModeName: string;

  protected formConfiguration: FormConfiguration = new FormConfiguration();

  public formData: any = {};
  protected viewFormService: IViewFormService;
  public formErrors: any = {};
  public formDataSource: any = {};
  public formLinks: FormLink = new FormLink();

  public resetFormData: Function = function () { };
  public canAccess: Function = function () { return true };
  public Access: boolean = true;


  private _validationRules: Array<ValidationRule> = new Array<ValidationRule>();
  constructor(
    protected activeRoute: ActivatedRoute
    , protected router: Router
    , protected notificationService: NotificationService
    , protected formService: IViewFormService | any
    , protected validationService: FormValidationService
    , private createFormService?: ICreateFormService
    , private editFormService?: IUpdateFormService
  ) {
    this.formName = "Form";
    this.viewFormService = formService;
    // Init Service
    if (createFormService === undefined || createFormService == null) {
      this.createFormService = formService;
      this.editFormService = formService;
    }
  }

  /**
   * Call this function once
   */
  protected formOnInit(formName: string, defaultFormData: object) {
    this.formName = formName;

    this._defaultFormData = defaultFormData;
    // Init Route
    this.activeRoute.params.subscribe((param: Params) => {
      this._formMode = param['mode'].toLowerCase();
      if (this.canAccess(this._formMode)) {
        this.constructorForFormMode(this._formMode, param['id']);
        this.constructorForFormData();
      }
      else {
        this.router.navigate(['/401']);
      }
    });
  }

  public deepClone(source) {
    return JSON.parse(JSON.stringify(source));
  }

  private constructorForFormMode(_formMode: string, formId: any) {
    this.formId = formId;
    this.isViewFormMode = false;
    this.isCreateFormMode = false;
    this.isUpdateFormMode = false;

    

    switch (this._formMode) {
      case "create":
        this.isCreateFormMode = true;
        this.formTitle = `Create ${this.formName}`;
        this.formModeName = "Create";
        this._formModeCode = FormMode.CREATE;
        break;
      case "update":
        this.isUpdateFormMode = true;
        this.formTitle = `Update ${this.formName}`;
        this.formModeName = "Edit";
        this._formModeCode = FormMode.EDIT;
        break;
      case "view":
        this.isViewFormMode = true;
        this.formTitle = `View ${this.formName}`;
        this.formModeName = "View";
        this._formModeCode = FormMode.VIEW;
        break;
      default:
        this.router.navigate(["home"]);
        alert(`Form mode = <${_formMode}> is not valid.`)
        //throw new Error(`Form mode = <${_formMode}> is not valid.`);
        break;
    }
    this.resetFormData(this.formData);
    this.formLinks.listPageUrl = this.getListPageUrl();
    this.formLinks.viewFormUrl = this.getViewFormUrl(this.formId);
  }

  private constructorForFormData() {
    // Form data
    if (this.isViewFormMode) {
      this.viewFormService.getFormData(this.formId).subscribe(data => {
        this.formConfiguration.events.onBeforeInitFormData(data);
        this.formData = data;
        this.formConfiguration.events.onAfterInitFormData(this.formData);
        this.constructorForFormDataSource();
      }, error => { this.router.navigate(['/404']) });
    } else if (this.isUpdateFormMode) {
      this.viewFormService.getFormData(this.formId).subscribe(data => {
        this.formConfiguration.events.onBeforeInitFormData(data);
        this.formData = data;
        this.formConfiguration.events.onAfterInitFormData(this.formData);
        this.constructorForFormDataSource();
      });
    } else {
      this.formConfiguration.events.onBeforeInitFormData(this._defaultFormData);
      this.formData = this._defaultFormData;
      this.formConfiguration.events.onAfterInitFormData(this.formData);
      this.constructorForFormDataSource();
      
    }
  }

  private constructorForFormDataSource() {
    // Data source data
    for (let map of this.formConfiguration.dataSourceMapper.getMap()) {

      let mapDataSourceObs: Observable<any>;
      if ((map.formMode & this._formModeCode) !== this._formModeCode) {
        // Ignore if user is in  diff context
        continue;
      }
      if ((map.source as IDataSourceService).getDataSource !== undefined) {
        mapDataSourceObs = (map.source as IDataSourceService).getDataSource();
      }
      else if (typeof (map.source) === 'function' || map.source instanceof Function) {
        mapDataSourceObs = (map.source as Function)();
      }
      mapDataSourceObs.subscribe(res => {
        if (map.onAfterGetData !== undefined && map.onAfterGetData !== null) {
          this.formDataSource[map.name] = map.onAfterGetData(res);
        } else {
          this.formDataSource[map.name] = res;
        }
      });
    }
  }

  // TODO: Improve to get url automatically
  private getListPageUrl(): string {
    if (this.isUpdateFormMode) {
      return this.editFormService.getListPageUrl();
    }
    return this.createFormService.getListPageUrl();
  }


  private getViewFormUrl(formId: any): string {
    if (this.isUpdateFormMode) {
      return this.editFormService.getViewFormUrl(formId);
    }
    return this.createFormService.getViewFormUrl(formId);
  }

  protected setValueIfUndefined(target: any, property: string, value: any) {
    if (target[property] === undefined || target[property] === null) {
      target[property] = value;
    }
  }

  public onNavigateToListPage() {
    this.router.navigateByUrl(this.getListPageUrl());
  }

  public onNavigateToViewPage() {
    this.navigateToViewPage(this.formId);
  }

  private navigateToViewPage(formId: any) {
    this.router.navigateByUrl(this.getViewFormUrl(formId));
  }

  public onSubmitForm(formGroup: NgForm) {
    this.formErrors = {};
    let validationResult = this.validationService.validateForm(formGroup, this._validationRules);
    if (!validationResult.isValid) {
      this.formErrors = validationResult.errors;
      console.log(this.formErrors);
      return;
    }
    

    this.submitForm(formGroup).subscribe(res => {
      formGroup.reset();
      if (this.isCreateFormMode) {
        this.navigateToViewPage(res.value);
      } else {
        this.navigateToViewPage(this.formId);
      }
    },
      err => {
        console.log(err);
        if (err.status === 400) {
          this.notificationService.prompError(err.error.message);
        } else {
          this.notificationService.prompError(err.message)
        }
      })
  }

  public changeToMode(formMode: string, id: any) {
    this._formMode = formMode;
    this.constructorForFormMode(formMode, { id: id });
  }

  public submitForm(formGroup: NgForm): Observable<any> {
    if (this.isUpdateFormMode) {
      return this.editFormService.edit(this.formId, this.formData);
    }
    else if (this.isCreateFormMode) {
      return this.createFormService.create(this.formData);
    }
    else if (this.isViewFormMode) {

    } 
    return throwError(new FormError(`Mode not support`));
  }


  protected reloadPage() {
    window.location.reload();
  }
  protected refreshForm() {
    this.constructorForFormData();
  }


  protected addValiationRule(validationRule: ValidationRule) {
    this._validationRules.push(validationRule);
  }
}
