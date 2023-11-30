
function validarFormulario() {
    var Contrasena = document.getElementsByName("Contrasena")[0].value;
    var confirmarContrasena = document.getElementsByName("Contrasena2")[0].value;
    var errorContrasena = document.getElementById("errorContrasena");
    var errorConfirmarContrasena = document.getElementById("errorConfirmarContrasena");


    // Verificar longitud mínima y otros requisitos usando expresiones regulares
    var regex = /[!@#$%^&*()_*{}\[\]:;<>,.?~\\]/;
    if (!(regex.test(Contrasena) && Contrasena.length > 8)) {
        errorContrasena.textContent = "The Password must be at least 8 characters, including at least one uppercase letter, one special character, and one number.";
        return false;
    } else {
        errorContrasena.textContent = "";
    }


    // Verificar coincidencia con la confirmación de Contrasena
    if (Contrasena !== confirmarContrasena) {
        errorConfirmarContrasena.textContent = "Passwords do not match. Please try again.";
        return false;
    } else {
        errorConfirmarContrasena.textContent = "";
    }


    return true;
}




function MostrarPromociones(pMes) {
    $.ajax({
        type: 'POST',
        dataType: 'text',
        url: '/Home/Promociones',
        data: { mes: pMes },
        success: function (response) {
            console.log(response);
            var promociones = response;
            $("#ModalTituloPromo").html("Promotions of the Month");
            $("#InfoPromo").html(promociones);
        }
       
    });
}

function MostrarExito() {
    $.ajax(
        {
            type: 'POST',
            dataType: 'text',
            url: '/Home/Exito',
            success: function (response) {
                console.log(response);
                var exito = response;
                    $("#ModalFotoExito").attr("src", "/Images/FotoExito.png");
                    $("#ModalTituloExit").html("Succesful checkout");
                    $("#InfoExit").html(exito); 
                    }
                });
            }

function MostrarTalles() {
    $.ajax(
        {
            type: 'POST',
            dataType: 'JSON',
            url: '/Home/infoTalles',
            success:
                function (response) {
                    $("#ModalTitulo").html("Size Chart");
                    let htmlfila="";
                    for(let item of response) {
                        htmlfila += "<tr>";
                        htmlfila += "<td>" +  item.numero +"</td>";
                        htmlfila += "<td>" +  item.centimetros + "cm" +"</td>";
                        htmlfila += "</tr>";
                    }
                    $("#DatosTalles").html(htmlfila);
                   
                }
        }
    );
}





