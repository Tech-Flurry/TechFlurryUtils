
function bindEvents (id, dotNetReference, closingClalback, closeCallback, openingCallback, openCallback, selectingCallabck, selectCallback, unselectingCallback, unselectCallback, clearingCallback, clearCallback) {
    $(id).on('select2:closing', function (e) {
        dotNetReference.invokeMethodAsync(closingClalback);
    });
    $(id).on('select2:close', function (e) {
        dotNetReference.invokeMethodAsync(closeCallback);
    });
    $(id).on('select2:opening', function (e) {
        dotNetReference.invokeMethodAsync(openingCallback);
    });
    $(id).on('select2:open', function (e) {
        dotNetReference.invokeMethodAsync(openCallback);
    });
    $(id).on('select2:selecting', function (e) {
        dotNetReference.invokeMethodAsync(selectingCallabck);
    });
    $(id).on('select2:select', function (e) {
        dotNetReference.invokeMethodAsync(selectCallback);
    });
    $(id).on('select2:unselecting', function (e) {
        dotNetReference.invokeMethodAsync(unselectingCallback);
    });
    $(id).on('select2:unselect', function (e) {
        dotNetReference.invokeMethodAsync(unselectCallback);
    });
    $(id).on('select2:clearing', function (e) {
        dotNetReference.invokeMethodAsync(clearingCallback);
    });
    $(id).on('select2:clear', function (e) {
        dotNetReference.invokeMethodAsync(clearCallback);
    });
}

function unBindEvents (id) {
    $(id).off('change');
    $(id).off('select2:closing');
    $(id).off('select2:close');
    $(id).off('select2:opening');
    $(id).off('select2:open');
    $(id).off('select2:selecting');
    $(id).off('select2:select');
    $(id).off('select2:unselecting');
    $(id).off('select2:unselect');
    $(id).off('select2:clearing');
    $(id).off('select2:clear');
}

function customMatcher (params, data, dotNetReference, searchCallback) {

    return (async function (params, data) {
        if ($.trim(params.term) === '') {
            return data;
        }
        if (typeof data.children !== 'undefined') {
            //Grouped Item
            var filterItems = [];
            data.children.forEach(function (e) {
                filterItems.push({ id: e.id, text: e.text, isDisabled: e.disabled, selected: e.selected });
            });
            var result = await dotNetReference.invokeMethodAsync(searchCallback, params.term, filterItems);
            if (result.length) {
                var modifiedData = $.extend({}, data, true);
                modifiedData.children = data.children.filter(function (x) {
                    return result.some((r) => r.id === x.id);
                });
                return modifiedData;
            }
        }
        else if (typeof data.id !== 'undefined' && data.id != "") {
            //UnGrouped Item
        }
        //No Item
        return null;
        console.log(params);
        console.log(data);
    })(params, data);
}

export function initSingleSelect (id, placeholder, isClearAllowed, isSearchable, minSearchResults, minimumInputLength, maximumInputLength, dotNetReference, changeCallback, closingClalback, closeCallback, openingCallback, openCallback, selectingCallabck, selectCallback, unselectingCallback, unselectCallback, clearingCallback, clearCallback, searchCallback) {

    if (isSearchable) {

        $.fn.select2.amd.define("CustomSelectAdapter", ["select2/utils", "select2/data/select", "select2/data/minimumInputLength", "select2/data/maximumInputLength"], function (Utils, Select, MinimumInputLength, MaximumInputLength) {
            Select.prototype.matches = async function (params, data) {
                var matcher = this.options.get('matcher');
                return await matcher(params, data);
            };
            Select.prototype.query = async function (params, callback) {
                var data = [];
                var self = this;

                var $options = this.$element.children();


                for (var i in $options) {
                    if (!$options[i].tagName || ($options[i].tagName.toLowerCase() !== 'option' && $options[i].tagName.toLowerCase() !== 'optgroup')) {
                        break;
                    }

                    var $option = $($options[i]);
                    var option = self.item($option);
                    var matches = await self.matches(params, option);
                    if (matches !== null) {
                        data.push(matches);
                    }
                }
                callback({
                    results: data
                });
            };

            var customSelectAdapter = Utils.Decorate(Select, MinimumInputLength);
            customSelectAdapter = Utils.Decorate(customSelectAdapter, MaximumInputLength);
            return customSelectAdapter;
        });


        $(id).select2({
            placeholder: placeholder,
            allowClear: isClearAllowed,
            minimumResultsForSearch: minSearchResults,
            minimumInputLength: minimumInputLength,
            maximumInputLength: maximumInputLength,
            dataAdapter: $.fn.select2.amd.require("CustomSelectAdapter"),
            matcher: async function (params, data) {
                if ($.trim(params.term) === '') {
                    return data;
                }
                if (typeof data.children !== 'undefined') {
                    //Grouped Item
                    var filterItems = [];
                    data.children.forEach(function (e) {
                        filterItems.push({ id: e.id, text: e.text, isDisabled: e.disabled, selected: e.selected });
                    });
                    var result = await dotNetReference.invokeMethodAsync(searchCallback, params.term, filterItems);
                    if (result.length) {
                        var modifiedData = $.extend({}, data, true);
                        modifiedData.children = data.children.filter(function (x) {
                            return result.some((r) => r.id === x.id);
                        });
                        return modifiedData;
                    }
                }
                else if (typeof data.id !== 'undefined' && data.id != "") {
                    //UnGrouped Item
                }
                //No Item
                return null;
            }
        });
    }
    else {
        $(id).select2({
            placeholder: placeholder,
            allowClear: isClearAllowed,
            minimumResultsForSearch: Infinity,
            minimumInputLength: minimumInputLength,
            maximumInputLength: maximumInputLength
        });
    }
    $(id).on('change', function (e) {
        var val = "";
        if ($(id).val()) {
            val = String($(id).val());
        }
        dotNetReference.invokeMethodAsync(changeCallback, val);
    });
    bindEvents(id, dotNetReference, closingClalback, closeCallback, openingCallback, openCallback, selectingCallabck, selectCallback, unselectingCallback, unselectCallback, clearingCallback, clearCallback);
}

export function open (id) {
    $(id).select2('open');
}

export function close (id) {
    $(id).select2('close');
}

export function isInitialized (id) {
    if ($(id).hasClass("select2-hidden-accessible")) {
        return true;
    }
    return false;
}

export function destroy (id) {
    $(id).select2('destroy');
    unBindEvents(id);
}
