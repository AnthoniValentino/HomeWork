var app = app || {};
function GetFiveBooks(userId) {
    $.ajax({
        url: 'Customers/GiveFiveBooks',
        data: { userId: userId }
    }).done(function (partial) {
        $('#fiveBooks').html(partial)
    });
}
  