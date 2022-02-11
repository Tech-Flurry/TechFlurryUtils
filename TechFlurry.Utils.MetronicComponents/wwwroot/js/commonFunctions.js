
export function getInnerHtml (e) {
    return e.innerHTML;
}

export function showModal (id) {
    $(id).modal('show');
}
export function hideModal (id) {
    $(id).modal('hide');
}
export function initToolTip (e) {
    $(e).tooltip()
}