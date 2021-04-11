<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContrasenaCorreo.aspx.cs" Inherits="UTTT.Ejemplo.Persona.ContrasenaCorreo" %>

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
                            <div class="form-label-group">
                                <label>Correo electrónico</label>
                                <%--<input type="email" id="inputEmail" class="form-control" placeholder="Email address" required autofocus>--%>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtCorreo" ViewStateMode="Disabled" TextMode="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCorreo" EnableClientScript="False" ErrorMessage="El correo es obligatorio"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCorreo" EnableClientScript="False" ErrorMessage="El correo no es válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                            <asp:Button runat="server" CssClass="btn btn-lg btn-primary btn-block text-uppercase" Text="Enviar correo" ID="btnCorreo" OnClick="btnCorreo_Click" />
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
