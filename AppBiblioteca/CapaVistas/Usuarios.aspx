<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="AppBiblioteca.CapaVistas.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link rel="stylesheet" type="text/css" href="~/css/Estilo.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                <div>
            <ul>
    <li><a class="active" href="/CapaVistas/Default.aspx">Home</a></li>
    <li><a href="/CapaVistas/Libros.aspx">Libros</a></li>
    <li><a href="/CapaVistas/Usuarios.aspx">Usuarios</a></li>
    <li><a href="/CapaVistas/Reservaciones.aspx">Reservaciones</a></li>
</ul>
        </div>
        <div>
            <h1>Usuarios</h1>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <br />
        </div>

        <div>
           <asp:Label ID="lcedula" runat="server" Text="Cedula"></asp:Label>
            <br />
            <asp:TextBox ID="txtcedula" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lnombre" runat="server" Text="Nombre"></asp:Label>
            <br />
            <asp:TextBox ID="txtnombre" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="ledad" runat="server" Text="Edad"></asp:Label>
            <br />
            <asp:TextBox ID="txtedad" runat="server"></asp:TextBox>
            <br />
        </div>
        <div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"/>
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"  />
        </div>
    </form>
</body>
</html>
