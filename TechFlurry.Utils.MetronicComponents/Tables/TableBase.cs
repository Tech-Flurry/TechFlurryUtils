using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public abstract class TableBase<TModel> : ComponentBase
    {
        protected Virtualize<DataRowHolder<TModel>> _virtualizer;
        protected string caption;
        protected int totalRecords;

        [Parameter]
        public List<TModel> Items { get; set; } = new List<TModel>();

        [Parameter]
        public string Id { get; set; } = "";

        [Parameter]
        public bool IsDark { get; set; }

        [Parameter]
        public string TableTitle { get; set; }

        [Parameter]
        public string CssClass { get; set; }


        [Parameter]
        public bool ShowSerialNumbers { get; set; }

        [Parameter]
        public Dictionary<string, object> TableAttributes { get; set; }

        [Parameter]
        public Dictionary<string, object> RowAttributes { get; set; }

        [Parameter]
        public bool AllowRowSelection { get; set; } = false;

        [Parameter]
        public ThemeColors? SelectedRowColor { get; set; }

        [Parameter]
        public EventCallback<TModel> RowClickedEvent { get; set; }

        [Parameter]
        public TModel LastSelectedItem { get; set; }

        [Parameter]
        public List<TModel> SelectedItems { get; set; }

        [Parameter]
        public string EmptyGridText { get; set; } = "No records to show";

        [Parameter]
        public int OverscanCount { get; set; } = 5;

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public Func<DataRequest, Task<DataHolder<TModel>>> ItemsProvider { get; set; }
        protected List<TModel> AllItems { get; set; } = new List<TModel>();
        protected List<ColumnBase<TModel>> Columns { get; set; } = new List<ColumnBase<TModel>>();
        public virtual void ClearSelectedItems()
        {
            SelectedItems.Clear();
            StateHasChanged();
        }

        public void AddColumn(ColumnBase<TModel> column)
        {
            Columns.Add(column);
            StateHasChanged();
        }

        public async Task Refresh()
        {
            StateHasChanged();
            if (ItemsProvider is not null)
            {
                await _virtualizer.RefreshDataAsync();
            }
        }

        public async void SelectItem(TModel item, bool triggerEvent = false)
        {
            SelectedItems.Add(item);
            if (triggerEvent)
            {
                await OnRowClickedEvent(null, item);
            }
        }

        protected override void OnInitialized()
        {
            if (Items == null) Items = new List<TModel>();
            AllItems = Items;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Id = string.IsNullOrEmpty(Id) ? "table-" + Guid.NewGuid().ToString() : Id;
            CssClass += " table";
            if (IsDark)
            {
                CssClass += " table-dark";
            }
            if (ItemsProvider is null)
            {
                AllItems = Items;
                UpdateTotalRecords();
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                foreach (var column in Columns)
                {
                    column.StateChanged += ColumnStateChanged;
                }
                StateHasChanged();
            }
        }

        public virtual void Dispose()
        {
            foreach (var column in Columns)
            {
                column.StateChanged -= ColumnStateChanged;
            }
            Columns.Clear();
        }
        protected virtual async Task OnRowClickedEvent(MouseEventArgs args, TModel item)
        {
            LastSelectedItem = item;
            await RowClickedEvent.InvokeAsync(item);
            StateHasChanged();
        }

        protected virtual void UpdateTotalRecords()
        {
            if (ItemsProvider is null)
            {
                totalRecords = Items.Count;
            }
        }

        protected virtual async ValueTask<ItemsProviderResult<DataRowHolder<TModel>>> LoadData(ItemsProviderRequest request)
        {
            var startRow = request.StartIndex;
            var numberOfRows = request.Count;
            var data = await ItemsProvider(new DataRequest
            {
                Count = numberOfRows,
                StartRow = startRow,
                CancellationToken = request.CancellationToken
            });
            return new ItemsProviderResult<DataRowHolder<TModel>>(data.Data, data.TotalRowsCount);
        }

        /// <summary>
        /// Event handler for when certain important properties of the column change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ColumnStateChanged(object sender, EventArgs args) => StateHasChanged();
    }
}
