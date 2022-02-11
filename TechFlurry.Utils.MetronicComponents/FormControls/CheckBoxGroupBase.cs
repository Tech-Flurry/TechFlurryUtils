using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TechFlurry.Utils.MetronicComponents.Common;

namespace TechFlurry.Utils.MetronicComponents.FormControls
{
    public abstract class CheckBoxGroupBase<T, TDisplay> : InputBase<List<T>>
    {
        protected string name;
        protected Dictionary<string, object> inputAttributes = new();
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string HelpText { get; set; }
        [Parameter]
        public Dictionary<T, TDisplay> Items { get; set; }
        [Parameter]
        public bool IsInline { get; set; }
        [Parameter]
        public bool IsOutlined { get; set; }
        [Parameter]
        public bool IsDoubledOutlined { get; set; }
        [Parameter]
        public RadioStyles? Style { get; set; }
        [Parameter]
        public ThemeColors? Color { get; set; }
        [Parameter]
        public Action<List<T>> OnValueChanged { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Value = new();
        }
        protected void OnChange(T key, ChangeEventArgs args)
        {
            var check = (bool)args.Value;
            Value = Value?.Distinct()?.ToList();
            if (!check)
            {
                Value.Remove(key);
            }
            else
            {
                Value.Add(key);
            }
            CurrentValueAsString = string.Join(',', Value);
            OnValueChanged?.Invoke(Value);
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            name = string.IsNullOrEmpty(Name)
                    ? name = Functions.GenerateElementId()
                    : name = Name;

            inputAttributes.Clear();
        }

        protected override bool TryParseValueFromString(string value, out List<T> result, out string errorMessage)
        {
            bool success = false;
            result = new();
            errorMessage = string.Empty;
            foreach (var val in value.Split(','))
            {
                success = BindConverter.TryConvertTo<T>(val, CultureInfo.CurrentCulture, out var parsedValue);
                if (success)
                {
                    result.Add(parsedValue);
                }
                else
                {
                    break;
                }
            }
            if (!success)
            {
                errorMessage = $"{FieldIdentifier.FieldName} field isn't valid.";

                return false;
            }
            return true;
        }
    }
}
