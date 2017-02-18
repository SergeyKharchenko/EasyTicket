﻿var RequestViewModel = function () {
    var viewModel = this;

    this.uzClient = new UzClient();

    this.stationFromSettings = new ViewModelSettings().select2;
    this.stationFromSettings.selectedText = ko.observable("Львов");
    this.stationFrom = ko.observable();
    this.stationFrom2 = ko.pureComputed(function () {
        return viewModel.stationFrom() ||
            "2218000";
    });

    this.stationToSettings = new ViewModelSettings().select2;
    this.stationToSettings.selectedText = ko.observable("Киев");
    this.stationTo = ko.observable();
    this.stationTo2 = ko.pureComputed(function () {
        return viewModel.stationTo() ||
            "2200001";
    });

    this.date = ko.observable(new Date());

    this.name = ko.observable("Сергей");
    this.surname = ko.observable("Харченко");
    this.email = ko.observable("hsvlis4@gmail.com");

    var Wagon = function(type, typeId) {
        this.type = type;
        this.typeId = typeId;
    };

    this.wagons = ko.observableArray([
        new Wagon("Плацкарт", "Economy"),
        new Wagon("Купэ", "Coupe")
    ]);
    this.selectedWagon = ko.observable();

    var places = [];
    for (var i = 0; i < 20; i++) {
        places.push(i + 1);
    }
    this.places = ko.observableArray(places);
    this.selectedPlaces = ko.observableArray([4]);

    this.loading = ko.observable(false);

    this.canBuy = ko.pureComputed(function() {
        return !viewModel.loading() &&
            viewModel.stationFrom2() &&
            viewModel.stationTo2() &&
            viewModel.name() &&
            viewModel.surname() &&
            viewModel.email() &&
            viewModel.selectedWagon() &&
            viewModel.selectedPlaces().length;
    });
    this.buy = function() {
        viewModel.loading(true);
        var date = viewModel.date().toUZFormat();
        var request = {
            stationFromId: viewModel.stationFrom2(),
            stationFromTitle: viewModel.stationFromSettings.selectedText(),
            stationToId: viewModel.stationTo2(),
            stationToTitle: viewModel.stationToSettings.selectedText(),
            date: date,
            passangerName: viewModel.name(),
            passangerSurname: viewModel.surname(),
            passangerEmail: viewModel.email(),
            wagonType: viewModel.selectedWagon().typeId,
            places: viewModel.selectedPlaces()
        };
        viewModel.uzClient.sendRequest(request, function() {
            console.log("request was successfully sent");
            viewModel.loading(false);
        }, function(data) {
            console.log(data.responseText);
            viewModel.loading(false);
        });
    };
};