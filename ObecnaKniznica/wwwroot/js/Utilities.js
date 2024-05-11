//deleteBtn.addEventListener('click', (event) => {
//    addBottomSpace(event);
//});

//deleteBtn.addEventListener('click', (event) => {
//    deleteBottomSpace(event);
//});

function addBottomSpace() {
    const divAdditionalBottom = document.getElementById('addedBottomSpace');
    if (divAdditional) {
        divAdditionalBottom.parentNode.removeChild(divAdditionalBottom); // vymazanie - navbar sa zavrel
    }
    else { // neexistuje 
        const navbarItems = document.getElementsByClassName('itemOfNavbar');
        if (navbarItems) {
            if (navbarItems.length > 0) {
                let count = 0;
                for (let i = 0; i < navbarItems.length; i++) {
                    let item = navbarItems[i];
                    if (item.className.includes("dropdown-toggle")) {
                        count += item.nextSibling.childNodes.length // child nodes elementu "ul"
                    } else {
                        count++;
                    }
                }
                // priprav element s prazdnym miestom
                const blankSpaceDiv = document.createElement('div');
                blankSpaceDiv.style.height = count * 5; // 5px * count
                blankSpaceDiv.className = "container-fluid";
                blankSpaceDiv.id = "addedBottomSpace";
                // najdi miesto, kde element zaradit
            }
        }
    }


    var divContent = document.getElementById('navbarClickedItem');
    var count = divContent.getChilds.count;
    var newDiv = Ddocument.createElement('div');
    newDiv.id = "addedBottomSpace;
    newDiv.style.width = 10 * count; // 10px * pocet vnorenych elementov
}

function deleteBottomSpace() {
    var div = document.getElementById('addedBottomSpace');
    if (div != null) {
        var parentDiv = div.getParent();
        parentDiv.delete(div);
    }
}

window.onload = (event) => {
    const toggleBtn = document.getElementById('hamburgerBtn');
    toggleBtn.addEventListener('click', (event) => {
        addBottomSpace();
    });

    toggleBtn.addEventListener('click', (event) => {
        deleteBottomSpace();
    });

    const tableHours = document.getElementById('tableHours');
    let dayPosition = new Date().getDay();
    if (tableHours != null && dayPosition !== 0) {
        let tbody = tableHours.childNodes.item(0).childNodes;
        tbody.item(2 * (dayPosition - 1)).style.fontWeight = 'bold'; //2,4,6
    }
}

