using System.Reflection;
using System.Runtime.Serialization;

namespace DocumentsStore.BL
{
    public static class EnumHelper
    {
        public static T GetEnumMemberValue<T>(string? value) where T : Enum
        {
            var type = typeof(T);
            if (value is not null)
            {
                foreach (var name in Enum.GetNames(type))
                {
                    var attr = type.GetRuntimeField(name).GetCustomAttribute<EnumMemberAttribute>(true);
                    if (attr != null && attr.Value == value)
                    {
                        return (T)Enum.Parse(type, name);
                    }
                }

                throw new InvalidOperationException($"Cannot parse {value} to Enum of type {type}");
            }

            throw new InvalidOperationException($"Cannot parse to Enum of type {type}. Value is null.");
        }
    }
}
