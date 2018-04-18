function openModalL(element) {
    debugger
    var url = $(element).data('url');
    $.get(url,
        function (data) {
            $('#listContainer').html(data);

            $('#listL').modal('show');
        });

};

function openModalM(element) {
    var url = $(element).data('url');
    $.get(url,
        function (data) {
            $('#listContainer').html(data);

            $('#listM').modal('show');
        });

};
