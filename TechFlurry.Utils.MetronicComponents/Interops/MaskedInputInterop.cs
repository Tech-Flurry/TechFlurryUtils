using Microsoft.JSInterop;
using System;
using TechFlurry.Utils.MetronicComponents.Common;

namespace TechFlurry.Utils.MetronicComponents.Interops
{
    public interface IMaskedInputInterop
    {
        event EventHandler<string> OnValueChanged;

        void Init(string id, string mask, string placeholder, bool isAutoUnmask, bool isNumeric);
        void InitDecimalMask(string id);
        void InitEmailMask(string id);
    }

    internal class MaskedInputInterop : InteropBase, IMaskedInputInterop
    {
        public MaskedInputInterop(IJSRuntime jsRuntime) : base($"../{Constants.CONTENT_BASE_PATH}js/masked-input-interop.js", jsRuntime)
        {
        }
        public event EventHandler<string> OnValueChanged;

        public async void Init(string id, string mask, string placeholder, bool isAutoUnmask, bool isNumeric)
        {
            var module = await Module;
            await module.InvokeVoidAsync("init", id, mask, placeholder, isAutoUnmask, isNumeric, DotNetObjectReference.Create(this), nameof(OnChange));
        }

        public async void InitEmailMask(string id)
        {
            var module = await Module;
            await module.InvokeVoidAsync("initEmailMask", id, DotNetObjectReference.Create(this), nameof(OnChange));
        }

        public async void InitDecimalMask(string id)
        {
            var module = await Module;
            await module.InvokeVoidAsync("initDecimalMask", id, DotNetObjectReference.Create(this), nameof(OnChange));
        }

        [JSInvokable]
        public void OnChange(string value)
        {
            if (OnValueChanged is not null)
            {
                OnValueChanged.Invoke(this, value);
            }
        }
    }
}
