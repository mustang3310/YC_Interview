namespace OfficialWebsite.Core.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class MaxItemCountAttribute : ValidationAttribute
    {
        public int Count { get; }

        public MaxItemCountAttribute(int count)
        {
            this.Count = count;

        }
    }
}