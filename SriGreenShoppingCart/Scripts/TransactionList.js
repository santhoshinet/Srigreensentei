$(function() {
    $('.Cart_Table_Sub a').fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'type': 'iframe',
        'autoScale': 'false',
        'height': 435,
        'width': 830,
        'autoDimensions': 'false',
        'onClosed': function() {
        }
    });
    $('.Cart_Table_Sub input[type="button"]').live('click', function() {
        var This = $(this);
        var transactionId = $(this).parents('tr:first').find('td').eq(1).html();
        var options = $(this).parents('td:first').find('select option');
        var option = $(this).parents('td:first').find('select option:selected');
        if (options.index(option) > 0) {
            $.ajax({
                type: 'GET',
                url: '/Controlpanel/UpdateTransaction?transactionID=' + transactionId + "&actions=" + option.html(),
                data: "{'transactionID':'" + transactionId + "','actions':'" + option.html() + "'}",
                async: true,
                cache: false,
                beforeSend: function() {
                    This.val('wait');
                },
                success: function(data) {
                    if (data == "cleared") {
                        This.parents('tr:first').find('td').eq(3).html("0");
                    }
                    This.val('Update');
                },
                error: function(e) {
                    This.val('Update');
                }
            });
        }
        else {
            alert("Please select option and then try.");
        }
    });

    // for Search operations
    $("#txtsearchcriteria").quicksearch(".Cart_Table_Sub tbody tr", {
        stripeRows: ['odd', 'even'],
        loader: 'span.loading',
        onBefore: function() {
            $('.Cart_Table_Sub tbody tr').removeHighlight();
        },
        onAfter: function() {
            if ($('#txtsearchcriteria').val() != null && $('#txtsearchcriteria').val() != "") {
                $('.Cart_Table_Sub tbody tr').highlight($('#txtsearchcriteria').val());
            }
        }
        ,
        hide: function() {
            $(this).parents('table:first').hide();
        },
        show: function() {
            $(this).parents('table:first').show();
        }
    });
});