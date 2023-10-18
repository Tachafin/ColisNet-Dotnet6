$(document).ready(function () {
    $('#myTable').DataTable();
});

function Popup(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
          
            $("#pop .modal-body").html(response);
            $("#pop").modal('show');
        }
    });
}
function Popup2(url) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (response) {
            $("#pop2 .modal-body").html(response);
            $("#pop2").modal('show');
        }
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

function uploadrecu() {

    var formData = new FormData($('#frm')[0]);

    $.ajax({
        type: 'POST',
        url: '/facture/UploadRecu',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            console.log("Successfully done");
            location.reload();
        }
    });
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

           function supprimer(a,b) {
           val1 = a;
           val2 = b;

        var data = $("#studenteForm").serialize();
        console.log(data);


        $.ajax({
            type: 'GET',
            url: "/facture/"+a+"/Supprimer/"+b,
            data: {id: val1, tr: val2},
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it

            success: function (result) {
                  ShowHint(val2);


            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    }


    function ajouter(a,b) {
          val1 = a;
          val2 = b;

        var data = $("#studenteForm").serialize();
  


        $.ajax({
            type: 'GET',
            url: "/facture/"+a+"/ajouter/"+b,
            data: {id: val1, tr: val2},
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it

            success: function (result) {

                  ShowHint(val2);

            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        })
    }


    function ShowHint(ids){
        $(document).ready(function(){
            $.ajax({

                url:"/FactureDetails/"+ids,
                data:{xx:ids},
                method:'GET',
                cache:false,
                success: function(response){

                    $('#placehere').html(response);
                },
                error: function () {
                alert('failed in showhint');
                console.log('Failed ');
            }


            });
        });



    }
