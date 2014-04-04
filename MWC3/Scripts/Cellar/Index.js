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
        $('#add_transaction').load("/Cellar/AddTransactionIn", function() {
            showHiddenFields();
            $('.datepicker').datepicker();
            useWatermark();
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
    }
};

var Save = {};

var Render = {};

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


$(document).ready(function () {
    Index.Initialize();
});