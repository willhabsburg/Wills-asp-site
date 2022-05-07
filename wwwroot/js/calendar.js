window.addEventListener("load", function () {
    $('td').on('click', tdclick);
    $('td').on('wheel', tablescroll);
});

function tdclick(e) {
    $('td').removeClass('today');
    $('#' + e.target.id).addClass('today');
    $('#addEventLink').attr('href', "/Calendar/AddEvent?selDate="+e.target.id);
}

function tablescroll(e) {
    let text = "";
    console.log(location);
    let url = "";
    if(location.toString().indexOf("CalByDate") > 0)
        url = "/Calendar/CalByDate?newDate=";
    else if(location.toString().indexOf("CalWeek") > 0)
    url = "/Calendar/CalWeek?newDate=";
    if(e.originalEvent.deltaY < 0) {
        text = $('#prevMonth').attr('href');
        location.replace(url + text.substring(text.length - 6, text.length));
    } else {
        text = $('#nextMonth').attr('href');
        location.replace(url + text.substring(text.length - 6, text.length));
    }
}