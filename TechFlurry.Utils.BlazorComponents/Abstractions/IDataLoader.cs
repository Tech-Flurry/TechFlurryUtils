using System;
using TechFlurry.Utils.BlazorComponents.EventArgs;

namespace TechFlurry.Utils.BlazorComponents.Abstractions
{
    public interface IDataLoader
    {
        event EventHandler<DataLoadedEventArgs> OnDataLoaded;
        event EventHandler<DataLoadFailedEventArgs> OnLoadFailed;
        int FormCode { get; }
    }
}
