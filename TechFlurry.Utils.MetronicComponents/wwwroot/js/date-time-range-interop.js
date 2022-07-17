

export function init (startDate, endDate, id, applyButtonCss, cancelButtonCss, format, includeTime, timePickerIncement, singleDatePicker, showDropDown, ranges, dotNetReference, searchCallback) {
    var convertedRanges = {};
    for (var key in ranges) {
        var arr = [];
        for (var dates in ranges[key]) {
            arr.push(moment(ranges[key][dates]));
        }
        convertedRanges[key] = arr;
    }

    $('#' + id + ', #' + id + '_modal').daterangepicker({
        buttonClasses: ' btn',
        applyClass: applyButtonCss,
        cancelClass: cancelButtonCss,
        timePicker: includeTime,
        timePickerIncrement: timePickerIncement,
        locale: {
            format: format
        },
        singleDatePicker: singleDatePicker,
        showDropdowns: showDropDown,
        startDate: moment(startDate, 'YYYY-MM-DD'),
        endDate: moment(endDate, 'YYYY-MM-DD'),
        ranges: convertedRanges
    }, function (start, end, label) {
        dotNetReference.invokeMethodAsync(searchCallback, start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'), label);
    });
}
