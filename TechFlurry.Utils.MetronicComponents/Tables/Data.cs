using System.Collections.Generic;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public class DataHolder<TModel>
    {
        public List<DataRowHolder<TModel>> Data { get; set; }
        public int TotalRowsCount { get; set; }
        public int FilteredRowsCount { get; set; }
    }
    public class DataRowHolder<TModel>
    {
        public int SerialNumber { get; set; }
        public TModel DataRow { get; set; }
    }
}
