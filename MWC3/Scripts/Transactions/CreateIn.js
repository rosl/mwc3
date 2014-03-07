var Create = {
    ShowWineForm: function () {
        $('#wineForm').show();
    },
    HideWineForm: function () {
        $('#wineForm').hide();
    },
    LoadDistributors: function () {
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
    LoadDistributor: function () {
        $.getJSON("/search/distributor/" + $('#BusinessId').val(), function (receivedData) {
            //bind data to form
            $('#distributor').text(receivedData.name + ", " + receivedData.address + ", " + receivedData.city);
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
        $('#WineId').change(function () {
            useWatermark();
            $('#SearchWine').blur();
            Create.ShowWineForm();
            Create.LoadWine();
        });
        $('#BusinessId').change(function () {
            $('#SearchDistributor').blur();
            useWatermark();
            Create.LoadDistributor();
        });
        $('#priceCents').change(function () {
            Create.NewPrice();
        });
        $('#priceInt').change(function () {
            Create.NewPrice();
        });
    },
    SetupClickBehavior: function () {
        $('#btAddWine').click(function () {
            alert('button clicked');
            $('#myModal').modal('show');
            var frameSrc = "/Wine/Create";
                //$('#myModal').on('show', function () {
                //    alert('show iframe');
                //    $('iframe').attr("src", "/Wine/Create");
                //});
            //$('#myModal').modal('show');
        });
    },
    NewPrice: function () {
        try {
            var priceInt = parseInt($('#priceInt').val());
            var priceCents = parseInt($('#priceCents').val());
            if (!(isNaN(priceInt) || isNaN(priceCents))) {
                $("#Price").val(priceInt + "." + priceCents);
            } else {
                throw ("invalid number");
            }
        } catch (e) {
            $("#Price").val("0");
            $("#priceInt").val("0");
            $("#priceCents").val("00");
        }
    },
    Initialize: function () {
        $('.datepicker').datepicker();
        useWatermark();
        Create.HideWineForm();
        Create.SetupChangeBehavior();
        Create.SetupClickBehavior();
        Create.LoadDistributors();
        Create.LoadWines();
    }
};

$(document).ready(function () {
    Create.Initialize();
});