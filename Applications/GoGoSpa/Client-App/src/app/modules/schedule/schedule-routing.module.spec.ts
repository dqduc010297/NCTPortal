import { ScheduleRoutingModule } from './schedule-routing.module';

describe('ScheduleRoutingModule', () => {
  let scheduleRoutingModule: ScheduleRoutingModule;

  beforeEach(() => {
    scheduleRoutingModule = new ScheduleRoutingModule();
  });

  it('should create an instance', () => {
    expect(scheduleRoutingModule).toBeTruthy();
  });
});
