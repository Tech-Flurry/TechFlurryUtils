using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;

namespace TechFlurry.Utils.MetronicComponents.Interops
{
    public interface ICommonFunctions
    {
        Task<string> GetInnerHtmlAsync(ElementReference element);

        Task<MarkupString> GetMarkupHtmlAsync(ElementReference element);
    }

    internal class CommonFunctions : IDisposable, ICommonFunctions
    {
        private readonly IJSRuntime _jsRuntime;
        private Task<IJSObjectReference> _module;

        public CommonFunctions(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public Task<IJSObjectReference> Module => _module ??= _jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./{Constants.CONTENT_BASE_PATH}js/commonFunctions.js").AsTask();

        public void Dispose()
        {
            ((IDisposable)_module)?.Dispose();
        }

        public async Task<string> GetInnerHtmlAsync(ElementReference element)
        {
            var module = await Module;
            var html = await module.InvokeAsync<string>("getInnerHtml", element);
            return html;
        }

        public async Task<MarkupString> GetMarkupHtmlAsync(ElementReference element)
        {
            var module = await Module;
            var html = await module.InvokeAsync<string>("getInnerHtml", element);
            return (MarkupString)html;
        }
    }
}