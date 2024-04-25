namespace OfficialWebsite.Core.Filters
{
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Reflection;
    using ValidationResult = FluentValidation.Results.ValidationResult;

    public class ModelStateActionFilter : Attribute, IActionFilter
    {
        private readonly string? validatorName;
        private readonly bool modelState;

        public ModelStateActionFilter(string? validatorName = null, bool modelState = true)
        {
            this.validatorName = validatorName;
            this.modelState = modelState;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //取得對應的 Validator
            Type? validatorType = GetFluentValidator(context);

            if (validatorType is null && !context.ModelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = 422;
            }

            if (validatorType is not null)
            {
                //如果不要使用其他注入的service應該要用這個
                //IValidator validator = (IValidator)Activator.CreateInstance(validatorType);

                IValidator validator = (IValidator)context.HttpContext.RequestServices.GetService(validatorType);

                //抓取 validator 對應的 GenericType
                Type validatorGenericArgumnet = validatorType.BaseType?.GetGenericArguments().Single();

                //抓取輸入參數中與 Validator GenericType 對應的參數
                var viewModel = context.ActionArguments.First(x => validatorGenericArgumnet.IsAssignableFrom(x.Value.GetType()));

                //抓取共用驗證方式
                var validMethod = validatorType.GetMethod("Validate", new Type[1] { validatorGenericArgumnet });

                //執行驗證方式
                ValidationResult result = (ValidationResult)validMethod.Invoke(validator, new object[1] { viewModel.Value });

                //是否要使用ModelAttribute的驗證
                bool useModelStateOrNot = modelState ? (!context.ModelState.IsValid) : false;

                if (useModelStateOrNot || !result.IsValid)
                {
                    context.HttpContext.Response.StatusCode = 422;
                }

                if (result is not null && !result.IsValid)
                {
                    //將客製化的驗證邏輯寫入 ModelState
                    result.Errors.ForEach(error => context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage));
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = 422;
            }
        }

        /// <summary>
        /// 抓取與 執行方式對應的 Validator
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Type? GetFluentValidator(ActionExecutingContext context)
        {
            Type controllerType = context.Controller.GetType();
            Assembly assembly = controllerType.Assembly;

            var route = context.ActionDescriptor.RouteValues;

            string validatorName = this.validatorName ?? route["controller"] + route["action"] + "Validator";

            Type? validatorType = assembly.GetType($"{assembly.GetName().Name}.Validators.{validatorName}");

            return validatorType;
        }
    }
}
