using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBiblioteca.CapaVistas
{
    public partial class Reservaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        protected void LlenarGrid()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("SELECT IdLibro as Codigo, cedulaUsuario as Cedula, fecha_reserva as Reserva FROM Reservacion", con))
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
            using (SqlCommand cmd = new SqlCommand("SELECT IdLibro as Codigo, cedulaUsuario as Cedula, fecha_reserva as Reserva FROM Reservacion WHERE IdLibro = @codigo", con))
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

        protected void IngresarReservacion()
        {
            if (Calendar1.SelectedDate == DateTime.MinValue)
            {
                string script = "alert('Por favor, seleccione una fecha válida en el calendario.');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorFecha", script, true);
                return;
            }
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(bd))
            {
                con.Open();

                // Verificar disponibilidad del libro
                string queryCheck = "SELECT disponible FROM Libro WHERE id = @codigo";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, con))
                {
                    cmdCheck.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtcodigo.Text));

                    
                    object resultado = cmdCheck.ExecuteScalar();

                    if (resultado != null)
                    {
                        bool estaDisponible = Convert.ToBoolean(resultado);

                        // Cancela si el libro no está disponible
                        if (!estaDisponible)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "NoDisponible", "alert('El libro solicitado no se encuentra disponible actualmente.');", true);
                            return; 
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "NoExiste", "alert('El código de libro ingresado no existe.');", true);
                        return;
                    }
                }

                // Procede si el libro se encuentra disponible
                string queryInsert = "INSERT INTO Reservacion (idlibro, cedulausuario, fecha_reserva) VALUES (@codigo, @cedula, @fecha)";
                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, con))
                {
                    cmdInsert.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtcodigo.Text));
                    cmdInsert.Parameters.AddWithValue("@cedula", txtcedula.Text);
                    cmdInsert.Parameters.AddWithValue("@fecha", Calendar1.SelectedDate);

                    try
                    {
                        cmdInsert.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(this.GetType(), "Exito", "alert('Reservación guardada con éxito.');", true);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                            ClientScript.RegisterStartupScript(this.GetType(), "ErrorFK", "alert('Error: Cédula no registrada.');", true);
                        else
                            ClientScript.RegisterStartupScript(this.GetType(), "ErrorSQL", $"alert('Error: {ex.Message}');", true);
                    }
                }
            }
        }

        protected void ActualizarReservacion()
        {
            // Validacion llave primaria
            if (string.IsNullOrEmpty(txtcodigo.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorId", "alert('Por favor, ingrese el código correspondiente para poder actualizar.');", true);
                return;
            }

    
            if (Calendar1.SelectedDate == DateTime.MinValue)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorFecha", "alert('Por favor, seleccione la nueva fecha en el calendario.');", true);
                return;
            }

            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(bd))

                using (SqlCommand cmd = new SqlCommand("UPDATE Reservacion SET cedulaUsuario = @cedula, fecha_reserva = @fecha WHERE IdLibro = @codigo", con))
                {
                    cmd.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtcodigo.Text));
                    cmd.Parameters.AddWithValue("@cedula", txtcedula.Text);
                    cmd.Parameters.AddWithValue("@fecha", Calendar1.SelectedDate); 
                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery(); 

                    if (filasAfectadas > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Exito", "alert('Reservación actualizada con éxito.');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "NoEncontrado", "alert('No se encontró ninguna reservación con el código especificado.');", true);
                    }
                }
            }
            catch (SqlException ex)
            {
                
                if (ex.Number == 547)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorFK", "alert('Error: La nueva cédula ingresada no pertenece a un usuario registrado.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorSQL", $"alert('Error en la base de datos: {ex.Message}');", true);
                }
            }
            LlenarGrid();
        }

        protected void ConsultarReservacion()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("SELECT IdLibro as Codigo, cedulaUsuario as Cedula, fecha_reserva as Reserva FROM Reservacion WHERE IdLibro = @codigo", con))
            {
                cmd.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtcodigo.Text));
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
        }

        protected void EliminarReservacion()
        {
            string bd = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(bd))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Reservacion WHERE IdLibro = @codigo", con))
            {
                cmd.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtcodigo.Text));
                con.Open();
                cmd.ExecuteNonQuery();
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
            IngresarReservacion();
            LlenarGrid();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarReservacion();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarReservacion();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarReservacion();
        }
    }
}