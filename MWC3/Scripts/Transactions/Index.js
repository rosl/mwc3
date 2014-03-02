var Index = {
    Initialize: function () {
        Index.SetupSelectBehavior();
    },
    SetupSelectBehavior: function () {
        $('#transactiontypeId').change(function () {
            var transactiontypeId = $('#transactiontypeId').val();
            if (transactiontypeId === "") {
                transactiontypeId = 0;
            }
            window.location.href = "/Transaction/Type/" + transactiontypeId;
        });
    }
};

$(document).ready(function () {
    Index.Initialize();
});