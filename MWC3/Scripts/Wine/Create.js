var Create = {
    Initialize: function() {
        Create.SetupMultiSelect();
        Create.SetupChangeBehavior();
    },
    LoadRegions: function(countryId) {
        $.getJSON("/api/Country/" + countryId + "/Region", function(data) {
            $("#RegionId").fillSelect(data);
            $("#RegionId").change();
        });
    },
    LoadBusinesses: function(countryId) {
        $.getJSON("/Business/GetProducers", { countryId: countryId }, function(data) {
            $("#BusinessId").fillSelect(data);
        });
    },
    LoadQualifications: function(countryId, regionId) {
        $("#QualificationId").clearSelect();
        $.getJSON("/Qualifiaction/GetQualifications", { countryId: countryId, RegionId: regionId }, function (data) {
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
        });
        $('#RegionId').change(function() {
            var countryId = $('#CountryId').val();
            var regionId = $('#RegionId').val();
            Create.LoadQualifications(countryId, regionId);
        });
    }
};

$(document).ready(function () {
    Create.Initialize();
});
