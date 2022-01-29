using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using Project.Exceptions;
using Project.Models;

namespace Project.Extentions
{
    public class ModelStateCheckFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var err in item.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }

                throw new ValidationException(errors);

                //context.Result = new Response<Guid>(ResponseStatus.BadRequest, errors).GetJsonResult();
            }
        }
    }
}