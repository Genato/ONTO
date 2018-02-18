$(document).ready(function () { 

        var myHub = $.connection.ontoHub;
        $.connection.hub.start();

        myHub.client.showNotificationBar = function () {
            AddCookieForNotificationBar();
        };
});


