using System;
using TechFlurry.Utils.Common.UserInterface.Events;

namespace TechFlurry.Utils.Common.UserInterface.Processes
{
    public interface IValueUpdator
    {
        event EventHandler<OnUpdateEventArgs> OnValueUpdate;
    }
}
