using System.Collections.Generic;
public class DETALLECARRITO
{
public int IdDetalleCarrito {get;set;}
public int FkCarrito {get;set;}

public int FkModelo{get;set;}
public int FkTalle{get;set;}
public int FkColor{get;set;}
public int Subtotal{get;set;}
public string NombreModelo{get;set;}
public string Foto{get;set;}
public string NombreMarca{get;set;}
public string Talle{get;set;}
public string Color{get;set;}
}
