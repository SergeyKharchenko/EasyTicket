export default class HttpClient {
    constructor(basePath) {
        this.basePath = basePath || '';
    }

    sendQueryGet(url, data, successCallback, failCallback) {
        $.get(this.basePath + url + "?" + $.param(data)).done(successCallback).fail(failCallback);
    }

    sendGet(url, successCallback, failCallback, ...path) {
        if (path && path.length) {
            url += path.join('/');
        }
        $.get(this.basePath + url).done(successCallback).fail(failCallback);
    }

    sendPost(url, data, successCallback, failCallback) {
        $.post(this.basePath + url, data).done(successCallback).fail(failCallback);
    }
}