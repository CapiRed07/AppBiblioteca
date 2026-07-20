<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppBiblioteca.CapaVistas.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    </form>
</body>
</html>
