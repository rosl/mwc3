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

useWatermark = function () {
    $(":input[data-watermark]").each(function () {
        $(this).val($(this).attr("data-watermark"));
        $(this).css("color", "#a8a8a8");
        $(this).bind("focus", function () {
            if ($(this).val() == $(this).attr("data-watermark")) $(this).val('');
            $(this).css("color", "#000000");
        });
        $(this).bind("blur", function () {
            if ($(this).val() == '') {
                $(this).val($(this).attr("data-watermark"));
                $(this).css("color", "#a8a8a8");
            }
            else {
                $(this).css("color", "#000000");
            }
        });
    });
};