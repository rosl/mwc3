var Index = {
    Initialize: function () {
        Index.SetupSelectBehavior();
    },
    SetupSelectBehavior: function () {
        $('#countryId').change(function () {
            window.location.href = "/Search/Country/" + $('#countryId').val() + "Region";
        });
    }
};

$(document).ready(function () {
    Index.Initialize();
});