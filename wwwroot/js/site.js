// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function showSolution() {
    var ddlEstados = document.getElementById("ddlEstados");
    var selectedValue = ddlEstados.options[ddlEstados.selectedIndex].value;
    var textarea = document.getElementsByName("textSolucion")[0];
    var input = document.getElementById("divSolucion");
    if (selectedValue <= "3") {
        input.classList.add("d-none");
        textarea.required = false;
     } else {
        input.classList.remove("d-none");
        textarea.required = true;

    }
}

function Ocultar(id) {

    let canvas = document.getElementById(id).style;
    if (canvas.display == "none") {
        canvas.display = "block";
    }
    else {
        canvas.display = "none";
    }
}

function cambio()
{ 
    const select = document.getElementById('ddlEstadoTecnico');
  
        const options = event.target.options;
        const selectedIndex = event.target.selectedIndex;
        let text = options[selectedIndex].text;
        text = text.replace(/\s/g, '');
        text = text.replace(/\./g, '');

        const div = document.querySelectorAll('#tablas table');

        for (const element of div) {
            if (!element.classList.contains(text)) {
                element.style.display = 'none';
            }
            else
            {
                element.style.display = 'table';
            }
        }

     console.log(text);


}

function mostrarFormulario(boton) {
    /*  var id = boton.getAttribute('data-id');*/
    var url = boton.getAttribute('data-url');
    $.ajax({
        url: url,
        type: 'GET',
        success: function (resultado) {
            $('#miModal .modal-body').html(resultado);
            $('#miModal').modal({
                backdrop: 'static',
                keyboard: false
            });
            $('#miModal').modal('show');
            $('#miModal').data('url', url);
        }
    });
}

$(function () {
    $('#miModal').on('click', '.btn-cerrar', function () {
        var url = $('#miModal').data('url');
        if (url == '/User/Create')
        { window.location = '/Home/Index'; }
        else
        {
            $('#miModal').modal('hide');
        }
    });
});

function darkmode() {
    document.documentElement.classList.toggle('dark-mode')
}

//$("#myButton").click(function () {
//    // Mostrar la notificación aquí

//    Notification.requestPermission().then(function (result) {
//        if (result === "granted") {
//            var notification = new Notification("Hola Joni");
//            // El usuario ha concedido permiso para mostrar notificaciones
//        } else
//            console.log("no se puede")
//    });

//});