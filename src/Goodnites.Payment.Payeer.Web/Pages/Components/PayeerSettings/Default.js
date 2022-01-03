(function ($) {
    $(function () {
        var l = abp.localization.getResource('AbpSettingManagement');

        $("#PayeerSettings").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();

            goodnites.payment.payeer.payeerSettings.update(form).then(function (result) {
                $(document).trigger("AbpSettingSaved");
            });
        });
    });
})(jQuery);