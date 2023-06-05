﻿var messsgae = " ";
$(document).ready(function () {
    $.noConflict();
});

$.get("/RoomType/GetRoomType", null, DataBind);
function DataBind(RoomTypeList) {
    if (RoomTypeList.length != 0) {
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < RoomTypeList.length; i++) {
            var Data = "<tr class='row_" + RoomTypeList[i].Id + "'>" +
                "<td><a href='#' onclick='EditRoleRecord(\"" + RoomTypeList[i].Id + "\")'><span class='glyphicon glyphicon-edit'></span></a>" + " " + "<a href='#'  onclick='DeleteRoleRecord(\"" + RoomTypeList[i].Id + "\")'><span class='glyphicon glyphicon-trash'></span></a></td>" +
                "<td>" + count++ + "</td>" +
                "<td hidden>" + RoomTypeList[i].Id + "</td>" +
                "<td>" + RoomTypeList[i].Name + "</td>" +
                "<td>" + RoomTypeList[i].Price + "</td>" +
                "<td>" + RoomTypeList[i].Capacity + "</td>" +
                "<td>" + RoomTypeList[i].Active + "</td>" +
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
    //$("#ModalTitle").html("Add New User Role");
    $("#data_Modal").modal();
    messsgae = "Record has been Saved"

}

// Show the Popup Function for Editing values
function EditRoleRecord(id) {
    $("#btn_sav").val('Update').addClass("btn btn-warning");
    var url = "/RoomType/GetRoomTypeById?id=" + id;
    $("#data_Modal").modal('show');
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var obj = JSON.parse(data);
            $("#txt_id").val(obj.Id);
            $("#txt_name").val(obj.Name);
            $("#txt_cap").val(obj.Capacity);
            $("#txt_price").val(obj.Price);
            if (!obj.Active) {
                $("#ck_act").prop("checked", false);
            } else { $("#ck_act").prop("checked", true); }
            messsgae = "Record has been Updated - " + obj.Id;
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
                url: "/RoomType/SaveDataInDatabase",
                data: _cre,
                success: function (result) {
                    Swal.fire({
                        icon: 'success',
                        title: messsgae,
                    }).then((result) => {
                        window.location.href = "/RoomType/RoomTypeIndex";
                    })
                    $("#data_Modal").modal("hide");
                }
            });
        }
    })
})

function DeleteRoleRecord(id) {
    $("#txt_id").val(id);
    $("#DeleteConfirmation").modal("show");
}

function confirmDelete() {
    var id = $("#txt_id").val();
    $.ajax({
        type: "POST",
        url: "/RoomType/DeleteRoomType?id=" + id,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            Swal.fire({
                icon: 'success',
                title: "Record has Been Deleted - " + id,
            })
            $(".row_" + id).remove();
        }
    })
}

function ckvalidation() {
    var ck = 0, _Error = '', _cre = '';
    var txtname = $('#txt_name');
    var txtprice = $('#txt_price');
    var txtcapacity = $('#txt_cap');
    if (txtcapacity.val() == '') {
        ck = 1;
        _Error = 'Please Enter Persons Capacity';
        txtcapacity.focus();
    }
    if (txtprice.val() == '') {
        ck = 1;
        _Error = 'Please Enter Price';
        txtprice.focus();
    }
    if (txtname.val() == '') {
        ck = 1;
        _Error = 'Please Enter Name';
        txtname.focus();
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