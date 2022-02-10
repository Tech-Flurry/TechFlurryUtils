using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using TechFlurry.Utils.MetronicComponents.Common;

namespace TechFlurry.Utils.MetronicComponents.FormControls
{
    public abstract class RadioGroupBase<TValue, TDisplay> : InputBase<TValue>
    {

        protected string name;
        protected Dictionary<string, object> inputAttributes = new();
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string HelpText { get; set; }
        [Parameter]
        public Dictionary<TValue, TDisplay> Items { get; set; }
        [Parameter]
        public bool IsInline { get; set; }
        [Parameter]
        public bool IsOutlined { get; set; }
        [Parameter]
        public bool IsDoubledOutlined { get; set; }
        [Parameter]
        public bool IsAccent { get; set; }
        [Parameter]
        public RadioStyles? Style { get; set; }
        [Parameter]
        public ThemeColors? Color { get; set; }
        [Parameter]
        public Action<TValue> OnValueChanged { get; set; }


        protected void OnChange(ChangeEventArgs args)
        {
            CurrentValueAsString = args.Value.ToString();
            OnValueChanged?.Invoke(Value);
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            name = string.IsNullOrEmpty(Name)
                    ? name = Functions.GenerateElementId()
                    : name = Name;
        }

        protected override bool TryParseValueFromString(string value, out TValue result, out string errorMessage)
        {
            var success = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue);
            if (success)
            {
                result = parsedValue;
                errorMessage = null;

                return true;
            }
            else
            {
                result = default;
                errorMessage = $"{FieldIdentifier.FieldName} field isn't valid.";

                return false;
            }
        }
    }
}
