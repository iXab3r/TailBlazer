using System;
using System.Windows.Markup;

namespace TailBlazer.Domain.Infrastructure
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private readonly Type _enumType;

        public EnumBindingSourceExtension(Type enumType)
        {
            _enumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
            {
                throw new InvalidOperationException("The EnumType must be specified.");
            }

            var actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            var enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
            {
                return enumValues;
            }

            var tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}