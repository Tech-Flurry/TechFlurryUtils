namespace TechFlurry.Utils.Common.UserInterface.Events
{
    public class OnLoadEventArgs<T> : ApplicationEventArgs
    {
        public T Data { get; set; }
    }
}
