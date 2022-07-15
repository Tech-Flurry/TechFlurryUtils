using System;
using System.Threading.Tasks;
using TechFlurry.Utils.Common.UserInterface.Events;

namespace TechFlurry.Utils.Common.UserInterface.Processes
{
    public interface IDataLoaderBase
    {
        event EventHandler<DataLoadedEventArgs> OnDataLoaded;
        event EventHandler<DataLoadFailedEventArgs> OnLoadFailed;
        int FormCode { get; }
    }

    public interface IDataLoader : IDataLoaderBase
    {
        void Load();
    }

    public interface IDataLoader<T> : IDataLoaderBase
    {
        void Load(T filter);
    }
    public interface IDataLoader<T, U> : IDataLoaderBase
    {
        void Load(T filter1, U param1);
    }
    public interface IDataLoader<T, U, V> : IDataLoaderBase
    {
        void Load(T filter1, U param1, V param2);
    }
    public interface IAsyncDataLoader : IDataLoaderBase
    {
        Task LoadAsync();
    }

    public interface IAsyncDataLoader<T> : IDataLoaderBase
    {
        Task LoadAsync(T filter);
    }
    public interface IAsyncDataLoader<T, U> : IDataLoaderBase
    {
        Task LoadAsync(T filter1, U param1);
    }
    public interface IAsyncDataLoader<T, U, V> : IDataLoaderBase
    {
        Task LoadAsync(T filter1, U param1, V param2);
    }
}
