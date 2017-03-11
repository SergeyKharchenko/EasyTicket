//var UzClient = function () {
//    this.getTrains = function(request, callback) {
//        $.post("http://easyticketuzapi.azurewebsites.net/api/Trains/", request).done(callback);
//    },
//    this.sendRequest = function(request, callback, failCallback) {
//        $.post("http://easyticketuzapi.azurewebsites.net/api/Request/", request).done(callback).fail(failCallback);
//        //$.post("http://localhost:7552/api/Request/", request).done(callback).fail(failCallback);
//    }
//};

import HttpClient from './httpClient';

export default class UzClient {
    constructor() {
        //this._httpClient = new HttpClient("http://localhost:7552/api/");
        this._httpClient = new HttpClient('http://easyticketuzapi.azurewebsites.net/api/');
    }

    getStations(term, successCallback, failCallback) {
        this._httpClient.sendPost('stations', { term: term }, successCallback, failCallback);
    }
}