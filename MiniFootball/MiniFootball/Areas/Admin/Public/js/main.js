//import { homedir } from "os";

$.noConflict();

jQuery(document).ready(function($) {

	"use strict";

	[].slice.call( document.querySelectorAll( 'select.cs-select' ) ).forEach( function(el) {
		new SelectFx(el);
	} );

	jQuery('.selectpicker').selectpicker;


	$('#menuToggle').on('click', function(event) {
		$('body').toggleClass('open');
	});

	$('.search-trigger').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').addClass('open');
	});

	$('.search-close').on('click', function(event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').removeClass('open');
	});

	// $('.user-area> a').on('click', function(event) {
	// 	event.preventDefault();
	// 	event.stopPropagation();
	// 	$('.user-menu').parent().removeClass('open');
	// 	$('.user-menu').parent().toggleClass('open');
	// });

    //Add Player begin
    $(".add_player").click(function (e) {
        var btn = this;
        //var selectedBooks;
        e.preventDefault();
        var playerId = $(this).attr("data-player_id");
        var teamId = $(this).attr("data-team_id");
        //playerId and teamID is OKKAY
        $.ajax({
            url: "/Admin/Ajax/AddPlayer/",
            data: { player_id: playerId, team_id: teamId },
            datatype: "json",
            type: "post",
            success: function (res) {
                console.log(btn);
                btn.remove();
                btn.classList.remove("btn-danger");
                btn.classList.add("btn-success");
                btn.innerText = "Əlavə olundu";

            }
        })
    });
    //Add player end
    //Add result begin
    $(".addResult").click(function (e) {
        var btn = this;
        //var selectedBooks;
        e.preventDefault();

        var gameID = $(this).attr("data-game-id");
        var homeScore = $(this).parent().children('.homeScore').val();
        var awayScore = $(this).parent().children('.awayScore').val();

        console.log("Game_id = " + gameID + " Home Score = " + homeScore + " awayScore = " + awayScore);

        if (gameID !== null && gameID !== '' && homeScore !== null && homeScore !== '' && awayScore !== null && awayScore !== ''){

            $.ajax({
                url: "/Admin/Ajax/AddResult/",
                data: {
                    game_id: gameID,
                    home_score: homeScore,
                    away_score: awayScore
                },
            datatype: "json",
            type: "post",
            success: function (res) {
                console.log(res.data);
                btn.classList.remove("btn-danger");
                btn.classList.add("btn-success");
                btn.innerText = "Nəticələr əlavə olundu";

            }
        })

        }else{
          console.log("No Datalar gelmir");
          }
    });
    //Add result end
    //Accept Offer begin
    $(".acceptOffer").click(function(e) {
        var btn = this;
        e.preventDefault();
        var offer_id = $(this).attr("data-offer-id");
        //offer_id is OKKAY
        $.ajax({
            url: "/Admin/Ajax/AcceptOffer/",
            data: { offer_id: offer_id },
            datatype: "json",
            type: "post",
            success: function(res) {
                if (res.status == 200) {
                    btn.parentElement.parentElement.remove();
                } else if (res.status == 505) {

                    btn.parentElement.parentElement.remove();
                }
               
            }
        })
    });
    //Accept offer end
    //Delete Offer begin
    $(".deleteOffer").click(function(e) {
        var btn = this;
        e.preventDefault();
        var offer_id = $(this).attr("data-offer-id");
        //offer_id is OKKAY
        $.ajax({
            url: "/Admin/Ajax/DeleteOffer/",
            data: { offer_id: offer_id },
            datatype: "json",
            type: "post",
            success: function(res) {
                if (res.status == 200) {
                    btn.parentElement.parentElement.remove();
                } else if (res.status == 505) {

                    btn.parentElement.parentElement.remove();
                }

            }
        })
    });
    //Delete offer end

});