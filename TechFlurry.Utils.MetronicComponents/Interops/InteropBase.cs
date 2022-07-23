using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace TechFlurry.Utils.MetronicComponents.Interops
{
    public abstract class InteropBase : IDisposable
    {
        private readonly string _filePath;
        protected readonly IJSRuntime _jsRuntime;
        private Task<IJSObjectReference> _module;

        public InteropBase(string filePath, IJSRuntime jsRuntime)
        {
            _filePath = filePath;
            _jsRuntime = jsRuntime;
        }

        public Task<IJSObjectReference> Module => _module ??= _jsRuntime.InvokeAsync<IJSObjectReference>("import", _filePath).AsTask();

        public void Dispose()
        {
            if (_module is not null)
            {
                ((IDisposable)_module).Dispose();
            }
        }
    }
}
