$.fn.clearSelect = function () {
    return this.each(function () {
        if (this.tagName == 'SELECT')
            this.options.length = 0;
    });
};

$.fn.fillSelect = function (data) {
    return this.clearSelect().each(function () {
        if (this.tagName == 'SELECT') {
            var dropdownList = this;
            $.each(data, function (index, optionData) {
                var option = new Option(optionData.Text, optionData.Value);

                //if ($.browser.msie()) {
                //    dropdownList.add(option);
                //}
                //else {
                    dropdownList.add(option, null);
                //}
            });
        }
    });
};