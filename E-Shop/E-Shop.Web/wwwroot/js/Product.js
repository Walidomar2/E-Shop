var dtble;

$(document).ready(function () {

    loaddata();

});



function loaddata() {

    if ($.fn.DataTable.isDataTable('#mytable')) {

        $('#mytable').DataTable().destroy();

    }



    dtble = $('#mytable').DataTable({

        "ajax": {

            "url": "/Admin/Product/GetData",

            "dataSrc": "data"

        },

        "columns": [
            { "data": 'name', "width":"15%" },
            { "data": 'description', "width": "20%" },
            {
                "data": 'price', "width": "10%", "render": function (data) {
                    return '<div style="text-align: left;">' + data + '</div>';
                }
},
            { "data": "category.name", "width":"10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Product/Edit/${data}" class="btn btn-success">Edit</a>
                        <a onClick=DeleteItem("/Admin/Product/Delete/${data}") class="btn btn-danger">Delete</a>
                    `
                },
                "width":"10%"
                
            }
        ]

    });

}

function DeleteItem(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        dtble.ajax.reload();
                        toaster.success(data.message)
                    }
                    else {
                        toaster.error(data.message)
                    }
                }
            });
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });

}