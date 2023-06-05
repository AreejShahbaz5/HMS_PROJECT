var messsgae = " ";
$(document).ready(function () {
    $.noConflict();
   
});

$(function () {
    $("#normalDropDown").select2({ placeholder: "Search Role",});
});

$.get("/AppUser/GetUsers", null, DataBind);
function DataBind(UserList) {
    console.log(UserList)
    if (UserList.length != 0) {
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < UserList.length; i++) {
            var Data = "<tr class='row_" + UserList[i].Id + "'>" +
                "<td><a href='#' onclick='EditUserRecord(\"" + UserList[i].Id + "\")'><span class='glyphicon glyphicon-edit'></span></a>" + " " + "<a href='#'  onclick='DeleteUserRecord(\"" + UserList[i].Id + "\")'><span class='glyphicon glyphicon-trash'></span></a></td>" +
                "<td>" + count++ + "</td>" +
                "<td hidden>" + UserList[i].Id + "</td>" +
                "<td>" + UserList[i].UserName + "</td>" +
                "<td>" + UserList[i].UserRoles.Role + "</td>" +
                "<td>" + UserList[i].LockEnable + "</td>" +
                "<td>" + UserList[i].LockEnableCount + "</td>" +
                "<td>" + UserList[i].Active + "</td>" +
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
    $("#txt_id").val('');
    $("#txt_pass").show();
    $("#txt_conpass").show();
    $("#lblconpass").show();
    $("#lblpass").show();
    messsgae = "Record has been Saved"

}

// Show the Popup Function for Editing values
function EditUserRecord(id) {
    $("#btn_sav").val('Update').addClass("btn btn-warning");
    var url = "/AppUser/GetUserById?id=" + id;
    $("#data_Modal").modal('show');
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var obj = JSON.parse(data);
            $("#txt_pass").val(obj.Password)
            $("#txt_conpass").val(obj.Password)

            $("#txt_pass").hide();
            $("#txt_conpass").hide();
            $("#lblconpass").hide();
            $("#lblpass").hide();

            $("#txt_id").val(obj.Id);
            $("#txt_fname").val(obj.FirstName);
            $("#txt_lname").val(obj.LastName);
            $("#txt_phoneNumber").val(obj.PhoneNumber);
            $("#normalDropDown").val(obj.UserRoleId);
            $('#normalDropDown').trigger('change')
            $("#txt_email").val(obj.Email);

            if (!obj.Active) {
                $("#ck_act").prop("checked", false);
            } else { $("#ck_act").prop("checked", true); }

            if (!obj.LockEnable) {
                $("#lokenb").prop("checked", false);
            } else { $("#lokenb").prop("checked", true); }
            messsgae = "Record has been Updated" + " " + obj.Id;
        }
    })
}

$("#btn_sav").click(function () {
    var ckpass = Passvalidation();
    var ck = ckvalidation();
    var ckval = ck.ckval;
    var valpass = ckpass.ckvalpass;
    if (ckval == 1 || valpass == 1) { return; }
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
                url: "/AppUser/SaveDataInDatabase",
                data: _cre,
                success: function (result) {
                    Swal.fire({
                        icon: 'success',
                        title: messsgae,
                    }).then((result) => {
                        window.location.href = "/AppUser/AppUserIndex";
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
        url: "/AppUser/DeleteUserRecord?id=" + id,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            Swal.fire({
                icon: 'success',
                title: "Record has Been Deleted",
            })
            $(".row_" + id).remove();
        }
    })
}

function ckvalidation() {
    var ck = 0, _Error = '', _cre = '';
    var txtfname = $('#txt_fname');
    var txtlname = $('#txt_lname');
    var txtphone = $('#txt_phoneNumber');
    var ddl_role = $('#normalDropDown');
    var txtemail = $('#txt_email');
    
    if (txtemail.val() == '') {
        ck = 1;
        _Error = 'Please Enter Email';
        txtemail.focus();
    }
    if (IsEmail(txtemail.val()) == false) {
        ck = 1;
        _Error = 'Please Enter Valid Email';
        txtemail.focus();
    }
    if (ddl_role.val() == '') {
        ck = 1;
        _Error = 'Please Select Role';
        ddl_role.focus();
    }
    if (txtphone.val() == '') {
        ck = 1;
        _Error = 'Please Enter Phone Nmber';
        txtphone.focus();
    }
    if (txtlname.val() == '') {
        ck = 1;
        _Error = 'Please Enter Last Name';
        txtlname.focus();
    }
    if (txtfname.val() == '') {
        ck = 1;
        _Error = 'Please Enter First Name';
        txtfname.focus();
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

function Passvalidation() {
    var pass = 0, _ErrorPass = '';
    var password = $("#txt_pass");
    var ConfPass = $("#txt_conpass");
    var passpattern = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/;

    if (password.val() == '') {
        pass = 1;
        _ErrorPass = 'Please Enter Password ';
        password.focus();
    }
    else if (ConfPass.val() == '') {
        pass = 1;
        _ErrorPass = 'Please Enter Confirm Password ';
        ConfPass.focus();
    }
    else if (password.val() != ConfPass.val()) {
        pass = 1;
        _ErrorPass = 'Password and Confirm Password is not matched';
        password.focus();
    }
    else if (!password.val().match(passpattern) && !ConfPass.val().match(passpattern)) {
        pass = 1
        _ErrorPass = '"Password Must contain at least one number and one uppercase and lowercase letter, and Minimum 8 characters" ';
        ConfPass.focus();
    }

    if (Boolean(pass)) {
        Swal.fire({
            title: _ErrorPass,
            icon: 'error'
        })
    }
    return { ckvalpass: pass };
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}