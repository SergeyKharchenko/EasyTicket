$(function () {
    $("#from-station")
        .select2({
            language: "ru",

            minimumInputLength: 3,
            placeholder: "Станция отправления",
            ajax: {
                url: "/Tickets/Values",
                dataType: 'json',
                type: "GET",
                quietMillis: 250,
                processResults: function (data) {
                    return data;
                }
            }
        });
})