var CreateOut = {
    ShowWineForm: function () {
        $('#wineForm').show();
    },
    HideWineForm: function () {
        $('#wineForm').hide();
    },
    LoadTransactions: function () {
        var userId = $('#userId').val();
        $.getJSON("/search/transaction/user/" + userId, function (receivedData) {
            var transactions = $.map(receivedData, function (value) { return { value: value.Value, data: value.Key }; });
            $('#SearchTransaction').autocomplete({
                lookup: transactions,
                onSelect: function (suggestion) {
                    $('#TransactionId').val(suggestion.data);
                    $('#TransactionId').change();
                    // alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                }
            });
        });
    },
    LoadTransaction: function () {
        $.getJSON("/search/transaction/" + $('#TransactionId').val(), function (receivedData) {
            //bind data to form
            $('#distributor').text(receivedData.businessName + ", " + receivedData.businessAddress + ", " + receivedData.businessCity);
            $('#wineName').text(receivedData.wineName);
            $('#isFortified').text(receivedData.wineIsFortified);
            $('#isSparkling').text(receivedData.wineIsSparkling);
            $('#wineColor').text(receivedData.color);
            $('#grapes').text($.map(receivedData.grapes, function (value) { return value; }));
            $('#producer').text(receivedData.producer.Name + ", " + receivedData.producer.City);
            $('#distributor').text(receivedData.distributor.Name + ", " + receivedData.distributor.Address + ", " + receivedData.distributor.City);
        });

    },
    LoadDistributors: function () {
        $.getJSON("/search/distributor", function (receivedData) {
            var distributors = $.map(receivedData, function (value) { return { value: value.Value, data: value.Key }; });
            $('#SearchDistributor').autocomplete({
                lookup: distributors,
                onSelect: function (suggestion) {
                    $('#BusinessId').val(suggestion.data);
                    $('#BusinessId').change();
                    alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                }
            });
        });
    },
    LoadDistributor: function () {
        $.getJSON("/search/distributor/" + $('#BusinessId').val(), function (receivedData) {
            //bind data to form
            $('#distributor').text(receivedData.businessName + ", " + receivedData.businessAddress + ", " + receivedData.businessCity);
        });

    },
    LoadWines: function () {
        $.getJSON("/search/wine", function (receivedData) {
            var wines = $.map(receivedData, function (value) { return { value: value.Value, data: value.Key }; });
            $('#SearchWine').autocomplete({
                lookup: wines,
                onSelect: function (suggestion) {
                    $('#WineId').val(suggestion.data);
                    $('#WineId').change();
                    // alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                }
            });
        });
    },
    LoadWine: function () {
        $.getJSON("/search/wine/" + $('#WineId').val(), function (receivedData) {
            //bind data to form
            $('#wineName').text(receivedData.name);
            $('#isFortified').text(receivedData.isFortified);
            $('#isSparkling').text(receivedData.isSparkling);
            $('#wineColor').text(receivedData.color);
            $('#grapes').text($.map(receivedData.grapes, function (value) { return value; }));
            $('#producer').text(receivedData.producer.Name + ", " + receivedData.producer.City);
        });
    },
    SetupChangeBehavior: function () {
        $('#TransactionId').change(function () {
            useWatermark();
            $('#SearchTransaction').blur();
            CreateOut.ShowWineForm();
            CreateOut.LoadTransaction();
        });
    },
    Initialize: function () {
        useWatermark();
        CreateOut.HideWineForm();
        CreateOut.SetupChangeBehavior();
        CreateOut.LoadTransactions();
    }

};

$(document).ready(function () {
    CreateOut.Initialize();
});