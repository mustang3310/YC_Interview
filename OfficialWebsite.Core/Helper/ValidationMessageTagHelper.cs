namespace OfficialWebsite.Core.Helper
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;


    public class ValidationImageTagHelper : TagHelper
    {

        private const string DataValidationForAttributeName = "data-valmsg-for";
        private const string ValidationForAttributeName = "asp-validation-for";

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; set; }

        public ValidationImageTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            return Task.CompletedTask;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (For != null)
            {
                // Ensure Generator does not throw due to empty "fullName" if user provided data-valmsg-for attribute.
                // Assume data-valmsg-for value is non-empty if attribute is present at all. Should align with name of
                // another tag helper e.g. an <input/> and those tag helpers bind Name.
                IDictionary<string, object> htmlAttributes = null;
                if (string.IsNullOrEmpty(For.Name) &&
                    string.IsNullOrEmpty(ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix) &&
                    output.Attributes.ContainsName(DataValidationForAttributeName))
                {
                    htmlAttributes = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                {
                    { DataValidationForAttributeName, "-non-empty-value-" },
                };
                }

                string message = null;
                if (!output.IsContentModified)
                {
                    var tagHelperContent = output.GetChildContentAsync();

                }
                var tagBuilder = Generator.GenerateValidationMessage(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    message: message,
                    tag: null,
                    htmlAttributes: htmlAttributes);

                if (tagBuilder != null)
                {
                    output.MergeAttributes(tagBuilder);
                }
            }
            if (!ViewContext.ModelState.IsValid)
            {
                var errors = ViewContext.ModelState.TryGetValue(For.Name, out var entry) ? entry.Errors : null;

                if (errors != null && errors.Count > 0)
                {
                    var span = new TagBuilder("span");
                    foreach (var error in errors)
                    {
                        span.AddCssClass("text-danger");
                        span.InnerHtml.Append(error.ErrorMessage);
                        span.InnerHtml.AppendHtml("<br />");
                    }
                    output.Content.AppendHtml(span);
                }
            }
        }
    }
}