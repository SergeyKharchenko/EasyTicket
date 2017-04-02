import HttpClient from './httpClient';

export default class EasyTicketClient {
    constructor() {
        this._httpClient = new HttpClient(window.easyTicket.config.apiUrl);
    }

    getStations(term, successCallback, failCallback) {
        this._httpClient.sendPost('stations', { term: term }, successCallback, failCallback);
    }

    sendRequest(request, successCallback, failCallback) {
        this._httpClient.sendPost('request', request, successCallback, failCallback);
    }

    getReservation(token, successCallback, failCallback) {
        this._httpClient.sendGet('reservation', successCallback, failCallback, token);
    }
}