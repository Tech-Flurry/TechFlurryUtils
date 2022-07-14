using Microsoft.AspNetCore.Components;
using TechFlurry.Utils.MetronicComponents.Common;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public partial class Table<TModel> : TableBase<TModel>
    {
        [Parameter]
        public bool IsHover { get; set; }

        [Parameter]
        public bool IsRounded { get; set; }

        [Parameter]
        public bool IsStriped { get; set; }
        [Parameter]
        public bool IsSmall { get; set; }
        [Parameter]
        public TableHeaderStyle HeaderStyle { get; set; } = TableHeaderStyle.None;

        [Parameter]
        public TableBorderStyle BorderStyle { get; set; } = TableBorderStyle.None;
        [Parameter]
        public BootstrapSizes? ResponsiveSizes { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            CssClass += " table-responsive";
            if (ResponsiveSizes is not null)
            {
                CssClass += "-" + ResponsiveSizes.ToString().ToLower();
            }
            if (IsHover)
            {
                CssClass += " table-hover";
            }
            if (IsRounded)
            {
                CssClass += " rounded";
            }
            if (IsStriped)
            {
                CssClass += " table-striped";
            }
            if (IsSmall)
            {
                CssClass += " table-sm";
            }
        }
        protected override void UpdateTotalRecords()
        {
            base.UpdateTotalRecords();
            caption = $"Showing {totalRecords} record{(totalRecords > 1 ? "s" : string.Empty)}";
        }
    }
}
