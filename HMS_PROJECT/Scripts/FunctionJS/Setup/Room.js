var messsgae = " ";
$(document).ready(function () {
    $.noConflict();
   
});

$(function () {
    $("#ddp_roomtype").select2({ placeholder: "Search Room Type",});
});

$.get("/Room/GetRooms", null, DataBind);
function DataBind(RoomList) {
    console.log(RoomList)
    if (RoomList.length != 0) {
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < RoomList.length; i++) {
            var Data = "<tr class='row_" + RoomList[i].Id + "'>" +
                "<td><a href='#' onclick='EditUserRecord(\"" + RoomList[i].Id + "\")'><span class='glyphicon glyphicon-edit'></span></a>" + " " + "<a href='#'  onclick='DeleteUserRecord(\"" + RoomList[i].Id + "\")'><span class='glyphicon glyphicon-trash'></span></a></td>" +
                "<td>" + count++ + "</td>" +
                "<td hidden>" + RoomList[i].Id + "</td>" +
                "<td>" + RoomList[i].Room_No + "</td>" +
                "<td>" + RoomList[i].RoomTypes.Name + "</td>" +
                "<td>" + RoomList[i].Description + "</td>" +
                "<td>" + RoomList[i].Status + "</td>" +
                "<td>" + RoomList[i].Floor + "</td>" +
                "<td>" + RoomList[i].Clean + "</td>" +
                "<td>" + RoomList[i].Active + "</td>" +
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

function AddNewRoom(id) {
    $("#btn_sav").val('Save').addClass("btn btn-success");
    $("#create_form")[0].reset();
    //$("#ModalTitle").html("Add New Room Role");
    $("#data_Modal").modal();
    $("#txt_id").val('');
    messsgae = "Record has been Saved"

}

// Show the Popup Function for Editing values
function EditUserRecord(id) {
    $("#btn_sav").val('Update').addClass("btn btn-warning");
    var url = "/Room/GetRoomById?id=" + id;
    $("#data_Modal").modal('show');
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var txtroomNum = $('#txt_roomno');
            var txtstatus = $('#txt_status');
            var txtfloor = $('#txt_floor');
            var ddp_roomtype = $('#ddp_roomtype');
            var txt_desc = $('#txt_desc');

            var obj = JSON.parse(data);

            $("#txt_id").val(obj.Id);

            $("#txt_roomno").val(obj.Room_No)
            $("#txt_status").val(obj.Status)

            $("#txt_floor").val(obj.Floor);
            $("#txt_lname").val(obj.LastName);
            $("#txt_phoneNumber").val(obj.PhoneNumber);
            $("#ddp_roomtype").val(obj.RoomTypeId);
            $('#ddp_roomtype').trigger('change')
            $("#txt_desc").val(obj.Description);

            if (!obj.Active) {
                $("#ck_act").prop("checked", false);
            } else { $("#ck_act").prop("checked", true); }
            console.log(obj.Clean)
            if (!obj.Clean) {
                $("#ck_clen").prop("checked", false);
            } else { $("#ck_clen").prop("checked", true); }
            messsgae = "Record has been Updated" + " " + obj.Id;
        }
    })
}

$("#btn_sav").click(function () {
    var ck = ckvalidation();
    var ckval = ck.ckval;
    if (ckval == 1) { return; }
    var _cre = ck.creteria;
    Swal.fire({
        title: 'Are you sure you want to save?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#5cb85c',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Save',
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
                url: "/Room/SaveDataInDatabase",
                data: _cre,
                success: function (result) {
                    Swal.fire({
                        icon: 'success',
                        title: messsgae,
                    }).then((result) => {
                        window.location.href = "/Room/RoomIndex";
                    })
                    $("#data_Modal").modal("hide");
                }
            });
        }
    })
})

function DeleteUserRecord(id) {
    $("#txt_id").val(id);
    $("#DeleteConfirmation").modal("show");
}

function confirmDelete() {
    var id = $("#txt_id").val();
    $.ajax({
        type: "POST",
        url: "/Room/DeleteRoom?id=" + id,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            Swal.fire({
                icon: 'success',
                title: "Record has Been Deleted",
            })
            $(".row_" + id).remove();
            window.location.href = "/Room/RoomIndex";
        }
    })
}

function ckvalidation() {
    var ck = 0, _Error = '', _cre = '';
    var txtroomNum = $('#txt_roomno');
    var txtstatus = $('#txt_status');
    var txtfloor = $('#txt_floor');
    var ddp_roomtype = $('#ddp_roomtype');
    var txt_desc = $('#txt_desc');
    
    if (txt_desc.val() == '') {
        ck = 1;
        _Error = 'Please Enter Description';
        txt_desc.focus();
    }   
    if (ddp_roomtype.val() == '') {
        ck = 1;
        _Error = 'Please Select Room Type';
        ddp_roomtype.focus();
    }
    if (txtfloor.val() == '') {
        ck = 1;
        _Error = 'Please Enter Floor Number';
        txtfloor.focus();
    }
    if (txtstatus.val() == '') {
        ck = 1;
        _Error = 'Please Enter Status of Room';
        txtstatus.focus();
    }
    if (txtroomNum.val() == '') {
        ck = 1;
        _Error = 'Please Enter Room Number';
        txtroomNum.focus();
    }
    if (Boolean(ck)) {
        Swal.fire({
            title: _Error,
            icon: 'error'
        })
    }
    else if (!Boolean(ck)) {
        _cre = $("#SubmitForm").serialize();
    }
    return { ckval: ck, creteria: _cre };
}

