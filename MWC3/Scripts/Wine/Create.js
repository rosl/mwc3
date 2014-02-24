var Create = {
    Initialize: function() {
        Create.SetupMultiSelect();
        Create.SetupChangeBehavior();
    },
    LoadRegions: function(countryId) {
        $.getJSON("/api/country/" + countryId + "/region", function(data) {
            $("#RegionId").fillSelect(data);
            $("#RegionId").change();
        });
    },
    LoadBusinesses: function (countryId) {
        var url = "/api/country/" + countryId + "/producer";
        $.getJSON(url, function(data) {
            $("#BusinessId").fillSelect(data);
        });
    },
    LoadQualificationsByCountry: function (countryId) {
        $("#QualificationId").clearSelect();
        var url = "/api/country/" + countryId + "/qualification";
        $.getJSON(url, function (data) {
            $("#QualificationId").fillSelect(data);
        });
    },
    LoadQualificationsByCountryAndRegion: function(countryId, regionId) {
        $("#QualificationId").clearSelect();
        var url = "/api/country/" + countryId + "/region/" + regionId + "/qualification";
        $.getJSON(url, function (data) {
            $("#QualificationId").fillSelect(data);
        });
    },
    SetupMultiSelect: function () {
        $('.multiselect').multiselect({
            maxHeight: 200,
            enableCaseInsensitiveFiltering: true
        });
    },
    SetupChangeBehavior: function () {
        $('#CountryId').change(function() {
            var countryId = $('#CountryId').val();
            $("#RegionId").clearSelect();
            $("#BusinessId").clearSelect();
            Create.LoadRegions(countryId);
            Create.LoadBusinesses(countryId);
            Create.LoadQualificationsByCountry(countryId);
        });
        $('#RegionId').change(function() {
            var countryId = $('#CountryId').val();
            var regionId = $('#RegionId').val();
            Create.LoadQualificationsByCountryAndRegion(countryId, regionId);
        });
    }
};

$(document).ready(function () {
    Create.Initialize();
});
