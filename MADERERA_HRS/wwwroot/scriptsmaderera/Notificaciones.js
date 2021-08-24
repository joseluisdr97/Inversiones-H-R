var request = $.ajax({
    url: '/LlamadasAjax/ObtenerNotificaciones'
});
request.done(function (resp) {
    $("#notificaciones").html(resp);
});
var request1 = $.ajax({
    url: '/LlamadasAjax/ObtenerNumerodenotificaciones'
});
request1.done(function (resp) {
    $("#cantidadnotificaciones").html(resp);
});
$("body").on("click", ".btneliminarnotificacion", function (e) {
    e.preventDefault();
    var $this = $(this);
    var idnotificacion = $this.data("idnotificacion");
    var request = $.ajax({
        url: '/LlamadasAjax/EliminarNotificacion?idnotificacion=' + idnotificacion
    });
    request.done(function (resp) {
        if (resp == true) {
            var request1 = $.ajax({
                url: '/LlamadasAjax/ObtenerNotificaciones'
            });
            request1.done(function (resp) {
                $("#notificaciones").html(resp);
            });
            var request2 = $.ajax({
                url: '/LlamadasAjax/ObtenerNumerodenotificaciones'
            });
            request2.done(function (resp) {
                $("#cantidadnotificaciones").html(resp);
            });
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            Toast.fire({
                icon: 'success',
                title: 'Se elimino con exito'
            })
        }
    });
});