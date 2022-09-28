var dataTable;


$(document).ready(function(){
    loadDataTable();
});

function loadDataTable(){
    dataTable = $('#tblData').dataTable({
        "ajax":{
            "url": "/Product/GetAll"
        },
        "columns":[
            {"data":"title", "width":"15%"},
            {"data":"isbn", "width":"15%"},
            {"data":"price", "width":"15%"},
            {"data":"author", "width":"15%"},
            {"data":"category.name", "width":"15%"},


        ]
    }); 
}