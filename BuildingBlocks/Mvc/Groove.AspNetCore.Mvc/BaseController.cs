using Groove.AspNetCore.Common.Identity;
using Groove.AspNetCore.Common.Messages;
using Groove.AspNetCore.Mvc.ActionResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Groove.AspNetCore.Mvc
{
    public abstract class BaseController : ControllerBase
    {
        public virtual string GetCurrentUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public virtual T GetCurrentUserId<T>()
        {
            return (T)Convert.ChangeType(GetCurrentUserId(), typeof(T));
        }

        public virtual string GetCurrentUserName()
        {
            return this.User.FindFirst(ClaimTypes.Name).Value;
        }


        public virtual UserIdentity<T> GetCurrentIdentity<T>()
        {
            return new UserIdentity<T>
            {
                Id = GetCurrentUserId<T>(),
                UserName = GetCurrentUserName()
            };
        }

        public virtual OkObjectResult OkValueObject(object value)
        {
            return Ok(new OkValueModel(value));
        }

        public new virtual BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            return base.BadRequest(CreateExceptionMessage(modelState));
        }

        public virtual BadRequestObjectResult BadRequest(string message)
        {
            return base.BadRequest(new ExceptionMessage(message));
        }

        private static ExceptionMessage CreateExceptionMessage(ModelStateDictionary modelState)
        {
            var result = new ExceptionMessage();

            result.Details = new List<ExceptionMessage>();

            // Add Error detail
            foreach (var state in modelState)
            {
                var stateError = new ExceptionMessage();

                foreach (var childError in state.Value.Errors)
                {
                    result.Details.Add(new ExceptionMessage
                    {
                        Message = childError.ErrorMessage
                    });
                }
            }

            if (result.Details.Any())
            {
                result.Message = result.Details.First().Message;
            }
            else
            {
                result.Details = null;
            }

            return result;
        }
    }
}
