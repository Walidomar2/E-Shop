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

            "url": "/Admin/Order/GetData",
            "dataSrc": "data"
        },

        "columns": [
            { "data": 'id'},
            { "data": 'name' },
            {
                "data": 'applicationUser.phoneNumber', "width": "10%", "render": function (data) {
                    return '<div style="text-align: left;">' + (data != null ? data : ' ') + '</div>';
                }
            },
            { "data": "applicationUser.email"},
            { "data": "orderStatus"},
            {
                "data": 'totalPrice', "width": "10%", "render": function (data) {
                    return '<div style="text-align: left;">' + data + '</div>';
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Order/Details?orderid=${data}" class="btn btn-success">Details</a>
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