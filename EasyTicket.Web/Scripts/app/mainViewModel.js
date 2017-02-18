var MainViewModel = function () {
    this.uzClient = new UzClient();

    this.stationFrom = ko.observable();
    this.stationFrom2 = ko.pureComputed(function () {
        return this.stationFrom() ||
            "2210900"; // Кривой Рог Главный
    }, this);

    this.stationTo = ko.observable(); 
    this.stationTo2 = ko.pureComputed(function () {
        return this.stationTo() ||
            "2200001"; // Киев
    }, this);

    this.date = ko.observable(new Date());
    this.trains = ko.observableArray();

    this.search = function () {
        var viewModel = this;
        var date = viewModel.date().toUZFormat();
        this.uzClient.getTrains({
            stationFromId: viewModel.stationFrom(),
            stationToId: viewModel.stationTo(),
            date: date
        }, function (data) {
            viewModel.trains.removeAll();
            for (var i = 0; i < data.trains.length; i++) {
                var train = data.trains[i];
                    viewModel.trains.push(train);
            }
        });
    };
};