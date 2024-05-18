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

//window.onload = (event) => {
//    const toggleBtn = document.getElementById('hamburgerBtn');
//    toggleBtn.addEventListener('click', (event) => {
//        toggleBlankSpace();
//    });
//}

