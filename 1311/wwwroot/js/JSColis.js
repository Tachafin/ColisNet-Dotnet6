

$(document).ready(function () {
    $('#myTable').DataTable();

});

function ShowPopUp(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
            $("#pop .modal-body").html(response);
            $("#pop").modal('show');
        }
    });
}

function Popup(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
            $("#pop2 .modal-body").html(response);
            $("#pop2").modal('show');
        }
    });
}

$("#frm").submit(function(event) {
    event.preventDefault(); // Prevent the default form submission
alert("submit success");
    // Call your AJAX function here
    savedata();
});

function savedata() {
    var ob = $('#frm').serialize();
    $.ajax({
        type: 'POST',
        url: '/colis/create',
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        data: ob,
        success: function (result) {
       
            $("#pop .modal-body").html(result);
            $("#pop").modal('show');
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert("Status: " + textStatus); alert("Error: " + errorThrown); 
                }    
    });
}

function sendFormData() {
    var form = $("#frm");
    var formData = form.serialize();

    $.ajax({
        type: 'POST',
        url: '/colis/create',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: formData,
        success: function (result) {
            if (result.success) {
                // Redirect to the provided URL
                window.location.href = result.redirectUrl;
            } else {
                // Handle modal popup or other actions as needed
                $("#pop .modal-body").html(result);
                $("#pop").modal('show');
            }
        }
    });
}
function savedataAdmin() {
    var ob = $('#frm').serialize();
    $.ajax({
        type: 'POST',
        url: '/colis/CreateColis',
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        data: ob,
        success: function (result) {
         
            $("#pop .modal-body").html(result);
            $("#pop").modal('show');
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) { 
                    alert("Status: " + textStatus); alert("Error: " + errorThrown); 
                }    
    });
}