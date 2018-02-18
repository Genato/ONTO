$(document).ready(function () {

    // Get NotificationBar cookie and show/hide notification bar depending whether cookie is exists or not
    var notificationBarCookie = $.cookie("NotificationBar");

    if (notificationBarCookie === undefined) {
        hideNotificationBar();
    }
    else {
        showNotificationBar();
    }
    //

    // Hide notification bar and remove NotificationBar cookie
    $(".close").click(function () {
        hideNotificationBar();
        $.removeCookie('NotificationBar', { path: '/' });
    });

});

//Adds cookie for notification bar. (After you read cookie, if cookie is NULL then hide notification bar otherwise keep it shown)
function AddCookieForNotificationBar() {

    var date = new Date();
    date.setTime(date.getTime() + (10 * 1000)); // 10seconds

    $.cookie('NotificationBar', 'DummyValue', { expires: date });
};

function hideNotificationBar() {
    $("#notification").slideUp("slow");
};

function showNotificationBar() {
    $("#notification").slideDown("slow");
};
