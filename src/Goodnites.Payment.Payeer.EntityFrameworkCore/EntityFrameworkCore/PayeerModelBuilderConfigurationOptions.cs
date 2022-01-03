using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Goodnites.Payment.Payeer.EntityFrameworkCore
{
    public class PayeerModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public PayeerModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}