using Newtonsoft.Json;

namespace Goodnites.Payment.Payeer
{
    public class PayeerResponseModel
    {
        /// <summary>
        /// A unique payment number in the Payeer system
        /// Example: 123456
        /// </summary>
        [JsonProperty("m_operation_id")]
        public string InternalPaymentNumber { get; set; }

        /// <summary>
        /// The ID of the method of payment used to make the  payment. All methods of  payment can be viewed in  the "Appearance" tab under  merchant settings
        /// </summary>
        [JsonProperty("m_operation_ps")]
        public string MethodOfPayment { get; set; }

        /// <summary>
        /// The date and time of the transaction (UTC+3)
        /// </summary>
        [JsonProperty("m_operation_date")]
        public string DateAndTimeOfTransaction { get; set; }

        /// <summary>
        /// The date and time of the transaction (UTC+3)
        /// </summary>
        [JsonProperty("m_operation_pay_date")]
        public string DateAndTimeOfPayment { get; set; }

        /// <summary>
        /// The merchant ID registered in the Payeer system
        /// </summary>
        [JsonProperty("m_shop")]
        public string MerchantId { get; set; }

        /// <summary>
        /// The order ID following the merchant's invoicing system
        /// </summary>
        [JsonProperty("m_orderid")]
        public string PaymentId { get; set; }

        /// <summary>
        /// The amount of the payment
        /// </summary>
        [JsonProperty("m_amount")]
        public string PaymentAmount { get; set; }

        /// <summary>
        /// The currency used in the payment
        /// </summary>
        [JsonProperty("m_curr")]
        public string PaymentCurrency { get; set; }

        /// <summary>
        /// A description of the product or service encoded using a  base64 algorithm
        /// </summary>
        [JsonProperty("m_desc")]
        public string PaymentDescription { get; set; }

        /// <summary>
        /// The status of the payment in the Payeer system
        /// Values used: success
        /// </summary>
        [JsonProperty("m_status")]
        public string PaymentStatus { get; set; }

        /// <summary>
        /// A security signature used to check the cohesiveness of  the information obtained and  directly identify the sender
        /// Example:9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00 A08
        /// </summary>
        [JsonProperty("m_sign")]
        public string ElectronicSignature { get; set; }

        /// <summary>
        /// The email address of the customer who paid the  invoice
        /// Example: customer@email.com
        /// </summary>
        [JsonProperty("client_email")]
        public string CustomerEmailAddress { get; set; }

        /// <summary>
        /// The Payeer account number of the customer who paid  the invoice
        /// Example: P1000001
        /// </summary>
        [JsonProperty("client_account")]
        public string CustomerAccountNumber { get; set; }
        
        /// <summary>
        /// The transaction ID of the transfer to the merchant's account for this payment
        /// Example: 12345
        /// </summary>
        [JsonProperty("transfer_id")]
        public string TransferId { get; set; }

        /// <summary>
        /// The amount of money, minus all fees, transferred to  the merchant's account for  this payment
        /// </summary>
        [JsonProperty("summa_out")]
        public string TransferAmount { get; set; }

        /// <summary>
        /// A JSON array of data of additional parameters
        /// </summary>
        [JsonProperty("m_params")]
        public string AdditionalParameters { get; set; }
    }
}