using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.FormControls.SelectBoxes.Select2.Internal;
using TechFlurry.Utils.MetronicComponents.Interops;
using TechFlurry.Utils.MetronicComponents.Models;

namespace TechFlurry.Utils.MetronicComponents.FormControls.SelectBoxes.Select2
{
    public partial class SingleSelectBox<TId> : Select2Base<TId>
    {
        [Parameter]
        public bool IsSearchable { get; set; }
        [Parameter]
        public int MinimumSearchResults { get; set; } = 1;

        [Parameter]
        public List<Select2Item<TId>> Items { get; set; }
        [Parameter]
        public Func<ValueTask<List<Select2Item<TId>>>> ItemsProvider { get; set; }

        public SingleSelectBox()
        {
            Items = new List<Select2Item<TId>>();
        }


        protected override string FormatValueAsString(TId value)
        {
            return base.FormatValueAsString(value);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await Interop.InitSingleSelect("#" + id, Placeholder, IsClearAllowed, IsSearchable, minimumInputLength: MinimumInputLength, maximumInputLength: MaximumInputLength, minSearchResults: MinimumSearchResults);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (!IsSearchable)
            {
                MinimumInputLength = 0;
                MaximumInputLength = 0;
            }
        }

        protected async override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (ItemsProvider is not null)
            {
                Items = await ItemsProvider();
            }
            SetDefaultSelectedValue();
        }

        protected override async void OnValueChanged(object sender, string e)
        {
            if (!string.IsNullOrEmpty(e))
            {
                Value = (TId)Convert.ChangeType(e, typeof(TId));
                CurrentValue = (TId)Convert.ChangeType(e, typeof(TId));
                CurrentValueAsString = e;
                OnSelectionChanged?.Invoke(Value);
            }
            else
            {
                SetDefaultValue();
            }
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            StateHasChanged();
        }

        protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TId result, [NotNullWhen(false)] out string validationErrorMessage)
        {
            var isParsed = false;
            result = default;
            validationErrorMessage = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                result = (TId)Convert.ChangeType(value, typeof(TId));
                isParsed = true;
            }
            else
            {
                SetDefaultValue();
            }
            if (!isParsed)
            {
                validationErrorMessage = $"{FieldIdentifier.FieldName} field isn't valid.";
            }
            return isParsed;
        }

        private void SetDefaultValue()
        {
            Value = default;
            CurrentValue = default;
            CurrentValueAsString = Value.ToString();
        }

        private void SetDefaultSelectedValue()
        {
            if (Items.Any(x => x.Id.Equals(Value)))
            {
                Items.FirstOrDefault(x => x.Id.Equals(Value)).IsSelected = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
