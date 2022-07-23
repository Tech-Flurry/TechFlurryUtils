using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using TechFlurry.Utils.MetronicComponents.Common;
using TechFlurry.Utils.MetronicComponents.Models;

namespace TechFlurry.Utils.MetronicComponents.FormControls.Dates
{
    public partial class DateTimeRange : InputBase<DateRange>
    {
        private readonly Dictionary<string, object> inputAttributes = new();
        protected string name, id, applyButtonCss, cancelButtonCss;


        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string HelpText { get; set; }
        [Parameter]
        public string PlaceHolder { get; set; }
        [Parameter]
        public bool IsDisabled { get; set; }
        [Parameter]
        public ThemeColors ApplyButtonColor { get; set; }
        [Parameter]
        public ThemeColors CancelButtonColor { get; set; }
        [Parameter]
        public string DateFormat { get; set; }
        [Parameter]
        public bool IsTimeIncluded { get; set; }
        [Parameter]
        public bool IsSingleDatePicker { get; set; }
        [Parameter]
        public bool ShowDropDown { get; set; }
        [Parameter]
        public int? TimeTicksIncrement { get; set; }
        [Parameter]
        public Action<DateRange> OnValueChanged { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DateTimeInterop.OnDateRangeChanged += OnDateRangeChanged;
            Value ??= new DateRange
            {
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(1).AddTicks(-1)
            };
        }

        private void OnDateRangeChanged(object sender, DateRange e)
        {
            Value = e;
            StateHasChanged();
            OnValueChanged?.Invoke(Value);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                DateTimeInterop.Init(Value.Start, Value.End, id, applyButtonCss, cancelButtonCss, DateFormat, IsTimeIncluded, IsSingleDatePicker, ShowDropDown, TimeTicksIncrement);
            }
        }

        public void AddPredefinedRange(string name, DateTime startDate, DateTime endDate)
        {
            DateTimeInterop.AddPredefinedRange(name, startDate, endDate);
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            id = "dt_" + Functions.GenerateElementId();
            name = string.IsNullOrEmpty(Name)
                    ? name = Functions.GenerateElementId()
                    : name = Name;
            applyButtonCss = "btn-" + ApplyButtonColor.ToString().ToLower();
            cancelButtonCss = "btn-" + CancelButtonColor.ToString().ToLower();


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

        protected override bool TryParseValueFromString(string value, out DateRange result, out string errorMessage)
        {
            throw new NotImplementedException();
        }
    }
}