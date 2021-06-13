
var _module;

export function initWithCallback (id, dotNetReference, searchCallback, iconId) {
    KTLayoutQuickSearch.init(iconId);
    _module = KTLayoutSearchCustom().initWithEvents(id, dotNetReference, searchCallback);
}

export function successResult (res) {
    _module.success(res);
}

export function errorResult (res) {
    _module.error(res);
}

var KTLayoutSearchCustom = function () {
    // Private properties
    var _target;
    var _form;
    var _input;
    var _closeIcon;
    var _resultWrapper;
    var _resultDropdown;
    var _resultDropdownToggle;
    var _closeIconContainer;
    var _inputGroup;
    var _query = '';

    var _hasResult = false;
    var _timeout = false;
    var _isProcessing = false;
    var _requestTimeout = 200; // ajax request fire timeout in milliseconds
    var _spinnerClass = 'spinner spinner-sm spinner-primary';
    var _resultClass = 'quick-search-has-result';
    var _minLength = 2;
    var _searchCallback;
    var _dotnetObjectReference;

    // Private functions
    var _showProgress = function () {
        _isProcessing = true;
        KTUtil.addClass(_closeIconContainer, _spinnerClass);

        if (_closeIcon) {
            KTUtil.hide(_closeIcon);
        }
    }

    var _hideProgress = function () {
        _isProcessing = false;
        KTUtil.removeClass(_closeIconContainer, _spinnerClass);

        if (_closeIcon) {
            if (_input.value.length < _minLength) {
                KTUtil.hide(_closeIcon);
            } else {
                KTUtil.show(_closeIcon, 'flex');
            }
        }
    }

    var _showDropdown = function () {
        if (_resultDropdownToggle && !KTUtil.hasClass(_resultDropdown, 'show')) {
            $(_resultDropdownToggle).dropdown('toggle');
            $(_resultDropdownToggle).dropdown('update');
        }
    }

    var _hideDropdown = function () {
        if (_resultDropdownToggle && KTUtil.hasClass(_resultDropdown, 'show')) {
            $(_resultDropdownToggle).dropdown('toggle');
        }
    }
    var _processSearch = function () {
        if (_hasResult && _query === _input.value) {
            _hideProgress();
            KTUtil.addClass(_target, _resultClass);
            _showDropdown();
            KTUtil.scrollUpdate(_resultWrapper);

            return;
        }

        _query = _input.value;

        KTUtil.removeClass(_target, _resultClass);
        _showProgress();
        _hideDropdown();

        setTimeout(function () {
            _dotnetObjectReference.invokeMethodAsync(_searchCallback, _query);
        }, _requestTimeout);
    }

    var _handleCancel = function (e) {
        _input.value = '';
        _query = '';
        _hasResult = false;
        KTUtil.hide(_closeIcon);
        KTUtil.removeClass(_target, _resultClass);
        _hideDropdown();
    }

    var _handleSearch = function () {
        if (_input.value.length < _minLength) {
            _hideProgress();
            _hideDropdown();

            return;
        }

        if (_isProcessing == true) {
            return;
        }

        if (_timeout) {
            clearTimeout(_timeout);
        }

        _timeout = setTimeout(function () {
            _processSearch();
        }, _requestTimeout);
    }

    // Public methods
    return {
        initWithEvents: function (id, dotnetObjectReference, searchCallback) {
            _target = KTUtil.getById(id);

            if (!_target) {
                return;
            }

            _form = KTUtil.find(_target, '.quick-search-form');
            _input = KTUtil.find(_target, '.form-control');
            _closeIcon = KTUtil.find(_target, '.quick-search-close');
            _resultWrapper = KTUtil.find(_target, '.quick-search-wrapper');
            _resultDropdown = KTUtil.find(_target, '.dropdown-menu');
            _resultDropdownToggle = KTUtil.find(_target, '[data-toggle="dropdown"]');
            _inputGroup = KTUtil.find(_target, '.input-group');
            _closeIconContainer = KTUtil.find(_target, '.input-group .input-group-append');
            _searchCallback = searchCallback;
            _dotnetObjectReference = dotnetObjectReference;
            // Attach input keyup handler
            $(_input).on('keyup', function () {
                _handleSearch();
            });
            $(_input).on('focus', function () {
                _handleSearch();
            });

            // Prevent enter click
            _form.onkeypress = function (e) {
                var key = e.charCode || e.keyCode || 0;
                if (key == 13) {
                    e.preventDefault();
                }
            }

            KTUtil.addEvent(_closeIcon, 'click', _handleCancel);
            return {
                success: function (res) {
                    _hasResult = true;
                    _hideProgress();
                    KTUtil.addClass(_target, _resultClass);
                    KTUtil.setHTML(_resultWrapper, res);
                    _showDropdown();
                    KTUtil.scrollUpdate(_resultWrapper);
                },
                error: function (res) {
                    _hasResult = false;
                    _hideProgress();
                    KTUtil.addClass(_target, _resultClass);
                    KTUtil.setHTML(_resultWrapper, '<span class="font-weight-bold text-muted">' + res + '</div>');
                    _showDropdown();
                    KTUtil.scrollUpdate(_resultWrapper);
                }
            }
        }
    };
};
