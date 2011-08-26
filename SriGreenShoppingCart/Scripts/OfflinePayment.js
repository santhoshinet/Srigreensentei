$(function() {
    $('a[href="/ShoppingCart/ViewCart"]').fancybox({
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'type': 'ajax',
        'autoScale': 'true',
        'height': 435,
        'width': 800,
        'autoDimensions': 'false',
        'href': this.href + Math.random(),
        'onClosed': function() {
            window.location = window.location;
        }
    });
});