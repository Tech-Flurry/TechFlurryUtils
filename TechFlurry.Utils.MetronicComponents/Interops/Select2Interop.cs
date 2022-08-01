using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;
using TechFlurry.Utils.MetronicComponents.Models;
using TechFlurry.Utils.MetronicComponents.Params;

namespace TechFlurry.Utils.MetronicComponents.Interops
{
    internal interface ISelect2Interop
    {
        Func<Select2SearchParams, Task<List<Select2SearchItem>>> CustomSearchFunction { get; set; }

        event EventHandler<string> OnValueChanged;
        event EventHandler OnClosing;
        event EventHandler OnClose;
        event EventHandler OnOpening;
        event EventHandler OnOpen;
        event EventHandler OnSelecting;
        event EventHandler OnSelect;
        event EventHandler OnUnSelecting;
        event EventHandler OnUnSelect;
        event EventHandler OnClearing;
        event EventHandler OnClear;

        Task CloseAsync();
        Task DestoryAsync();
        Task InitSingleSelect(string id, string placeholder, bool isClearAllowed, bool isSearchable, int minSearchResults = 1, int minimumInputLength = 3, int maximumInputLength = 20);
        Task<bool> IsInitializedAsync();
        void OnChange(string value);
        Task OpenAsync();
    }

    internal class Select2Interop : InteropBase, ISelect2Interop
    {
        private string id, placeholder;
        private bool isClearAllowed, isMultiselect;
        public Select2Interop(IJSRuntime jsRuntime) : base($"../{Constants.CONTENT_BASE_PATH}js/select2-interop.js", jsRuntime)
        {
        }

        public Func<Select2SearchParams, Task<List<Select2SearchItem>>> CustomSearchFunction { get; set; }

        public event EventHandler<string> OnValueChanged;
        public event EventHandler OnClosing;
        public event EventHandler OnClose;
        public event EventHandler OnOpening;
        public event EventHandler OnOpen;
        public event EventHandler OnSelecting;
        public event EventHandler OnSelect;
        public event EventHandler OnUnSelecting;
        public event EventHandler OnUnSelect;
        public event EventHandler OnClearing;
        public event EventHandler OnClear;
        public async Task InitSingleSelect(string id, string placeholder, bool isClearAllowed, bool isSearchable, int minSearchResults = 1, int minimumInputLength = 3, int maximumInputLength = 20)
        {
            this.id = id;
            this.placeholder = placeholder;
            this.isClearAllowed = isClearAllowed;
            isMultiselect = false;
            var module = await Module;
            await module.InvokeVoidAsync("initSingleSelect", id, placeholder, isClearAllowed, isSearchable, minSearchResults, minimumInputLength, maximumInputLength, DotNetObjectReference.Create(this), nameof(OnChange), nameof(TriggerOnClosing), nameof(TriggerOnClose), nameof(TriggerOnOpening), nameof(TriggerOnOpen), nameof(TriggerOnSelecting), nameof(TriggerOnSelect), nameof(TriggerOnUnSelecting), nameof(TriggerOnUnSelect), nameof(TriggerOnClearing), nameof(TriggerOnClear), nameof(Search));
        }

        public async Task OpenAsync()
        {
            var module = await Module;
            await module.InvokeVoidAsync("open", id);
        }
        public async Task CloseAsync()
        {
            var module = await Module;
            await module.InvokeVoidAsync("close", id);
        }
        public async Task DestoryAsync()
        {
            var module = await Module;
            await module.InvokeVoidAsync("destroy", id);
        }
        public async Task<bool> IsInitializedAsync()
        {
            var module = await Module;
            var result = await module.InvokeAsync<bool>("destroy", id);
            return result;
        }

        [JSInvokable]
        public void OnChange(string value)
        {
            OnValueChanged?.Invoke(this, value);
        }
        [JSInvokable]
        public void TriggerOnClosing()
        {
            OnClosing?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnClose()
        {
            OnClose?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnOpening()
        {
            OnOpening?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnOpen()
        {
            OnOpen?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnSelecting()
        {
            OnSelecting?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnSelect()
        {
            OnSelect?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnUnSelecting()
        {
            OnUnSelecting?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnUnSelect()
        {
            OnUnSelect?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnClearing()
        {
            OnClearing?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public void TriggerOnClear()
        {
            OnClear?.Invoke(this, new EventArgs());
        }
        [JSInvokable]
        public async Task<List<Select2SearchItem>> Search(string term, Select2SearchItem[] data, string group)
        {
            var result = data is not null
                            ? data.ToList()
                            : new List<Select2SearchItem>();
            if (CustomSearchFunction is not null)
            {
                result = await CustomSearchFunction(new Select2SearchParams
                {
                    SearchTerm = term,
                    Group = group
                });
            }
            else
            {
                result = result.Where(x => x.Text.ToLower().Contains(term.ToLower())).ToList();
            }
            return result;
        }
    }
}
