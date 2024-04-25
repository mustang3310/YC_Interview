namespace OfficialWebsite.Core.Helper
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateHelper
    {
        public static object GetModelErrors(this ModelStateDictionary modelState)
            => modelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    k => k.Key,
                    k => k.Value?.Errors.Select(x => x.ErrorMessage).ToArray());
    }
}
