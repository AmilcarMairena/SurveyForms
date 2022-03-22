

let table;

$(document).ready(function () {
    loadTable();
})

function loadTable() {
    table = $("#SurveyTable").DataTable({
        "ajax": {
            "url": "Survey/GetAllSurveyForms",
            "type": "GET",
            "datatype":"json"
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
                }}
        ],
        "language": {
            "emptyTable": "No has generado ningun formulario!"
        },
        "width": "100%"
    })
}

function Delete(url) {
    Swal.fire({
        title: 'Estas seguro?',
        text: "Ya no podras restablecer este registro!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, borralo!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                contentType: "application/json",
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        table.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })


}