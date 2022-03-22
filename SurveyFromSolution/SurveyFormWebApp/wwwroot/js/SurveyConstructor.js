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
            { "data": "title", "width": "35%" },
            {
                "data": "isRequired", "render": function (data) {
                    return (data?"Requerido":"No requerido")
                }
            },
            {
                "data": "dataType", "render": function (data) {
                    switch (data) {
                        case 0:
                            return "Texto";
                        case 1:
                            return "Numerico";
                        case 2:
                            return "Fecha"
                        default:
                            return "Sin tipo";
                    }
                }
            },
            {
                "data": "id", "render": function (data) {
                    return `  <div class="text-center">
                                        <button type="button" class="btn btn-primary" id="btnFieldEdit" data-target="#fieldModal" data-toggle="modal" data-id="${data}">Edit</button>
                                        <button type="button" class="btn btn-danger" onclick=deleteField("/Survey/DeleteField/${data}")>Del</button>
                                    </div>
                                `;
                },"width":"20%"}
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
                x.value = null;
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
                $("#fieldModal").modal('toggle');
                $('#fieldForm').trigger("reset");
            }
        }
    })
}

$(document).on("click", "#btnFieldEdit", function () {
    let id = $(this).data('id');

    $.ajax({
        url: "/Survey/GetField/" + id,
        type: "GET",
        dataType: "json",
        success: function (data) {
            console.log(data)
            if (data.success) {
                $("#FieldId").val(data.data.id);
                $("#txtFieldName").val(data.data.name);
                $("#txtTitulo").val(data.data.title);
                if (data.data.isRequired) {
                    $("#checkIsrequired").prop("checked", true);
                }
                
                $("#txtDataType").val(data.data.dataType);
            }
        }
    })
})

function resetForm() {
    $('#fieldForm').trigger("reset");
}

function deleteField(url) {
    $.ajax({
        url: url,
        type: "DELETE",
        dataType: "json",
        success: function (data) {
            fieldTable.ajax.reload();
        }
    })
}