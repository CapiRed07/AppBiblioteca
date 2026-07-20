using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AppBiblioteca.CapaVistas
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        protected void LlenarGrid()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            
            using (SqlCommand cmd = new SqlCommand("SELECT cedula, nombre, edad FROM Usuario", con))
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
        }

        protected void consultarconfiltro()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("SELECT cedula, nombre, edad FROM Usuario WHERE cedula = @cedula", con))
            {
                cmd.Parameters.AddWithValue("@cedula", txtcedula.Text);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
        }

        protected void IngresarUsuario()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario (cedula, nombre, edad) VALUES (@cedula, @nombre, @edad)", con))
            {
                cmd.Parameters.AddWithValue("@cedula", txtcedula.Text);
                cmd.Parameters.AddWithValue("@nombre", txtnombre.Text);
                cmd.Parameters.AddWithValue("@edad", txtedad.Text);
                con.Open();
                
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
            LlenarGrid();
        }

        protected void BorrarUsuario()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE cedula = @cedula", con))
            {
                cmd.Parameters.AddWithValue("@cedula", txtcedula.Text);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
            LlenarGrid();
        }

        protected void ActualizarUsuario()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("UPDATE Usuario SET nombre = @nombre, edad = @edad WHERE cedula = @cedula", con))
            {
                cmd.Parameters.AddWithValue("@cedula", txtcedula.Text);
                cmd.Parameters.AddWithValue("@nombre", txtnombre.Text);
                cmd.Parameters.AddWithValue("@edad", txtedad.Text);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
            LlenarGrid();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            IngresarUsuario();
            LlenarGrid();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            BorrarUsuario();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarUsuario();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            consultarconfiltro();
        }
    }
}
