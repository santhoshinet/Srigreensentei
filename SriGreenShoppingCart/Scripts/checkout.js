$(function() {
    $('input[type="text"]').keydown(function(e) {
        if ((e.which > 47 && e.which < 58) || (e.which == 8) || (e.which == 127) || (e.which == 9) || (e.which > 36 && e.which < 41)) {
        }
        else
            return false;
    }).keyup(function(e) {
        if ((e.which > 47 && e.which < 58) || (e.which == 8) || (e.which == 127) || (e.which == 9) || (e.which > 36 && e.which < 41)) {
            if ($(this).val() != "") {
                var amount = parseFloat($(this).attr('price')) * parseFloat($(this).val());
                $(this).attr('amount', amount.toString());
                $(this).parents('tr').eq(0).find('.amount_td').html("Rs. " + amount.toString());
                CalculateAmount();
            }
        }
        else
            return false;
    });
    function CalculateAmount() {
        var ele = $('#Cart_Table').find('tr:last').find('td').eq(1);
        amount = 0;
        $('#Cart_Table').find('input').each(function() {
            amount += parseFloat($(this).attr('amount'));
        });
        ele.attr('amount', amount.toString()).html("Rs. " + amount.toString() + "/-");
    };
    $('.delete_button').css({ 'cursor': 'pointer' }).click(function() {
        if (confirm("Make sure that you really want to remove this from your cart?")) {
            var This = $(this);
            $.ajax({
                type: 'POST',
                url: '/ShoppingCart/DeleteCart?productId=' + This.attr('id'),
                data: "{'productId':'" + This.attr('id') + "'}",
                async: true,
                cache: false,
                beforeSend: function() {
                    This.find('img').css({ 'width': 'auto', 'height': 'auto' });
                    This.find('img').attr('src', '/images/ajax-loader.gif');
                },
                success: function(data) {
                    if (data == "removed") {
                        This.parents('tr').eq(0).fadeIn(500).delay(500).remove();
                        CalculateAmount();
                    }
                    else if (data == "loggedout") {
                        window.location = "/Account/LogOn";
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
});