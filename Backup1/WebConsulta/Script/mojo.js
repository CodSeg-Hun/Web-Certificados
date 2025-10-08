/*
* --------------------------------------------------------------------
* Beginning jQuery animations
* by Siddharth S, www.ssiddharth.com, hello@ssiddharth.com
* for Net Tuts, www.net.tutsplus.com
* Version: 1.0, 06.01.2010 	
* --------------------------------------------------------------------
*/

$(document).ready(function () {
    $(".item").children("div.title").animate({ top: -60 }, 300);
    $(".item").children("div.desc").animate({ bottom: -300 }, 300);

    $(".item2").children("div.title2").animate({ top: -60 }, 300);
    $(".item2").children("div.desc2").animate({ bottom: -300 }, 300);


    var list = $("#list");
    var box = $("#box");
    var ntoggle = $("#ntoggle");
    var nbox = $("#nbox");

    for (effect in jQuery.easing) {
        list.append($('<option>').text(effect));
    }

    ntoggle.click(function () {
        nbox.animate({ height: "toggle" }, 1000, function () { nbox.animate({ height: "toggle" }, 1000) });
        return false;
    });

    list.change(function () {
        box.animate({ height: "toggle" }, 1000, function () { box.animate({ height: "toggle" }, 400, list.val()); });
    });

    $("#wqbox").hover(function () {
        $(this).animate({ height: "toggle" }, 'slow');
    }, function () {
        $(this).animate({ height: "toggle" }, 'slow');
    });


    $("#wbox").hover(function () {
        $(this).animate({ height: 0 }, 'fast');
    }, function () {
        $(this).stop(true, true).animate({ height: 260 }, 'fast');
    });


    $(".item").hover(
			function () {
			    //$(this).children("img").stop().animate({opacity: 0.8}, 700, "easeInSine");
			    $(this).children("div.title").stop().animate({ top: 0 }, 700, "easeOutBounce");
			    $(this).children("div.desc").stop().animate({ bottom: 0 }, 700, "easeOutBounce");

			},
			function () {
			    //$(this).children("img").stop().animate({opacity: 1}, 700);
			    $(this).children("div.title").stop().animate({ top: -60 }, 500);
			    /*$(this).children("div.desc").stop().animate({bottom: -40}, 400);*/
			    $(this).children("div.desc").stop().animate({ bottom: -300 }, 400);

			}
			);

    $(".title, .desc").hover(
			function () {
			    $(this).stop().animate({ backgroundColor: "#333" }, 700, "easeOutSine");
			},
			function () {
			    /*$(this).stop().animate({backgroundColor: "#000"}, 700);*/
			    $(this).stop().animate({ backgroundColor: "#000" }, 700);
			}
			);

    $(".item2").hover(
			function () {
			    //$(this).children("img").stop().animate({opacity: 0.8}, 700, "easeInSine");
			    $(this).children("div.title2").stop().animate({ top: 0 }, 700, "easeOutBounce");
			    $(this).children("div.desc2").stop().animate({ bottom: 0 }, 700, "easeOutBounce");

			},
			function () {
			    //$(this).children("img").stop().animate({opacity: 1}, 700);
			    $(this).children("div.title2").stop().animate({ top: -60 }, 500);
			    /*$(this).children("div.desc").stop().animate({bottom: -40}, 400);*/
			    $(this).children("div.desc2").stop().animate({ bottom: -300 }, 400);

			}
			);

    $(".title2, .desc2").hover(
			function () {
			    $(this).stop().animate({ backgroundColor: "#333" }, 700, "easeOutSine");
			},
			function () {
			    /*$(this).stop().animate({backgroundColor: "#000"}, 700);*/
			    $(this).stop().animate({ backgroundColor: "#000" }, 700);
			}
			);


}); 

