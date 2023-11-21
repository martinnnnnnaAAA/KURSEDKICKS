using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_PREGUNTADORT;

namespace TP_FINAL.Controllers;

public class HomeController : Controller
{

    //SISTEMA LOGEO
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult LogIn()
    {
        return View();
    }
    [HttpPost]
    public IActionResult VerificarLogIn(string UserName, string Contrasena)
    {
        ViewBag.MensajeErrorLogIn = null;
        ViewBag.User = BD.TraerUsuario(UserName, "UserName");
        if (ViewBag.User != null && Contrasena == ViewBag.User.Contrasena)
        {
            BD.user = ViewBag.User;
            return RedirectToAction("HomeTienda");
        }
        ViewBag.MensajeErrorLogIn = "Usuario y/o Contraseña no válidos";
        return View("LogIn");
    }

    public IActionResult SignIn()
    {
        return View();
    }
    [HttpPost]
    public IActionResult VerificarSignIn(string UserName, string Contrasena, string Nombre, string Email, int Telefono)
    {
        ViewBag.MensajeError = BD.AgregarUsuario(UserName, Contrasena, Nombre, Email, Telefono);
        if (ViewBag.MensajeError == null)
        {
            return View("LogIn");
        }
        return View("SignIn");
    }

    public IActionResult RecuperarContrasena()
    {
        return View();
    }


    public IActionResult VerificarRecuperarContrasena(string Email)
    {
        ViewBag.MensajeError = null;
        USUARIO user = BD.TraerUsuario(Email, "Email");
        ViewBag.user = user;
        if (ViewBag.user != null)
        {
            return View("CambiarContrasena");
        }
        ViewBag.MensajeError = "El Email ingresado no se encuentra registrado";
        return View("RecuperarContrasena");
    }

    [HttpPost]
    public IActionResult CambiarContrasena(string nuevaContrasena, string UserName)
    {
        BD.UpdateContrasena(UserName, nuevaContrasena);
        return View("LogIn");
    }

    // SISTEMA KURSED KICKS

    public IActionResult HomeTienda()
    {
        ViewBag.User = BD.user;
        ViewBag.Generos = BD.ObtenerGeneros();
        ViewBag.Marcas = BD.ObtenerMarcas();
        ViewBag.Colores = BD.ObtenerColores();
        ViewBag.Talles = BD.ObtenerTalles();
        ViewBag.MXTXC = BD.ObtenerMXTXC();
        ViewBag.Modelo = BD.ObtenerModelos();
        return View();
    }
    public IActionResult Modelo(MODELO item)
    {
        ViewBag.User = BD.user;
        ViewBag.Zapatilla = item;
        ViewBag.Colores = BD.ObtenerColores();
        ViewBag.Talles = BD.ObtenerTalles();
        return View();
    }
   
    
}

