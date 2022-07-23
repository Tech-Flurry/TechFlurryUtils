using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;
using TechFlurry.Utils.MetronicComponents.Models;

namespace TechFlurry.Utils.MetronicComponents.Interops
{
    public interface IGlobalSearchInterop
    {
        void Init(string elementId, string iconId);
        void Init(string elementId, string iconId, string highlightedStartTag, string highlightedEndTag);
        void InitSearchFunction(Func<string, Task<SearchResultDataModel>> searchFunction);
    }

    internal class GlobalSearchInterop : InteropBase, IGlobalSearchInterop
    {
        private Func<string, Task<SearchResultDataModel>> _searchFunction;
        private bool isHighlighted = false;
        private string highlightedStartTag, highlightedEndTag;


        public GlobalSearchInterop(IJSRuntime jsRuntime) : base($"../{Constants.CONTENT_BASE_PATH}js/global-search-interop.js", jsRuntime)
        {
        }

        public async void Init(string elementId, string iconId)
        {
            var module = await Module;
            await module.InvokeVoidAsync("initWithCallback", elementId, DotNetObjectReference.Create(this), nameof(Search), iconId);
        }
        public async void Init(string elementId, string iconId, string highlightedStartTag, string highlightedEndTag)
        {
            if (highlightedStartTag is not null && !string.IsNullOrEmpty(highlightedEndTag))
            {
                this.highlightedEndTag = highlightedEndTag;
                this.highlightedStartTag = highlightedStartTag;
                isHighlighted = true;
            }

            var module = await Module;
            await module.InvokeVoidAsync("initWithCallback", elementId, DotNetObjectReference.Create(this), nameof(Search), iconId);
        }

        public void InitSearchFunction(Func<string, Task<SearchResultDataModel>> searchFunction)
        {
            _searchFunction = searchFunction;
        }

        [JSInvokable]
        public async void Search(string query)
        {
            var module = await Module;
            try
            {
                if (_searchFunction is not null)
                {
                    var data = await _searchFunction.Invoke(query);
                    string html;
                    if (data.Status == true)
                    {
                        html = GetSearchResultHtml(data.Data);
                    }
                    else
                    {
                        html = GetMessageHtml(data.Message);
                    }
                    await module.InvokeVoidAsync("successResult", html);
                }
                else
                {
                    await module.InvokeVoidAsync("errorResult", "Search functionality is not implemented");
                }
            }
            catch (Exception ex)
            {
                await module.InvokeVoidAsync("errorResult", "Error in connection...");
            }
        }
        private static string GetMessageHtml(string message)
        {
            return $@"<div class=""text-muted"">
                        {message}
                    </div>";
        }
        private string GetSearchResultHtml(List<SearchResultModel> model)
        {
            StringBuilder html = new();
            string prevGroup = string.Empty;
            foreach (var group in model.GroupBy(x => x.Group))
            {
                html.Append($@"<div class=""font-size-sm text-primary font-weight-bolder text-uppercase mb-2"">
                                    {group.Key}
                                </div>");
                html.Append($@"<div class=""mb-10"">");
                foreach (var item in group)
                {
                    html.Append($@"<div class=""d-flex align-items-center flex-grow-1 mb-2"">
                                        <div class=""symbol symbol-30 bg-transparent flex-shrink-0"">
                                            <img src=""{item.ImgSrc}"" alt="""">
                                        </div>
                                        <div class=""d-flex flex-column ml-3 mt-2 mb-2"">
                                            <a href=""{item.ActionLink}"" class=""font-weight-bold text-dark text-hover-primary"">
                                                {HighlightMarkedText(item.Title)}
                                            </a>
                                            <span class=""font-size-sm font-weight-bold text-muted"">
                                                {HighlightMarkedText(item.SubTitle)}
                                            </span>
                                        </div>
                                    </div>");
                }
                html.Append("</div>");
            }
            return html.ToString();
        }
        private string HighlightMarkedText(string content)
        {
            if (!isHighlighted)
            {
                return content;
            }
            var markedString = content.Replace(highlightedStartTag, "<mark>")
                                       //.Replace("#S#", "<mark>")
                                       .Replace(highlightedEndTag, "</mark>");
            //.Replace("#E#", "</mark>");
            return markedString;
        }
    }
}