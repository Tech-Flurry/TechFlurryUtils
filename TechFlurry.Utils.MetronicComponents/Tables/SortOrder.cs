namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public class SortOrder
    {
        public string Column { get; set; }
        public SortDirection Direction { get; set; }
    }
    public enum SortDirection
    {
        Ascending,
        Descending
    }
}