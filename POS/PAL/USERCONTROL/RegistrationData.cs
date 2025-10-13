namespace POS.PAL.USERCONTROL
{
    internal static class RegistrationData
    {
        // Business data
        public static string BusinessName { get; set; }
        public static string BusinessPhone { get; set; }
        public static string BusinessEmail { get; set; }
        public static string BusinessAddress { get; set; }
        public static string BusinessCity { get; set; }
        public static string BusinessCountry { get; set; }
        public static string BusinessTaxNo { get; set; }

        // Store data
        public static string StoreName { get; set; }
        public static string ManagerName { get; set; }
        public static string StorePhone { get; set; }
        public static string StoreEmail { get; set; }
        public static string StoreAddress { get; set; }
        public static string StoreCity { get; set; }
        public static string StoreState { get; set; }
        public static string StoreCountry { get; set; }
        public static string StorePostalCode { get; set; }

        // Method to clear all data
        public static void Clear()
        {
            BusinessName = string.Empty;
            BusinessPhone = string.Empty;
            BusinessEmail = string.Empty;
            BusinessAddress = string.Empty;
            BusinessCity = string.Empty;
            BusinessCountry = string.Empty;
            BusinessTaxNo = string.Empty;

            StoreName = string.Empty;
            ManagerName = string.Empty;
            StorePhone = string.Empty;
            StoreEmail = string.Empty;
            StoreAddress = string.Empty;
            StoreCity = string.Empty;
            StoreState = string.Empty;
            StoreCountry = string.Empty;
            StorePostalCode = string.Empty;
        }
    }
}
