var messsgae = " ";
$(document).ready(function () {
    $.noConflict();

});

$.get("/Customer/GetCustomer", null, DataBind);
function DataBind(CustomerList) {
    console.log(CustomerList)
    if (CustomerList.length != 0) {
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < CustomerList.length; i++) {
            var Data = "<tr class='row_" + CustomerList[i].Id + "'>" +
                "<td><a href='#' onclick='EditCustomerRecord(\"" + CustomerList[i].Id + "\")'><span class='glyphicon glyphicon-edit'></span></a>" + " " + "<a href='#'  onclick='DeleteCustomerRecord(\"" + CustomerList[i].Id + "\")'><span class='glyphicon glyphicon-trash'></span></a></td>" +
                "<td>" + count++ + "</td>" +
                "<td hidden>" + CustomerList[i].Id + "</td>" +
                "<td>" + CustomerList[i].FullName + "</td>" +
                "<td>" + CustomerList[i].CNIC + "</td>" +
                "<td>" + CustomerList[i].Address + "</td>" +
                "<td>" + CustomerList[i].Email + "</td>" +
                "<td>" + CustomerList[i].Contact + "</td>" +
                "<td>" + CustomerList[i].AnotherContact + "</td>" +
                "<td>" + CustomerList[i].Active + "</td>" +
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

function AddNewCustomer(id) {
    $("#btn_sav").val('Save').addClass("btn btn-success");
    $("#create_form")[0].reset();
    $("#data_Modal").modal();
    $("#txt_id").val('');
    messsgae = "Record has been Saved"
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
                url: "/Customer/SaveDataInDatabase",
                data: _cre,
                success: function (result) {
                    Swal.fire({
                        icon: 'success',
                        title: messsgae,
                    }).then((result) => {
                        window.location.href = "/Customer/CustomerIndex";
                    })
                    $("#data_Modal").modal("hide");
                }
            });
        }
    })
})


// Show the Popup Function for Editing values
function EditCustomerRecord(id) {
    $("#btn_sav").val('Update').addClass("btn btn-warning");
    var url = "/Customer/GetCustomerById?id=" + id;
    $("#data_Modal").modal('show');
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
          
            var obj = JSON.parse(data);

            $("#txt_id").val(obj.Id);

            $("#txt_fname").val(obj.FirstName)
            $("#txt_lname").val(obj.LastName)
            $("#txt_Cnic").val(obj.CNIC);
            $("#txt_address").val(obj.Address);
            $("#txt_cont").val(obj.Contact);
            $("#txt_ancont").val(obj.AnotherContact);
            $("#txt_email").val(obj.Email);

            if (!obj.Active) {
                $("#ck_act").prop("checked", false);
            } else { $("#ck_act").prop("checked", true); }
            messsgae = "Record has been Updated" + " " + obj.Id;
        }
    })
}


function DeleteCustomerRecord(id) {
    $("#txt_id").val(id);
    $("#DeleteConfirmation").modal("show");
}

function confirmDelete() {
    var id = $("#txt_id").val();
    $.ajax({
        type: "POST",
        url: "/Customer/DeleteCustomer?id=" + id,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            Swal.fire({
                icon: 'success',
                title: "Record has Been Deleted",
            })
            $(".row_" + id).remove();
            window.location.href = "/Customer/CustomerIndex";
        }
    })
}


function ckvalidation() {
    var ck = 0, _Error = '', _cre = '';
    var txtfname = $('#txt_fname');
    var txtlname = $('#txt_lname');
    var txtcnic = $('#txt_Cnic');
    var txtAddress = $('#txt_address');
    var txtphone = $('#txt_cont');
    var txtanothercount = $('#txt_ancont');
    var txtemail = $('#txt_email');

    if (txtanothercount.val() == '') {
        ck = 1;
        _Error = 'Please Enter Another Phone number';
        txtanothercount.focus();
    }
    if (txtphone.val() == '') {
        ck = 1;
        _Error = 'Please Enter Phone Number';
        txtphone.focus();
    }

    if (txtAddress.val() == '') {
        ck = 1;
        _Error = 'Please Enter Address';
        txtAddress.focus();
    }

    if (txtcnic.val() == '') {
        ck = 1;
        _Error = 'Please Enter C.N.I.C';
        txtcnic.focus();
    }

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




function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}