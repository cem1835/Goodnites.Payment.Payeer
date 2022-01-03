namespace Goodnites.Payment.Payeer
{
    public static class PayeerDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Payeer";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Payeer";
    }
}
