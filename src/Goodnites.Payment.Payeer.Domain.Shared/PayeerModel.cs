using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Volo.Abp;

namespace Goodnites.Payment.Payeer
{
    public sealed class PayeerModel
    {
        /// <summary>
        /// In this field, the seller enters a purchase ID that matches their invoicing system.
        /// Using a unique number for each payment is recommended.
        /// The ID can be a line of any length up to 32 characters and can contain the characters “A-Z”, “_”, “0-9”,and “-”.
        /// Example: 12345
        /// </summary>
        [JsonProperty("m_orderid")]
        public string PaymentId { get; private set; }

        /// <summary>
        /// The amount of the payment the seller wishes to receive from the purchaser.
        /// Theamount must be greaterthan zero.
        /// There can onlybe two decimal places, and they must be separated from the whole number by a period.
        /// Example: 1.00
        /// </summary>
        [JsonProperty("m_amount")]
        public string Amount { get; private set; }

        /// <summary>
        /// The currency used in thepayment Potential currencies: USD,EUR, RUB
        /// </summary>
        [JsonProperty("m_curr")]
        public string Currency { get; private set; }

        /// <summary>
        /// A description of the product or service.
        /// Generated by the seller.
        /// This line is added to the payment designation.
        /// It is encoded by a base64 algorithm.Example: dGVzdA==
        /// </summary>
        [JsonProperty("m_desc")]
        public string Description { get; private set; }


        /// <summary>
        ///  The merchant’s ID
        /// </summary>
        [JsonProperty("m_shop")]
        public string MerchantId { get; private set; }

        /// <summary>
        /// A security signature used to check the cohesiveness of the information obtained and directly identify the sender.
        /// </summary>
        [JsonProperty("m_sign")]
        public string Hash { get; private set; }

        [JsonProperty("m_params")] public string AdditionalParamaters { get; set; }

        [JsonIgnore] private string SecretKey { get; set; }

        [JsonIgnore] public string RawDescription => Encoding.UTF8.GetString(Convert.FromBase64String(Description));

        [JsonIgnore] public bool HashCreated => Hash.IsNullOrEmpty() == false;

        private PayeerModel()
        {
        }

        public PayeerModel(
            string paymentId,
            string amount,
            string currency,
            string description
        )
        {
            PaymentId = paymentId;
            Amount = amount;
            Currency = currency;
            Description = Convert.ToBase64String(Encoding.UTF8.GetBytes(description));
        }

        public PayeerModel(
            string merchantId,
            string secretKey,
            string paymentId,
            string amount,
            string currency,
            string description
        )
        {
            MerchantId = merchantId;
            SecretKey = secretKey;
            PaymentId = paymentId;
            Amount = int.Parse(amount).ToString("#.00"); // TODO : 
            Currency = currency;
            Description = Convert.ToBase64String(Encoding.UTF8.GetBytes(description));

            GenerateHash();
        }

        public PayeerModel SetMerchantId(string merchantId)
        {
            MerchantId = merchantId;

            return this;
        }

        public PayeerModel SetSecretKey(string secretKey)
        {
            SecretKey = secretKey;

            return this;
        }

        public void GenerateHash()
        {
            if (MerchantId.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("Undefined MerchantId");
            }

            if (SecretKey.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("Undefined SecretKey");
            }

            var paymentInfoArray = new string[] {MerchantId, PaymentId, Amount, Currency, Description, SecretKey};
            var paymentInfoStr = String.Join(":", paymentInfoArray);

            byte[] data = Encoding.Default.GetBytes(paymentInfoStr);
            var result = new SHA256Managed().ComputeHash(data);
            Hash = BitConverter.ToString(result).Replace("-", "").ToUpper();
        }
    }
}