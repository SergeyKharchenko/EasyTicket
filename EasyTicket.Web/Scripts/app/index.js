$(function () {
    $("#from-station")
        .select2({
            language: "ru",

            minimumInputLength: 3,
            placeholder: "Станция отправления",
            ajax: {
                url: "http://easyticketuzapi.azurewebsites.net/api/Stations/",
                dataType: 'json',
                type: "POST",
                params: { // extra parameters that will be passed to ajax
                    term: "Киев"
                },
                quietMillis: 250,
                processResults: function (data) {
                    var stations = data.stations.map(function(station) {
                        return {
                            id: station.id,
                            text: station.title
                        }
                    });
                    return { results: stations };
                }
            }
        });



    

    function State(id, text) {
        this.id = id;
        this.text = text;
    }

    State.prototype.toString = function () {
        return this.text + " (" + this.id + ")";
    };

    var ViewModel = function () {

        this.states = [
          new State("AL", "Alabama"),
          new State("AK", "Alaska"),
          new State("AZ", "Arizona"),
          /* ... */
          new State("WV", "West Virginia"),
          new State("WI", "Wisconsin"),
          new State("WY", "Wyoming")
        ];

        this.format = function (state) {
            var originalOption = state.element;
            return state.text.toUpperCase();
        }

        this.selectedState = ko.observable();

    };
    var viewModel = new ViewModel();
    window.viewModel = viewModel;
    ko.applyBindings(viewModel);
})