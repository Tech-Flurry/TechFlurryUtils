using System;

namespace TechFlurry.Utils.Common.UserInterface.Events
{
    public class ApplicationEventArgs
    {
        public object CallingObject { get; set; }
        public Type CallerType { get; set; }
        public string CallingMethod { get; set; }
    }
}
