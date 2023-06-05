
function CheckUser() {
    var _cre = $("#SubmitForm").serialize();
    $.ajax({
        type: "Get",
        url: "/Login/CheckUser",
        data: _cre,
        success: function (result) {
            if (result == true) {
                window.location.href = "/Home/Index";
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: "Login Failed",
                }).then((result) => {
                    window.location.href = "/Login/Login";
                })
            }
            
        }
    });
}

$("#btn_log").click(function () {
    

})
