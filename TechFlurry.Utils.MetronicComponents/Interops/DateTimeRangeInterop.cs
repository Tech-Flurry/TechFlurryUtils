using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;
using TechFlurry.Utils.MetronicComponents.Models;

namespace TechFlurry.Utils.MetronicComponents.Interops
{
    internal interface IDateTimeRangeInterop
    {
        Task<IJSObjectReference> Module { get; }

        event EventHandler<DateRange> OnDateRangeChanged;

        void AddPredefinedRange(string rangeTitle, DateTime startDate, DateTime endDate);
        void Init(DateTime startDate, DateTime endDate, string id, string applyCss, string cancelCss, string dateFormat, bool includeTime, bool isSingleDatePicker, bool showDropDown, int? timePickerIncement = null);
    }
    internal class DateTimeRangeInterop : InteropBase, IDateTimeRangeInterop
    {
        private Dictionary<string, DateTime[]> _predefinedRanges;


        public DateTimeRangeInterop(IJSRuntime jsRuntime) : base($"../{Constants.CONTENT_BASE_PATH}js/date-time-range-interop.js", jsRuntime)
        {
        }

        public event EventHandler<DateRange> OnDateRangeChanged;

        public void AddPredefinedRange(string rangeTitle, DateTime startDate, DateTime endDate)
        {
            _predefinedRanges ??= new Dictionary<string, DateTime[]>();
            _predefinedRanges.Add(rangeTitle, new DateTime[] { startDate, endDate });
        }

        public async void Init(DateTime startDate, DateTime endDate, string id, string applyCss, string cancelCss, string dateFormat, bool includeTime, bool isSingleDatePicker, bool showDropDown, int? timePickerIncement = null)
        {
            var module = await Module;
            timePickerIncement ??= 30;
            await module.InvokeVoidAsync("init", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), id, applyCss, cancelCss, dateFormat, includeTime, timePickerIncement, isSingleDatePicker, showDropDown, _predefinedRanges, DotNetObjectReference.Create(this), nameof(OnValueChanged));
        }
        [JSInvokable]
        public void OnValueChanged(string startDate, string endDate, string label)
        {
            var result = DateTime.TryParse(startDate, out DateTime parsedStartDate);
            result = DateTime.TryParse(endDate, out DateTime parsedEndDate);
            OnDateRangeChanged?.Invoke(this, new DateRange
            {
                End = parsedEndDate,
                Start = parsedStartDate
            });
        }
    }
}
