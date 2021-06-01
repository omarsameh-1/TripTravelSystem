$("#ex2").slider({});
var rangepicker = new RangePicker("#ex6");
rangepicker.on("slide", function (slideEvt) {
    $("#ex6RangePickerVal").text(slideEvt.value);
});