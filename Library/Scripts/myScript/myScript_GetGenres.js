var app = app || {};
function GetBooksGenres() {
    $.ajax({
        type: "GET",
        datatype: "json",
        url: '/Books/GetBooksGenres',
        data: {}
    }).done(function (data) {
        $.each(data, function (i) {
           
            $("#GenreId").append("<option value = " + this.id + ">" + this.genreName + "</option>");

        })

    })
}