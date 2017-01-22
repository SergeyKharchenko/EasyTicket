var UZClient = function () {
    this.getTrains = function(request, callback) {
        $.post("http://easyticketuzapi.azurewebsites.net/api/Trains/", request).done(callback);
    }
};