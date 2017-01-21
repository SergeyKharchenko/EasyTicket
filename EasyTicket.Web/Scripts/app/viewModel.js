var ViewModel = function () {
    this.settings = new ViewModelSettings();

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

    this.test_date = ko.observable();

    this.search = function() {
        
    }
};

var viewModel = new ViewModel();
window.viewModel = viewModel;
ko.applyBindings(viewModel);