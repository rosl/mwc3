var Index = {
    Initialize: function () {
        Load.CurrentWines();
        Click.ToTabs();

    }


};

var Load = {
    CurrentWines: function () {
        $('#wines').load("/Cellar/GetCurrentWines");
        return true;
    },
    AddTransaction: function () {
        $('#add_transaction').load("/Cellar/AddTransactionIn", function () {
            // showHiddenFields();
            Hide.WineForm();
            $('.datepicker').datepicker();
            useWatermark();
            $('#priceCents').change(function () { AddTransactionIn.NewPrice(); });
            $('#priceInt').change(function () { AddTransactionIn.NewPrice(); });
            Load.Wines();
            Load.Distributors();
            $('#WineId').change(function () {
                useWatermark();
                $('#SearchWine').blur();
                Show.WineForm();
                Load.Wine();
            });
        });
        return true;
    },
    TransactionsIn: function () {
        $('#transactions_in').load("/Cellar/GetTransactionsIn");
        return true;
    },
    TransactionsOut: function () {
        $('#transactions_out').load("/Cellar/GetTransactionsOut");
        return true;
    },
    TransactionsByWine: function (wineId) {
        $('#wines').load("/Cellar/GetTransactionsByWineId", { wineId: wineId });
        return true;
    },
    Distributors: function () {
        $.getJSON("/search/distributor", function (receivedData) {
            var distributors = $.map(receivedData, function (value) { return { value: value.Value, data: value.Key }; });
            $('#SearchDistributor').autocomplete({
                lookup: distributors,
                onSelect: function (suggestion) {
                    $('#BusinessId').val(suggestion.data);
                    $('#BusinessId').change();
                    //alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                }
            });
        });
    },
    Distributor: function () {
        $.getJSON("/search/distributor/" + $('#BusinessId').val(), function (receivedData) {
            //bind data to form
            $('#distributor').text(receivedData.name + ", " + receivedData.address + ", " + receivedData.city);
        });

    },
    Wines: function () {
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
    Wine: function () {
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
};

var Save = {};

var Render = {};

var Show = {
    WineForm: function () {
        $('#wineForm').show();
    }
};

var Hide = {
    WineForm: function () {
        $('#wineForm').hide();
    },
};


var Click = {
    ToTabs: function () {
        $('#wines-tab').click(function () { Load.CurrentWines(); });
        $('#add-transaction-tab').click(function () { Load.AddTransaction(); });
        $('#transactions-in-tab').click(function () { Load.TransactionsIn(); });
        $('#transactions-out-tab').click(function () { Load.TransactionsOut(); });
    },
    //ToWines: function () { Load.CurrentWines(); },
    //ToTransactions: function () { Load.Transactions(); },
    //ToTransactionsIn: function () { Load.TransactionsIn(); },
    //ToTransactionsOut: function () { Load.TransactionsOut(); },

};

var AddTransactionIn = {
    NewPrice: function () {
        try {
            var priceInt = parseInt($('#priceInt').val());
            var priceCents = parseInt($('#priceCents').val());
            if (!(isNaN(priceInt) || isNaN(priceCents))) {
                $("#Price").val(priceInt + "." + priceCents);
                $('#priceCents').val(priceCents);
                $('#priceInt').val(priceInt);
            } else {
                throw ("invalid number");
            }
        } catch (e) {
            $("#Price").val("0");
            $("#priceInt").val("0");
            $("#priceCents").val("00");
        }
    },
};


$(document).ready(function () {
    Index.Initialize();
});