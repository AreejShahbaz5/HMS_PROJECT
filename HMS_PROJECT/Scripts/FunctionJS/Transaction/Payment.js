var messsgae = " ";
$(document).ready(function () {
	$.noConflict();

});

$.get("/Payment/Getpayrecord", null, DataBind);
function DataBind(PaymentList) {
    if (PaymentList.length != 0) {
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < PaymentList.length; i++) {
            var Data = "<tr class='row_" + PaymentList[i].Id + "'>";
            Data += "<td>" + count++ + "</td>" +
                "<td hidden>" + PaymentList[i].Id + "</td>" +
                "<td>" + PaymentList[i].Pay_No + "</td>" +
                "<td>" + PaymentList[i].Reservations.Booking_No + "</td>" +
                "<td>" + moment(PaymentList[i].Reservations.CheckIn).format('DD/MMM/YYYY') + "</td>" +
                "<td>" + moment(PaymentList[i].Reservations.CheckOut).format('DD/MMM/YYYY') + "</td>" +
                "<td>" + PaymentList[i].TotalDays + "</td>" +
                "<td>" + PaymentList[i].Total_Payment + "</td>" +
                "<td>" + PaymentList[i].UpFront_Amount + "</td>" +
                "<td>" + PaymentList[i].Remaining_Balance + "</td>";
            if (PaymentList[i].UpFront_Amount == PaymentList[i].Total_Payment) {
                Data += "<td> Done </td>";
            }
            else {
                Data += "<td> Panding </td>";
            }
            Data += "</tr>";
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