function showAlert(alertSelector, timeToHide) {
    jQuery(alertSelector)
        .css('opacity', '0')
        .show();

    jQuery(alertSelector).animate({
        opacity: 1
    }, 500);

    setTimeout(function () {
        jQuery(alertSelector).animate({
            opacity: 0
        }, {
            duration: 500,
            complete: function () {
                jQuery(alertSelector).hide();            
            }
        });
    }, timeToHide);
}