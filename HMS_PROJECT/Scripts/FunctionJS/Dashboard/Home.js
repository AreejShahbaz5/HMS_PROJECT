$(document).ready(function () {
    $.noConflict();
});

$.get("/Home/GetRoomsInfo", null, DataBind);
function DataBind(RoomInfoList) {
    //$('#Table_View').DataTable({ "autoWidth": false });
    if (RoomInfoList.length != 0) {
        console.log(RoomInfoList);
        var setData = $("#Table_View");
        var count = 1;
        for (var i = 0; i < RoomInfoList.length; i++) {
            var Data = "<tr class='row_'>" +
                "<td>" + count++ + "</td>" +
                "<td>" + RoomInfoList[i].RoomTypes.Name + "</td>" +
                "<td>" + RoomInfoList[i].Room_No + "</td>" +
                "<td>" + RoomInfoList[i].Floor + "</td>" +
                "<td>" + RoomInfoList[i].Description + "</td>" +
                "<td>" + RoomInfoList[i].RoomTypes.Capacity + "</td>" +
                "<td>" + RoomInfoList[i].RoomTypes.Price + "</td>";
                if (RoomInfoList[i].Status == "Available") {
                    Data += "<td><span class='status delivered'>" + RoomInfoList[i].Status + "</span></td>";
                }
                else { Data += "<td><span class='status return'>" + RoomInfoList[i].Status + "</span></td>";}
                
                Data+= "<td>" + RoomInfoList[i].Active + "</td>" +
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


//<td><span class="status delivered">Delivered</span></td>
//<td><span class="status inprogress">In Progress</span></td>
//<td><span class="status return">Return</span></td>
//<td><span class="status pending">Pending</span></td>