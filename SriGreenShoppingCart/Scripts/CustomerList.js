$(function() {
    $('.ViewProfile').fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'type': 'iframe',
        'autoScale': 'true',
        'height': 435,
        'width': 470,
        'autoDimensions': 'false',
        'onClosed': function() { }
    });

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
});