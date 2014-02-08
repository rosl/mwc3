var Index = {
    Initialize: function () {
        Index.SetupSelectBehavior();
    },
    SetupSelectBehavior: function () {
        $('#countryId').change(function () {
            window.location.href = "/Qualifiaction/" + $('#countryId').val();
        });
    }
};

$(document).ready(function () {
    Index.Initialize();
});