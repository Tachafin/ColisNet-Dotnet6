// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.

function savedata() {

    console.log("sisio");
    var objdata = {
        Name: $('#Name').val(),
        telephone: $('#telephone').val(),
        ville: $('#ville').val(),
        adresse: $('#adresse').val(),
        Prix: $('#Prix').val(),
        User: $('#User').val(),
    }
    console.log(objdata);
    var ob = $('#frm').serialize();
    console.log(ob);
    $.post('/colis/CreateColis', ob, function (response) {
        console.log("seccesuflyyy done");
        location.reload()
    });


}

ShowComment = (url) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {

      
            $("#pop .modal-body").html(response);
            $("#pop").modal('show');


        }
        , error: function (response) {
            console.log(response);
        }
    })
}
function SaveComment() {

    console.log("sisio");

    var ob = $('#frm').serialize();
    alert(ob);
    $.post('/Comment/CreateComment', ob, function (response) {
        console.log("seccesuflyyy done");
        location.reload()
    });


}


function Show(id) {

    $.ajax({
        url: "/comment/ShowComment/" + id,
        data: { xx: id },
        method: 'GET',
        cache: false,
        success: function (response) {
            $('#placehere').html(response);
        }



    })
}
function te(id, select) {
    let b= select.value;
    let a = id;
    $.ajax({
        type: 'GET',
        url: "/Colis/" + a + "/changeEtat/" + b,
       
        data: { id: a, id2: b },
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it

        success: function (result) {
            location.reload();

        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');
        }
    })
}


ShowRecu = (url) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {

            console.log(response);
            console.log("DonePop")
            $("#pop .modal-body").html(response);
            $("#pop").modal('show');


        }
        , error: function (response) {
            console.log(response);
        }
    })
}
