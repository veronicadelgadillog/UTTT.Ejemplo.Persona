<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperacionContrasena.aspx.cs" Inherits="UTTT.Ejemplo.Persona.RecuperacionContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Reestablecer contraseña</title>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap_css"/>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!------ Include the above in your HEAD tag ---------->
    <link href="controlformato.css" rel="stylesheet" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.4.1/css/simple-line-icons.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="assets/css/style.css"/>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Reestablecer contraseña</h5>
                        <form class="form-signin" runat="server">
                            <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                            <div class="form-label-group mb-3">
                                <label>Correo electrónico</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtCorreo" ViewStateMode="Disabled" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-label-group mb-3">
                                <label>Persona</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtPersona" ViewStateMode="Disabled" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-label-group mb-3">
                                <label>Nombre de Usuario</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtNombreUsuario" ViewStateMode="Disabled" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-label-group">
                                <label>Nueva contraseña</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtContrasena" ViewStateMode="Disabled" TextMode="Password" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContrasena" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo" ValidationGroup="validarFormulario"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtContrasena" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="validarFormulario"></asp:CustomValidator>
                            </div>
                            <div class="form-label-group">
                                <label>Confirmar contraseña</label>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtContrasena2" ViewStateMode="Disabled" TextMode="Password" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContrasena2" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo" ValidationGroup="validarFormulario"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtContrasena2" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="validarFormulario"></asp:CustomValidator>
                                <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtContrasena2" EnableClientScript="False" ErrorMessage="Las contraseñas ingresadas no son iguales" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="validarFormulario"></asp:CustomValidator>
                            </div>
                            <asp:Button runat="server" CssClass="btn btn-lg btn-primary btn-block text-uppercase" Text="Cambiar contraseña" ID="btnReestablecerContrasena" ValidationGroup="validarFormulario" OnClick="btnReestablecerContrasena_Click" />
                            <a href="Login.aspx" class="btn btn-link">Regresar</a>
                            <hr class="my-4" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
