$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "https://localhost:44379/Home/Insert",
        data: {
            requestData: JSON.stringify({ Username: "Swaroop" })
        },
        success: function (responseObj) {
            console.log(responseObj)
        }
    });

    ////////////////////////////////////////////////////////////////////////////////////
    $.ajax({
        type: "GET",
        url: "https://localhost:44379/Home/Data",
        
        success: setAllData
    });
});

function setAllData(responseObj) {
    console.log(responseObj)
    const table = document.getElementById("tbody");
    var row;
    responseObj.forEach((e) => {
        row += `
                <tr id="${e.u_id}">
                        <th scope="row">${e.u_id}</th>
                        <td>${e.username}</td>
                        <td>${e.email}</td>
                        <td>${e.password}</td>
                        <td>${e.registerDate}</td>
                        <td><button class="btn btn-primary" id="edit" value="${e.u_id}">Edit</button></td>
                        <td>
                            <button class="btn btn-danger" id="delete" value="${e.u_id}">Delete</button>
                        </td>
                    </tr>
                `;
    })
    table.innerHTML = row;
    document.querySelectorAll("#delete").forEach((e) => {
        e.addEventListener('click', deleteProcess)
    });
    //document.getElementById("edit").addEventListener('click', editProcess);
}

    /////////////////////////////////////////////////////////////////////////////////
function deleteProcess(element) {
    var uid = element.target.value;
    var b = document.getElementById(uid);
    b.remove();


    $.ajax({
        type: "GET",
        url: "https://localhost:44379/Home/DeleteProcess",
        data: { id: JSON.stringify(uid) },
        success: function (responseObj) {
            console.log(responseObj)
        }
    });
}

