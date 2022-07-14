using System;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    [Serializable]
    public class ColumnTypeMissmatchException : InvalidCastException
    {
        public ColumnTypeMissmatchException(Type tableType, Type columnType) : base($"{tableType.Name} only accepts {columnType.Name}") { }
    }
}
