using System;
using TechFlurry.Utils.Common.UserInterface.Events;

namespace TechFlurry.Utils.Common.UserInterface.Processes
{
    public interface IDataLoader
    {
        event EventHandler<DataLoadedEventArgs> OnDataLoaded;
        event EventHandler<DataLoadFailedEventArgs> OnLoadFailed;
        int FormCode { get; }
    }
}
