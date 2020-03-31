/**
 * Created by Nasir on 15.12.2015.
 */


$(document).on('click', 'span.opener', function () {

    obj = $(this).parent('.u-menu').find('ul');

    if (obj.is(':visible'))
        obj.slideUp('fast');
    else
        obj.slideDown('fast');
})

