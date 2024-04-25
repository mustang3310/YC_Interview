namespace OfficialWebsite.Core.Extensions
{
    using System.ComponentModel;

    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            System.Reflection.FieldInfo? field =
                value.GetType().GetField(value.ToString());

            if (field is null)
                return string.Empty;


            return Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is not DescriptionAttribute attribute
                ? value.ToString()
                : attribute.Description;
        }
    }
}