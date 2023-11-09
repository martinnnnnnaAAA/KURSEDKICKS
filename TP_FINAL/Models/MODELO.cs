using System.Collections.Generic;
public class MODELO{

public int IdModelo{get;set;}
public string Nombre{get;set;}
public float Precio{get;set;}
public string Descripcion{get;set;}
public bool Destacado{get;set;}
public int FkMarca{get;set;}
public int FkGenero{get;set;}
public string Foto{get;set;}
}