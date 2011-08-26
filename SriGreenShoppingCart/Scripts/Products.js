$(function() {
    $('.rightbox a').click(function() {
        var This = $(this);
        var selectbox = $('.rightbox select option:selected');
        $.ajax({
            type: 'POST',
            url: '/Controlpanel/ProductList?categoryid=' + selectbox.val(),
            data: "{'categoryid':'" + selectbox.val() + "'}",
            async: true,
            cache: false,
            beforeSend: function() {
                var img = jQuery("<img>", {});
                img.css({ 'width': 'auto', 'height': 'auto' });
                img.attr('src', '/images/ajax-loader.gif');
                This.after(img);
            },
            success: function(data) {
                if (data == "loggedout") {
                    window.location = "/Controlpanel/LogOn";
                }
                else {
                    $('#Cart_Table tbody').html($(data).find('tr'));
                    $('.rightbox img').remove();
                    edit_delete();
                    // for Search operations
                    $("#txtsearchcriteria").quicksearch("#Cart_Table tbody tr", {
                        noResults: '#noresultsrow',
                        stripeRows: ['odd', 'even'],
                        loader: 'span.loading',
                        onBefore: function() {
                            $('#Cart_Table tbody tr').removeHighlight();
                        },
                        onAfter: function() {
                            if ($('#txtsearchcriteria').val() != null && $('#txtsearchcriteria').val() != "") {
                                $('#Cart_Table tbody tr').highlight($('#txtsearchcriteria').val());
                            }
                        }
                    });
                }
            },
            error: function(e) {
                $('.rightbox img').remove();
                $('#Cart_Table tbody').html("<td colspan='5'>Unable to load product list due to some technical issues, please refresh the page and try again.</td>");
            }
        });
    });
    function edit_delete() {
        $('.edit_button').fancybox({
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'type': 'iframe',
            'autoScale': 'true',
            'height': 435,
            'width': 470,
            'autoDimensions': 'false',
            'onClosed': function() {
                var self = $(this)[0].orig;
                var tr = self.parents('tr').eq(0);
                var img = self.find('img');
                img.css({ 'width': 'auto', 'height': 'auto' });
                $.ajax({
                    type: 'POST',
                    url: '/Controlpanel/Viewproductinfo?productid=' + tr.attr('id'),
                    data: "{'productid':'" + tr.attr('id') + "'}",
                    async: true,
                    cache: false,
                    beforeSend: function() {
                        img.attr('src', '/images/ajax-loader.gif');
                    },
                    success: function(data) {
                        if (data != "") {
                            var index = tr.find('td').eq(0).html();
                            tr.html(data);
                            tr.find('td').eq(0).html(index);
                            edit_delete();
                        }
                        img.attr('src', '/Images/edit.gif');
                    },
                    error: function(e) {
                        img.attr('src', '/Images/edit.gif');
                    }
                });
            }
        }
        );
        $('.product_image').live('click', function() {
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
        $('.delete_button').css({ 'cursor': 'pointer' }).click(function() {
            if (confirm("It remove the product permanently from this category. Are you sure?")) {
                var This = $(this);
                $.ajax({
                    type: 'POST',
                    url: '/Controlpanel/Deleteproduct?productid=' + This.parents('tr').eq(0).attr('id'),
                    data: "{'productid':'" + This.parents('tr').eq(0).attr('id') + "'}",
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
    }
});