using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Collections.Generic;

namespace TP_PREGUNTADORT;

public static class BD
{
    private static string _connectionString = @"Server=localhost;DataBase=DB_TPFINAL;Trusted_Connection=True;";
    public static USUARIO user;

    //SISTEMA CUENTAS

    public static string AgregarUsuario(string Username, string Contrasena, string Nombre, string Email, int Telefono)
    {
        USUARIO VerificarUserName = null;
        string MensajeError = null;
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Usuario WHERE Username = @pUsername";
            VerificarUserName = DB.QueryFirstOrDefault<USUARIO>(SQL, new { @pUsername = Username });
            if (VerificarUserName == null)
            {
                SQL = "SELECT * FROM Usuario WHERE Email = @pEmail";
                VerificarUserName = DB.QueryFirstOrDefault<USUARIO>(SQL, new { @pUsername = Username, @pEmail = Email });
                if (VerificarUserName == null)
                {
                    SQL = "INSERT INTO Usuario(Username,Contrasena,Nombre,Email,Telefono) VALUES(@pUsername,@pContrasena,@pNombre,@pEmail,@pTelefono)";
                    DB.Execute(SQL, new { pUsername = Username, pContrasena = Contrasena, pNombre = Nombre, pEmail = Email, pTelefono = Telefono });
                }
                else
                {
                    MensajeError = "El Email se encuentra en uso, intente Iniciar Sesión o utilizar otro Email";
                }
            }
            else
            {
                MensajeError = "El Nombre de Usuario se encuentra en uso";
            }
            return MensajeError;
        }
    }
    public static USUARIO TraerUsuario(string datoEnviado, string campoEnviado)
    {
        USUARIO user = null;
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = $"SELECT * FROM Usuario WHERE {campoEnviado} = @pdatoEnviado";
            user = DB.QueryFirstOrDefault<USUARIO>(SQL, new { pdatoEnviado = datoEnviado });
            return user;
        }
    }
    public static void UpdateContrasena(string UserName, string nuevaContrasena)
    {
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = "UPDATE Usuario SET Contrasena = @pnuevaContrasena WHERE UserName = @pUserName";
            DB.Execute(SQL, new { pnuevaContrasena = nuevaContrasena, pUserName = UserName });
        }
    }


    // SISTEMA KURSED KICKS


    public static List<GENERO> ObtenerGeneros()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Genero";
            List<GENERO> ListadoGeneros = db.Query<GENERO>(SQL).ToList();
            return ListadoGeneros;
        }
    }
    public static List<MARCA> ObtenerMarcas()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Marca";
            List<MARCA> ListadoMarcas = db.Query<MARCA>(SQL).ToList();
            return ListadoMarcas;
        }
    }
    public static List<COLOR> ObtenerColores()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Color";
            List<COLOR> ListadoColores = db.Query<COLOR>(SQL).ToList();
            return ListadoColores;
        }
    }
    public static List<TALLE> ObtenerTalles()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Talle";
            List<TALLE> ListadoTalles = db.Query<TALLE>(SQL).ToList();
            return ListadoTalles;
        }
    }
    public static List<MODELOXTALLEXCOLOR> ObtenerMXTXC()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM ModeloXTalleXColor";
            List<MODELOXTALLEXCOLOR> ListadoMXTXC = db.Query<MODELOXTALLEXCOLOR>(SQL).ToList();
            return ListadoMXTXC;
        }
    }

    public static List<MODELO> ObtenerModelos(int pagMandada = 1)
    {
        List<MODELO> ListadoModelos = null;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SP = "SP_ObtenerModelosXDefault";
            ListadoModelos = db.Query<MODELO>(SP, new { pag = pagMandada }, commandType: CommandType.StoredProcedure).ToList();
            return ListadoModelos;
        }
    }

      public static List<TALLE> ObtenerTallePorId(int IdTalle)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SQL = "SELECT * FROM Talle where IdTale = @pIdTalle";
            return db.Query<TALLE>(SQL, new{pIdTalle= IdTalle}).ToList();
        }
    }
      public static void InsertarCarrito(int Modelo, int Talle, int Color, float Precio)
    {
        USUARIO user = BD.user;
      using (SqlConnection db = new SqlConnection(_connectionString))
      {
        string SP = "SP_InsertarAlDetalleCarrito";
        db.Execute(SP, new { IdUsuario = user.IdUsuario, FkModelo = Modelo, FkTalle = Talle,  FkColor = Color, Subtotal = Precio}, commandType: CommandType.StoredProcedure);
      }
    }
    public static List<DETALLECARRITO> ObtenerDetalleCarrito()
    {
        USUARIO user = BD.user;
        int IdUsuario = user.IdUsuario;
        List<DETALLECARRITO> ListaCarrito = null;
         using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SP = "SP_TraerDetalleCarrito";
            ListaCarrito = db.Query<DETALLECARRITO>(SP, new { IdUsuario = IdUsuario }, commandType: CommandType.StoredProcedure).ToList();
            return ListaCarrito;
        }
    }
    public static CARRITO ObtenerCarrito()
    {
        USUARIO user = BD.user;
        int IdUsuario = user.IdUsuario;
        CARRITO Carrito = null;
         using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string SP = "SP_TraerCarrito";
            Carrito = db.QueryFirstOrDefault<CARRITO>(SP, new { IdUsuario = IdUsuario }, commandType: CommandType.StoredProcedure);
            return Carrito;
        }
    }
}




