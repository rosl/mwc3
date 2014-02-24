var Index = {
    Initialize: function () {
        Index.SetupSelectBehavior();
    },
    SetupSelectBehavior: function () {
        $('#filterId').change(function () {
            window.location.href = "/Grape/" + $('#filterId').val();
        });
    }
};

$(document).ready(function () {
    Index.Initialize();
});