function validarFormulario() {
    var Contrasena = document.getElementsByName("Contrasena")[0].value;
    var confirmarContrasena = document.getElementsByName("Contrasena2")[0].value;
    var errorContrasena = document.getElementById("errorContrasena");
    var errorConfirmarContrasena = document.getElementById("errorConfirmarContrasena");

    // Verificar longitud mínima y otros requisitos usando expresiones regulares
    var regex = /[!@#$%^&*()_*{}\[\]:;<>,.?~\\]/;
    if (!(regex.test(Contrasena) && Contrasena.length > 8)) {
        errorContrasena.textContent = "La Contrasena debe tener al menos 8 caracteres, incluyendo al menos una letra mayúscula, un carácter especial y un número.";
        return false;
    } else {
        errorContrasena.textContent = "";
    }

    // Verificar coincidencia con la confirmación de Contrasena
    if (Contrasena !== confirmarContrasena) {
        errorConfirmarContrasena.textContent = "Las Contrasenas no coinciden. Por favor, inténtelo de nuevo.";
        return false;
    } else {
        errorConfirmarContrasena.textContent = "";
    }

    return true;
}
// JavaScript para manejar la expansión del campo de búsqueda y el menú desplegable
const searchInput = document.getElementById("search-input");
const searchIcon = document.getElementById("search-icon");
const menuButton = document.getElementById("menu-button");

searchIcon.addEventListener("click", toggleSearch);
menuButton.addEventListener("click", toggleMenu);

function toggleSearch() {
    searchInput.classList.toggle("active");
}

function toggleMenu() {
    // Agrega aquí el código para mostrar/ocultar el menú lateral izquierdo
}

// Manejar la pulsación de la tecla "Enter" en el campo de búsqueda
searchInput.addEventListener("keyup", function(event) {
    if (event.key === "Enter") {
        // Realizar la acción deseada, por ejemplo, redirigir a la página de resultados de búsqueda
        const query = searchInput.value;
        window.location.href = `/search?query=${query}`;
    }
});

// Agregar cualquier otro comportamiento interactivo necesario
