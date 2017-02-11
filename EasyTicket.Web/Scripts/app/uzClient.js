var UzClient = function () {
    this.getTrains = function(request, callback) {
        $.post("http://easyticketuzapi.azurewebsites.net/api/Trains/", request).done(callback);
    },
    this.sendRequest = function(request, callback, failCallback) {
        $.post("http://easyticketuzapi.azurewebsites.net/api/Request/", request).done(callback).fail(failCallback);
        //$.post("http://localhost:7552//api/Request/", request).done(callback).fail(failCallback);
    }
};