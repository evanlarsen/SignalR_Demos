/// <reference path="~/Scripts/jquery-1.7.1.min.js" />


$(function ($) {
    /*Watermark Input Textbox*/
    // properties
    var _OriginalChatBoxProperties = {
        'background-color': $('#ChatBox').css('background-color')
    };

    // initialize box
    $('#ChatBox').css('background-color', 'white');

    // behaviors
    $('#ChatBox').on('focus', function () {
        $(this).css('background-color', _OriginalChatBoxProperties['background-color']);
    }).on('blur', function () {
        $(this).css('background-color', 'white');
    });


    /*Main Layout, right col minimizing*/
    var collapsed = false;
    $('.top.left.collapseButton').click(function () {
        if (!collapsed) {
            $('.right.col').animate({
                opacity: 0.25,
                width: '50px'
            }, 500, function () {
                $('.top.left.collapseButton').text('<<');
            });
            $('.left.col').animate({ right: '50px' }, 500);
        }
        else {
            $('.right.col').animate({
                opacity: 1,
                width: '250px'
            }, 500, function () {
                $('.top.left.collapseButton').text('>>');
            });
            $('.left.col').animate({ right: '250px' }, 500);
        }
        collapsed = !collapsed;
    });
});