
let days = document.getElementById('days');
let Month = document.getElementById('Month');
let starts = "@null";
let ends = "@null";
let dt1 = document.getElementById("dt1");
let dt2 = document.getElementById("dt2");

last7day.addEventListener('click', function () {

    starts = "@p1";
    ends = "@p2";
    dt1.value = starts;
    dt2.value = ends;

});
Month.addEventListener('click', function () {

    starts = "@m1";
    ends = "@m2";
    dt1.value = starts;
    dt2.value = ends;

});

document.getElementById("bs").addEventListener("click", function (event) {

    var a = document.getElementById("s1").value;
    var b = document.getElementById("s2").value;
    var c = dt1.value;
    var d = dt2.value;
    alert(c);
    alert(d);
    window.location.href = "/Admin/listeramassage?userid=" + encodeURIComponent(a) + "&etat=" + encodeURIComponent(b) + "&start=" + encodeURIComponent(c) + "&end=" + encodeURIComponent(d);


});



function check(a) {


    var data = $("#placehere").serialize();
    console.log(data);


    $.ajax({
        type: 'GET',
        url: "/Admin/DetailsColis/" + a,
        data: { nom: a },
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it

        success: function (response) {

            $('#placehere').html(response);


        },
        error: function () {
            alert('Failed to receive the Data');
            console.log('Failed ');

        }
    })
}