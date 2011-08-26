$(function() {
    $('.delete_button').css({ 'cursor': 'pointer' }).click(function() {
        if (confirm("All the products in this category and its references will be removed. Do you really want to delete this category?")) {
            var This = $(this);
            $.ajax({
                type: 'POST',
                url: '/Controlpanel/Deletecategory?categoryid=' + This.parents('tr').eq(0).attr('id'),
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

    $('.edit_button').css({ 'cursor': 'pointer' }).click(function() {
        var tr = $(this).parents('tr').eq(0);
        var td1 = tr.find('td').eq(1);
        var html = "<div class='editor-label'>Name</div><div class='editor-field'>";
        html += "<input prev_value='" + $.trim(td1.html()) + "' type='text' value='" + $.trim(td1.html()) + "' />";
        html += "</div><div class='editor-label'>Description</div><div class='editor-field'>";
        var td2 = tr.find('td').eq(2);
        html += "<textarea prev_value='" + $.trim(td2.html()) + "' value='" + $.trim(td2.html()) + "' >" + $.trim(td2.html()) + "</textarea>";
        html += "</div><div class='editor-label'><span class='edit_confirm_button'><img src='/Images/added.gif' /> update</span> <span class='edit_cancel_button'><img src='/Images/ico-delete.gif' /> cancel</span></div>";
        td1.attr('colspan', '2');
        td2.remove();
        td1.html(html);
        td1.find('input[type="text"]').focus();
        $(this).hide();
    });

    $('.edit_cancel_button').live('click', function() {
        var This = $(this);
        var Td = This.parents('td').eq(0);
        var desc = Td.find('textarea').attr('prev_value');
        Td.html(Td.find('input[type="text"]').attr('prev_value'));
        Td.after("<td>" + desc + "</td>");
        Td.attr('colspan', 1);
        Td.parents('tr:first').find('.edit_button').show();
    });

    $('.edit_confirm_button').live('click', function() {
        var This = $(this);
        var Td = This.parents('td').eq(0);
        $.ajax({
            type: 'POST',
            url: '/Controlpanel/Editcategory?categoryid=' + This.parents('tr').eq(0).attr('id') + '&categoryname=' + Td.find('input[type="text"]').val() + '&categorydesc=' + Td.find('textarea').val(),
            data: "{'categoryid':'" + This.parents('tr').eq(0).attr('id') + "','categoryname':'" + Td.find('input[type="text"]').val() + "','categorydesc':'" + Td.find('textarea').val() + "'}",
            async: true,
            cache: false,
            beforeSend: function() {
                Td.find('.edit_cancel_button').remove();
                This.find('img').css({ 'width': 'auto', 'height': 'auto' });
                This.find('img').attr('src', '/images/ajax-loader.gif');
            },
            success: function(data) {
                if (data == "updated") {
                    var desc = Td.find('textarea').val();
                    Td.html(Td.find('input[type="text"]').val());
                    Td.after("<td>" + desc + "</td>");
                    Td.attr('colspan', 1);
                }
                else if (data == "loggedout") {
                    window.location = "/Controlpanel/LogOn";
                }
                else {
                    alert("An unknown error has been raised, please refresh the page and try again");
                    $('.edit_cancel_button').trigger('click');
                }
                Td.parents('tr:first').find('.edit_button').show();
            },
            error: function(e) {
                This.attr('src', '/images/product-images/icon/ico-delete.gif');
                Td.parents('tr:first').find('.edit_cancel_button').trigger('click');
            }
        });
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