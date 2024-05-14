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

//window.onload = (event) => {
//    const toggleBtn = document.getElementById('hamburgerBtn');
//    toggleBtn.addEventListener('click', (event) => {
//        toggleBlankSpace();
//    });
//}

