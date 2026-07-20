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
    public partial class Libros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }


        protected void LlenarGrid()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("SELECT id as codigo, nombre as Descripcion, disponible as Disponibilidad FROM libro", con))
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
            using (SqlCommand cmd = new SqlCommand("SELECT id as codigo, nombre as Descripcion FROM libro where id = @codigo", con))
            {
                cmd.Parameters.AddWithValue("@codigo", txtcodigo.Text);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
        }

        protected void IngresarLibros()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("insert into libro (nombre) values (@nombre)", con))
            {
                cmd.Parameters.AddWithValue("@nombre", txtnombre.Text);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
           LlenarGrid();
        }

        protected void BorrarLibro()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("delete from libro where id = @codigo", con))
            {
                cmd.Parameters.AddWithValue("@codigo", txtcodigo.Text);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
            LlenarGrid();
        }

        protected void ActualizarLibro()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("update libro set nombre = @nombre where id = @codigo", con))
            {
                cmd.Parameters.AddWithValue("@codigo", txtcodigo.Text);
                cmd.Parameters.AddWithValue("@nombre", txtnombre.Text); 
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
            IngresarLibros();
            LlenarGrid();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            BorrarLibro();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarLibro();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            consultarconfiltro();
        }
    }
}