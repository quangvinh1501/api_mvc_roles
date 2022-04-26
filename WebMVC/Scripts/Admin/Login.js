$(document).ready(function () {
   $('.errormessage').css('display', 'none');
   $('#loader').addClass('hidden');
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    $("#btnlogin").click(function () {
        $(".errormessage").find("span").remove();
        handleLogin();
    });

    function callError(jqXHR, exception) {
        $('#loader').addClass('hidden');
        $('.errormessage').css('display', 'flex');
        var msg = '';
        if (jqXHR.status === 0) {
            msg = 'Not connect.\n Verify Network.';
        } else if (jqXHR.status == 400) {
            msg = jqXHR.responseJSON.title;
            var data = JSON.stringify(jqXHR.responseJSON.errors);
            $.each(JSON.parse(data), function (i, item) {
                $.each(item, function (j, items) {
                    console.log(items);
                    var errorMessage = "<span>" + items + "</span>";
                    $(".errormessage").append(errorMessage);
                });
            });
        } else if (jqXHR.status == 404) {
            msg = 'Requested page not found. [404]';
            setTimeout(function () {
                var errorMessage = "<span>" + jqXHR.responseJSON.title + "</span>";
                $(".errormessage").append(errorMessage);
            }, 1000);
        } else if (jqXHR.status == 500) {
            msg = 'Internal Server Error [500].';
        } else if (exception === 'parsererror') {
            msg = 'Requested JSON parse failed.';
        } else if (exception === 'timeout') {
            msg = 'Time out error.';
        } else if (exception === 'abort') {
            msg = 'Ajax request aborted.';
        } else {
            msg = 'Uncaught Error.\n' + jqXHR.responseText;
        }
        toastr.error(msg);
    }
    function handleLogin() {
        var data = {
            Email: $('#txtemail').val(),
            Password: $('#txtpassword').val()
        };
        $(".errormessage").find("span").remove();
        $.ajax({
            async: true,
            type: "POST",
            url: "http://localhost:5000/api/Admin/Login",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            beforeSend: function () {
                $('#loader').removeClass('hidden');
            },
            success: function (response) {
                console.log(response);
                setTimeout(function () {
                    $('#loader').addClass('hidden');
                    var jsonparse = JSON.stringify(response);
                    localStorage.setItem("token", jsonparse);
                    window.location.href = "/Home/Index";
                }, 1000);
            },
            error: function (jqXHR, exception) {
                $("#txtemail").val(data.Email);
                $("#txtpassword").val(data.Password);
                callError(jqXHR, exception);
            },
        });
    }
});