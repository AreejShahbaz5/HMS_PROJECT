
var messsgae = " ";
$(document).ready(function () {
	$.noConflict();

	$('#txt_checkin').datepicker({
        'format': 'yyyy/mm/dd',
		'autoclose': true
	});

	$('#txt_checkout').datepicker({
        'format': 'yyyy/mm/dd',
		'autoclose': true
    });

});

$(function() {
	$("#ddp_roomtype").select2({ placeholder: "select Room Type", });
	$("#ddp_room").select2({ placeholder: "Select Room ", });
    $("#ddp_customer").select2({ placeholder: "Select Customer", });
});

$.get("/Reservation/GetReservation", null, DataBind);
function DataBind(ReservationList) {
    if (ReservationList.length != 0) {
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < ReservationList.length; i++) {
            var Data = "<tr class='row_" + ReservationList[i].Id + "'>";
            if (ReservationList[i].Status != "Cancel") {
                Data += "<td><a href='#' onclick='EditUserRecord(\"" + ReservationList[i].Id + "\")'><span class='glyphicon glyphicon-edit'></span></a>" + " " + "<a href='#'  onclick='DeleteUserRecord(\"" + ReservationList[i].Id + "\")'><span class='glyphicon glyphicon-trash'></span></a></td>";
            }
            else {
                Data += "<td></td>";
            }
            Data +="<td>" + count++ + "</td>" +
                "<td hidden>" + ReservationList[i].Id + "</td>" +
                "<td>" + ReservationList[i].Booking_No + "</td>" +
                "<td>" + ReservationList[i].Customers.FullName + "</td>" +
                "<td>" + moment(ReservationList[i].CheckIn).format('DD/MMM/YYYY')+ "</td>" +
                "<td>" + moment(ReservationList[i].CheckOut).format('DD/MMM/YYYY')+ "</td>" +
                "<td>" + ReservationList[i].Rooms.Room_No + "</td>" +
                "<td>" + ReservationList[i].Rooms.RoomTypes.Price + "</td>" +
                "<td>" + ReservationList[i].Rooms.Description + "</td>" +
                "<td>" + ReservationList[i].Status + "</td>" +
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
	$("#data_Modal").modal();
    $("#txt_id").val('');
    $("#ddp_customer").show();
    $("#lbl_Custo").show();
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
                url: "/Reservation/SaveDataInDatabase",
                data: _cre,
                success: function (result) {
                    Swal.fire({
                        icon: 'success',
                        title: messsgae,
                    }).then((result) => {
                        window.location.href = "/Reservation/ReservationIndex";
                    })
                    $("#data_Modal").modal("hide");
                }
            });
        }
    })
})


function EditUserRecord(id) {
    $("#btn_sav").val('Update').addClass("btn btn-warning");
    $("#ddp_customer").hide();
    $("#lbl_Custo").hide();
    var url = "/Reservation/GetReservationById?id=" + id;
    $("#data_Modal").modal('show');
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var obj = JSON.parse(data);
            $("#ddp_customer").val(obj.CustomerId);
            $('#ddp_customer').trigger('change');

            $("#txt_id").val(obj.Id); 
            $("#ddp_roomtype").val(obj.Roomtype_Id);
            $('#ddp_roomtype').trigger('change');
            $("#ddp_room").val(obj.RoomId);
            $('#ddp_room').trigger('change');
            $("#txt_checkin").val(moment(obj.CheckIn).format('yyyy/MM/DD'));
            $("#txt_checkout").val(moment(obj.CheckOut).format('yyyy/MM/DD'));
            $("#txt_totper").val(obj.TotalPerson);

            $("#txt_totday").val(obj.TotalDays);
            $("#txt_totpay").val(obj.Total_Payment);
            $("#txt_upframnt").val(obj.UpFront_Amount);
            $("#txt_rembal").val(obj.Remaining_Balance);

            messsgae = "Record has been Updated" + " " + obj.Id;
        }
    })
}



function DeleteUserRecord(id) {
    $("#txt_id").val(id);
    $("#DeleteConfirmation").modal("show");
}

function confirmDelete() {
    var id = $("#txt_id").val();
    $.ajax({
        type: "POST",
        url: "/Reservation/CancelBooking?id=" + id,
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            Swal.fire({
                icon: 'success',
                title: "Record has Been Deleted",
            })
            $(".row_" + id).remove();
            window.location.href = "/Reservation/ReservationIndex";
        }
    })
}



function ckvalidation() {
    var ck = 0, _Error = '', _cre = '';
    var ddpxustomer = $('#ddp_customer');
    var txtStatus = $('#txt_status');
    var txtcheckin = $('#txt_checkin');
    var txtcheckout = $('#txt_checkout');
    var ddp_roomtype = $('#ddp_roomtype');
    var ddp_room = $('#ddp_room');
    var txttotper = $('#txt_totper');

    if (txtStatus.val() == '') {
        ck = 1;
        _Error = 'Select Status';
        txtStatus.focus();
    }

    if (txttotper.val() == '' || txttotper.val() == 0) {
        ck = 1;
        _Error = 'Please Enter Total Person';
        txttotper.focus();
    }

    if (ddp_room.val() == '') {
        ck = 1;
        _Error = 'Please Select Room';
        ddp_room.focus();
    }
    if (ddp_roomtype.val() == '') {
        ck = 1;
        _Error = 'Please Select Room Type';
        ddp_roomtype.focus();
    }
    if (txtcheckout.val() == '') {
        ck = 1;
        _Error = 'Please Enter Check-Out Date';
        txtcheckout.focus();
    }
    if (txtcheckin.val() == '') {
        ck = 1;
        _Error = 'Please Enter check-In Date';
        txtcheckin.focus();
    }
    if (ddpxustomer.val() == '') {
        ck = 1;
        _Error = 'select Customer';
        ddpxustomer.focus();
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

function FillRooms(value) {
    $.get("/Reservation/GetRoomsByRoomType", { id: value}, function (data) {
        $("#ddp_room").empty();
        $.each(data, function (Index, row) {
            $("#ddp_room").append("<option value='" + row.Id + "'>" + row.Room_No + "</option>")
        });
    });
}

function FillRoomType(value) {
    var _id;
    $.get("/Reservation/GetRoomtypeByPerson", { Num: value }, function (data) {
        $("#ddp_roomtype").empty();
        $.each(data, function (Index, row) {
            $("#ddp_roomtype").append("<option value='" + row.Id + "'>" + row.Name + "</option>")
            _id = row.Id;
        });
    });
}

function GenerateTotalBillByCheckout() {
    $.get("/Reservation/GeneratePayment", { RoomtypeId: $("#ddp_roomtype").val() }, function (data) {
        $("#txt_totday").empty();
        $("#txt_totpay").empty();
        console.log("PAYYYYYYYYYYYYY");
        var totaldays = datediff(parseDate(txt_checkin.value), parseDate(txt_checkout.value));
        console.log(totaldays)
        var totalpay = totaldays * data[0].Price
        console.log(totalpay)
        $("#txt_totday").val(totaldays);
        $("#txt_totpay").val(totalpay);

    });
}


function CalulateRemaining() {
    $("#txt_rembal").empty();
    var TotalPay = $("#txt_totpay").val();
    var Upfrontamount = $("#txt_upframnt").val();
    var totalremaing = TotalPay - Upfrontamount;
    $("#txt_rembal").val(totalremaing);
}


function parseDate(str) {
    var mdy = str.split('/');
    return new Date(mdy[1] , mdy[0] - 1, mdy[2]);
}

function datediff(first, second) {
    // Take the difference between the dates and divide by milliseconds per day.
    // Round to nearest whole number to deal with DST.
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
}

