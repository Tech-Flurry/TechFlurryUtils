using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;
using TechFlurry.Utils.MetronicComponents.Interops;
using TechFlurry.Utils.MetronicComponents.Models;

namespace TechFlurry.Utils.MetronicComponents.FormControls.SelectBoxes.Select2.Internal
{
    public abstract class Select2Base<TId> : InputBase<TId>, IAsyncDisposable
    {
        protected string id, name;
        protected readonly Dictionary<string, object> inputAttributes = new();

        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public bool IsDisabled { get; set; }
        [Parameter]
        public string Placeholder { get; set; }
        [Parameter]
        public bool IsClearAllowed { get; set; }
        [Parameter]
        public int MinimumInputLength { get; set; } = 3;
        [Parameter]
        public int MaximumInputLength { get; set; } = 20;
        [Parameter]
        public Action<TId> OnSelectionChanged { get; set; }

        [Inject]
        internal ISelect2Interop Interop { get; set; }
        
        public async Task OpenAsync() => await Interop.OpenAsync();
        public async Task CloseAsync() => await Interop.CloseAsync();
        public async Task DestoryAsync() => await Interop.DestoryAsync();
        public event EventHandler OnUnSelecting;

        public event EventHandler OnUnSelect;

        public event EventHandler OnSelecting;

        public event EventHandler OnSelect;

        public event EventHandler OnOpening;

        public event EventHandler OnOpen;

        public event EventHandler OnClearing;

        public event EventHandler OnClosing;

        public event EventHandler OnClose;

        public event EventHandler OnClear;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Interop.OnValueChanged += OnValueChanged;
            Interop.OnClear += OnClear;
            Interop.OnClose += OnClose;
            Interop.OnClosing += OnClosing;
            Interop.OnClearing += OnClearing;
            Interop.OnOpen += OnOpen;
            Interop.OnOpening += OnOpening;
            Interop.OnSelect += OnSelect;
            Interop.OnSelecting += OnSelecting;
            Interop.OnUnSelect += OnUnSelect;
            Interop.OnUnSelecting += OnUnSelecting;
        }

        protected abstract void OnValueChanged(object sender, string e);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            id ??= "sl2_" + Functions.GenerateElementId();
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

        protected override void Dispose(bool disposing)
        {
            Interop.OnValueChanged -= OnValueChanged;
            Interop.OnClear -= OnClear;
            Interop.OnClose -= OnClose;
            Interop.OnClosing -= OnClosing;
            Interop.OnClearing -= OnClearing;
            Interop.OnOpen -= OnOpen;
            Interop.OnOpening -= OnOpening;
            Interop.OnSelect -= OnSelect;
            Interop.OnSelecting -= OnSelecting;
            Interop.OnUnSelect -= OnUnSelect;
            Interop.OnUnSelecting -= OnUnSelecting;
            base.Dispose(disposing);
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await Interop.DestoryAsync();
            }
            catch (Exception)
            {
            }
            GC.SuppressFinalize(this);
        }
    }
}
