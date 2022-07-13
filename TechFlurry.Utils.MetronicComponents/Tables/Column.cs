using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechFlurry.Utils.MetronicComponents.Common;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public partial class Column<TModel> : ComponentBase
    {
        [CascadingParameter]
        private Table<TModel> Table { get; set; }

        [Parameter]
        public string CssClass { get; set; } = "";

        [Parameter]
        public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;

        [Parameter]
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Bottom;

        [Parameter]
        public string CustomTitle { get; set; }


        [Parameter]
        public Expression<Func<TModel, object>> Property { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<string> HeaderTemplate { get; set; }

        [Parameter]
        public RenderFragment<TModel> RowTemplate { get; set; }

        /// <summary>
        /// Indicates whether or not the column is visible
        /// </summary>
        [Parameter]
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    RaiseStateChanged();
                }
            }
        }
        private bool isVisible = true;

        [Parameter]
        public string DateTimeFormat { get; set; } = "dd/MM/yyyy";

        public Guid Guid { get; set; }

        public Type PropertyType
        {
            get
            {
                if (Property != null) return Functions.GetPropertyType(Property);
                return typeof(string);
            }
        }

        protected override Task OnInitializedAsync()
        {
            if (Table == null) throw new ArgumentNullException($"A 'Column' must be a child of a 'Table' component");

            Guid = Guid.NewGuid();
            Table.AddColumn(this);

            return Task.CompletedTask;
        }

        public string GetColumnPropertyName()
        {
            if (Property != null) return Functions.GetPropertyName(Property);
            return "";
        }

        public string GetColumnVisualPropertyName()
        {
            //Don't return custom title anymore, since it causes issues with trying to sort or filter
            if (Property != null)
            {
                string fullColumnName = GetColumnPropertyName();
                string propertyName = Functions.GetPropertyName(Property);

                var parts = propertyName.Split('.');
                if (parts.Length > 0) return parts.Last();
            }

            return "";
        }

        public event EventHandler StateChanged;
        private void RaiseStateChanged()
        {
            EventHandler handler = StateChanged;
            handler?.Invoke(this, new EventArgs());
        }
    }
}
