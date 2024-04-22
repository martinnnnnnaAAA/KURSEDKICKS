using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_FINAL.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

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
    public IActionResult LogOut(){
        BD.user = null;
        return View("LogIn");
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
        ViewBag.MensajeErrorLogIn = "Invalid username and/or password";
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

    public IActionResult HomeTienda(int opcionSort, int pagMandada = 1)
    {
        ViewBag.User = BD.user;
        ViewBag.Generos = BD.ObtenerGeneros();
        ViewBag.Marcas = BD.ObtenerMarcas();
        ViewBag.Colores = BD.ObtenerColores();
        ViewBag.Talles = BD.ObtenerTalles();
        ViewBag.MXTXC = BD.ObtenerMXTXC();
        ViewBag.Modelo = BD.ObtenerModelos(pagMandada, opcionSort);
        ViewBag.TotalPaginas = BD.ObtenerTotalPaginas();
        ViewBag.PaginaActual = pagMandada;
        ViewBag.OpcionSort = opcionSort;
        return View();
    }
    public IActionResult Modelo(MODELO item)
    {
        ViewBag.IsFavorito = BD.IsFavorito(item.IdModelo);
        ViewBag.User = BD.user;
        ViewBag.Zapatilla = item;
        ViewBag.Colores = BD.ObtenerColores();
        ViewBag.Talles = BD.ObtenerTalles();
        ViewBag.Mes = DateTime.Now.Month;
        return View();
    }
    
    public IActionResult Carrito(){
        ViewBag.User = BD.user;
        if(ViewBag.User != null){
        ViewBag.ListaDetalleCarrito = BD.ObtenerDetalleCarrito();
        ViewBag.Carrito = BD.ObtenerCarrito();
        }
        return View("Carrito");
    }
     public IActionResult AgregarAlCarrito(int Color, int Talle, int Modelo, float Precio){
        BD.InsertarCarrito(Modelo,Talle,Color,Precio);
        ViewBag.User = BD.user;
        ViewBag.ListaDetalleCarrito = BD.ObtenerDetalleCarrito();
        ViewBag.Carrito = BD.ObtenerCarrito();
        return View("Carrito");
    }
    public IActionResult EliminarDetalleCarrito(DETALLECARRITO item){
        BD.EliminarDetalleCarrito(item);
        ViewBag.User = BD.user;
        ViewBag.ListaDetalleCarrito = BD.ObtenerDetalleCarrito();
        ViewBag.Carrito = BD.ObtenerCarrito();
        return View("Carrito");
    }
        public IActionResult Favorito(){
        ViewBag.User = BD.user;
        if(ViewBag.User != null){
        ViewBag.Favorito = BD.ObtenerFavorito();
        ViewBag.ListaFavorito = BD.ObtenerDetalleFavorito();
        }
        return View("Favorito");
    }
        public IActionResult AgregarAlFavorito(MODELO item){
        BD.InsertarFavorito(item.IdModelo);
        ViewBag.User = BD.user;
        ViewBag.Favorito = BD.ObtenerFavorito();
        ViewBag.ListaFavorito = BD.ObtenerDetalleFavorito();
        ViewBag.Zapatilla = item;
        return View("Modelo");
    }
    public IActionResult EliminarFavorito(MODELO item){
        BD.EliminarFavorito(item.IdModelo);
        ViewBag.Zapatilla = item;
        ViewBag.User = BD.user;
        ViewBag.ListaDetalleFavorito = BD.ObtenerDetalleFavorito();
        ViewBag.Favorito = BD.ObtenerFavorito();
        return View("Modelo");
    }
     
    public List<TALLE> infoTalles()
    {
        List<TALLE> talles = BD.ObtenerTalles();
        return talles;
    }
    
    public string Promociones(int mes)
{
    string Promociones = "There are no promos";
    switch(mes){
        case 1:
            Promociones = "In the month of January there is a keychain, as a gift";
            break;
        case 11:
            Promociones = "In the month of November there is a gift doll with the purchase of a shoe";
            break;
        case 12:
            Promociones = "Fluorescent gift cords for Christmas";
            break;
    }
    return Promociones;
}
    public string Exito()
{
    string exito = "Thank you for your purchase at Kursed Kicks!";
    BD.EliminarCarrito();
    return exito;
}

public IActionResult Perfil()
    {
        ViewBag.User = BD.user;
        return View("Perfil");
    }

public IActionResult AgregarZapatilla(){
    if(BD.user.Administrador == false || BD.user == null){
        return View("HomeTienda");
    }
        ViewBag.Talles = BD.ObtenerTalles();
        ViewBag.Generos = BD.ObtenerGeneros();
        ViewBag.Marcas = BD.ObtenerMarcas();
        ViewBag.Colores = BD.ObtenerColores();

    return View();
}
public IActionResult AgregarABD(string Nombre, float Precio, string Descripcion, int Genero, int Marca, int Stock, string Foto){
    BD.AgregarABD(Nombre, Precio, Descripcion, Genero, Marca, Stock, Foto);
    return RedirectToAction("HomeTienda");
}


public IActionResult AboutUs(){
    return View();
}
public IActionResult OurServices(){
    return View();
}
public IActionResult PrivacyPolicy(){
    return View();
}
public IActionResult FAQ(){
    return View();
}
public IActionResult Shipping(){
    return View();
}
public IActionResult Returns (){
    return View();
}
public IActionResult PaymentOptions(){
    return View();
}




}
