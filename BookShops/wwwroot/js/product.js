var dataTable;

$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $("#tblData").dataTable({
    ajax: {
      url: "/Product/GetAll",
    },
    columns: [
      { data: "title", width: "15%" },
      { data: "isbn", width: "15%" },
      { data: "price", width: "15%" },
      { data: "author", width: "15%" },
      { data: "category.name", width: "15%" },
      {
        data: "id",
        render: function (data) {
          return `
            <div class="w-20 btn-group" role="group">
            <a href="/Product/Upsert?id=${data}"
            class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
            <a onClick=Delete('/Product/Delete/+${data}')
            <a class="btn btn-danger mx-2"><i class="bi bi-trash2-fill"></i> Delete</a>
        </div> 
            `;
        },
        width: "15%",
      },
    ],
  });
}

function Delete(url){
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then((result) => {
        if (result.isConfirmed) {
          Swal.fire(
            $.ajax({
                url:url,
                type: 'DELETE',
                success: function(data){
                    if(data.success){
                        dataTable.ajax.reload();
                        toastr.success("Its deleted");
                        console.log("its deleted");
                    }
                    else{
                        toastr.error(data.message);

                    }
                }
            })
          )
        }
      })
}