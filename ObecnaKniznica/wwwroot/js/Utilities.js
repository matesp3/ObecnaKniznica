window.updateWorkingHours = () => {
    const tableHours = document.getElementById('tableHours');
    let dayPosition = new Date().getDay();
    if (tableHours != null && dayPosition !== 0) {
        const editClassName = "highlighted";
        const normalClassName = "";
        
        let tableRow = tableHours.firstChild.firstChild;
        let i = 1;
        while (tableRow) {
            if (tableRow.nodeName === "TR") { // strieda sa s textom
                const newClassName = (i === dayPosition) ? editClassName : normalClassName;
                tableRow.firstChild.className = newClassName;
                tableRow.lastChild.className = newClassName;
                i++;
            }
            tableRow = tableRow.nextSibling;
        }
    }
};

window.toggleBorderVisibility = (showBorder, index) => {
    if (typeof (showBorder) === "boolean" & typeof (index) === "number") {
        const wrapperDiv = document.querySelector('#author-wrapper-' + index.toString());
        if (wrapperDiv !== null) {
            if (showBorder === true)
                wrapperDiv.className += " showBorder";
            else {
                const idx = wrapperDiv.className.indexOf('showBorder');
                if (idx > -1)
                    wrapperDiv.className = wrapperDiv.className.substring(0, idx - 1);
            }
        }
    }
}

window.ShowAuthorsAlert = (message, errInputId) => {
    if (typeof (message) === "string" & typeof (errInputId) === "string") {
        const inputEl = document.querySelector('#' + errInputId);
        if (inputEl !== null) {
            const alertDiv = document.createElement('div');
            alertDiv.id = "authors-alert-div";
            alertDiv.role = "alert";
            alertDiv.className = "alert alert-danger mt-1 mb-3";
            alertDiv.innerText = message;
            alertDiv.style.color = "red";
            document.querySelector('#authorsContainer').appendChild(alertDiv);

            const inputToHighlight = document.querySelector('#' + errInputId);
            if (inputToHighlight !== null) {
                inputToHighlight.classList.replace('valid', 'invalid')
            }
        }
    }
}

window.HideAuthorsAlertIfPresent = () => {
    const alertDiv = document.querySelector('#authors-alert-div');
    if (alertDiv !== null) {
        alertDiv.remove();
    }
}

window.ToggleFirstAuthorDeleteButton = (showButton) => {
    if (typeof (showButton) === "boolean") {
        const delBtn = document.querySelector('#del-btn-0');
        if (delBtn !== null)
            delBtn.style.display = (showButton ? 'block' : 'none');
    }
}

