let table;

$(document).ready(function () {
    getTable();
})

function getTable() {
    table = $("#fieldsTable").DataTable({
        "ajax": {
            "url": "Survey/GetAllFieldInputs",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "surveyDescription", "width": "35%" },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center">
                        <a class="btn btn-primary" href="/Survey/Upsert?id=${data}" style="cursor:pointer"><i class="fas fa-edit"></i></a>
                        <a class="btn btn-danger" onclick=Delete("/Survey/Delete/${data}")  style="cursor:pointer")><i class="fas fa-trash-alt"></i></a>
                </div>`;
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
    console.log("btn presionado");
})