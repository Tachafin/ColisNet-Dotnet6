$(document).ready(function () {
    $('#myTable').DataTable();
});

function ShowPopUptwo(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
            alert("dkhelt");
            $("#poptwo .modal-body").html(response);
            $("#poptwo").modal('show');
        }
    });
}


function ShowPopUp(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
            $("#pop .modal-body").html(response);
            $("#pop").modal('show');
        },
        error: function (xhr, status, error) {
            alert("An error occurred: " + error);
        }
    });
}

function savedata() {
    var ob = $('#frm').serialize();
    console.log(ob);
    $.post('/colis/create', ob, function (response) {
        console.log("Successfully done");
        location.reload();
    });
}

function savedatafacture() {
    var formms = $('#frm').serialize();
    console.log(formms);
    $.post('/facture/create', formms, function (response) {
        console.log("Successfully done");
        location.reload();
    });
}
