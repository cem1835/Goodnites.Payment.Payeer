namespace Goodnites.Payment.Payeer
{
    public class PayeerSettingsDto
    {
        public string MerchantId { get; set; }
        public string SecretKey { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
}