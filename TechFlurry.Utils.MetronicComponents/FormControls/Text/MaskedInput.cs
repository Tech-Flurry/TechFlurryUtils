using Microsoft.AspNetCore.Components;
using TechFlurry.Utils.MetronicComponents.FormControls.Text.Internal;

namespace TechFlurry.Utils.MetronicComponents.FormControls.Text
{
    public partial class MaskedInput : MaskedInputBase
    {
        [Parameter]
        public string Mask { get; set; }
        [Parameter]
        public bool ShowHelpText { get; set; }
        [Parameter]
        public bool IsAutoUnmask { get; set; }
        [Parameter]
        public bool IsNumeric { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                MaskedInputInterop.Init("#" + id, Mask, Placeholder, IsAutoUnmask, IsNumeric);
            }
        }
    }
}
