$(function() {
    $('img[product_id]').css({ 'cursor': 'pointer' });
    $('img[product_id]').live('click', function() {
        var This = $(this);
        $.ajax({
            type: 'POST',
            url: '/ShoppingCart/AddCart?productId=' + This.attr('product_id'),
            data: "{'productId':'" + This.attr('product_id') + "'}",
            //contentType: 'application/json; charset=utf-8',
            async: true,
            cache: false,
            beforeSend: function() {
                This.css({ 'width': 'auto', 'height': 'auto' });
                This.attr('src', '/images/ajax-loader.gif');
            },
            success: function(data) {
                if (data == "added") {
                    This.after("Added to cart");
                    This.attr('src', '/images/added.gif');
                    //This.remove();
                }
                else if (data == "loggedout") {
                    window.location = "/Account/LogOn";
                }
                else {
                    alert("An unknown error has been raised, please refresh the page and try again");
                    This.attr('src', '/images/product-images/icon/add.png');
                }
            },
            error: function(e) {
                This.attr('src', '/images/product-images/icon/add.png');
            }
            //,dataType: 'json'
        });
    });
    $('.full-width .one-fourth .img-sha-217 .fancybox').live('click', function() {
        openfancybox($(this).attr('href'));
        return false;
    });
    $('.full-width .one-fourth .img-sha-217 .fancybox img').live('click', function() {
        openfancybox($(this).attr('src'));
        return false;
    });
    function openfancybox(href) {
        $.fancybox("<img src='" + href + "' />", {
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'autoScale': 'true',
            'autoDimensions': 'true',
            'titlePosition': 'inside',
            'onComplete': function() {
            }
        });
    }
});