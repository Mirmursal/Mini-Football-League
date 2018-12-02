$(document).ready(function() {

    //Ajax Get Offer
    $(".getOffer").click(function(e) {
        var btn = this;
        //var selectedBooks;
        e.preventDefault();
        var team_id = $(this).attr("data-team-id");
        //playerId and teamID is OKKAY
        $.ajax({
            url: "/Ajax/GetOffer/",
            data: { team_id: team_id},
            datatype: "json",
            type: "post",
            success: function(res) {

                if (res.status == 200) {
                    $("ul.offerTeams").empty();
                    var div = $("<div class='alert alert-warning'>" + res.data + "</div>");
                    $("div.center-wrapper").append(div);
                } else if (res.status == 505) {
                    $("ul.offerTeams").empty();
                    var div = $("<div class='alert alert-warning'>" + res.data + "</div>");
                    $("div.center-wrapper").append(div);
                }
            }
        })
    });
   
});
