
var dataTable;

$(document).ready(function () {
    LoadDataTable()
});

function LoadDataTable() {
    dataTable = $("#DT_Load").DataTable({

        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center"> 
                            <a href= "/BookList/Edit?id=${data}" class= "btn btn-success text-white" style="cursor:pointer;width:100px">
                            Edit
                            </a>
                                &nbsp;
                            <a class= "btn btn-danger text-white" style="cursor:pointer;width:100px"
                            onclick=Delete('/api/book?id='+${data})>
                            Delete
                            </a>
                        </div>`;
                }, "width": "40%"
            },
        ],
        "languager": {
            "eemptyTable": "No data found"
        },
        "width": "100%"
    })
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once you delete, you will not be able to recover",
        icon: "error",
        buttons: {
            cancel: true,
            confirm: true,
        }, 
        dangerMode: true
    }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "DELETE",
                    url: url,
                    success: function (data) {
                        if (data.success) {
                            toastr.success(data.message);
                            dataTable.ajax.reload();
                        }
                        else {
                            toastr.error(data.message);
                        }
                    }
                });
            }
        });
}