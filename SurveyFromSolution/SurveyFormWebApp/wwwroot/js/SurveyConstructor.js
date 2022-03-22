let fieldTable;

$(document).ready(function () {
    getTable();
})

function getTable() {
    fieldTable = $("#fieldsTable").DataTable({
        "ajax": {
            "url": "/Survey/GetAllFieldInputs",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "title", "width": "15%" },
            {
                "data": "isRequired", "render": function (data) {
                    return (data?"Requerido":"No requerido")
                }
            },
            {
                "data": "dataType", "render": function (data) {
                    switch (data) {
                        case "1":
                            return "Texto";
                        case "2":
                            return "Numerico";
                        case "3":
                            return "Fecha"
                        default:
                            return "Sin tipo";
                    }
                }
            }
        ],
        "language": {
            "emptyTable": "No has generado ningun formulario!"
        },
        "width": "100%"
    })
}



$("#add-row").click(function () {
    var form = $("#fieldForm");
    form.validate();
    $.validator.messages.required = 'Este campo no puede estar vacio!';
    if (form.valid()) {
        let objForm = form.serializeArray();
        let objRequest ={}
        objForm.forEach((x) => {
            if (x.name == "Id" && x.value == '') {
                x.value = 0;
            }
            if (x.name == "isRequired") {
                x.value = true;
            }
            objRequest[x.name] = x.value;
        })

        sendAjax(objRequest);
    } 
})

function sendAjax(obj) {
    $.ajax({
        url: "/Survey/CollectField",
        type: "POST",
        dataType: "json",
        data: {
            field: JSON.stringify(obj)
        },
        success: function (data) {
            if (data.success) {
                fieldTable.ajax.reload();
            }
        }
    })
}