<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="UTTT.Ejemplo.Persona.NuevoUsuario" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap_css"/>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!------ Include the above in your HEAD tag ---------->
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Veronica Delgadillo Gonez</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.4.1/css/simple-line-icons.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="assets/css/style.css"/>
    <link href="controlformato.css" rel="stylesheet" />
</head>
<script>
    //$(document).ready(function () {
    //    $('#birth-date').mask('00/00/0000');
    //    $('#phone-number').mask('0000-0000');
    //})
</script>
<body>
    <div class="registration-form">
        <form runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="form-icon">
                <span><i class="icon icon-user"></i></span>
            </div>
            <p>
                <asp:Label runat="server" ID="lblAccion"></asp:Label>
            </p>
            <p>
                <asp:Label runat="server" ID="lblError" CssClass="text-danger"></asp:Label>
            </p>
            <div class="form-group mb-3">
                <asp:DropDownList ID="ddlPersona" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:TextBox runat="server" ID="txtPersonaEditar" ReadOnly="true" CssClass="form-control" ViewStateMode="Disabled" Visible="false" MaxLength="50"></asp:TextBox>
            </div>
            <div class="form-group">
                <%--<input type="text" class="form-control item" id="username" placeholder="Username">--%>
                <asp:TextBox runat="server" ID="txtNombreUsuario" CssClass="form-control" placeholder="Nombre de Usuario" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreUsuario" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo" ValidationGroup="validarFormulario"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtNombreUsuario" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="validarFormulario"></asp:CustomValidator>
            </div>
            <div class="form-group">                
                <%--<input type="password" class="form-control item" id="password" placeholder="Password">--%>
                <asp:TextBox runat="server" ID="txtContrasena" CssClass="form-control" placeholder="Contraseña" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContrasena" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo" ValidationGroup="validarFormulario"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtContrasena" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="validarFormulario"></asp:CustomValidator>
            </div>
            <div class="form-group">
                <%--<input type="text" class="form-control item" id="confirmar-contraseña" placeholder="Confirmar Contraseña">--%>
                <asp:TextBox runat="server" ID="txtContrasena2" CssClass="form-control" placeholder="Confirmar contraseña" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContrasena2" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo" ValidationGroup="validarFormulario"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtContrasena2" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="validarFormulario"></asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtContrasena2" EnableClientScript="False" ErrorMessage="Las contraseñas ingresadas no son iguales" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="validarFormulario"></asp:CustomValidator>
            </div>
            <div class="form-group">
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" Visible="false"></asp:DropDownList>
            </div>
            <div class="form-group">
                <%--<input type="text" class="form-control item" id="confirmar-contraseña" placeholder="Confirmar Contraseña">--%>
                <asp:TextBox runat="server" ID="txtFechaIngreso" CssClass="form-control" autocomplete="off" ViewStateMode="Disabled" placeholder="Fecha de ingreso"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="cdlFechaInicio" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaIngreso"/>
            </div>
            <div class="form-group">
                <%--<button type="button" class="btn btn-block create-account">Agregar</button>--%>
                <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ViewStateMode="Disabled" CssClass="btn btn-block create-account" OnClick="btnAceptar_Click" ValidationGroup="validarFormulario"/>
                <%--<button type="button" class="btn btn-block create-account">Cancelar</button>--%>
                <a class="btn btn-block create-account" href="PrincipalUsuarios.aspx">Cancelar</a>
            </div>
        </form>
        <%--<div class="social-media">
            <h5>Gracias por registrate</h5>
        </div>--%>
    </div>
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.15/jquery.mask.min.js"></script>
    <script src="assets/js/script.js"></script>
</body>
</html>