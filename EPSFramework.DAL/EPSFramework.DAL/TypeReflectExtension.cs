namespace EPSFramework.DAL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class TypeReflectExtension
    {
        /// <summary>
        /// Unboxes the nullable argument.
        /// </summary>
        /// <param name="conversionType">Type of the conversion.</param>
        /// <returns></returns>
        public static Type UnboxNullableArgument(this Type conversionType)
        {
            Type[] genericArgs = conversionType.GetGenericArguments();
            Type arg = genericArgs[0];
            return arg;
        }

        /// <summary>
        /// Unboxes the nullable argument.
        /// </summary>
        /// <param name="conversionType">Type of the conversion.</param>
        /// <returns></returns>
        public static bool IsCollection(this Type conversionType)
        {
            bool isGeneric = conversionType.IsGenericType;
            var interfaces = conversionType.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                if (isGeneric)
                {
                    if (interfaces[i].GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        return true;
                    }
                }
                else
                {
                    if (interfaces[i] == typeof(ICollection))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified target type is nullable.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public static bool IsNullable(this Type targetType)
        {
            return targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}