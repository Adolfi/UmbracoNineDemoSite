function updateDataConsentTextField(obj) {
    const hidden = document.getElementById('DataConsentText');
    if (!hidden) return;

    hidden.value = obj.checked ? "true" : "";
    //console.debug(hidden.value);
    const form = obj.closest("form");
    if (form) {
        $(form).validate().element("#DataConsentText");
    }
}