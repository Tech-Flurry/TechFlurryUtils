using System;

namespace TechFlurry.Utils.MetronicComponents.Models
{
    public class Select2Item<TId>
    {
        public TId Id { get; set; }
        public string Text { get; set; }
        public string Group { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }
    }
}
