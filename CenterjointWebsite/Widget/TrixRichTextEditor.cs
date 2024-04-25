namespace YCInterview.Web.Widget
{
    /// <summary>
    /// ref: https://github.com/basecamp/trix
    /// </summary>
    public class TrixRichTextEditor
    {
        public TrixRichTextEditor(string? value = null)
        {
            this.Value = value ?? string.Empty;
        }

        public string Value { get; set; }
    }
}
