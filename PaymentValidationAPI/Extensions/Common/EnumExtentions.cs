using System.ComponentModel;
using System.Reflection;

namespace PaymentValidationAPI.Extensions.Common
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
