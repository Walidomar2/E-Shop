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
                        <a href="/Admin/Product/Delete/${data}" class="btn btn-danger">Delete</a>
                    `
                },
                "width":"10%"
                
            }
        ]

    });

}