$(function() {
    $('.delete_button').css({ 'cursor': 'pointer' }).click(function() {
        if (confirm("This category will be removed permanently. Do you really want to do this?")) {
            var This = $(this);
            $.ajax({
                type: 'POST',
                url: '/Controlpanel/PermanentDeletecategory?categoryid=' + This.parents('tr').eq(0).attr('id'),
                data: "{'categoryid':'" + This.parents('tr').eq(0).attr('id') + "'}",
                async: true,
                cache: false,
                beforeSend: function() {
                    This.find('img').css({ 'width': 'auto', 'height': 'auto' });
                    This.find('img').attr('src', '/images/ajax-loader.gif');
                },
                success: function(data) {
                    if (data == "removed") {
                        This.parents('tr').eq(0).fadeIn(500).delay(500).remove();
                    }
                    else if (data == "loggedout") {
                        window.location = "/Controlpanel/LogOn";
                    }
                    else {
                        alert("An unknown error has been raised, please refresh the page and try again");
                        This.attr('src', '/images/product-images/icon/ico-delete.gif');
                    }
                },
                error: function(e) {
                    This.attr('src', '/images/product-images/icon/ico-delete.gif');
                }
            });
        }
    });
    $('.restore_button').live('click', function() {
        if (confirm("This category will restored to live. Do you really want to do this?")) {
            var This = $(this);
            var Td = This.parents('td').eq(0);
            $.ajax({
                type: 'POST',
                url: '/Controlpanel/Restorecategory?categoryid=' + This.parents('tr').eq(0).attr('id'),
                data: "{'categoryid':'" + This.parents('tr').eq(0).attr('id') + "'}",
                async: true,
                cache: false,
                beforeSend: function() {
                    Td.find('.edit_cancel_button').remove();
                    This.find('img').css({ 'width': 'auto', 'height': 'auto' });
                    This.find('img').attr('src', '/images/ajax-loader.gif');
                },
                success: function(data) {
                    if (data == "updated") {
                        This.parents('tr').eq(0).fadeIn(500).delay(500).remove();
                    }
                    else if (data == "loggedout") {
                        window.location = "/Controlpanel/LogOn";
                    }
                    else {
                        alert("An unknown error has been raised, please refresh the page and try again");
                        Td.html(Td.find('input[type="text"]').attr('prev_value'));
                    }
                },
                error: function(e) {
                    This.attr('src', '/images/product-images/icon/ico-delete.gif');
                }
            });
        }
    });
});