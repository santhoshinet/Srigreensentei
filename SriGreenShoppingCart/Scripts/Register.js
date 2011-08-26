$(function() {
    $('input[name="yes"]').click(function() {
        if ($(this).attr('checked') == true) {
            $('input[name="no"]').attr('checked', false);
            $('.delivery').fadeOut(500);
        }
    });
    $('input[name="no"]').click(function() {
        if ($(this).attr('checked') == true) {
            $('input[name="yes"]').attr('checked', false);
            $('.delivery').fadeIn(500);
        }
    });
});