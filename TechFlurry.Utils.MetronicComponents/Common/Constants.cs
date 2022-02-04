﻿namespace TechFlurry.Utils.MetronicComponents.Common
{
    public static class Constants
    {
        public static readonly string CONTENT_BASE_PATH = $"_content/{typeof(Constants).Assembly.FullName?.Split(",")[0]}/";
        public static readonly string CANCEL_BUTTON_TEXT = "Cancel";
    }
}