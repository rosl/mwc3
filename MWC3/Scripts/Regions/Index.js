var Index = {
    Initialize: function () {
        Index.SetupSelectBehavior();
    },
    SetupSelectBehavior: function () {
        $('#countryId').change(function () {
            window.location.href = "/Region/" + $('#countryId').val();
        });
    }
};

$(document).ready(function () {
    Index.Initialize();
});