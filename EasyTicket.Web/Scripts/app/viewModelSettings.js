var ViewModelSettings = function () {

    this.select2 = {
        language: "ru",

        minimumInputLength: 3,
        placeholder: "Станция отправления",
        ajax: {
            //url: "http://easyticketuzapi.azurewebsites.net/api/Stations/",
            url: "http://localhost:7552/api/Stations/",
            dataType: 'json',
            type: "POST",
            params: {
                // extra parameters that will be passed to ajax
                // term: "Киев"
            },
            quietMillis: 1000,
            processResults: function(data) {
                var stations = data.stations.map(function(station) {
                    return {
                        id: station.id,
                        text: station.title
                    }
                });
                return { results: stations };
            }
        }
    };

};