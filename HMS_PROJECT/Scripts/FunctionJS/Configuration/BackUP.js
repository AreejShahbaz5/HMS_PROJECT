

$.get("/BackUP/GetBackupData", null, DataBind);
function DataBind(BackUPList) {
    console.log(BackUPList)
    if (BackUPList.length != 0) {
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < BackUPList.length; i++) {
            var Data = "<tr class='row_" + BackUPList[i].ID + "'>" +
                "<td>" + count++ + "</td>" +
                "<td hidden>" + BackUPList[i].ID + "</td>" +
                "<td>" + BackUPList[i].Name + "</td>" +
                "<td>" + moment(BackUPList[i].Date).format('"dddd, MMMM Do YYYY, h:mm:ss a"') + "</td>" +
                "<td>" + BackUPList[i].ApplicationUsers.UserName + "</td>" +
                "</tr>";
            setData.append(Data);
        }
        setData = $("#Table_View").DataTable({ "autoWidth": false });
    }
    else {
        Swal.fire({
            title: "Record Not Found",
            icon: 'error'
        })
    }
}

$("#btn_backup").click(function () {
    Swal.fire({
        title: 'Are you sure you want to Back Up the Database?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#5cb85c',
        cancelButtonColor: '#d33',
        confirmButtonText: 'BackUp',
        showClass: {
            popup: 'animated fadeInDown faster'
        },
        hideClass: {
            popup: 'animated fadeOutUp faster'
        }
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "Post",
                url: "/BackUP/SaveDataInDatabase",
                data: $("#SubmitForm").serialize(),
                success: function (result) {
                    if (result == true) {
                        Swal.fire({
                            icon: 'success',
                            title: messsgae,
                        }).then((result) => {
                            window.location.href = "/BackUP/BackUPIndex";
                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: "Some thing went wrong",
                        })
                    }
                }
            });
        }
    })
})

