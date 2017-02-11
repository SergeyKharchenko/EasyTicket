var ViewModel = function () {
    this.settings = new ViewModelSettings();
    this.uzClient = new UzClient();
    this.main = new MainViewModel();
    this.request = new RequestViewModel();

};

var viewModel = new ViewModel();
window.viewModel = viewModel;
ko.applyBindings(viewModel);