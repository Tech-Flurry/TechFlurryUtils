﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;
using TechFlurry.Utils.MetronicComponents.Interops;

namespace TechFlurry.Utils.MetronicComponents.FormControls.Text
{
    public partial class MaskedInput : InputBase<string>
    {
        private string id, name;
        private readonly Dictionary<string, object> inputAttributes = new();

        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string Mask { get; set; }
        [Parameter]
        public bool ShowHelpText { get; set; }
        [Parameter]
        public string HelpText { get; set; }
        [Parameter]
        public bool IsDisabled { get; set; }
        [Parameter]
        public string Placeholder { get; set; }
        [Parameter]
        public bool IsAutoUnmask { get; set; }
        [Parameter]
        public Action<string> OnValueChanged { get; set; }
        [Parameter]
        public bool IsNumeric { get; set; }

        [Inject]
        public IMaskedInputInterop MaskedInputInterop { get; set; }
        protected override string FormatValueAsString(string value)
        {
            return base.FormatValueAsString(value);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MaskedInputInterop.OnValueChanged += OnValueChangedFromInterop;
        }

        private async void OnValueChangedFromInterop(object sender, string e)
        {
            Value = e;
            CurrentValue = e;
            CurrentValueAsString = Value;
            OnValueChanged?.Invoke(e);
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            StateHasChanged();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                MaskedInputInterop.Init("#" + id, Mask, Placeholder, IsAutoUnmask, IsNumeric);
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            id ??= "mi_" + Functions.GenerateElementId();
            name = string.IsNullOrEmpty(Name)
                    ? name = Functions.GenerateElementId()
                    : name = Name;
            inputAttributes.Clear();
            inputAttributes.Add("name", name);
            inputAttributes.Add("id", id);
            if (IsDisabled)
            {
                inputAttributes.Add("disabled", "disabled");
            }
            if (AdditionalAttributes is not null)
            {
                foreach (var attr in AdditionalAttributes)
                {
                    inputAttributes.Add(attr.Key, attr.Value);
                }
            }
        }

        protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out string result, [NotNullWhen(false)] out string validationErrorMessage)
        {
            validationErrorMessage = string.Empty;
            result = string.Empty;
            bool isParsed = false;
            if (!string.IsNullOrEmpty(value))
            {
                result = value;
                isParsed = true;
            }
            return isParsed;
        }
    }
}
