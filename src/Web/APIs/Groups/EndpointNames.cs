namespace Web.APIs.Groups;

/// <summary>
/// Endpoint operation names.
/// </summary>
public static class EndpointNames
{
    /// <summary>
    /// Battery operations.
    /// </summary>
    public static class Batteries
    {
        /// <summary>
        /// Add a battery.
        /// </summary>
        public const string Add = "AddBattery";
        /// <summary>
        /// Replace a battery.
        /// </summary>
        public const string Replace = "ReplaceBattery";
        /// <summary>
        /// Get a battery.
        /// </summary>
        public const string Get = "GetBattery";
        /// <summary>
        /// Search batteries.
        /// </summary>
        public const string Search = "SearchBatteries";
        /// <summary>
        /// Delete a battery.
        /// </summary>
        public const string Delete = "DeleteBattery";

        /// <summary>
        /// Battery image operations.
        /// </summary>
        public static class Images
        {
            /// <summary>
            /// Add an image to a battery.
            /// </summary>
            public const string Add = "AddBatteryImage";

            /// <summary>
            /// Get an image.
            /// </summary>
            public const string Get = "GetBatteryImage";
        }
    }

    /// <summary>
    /// Image operations.
    /// </summary>
    public static class Images
    {
        /// <summary>
        /// Get an image.
        /// </summary>
        public const string Get = "GetImage";
    }
}
