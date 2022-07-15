using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public partial class DataTable<TModel> : TableBase<TModel>
    {
        private int filteredRecords = 0;
        private string _keyword;
        protected readonly Filter _filter = new();
        protected readonly List<SortOrder> _sortOrder = new();


        [Parameter]
        public string SerialNumberText { get; set; } = null;
        [Parameter]
        public int KeywordLength { get; set; } = 3;
        [Parameter]
        public bool IgnoreFilterCase { get; set; }
        [Parameter]
        public bool MatchExactFilter { get; set; }
        [Parameter]
        public List<string> FilterDateFormats { get; set; }
        [Parameter]
        public bool IsExportable { get; set; }
        [Parameter]
        public RenderFragment CustomContextMenu { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            KeywordLength = KeywordLength < 1 ? 3 : KeywordLength;
        }
        protected override void UpdateTotalRecords()
        {
            UpdateTotalRecords(Items.Count);
        }

        protected virtual void UpdateTotalRecords(int filteredItems)
        {
            base.UpdateTotalRecords();
            if (ItemsProvider is null)
            {
                filteredRecords = Items.Count;
            }
            caption = $"Showing {filteredRecords} of {totalRecords} record{(totalRecords > 1 ? "s" : string.Empty)}";
        }

        private void RaiseColumnTypeMissmatchException()
        {
            throw new ColumnTypeMissmatchException(GetType(), typeof(DataColumn<TModel>));
        }

        protected virtual void UpdateFilter(KeyboardEventArgs e)
        {
            Filter filter = null;
            if (e.Code == "Escape")
            {
                //on escape clear the filter
                _keyword = string.Empty;
                NotifyFilterUpdate(filter);
            }
            else if (_keyword?.Length >= 3 || e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                //filter the content if the keyword length is greater or equal to specified keyword length or if the user press enter
                if (!string.IsNullOrEmpty(_keyword))
                {
                    filter = new Filter
                    {
                        IsIgnoreCase = IgnoreFilterCase,
                        IsMatchExact = MatchExactFilter,
                        Keyword = _keyword,
                        IsSearchAll = Columns.All(x =>
                        {
                            var dataColumn = (DataColumn<TModel>)x;
                            return dataColumn.IsFilterable;
                        })
                    };
                    foreach (var column in Columns)
                    {
                        var dataColumn = (DataColumn<TModel>)column;
                        if (dataColumn.IsFilterable)
                        {
                            filter.SearchableColumns.Add(dataColumn.GetColumnPropertyName());
                        }
                    }
                }
                NotifyFilterUpdate(filter);
            }
        }

        protected virtual async void NotifyFilterUpdate(Filter filter)
        {
            if (filter is not null)
            {
                _filter.SearchableColumns.Clear();
                _filter.SearchableColumns.AddRange(filter.SearchableColumns);
                _filter.IsMatchExact = filter.IsMatchExact;
                _filter.Keyword = filter.Keyword;
                _filter.IsSearchAll = filter.IsSearchAll;
                _filter.IsIgnoreCase = filter.IsIgnoreCase;
                if (ItemsProvider is null)
                {
                    //StateHasChanged();
                }
                else
                {
                    await _virtualizer.RefreshDataAsync();
                }
            }
            else
            {
                _filter.IsMatchExact = false;
                _filter.SearchableColumns.Clear();
                _filter.IsIgnoreCase = false;
                _filter.IsSearchAll = false;
                if (ItemsProvider is not null)
                {
                    await _virtualizer.RefreshDataAsync();
                }
            }
        }

        private void ClickOrderColumnOrder(DataColumn<TModel> column)
        {
            if (_sortOrder.Any(x => x.Column == column.GetColumnPropertyName()))
            {
                var sortColumn = _sortOrder.FirstOrDefault(x => x.Column == column.GetColumnPropertyName());
                if (sortColumn.Direction == SortDirection.Ascending)
                {
                    sortColumn.Direction = SortDirection.Descending;
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                    column.TextColor = Common.ThemeColors.Danger;
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
                }
                else if (sortColumn.Direction == SortDirection.Descending)
                {
                    _sortOrder.Remove(sortColumn);
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                    column.TextColor = null;
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
                }
            }
            else
            {
                _sortOrder.Add(new SortOrder
                {
                    Column = column.GetColumnPropertyName(),
                    Direction = SortDirection.Ascending
                });
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                column.TextColor = Common.ThemeColors.Success;
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            }
            NotifyOrderUpdated();
        }

        protected virtual async void NotifyOrderUpdated()
        {
            if (ItemsProvider is not null)
            {
                await _virtualizer.RefreshDataAsync();
            }
            StateHasChanged();
        }

        protected virtual void UpdateCaption(int selectedItemsCount, int totalItems, int filteredItems)
        {
            totalRecords = totalItems;
            filteredRecords = filteredItems;
            if (_caption is not null)
            {
#pragma warning disable BL0005 // Component parameter should not be set outside of its component. Needed to be updated runtime
                _caption.TotalRecords = totalItems;
                _caption.FilteredRecords = filteredItems;
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
                _caption?.RefreshState();
            }
        }
        protected override async ValueTask<ItemsProviderResult<DataRowHolder<TModel>>> LoadData(ItemsProviderRequest request)
        {
            var startRow = request.StartIndex;
            var numberOfRows = request.Count;
            var data = await ItemsProvider(new DataRequest
            {
                Count = numberOfRows,
                StartRow = startRow,
                Filter = _filter,
                SortOrder = _sortOrder
            });
            totalRecords = data.TotalRowsCount;
            filteredRecords = data.FilteredRowsCount;
            UpdateCaption(SelectedItems.Count, data.TotalRowsCount, data.FilteredRowsCount);
            return new ItemsProviderResult<DataRowHolder<TModel>>(data.Data, data.TotalRowsCount);
        }
    }
}
