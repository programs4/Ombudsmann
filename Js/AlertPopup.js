var timerFadeOut;

function alertPopup(T, V) {

    //Ard arda basdıqda bağlasın.
    $('.message-alert').fadeOut(100);
    clearTimeout(timerFadeOut);

    $('.message-alert').fadeIn(200);

    var Bg = '#DF4255';
    var Icon = '/images/nida.png';

    //IsSuccess
    if (T == 'Success') {
        Bg = '#49A66B';
        Icon = '/images/success.png';
    }

    $('.message-alert span').html(V);
    $('.message-alert .icon').attr('src', Icon);
    $('.message-alert').css('background', Bg);

    timerFadeOut = setTimeout("$('.message-alert').fadeOut(200);", 5000);
}


function closePopup() {
    //Ard arda basdıqda bağlasın.
    $('.message-alert').fadeOut(300);
    clearTimeout(timerFadeOut);
}