

export function init (id, mask, placeholder, isAutoUnmask, isNumeric, dotNetReference, changeCallback) {
    $(id).inputmask(mask, {
        "placeholder": placeholder,
        autoUnmask: isAutoUnmask,
        numericInput: isNumeric
    });
    $(id).on('change', function (e) {
        dotNetReference.invokeMethodAsync(changeCallback, $(id).val());
    });
}

export function initEmailMask (id, dotNetReference, changeCallback) {
    $(id).inputmask({
        mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
        greedy: false,
        onBeforePaste: function (pastedValue, opts) {
            pastedValue = pastedValue.toLowerCase();
            return pastedValue.replace("mailto:", "");
        },
        definitions: {
            '*': {
                validator: "[0-9A-Za-z!#$%&'*+/=?^_`{|}~\-]",
                cardinality: 1,
                casing: "lower"
            }
        }
    });
    $(id).on('change', function (e) {
        dotNetReference.invokeMethodAsync(changeCallback, $(id).val());
    });
}

export function initDecimalMask (id, dotNetReference, changeCallback) {
    $(id).inputmask('decimal', {
        rightAlignNumerics: false
    });
    $(id).on('change', function (e) {
        dotNetReference.invokeMethodAsync(changeCallback, $(id).val());
    });
}