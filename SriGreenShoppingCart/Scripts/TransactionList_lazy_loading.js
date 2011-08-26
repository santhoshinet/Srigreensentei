$(function() {
    var pageIndex = 2;
    function LoadLinks() {
        $.ajax({
            url: "/controlpanel/TransactionList/" + pageIndex,
            async: true,
            cache: false,
            type: 'GET',
            data: {},
            contentType: 'text/html; charset=utf-8',
            dataType: 'text/html',
            beforeSend: function() {
                $('.AjaxLoadingPanel').fadeIn(500);
            },
            success: function(data) {
                if ($(data).find('.Cart_Table tr').size() > 0) {
                    $('.Cart_Table').append($(data).find('.Cart_Table tr'));
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
                    pageIndex += 1;
                    scrollLock = false;
                }
                else
                    scrollLock = true;
                $('.AjaxLoadingPanel').fadeOut(500);
            },
            error: function(e) {
                //setTimeout(LoadLinks(),10000);
                $('.AjaxLoadingPanel').fadeOut(500);
            }
        });
    }
    var scrollLock = false;
    $(window).scroll(function() {
        if (!scrollLock) {
            var scrollheight = $(window).height() + $(window).scrollTop();
            var docHeight = $.getDocHeight();
            if ((docHeight - scrollheight) < 10) {
                scrollLock = true;
                LoadLinks();
            }
        }
    });
    $.getDocHeight = function() { var D = document; return Math.max(Math.max(D.body.scrollHeight, D.documentElement.scrollHeight), Math.max(D.body.offsetHeight, D.documentElement.offsetHeight), Math.max(D.body.clientHeight, D.documentElement.clientHeight)); };
});