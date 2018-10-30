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

    /*
     * AJAX - section
     */

    //$('.manage-roles-div-table #DeleteRole').on( "click", function() {

    //    var roleName = $('#DeleteRole').text();

    //    $.ajax({
    //        url: "/role/delete/" + roleName,
    //        type: "DELETE",
    //        dataType: "json",
    //        success: function (resultMessage) {
    //            AddCookieForNotificationBar("lightgreen", resultMessage)
    //        },
    //        error: function (resultMessage) {
    //            AddCookieForNotificationBar("orangered", resultMessage)
    //        }
    //    });
    //});

    /*
     * AJAX - section
     */

});

/*
 * HELPER METHODS
 */

//Adds cookie for notification bar. (After you read cookie, if cookie is NULL then hide notification bar otherwise keep it shown)
function AddCookieForNotificationBar(notificationTypeColor, message) {

    var date = new Date();
    date.setTime(date.getTime() + (10 * 1000)); // 10seconds

    $.cookie('NotificationBar', message + "/" + notificationTypeColor, { expires: date });
    showNotificationBar();
};

function hideNotificationBar() {
    $("#notification").slideUp("slow");
};

function showNotificationBar() {
    var cookieValues = $.cookie("NotificationBar").split('/');
    var notificatnotificationTypeColorionType = cookieValues[1];
    var valueMessage = cookieValues[0];

    $("#notification_text").text(valueMessage);
    $("#notification").css("background-color", notificatnotificationTypeColorionType);
    $("#notification").slideDown("slow");
};

function refreshPartialView_UsersInRole(elementID, roleID) {
    $("#" + elementID).load("ListOfUsersForRole?roleID=" + roleID);
}


/*
 * HELPER METHODS
 */
