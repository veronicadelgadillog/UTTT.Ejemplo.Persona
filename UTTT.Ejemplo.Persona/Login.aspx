<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap_css"/>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <!------ Include the above in your HEAD tag ---------->
    <link href="controlformato.css" rel="stylesheet" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Sistema Veronica Delgadillo Gomez</title>
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
            <h5 class="card-title text-center">LOGIN</h5>
            <form class="form-signin" runat="server">
              <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
              <div class="form-label-group">
                <label>Nombre de usuario</label>
                <%--<input type="email" id="inputEmail" class="form-control" placeholder="Email address" required autofocus>--%>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtNombreUsuario" MaxLength="20" ViewStateMode="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreUsuario" EnableClientScript="False" ErrorMessage="El nombre de usuario es obligatorio"></asp:RequiredFieldValidator>
              </div>

              <div class="form-label-group mb-3">
                <%--<input type="password" id="inputPassword" class="form-control" placeholder="Password" required>--%>
                <label>Contraseña</label>
                  <asp:TextBox CssClass="form-control" runat="server" ID="txtContrasena" MaxLength="20" ViewStateMode="Disabled" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContrasena" EnableClientScript="False" ErrorMessage="La contraseña es obligatoria"></asp:RequiredFieldValidator>
              </div>

              <%--<div class="custom-control custom-checkbox mb-3">
                <input type="checkbox" class="custom-control-input" id="customCheck1">
                <label class="custom-control-label" for="customCheck1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Guardar Contraseña</label>
              </div>--%>
              <%--<button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Iniciar sesión</button>--%>
                <asp:Button runat="server" CssClass="btn btn-lg btn-primary btn-block text-uppercase" Text="Iniciar sesión" ID="btnIngresar" OnClick="btnIngresar_Click"/>
                <%--<button type="button" class="btn btn-link">¿No recuerdas tu contraseña? Restablecer contraseña</button>--%>
                <a href="ContrasenaCorreo.aspx" class="btn btn-link">¿No recuerdas tu contraseña? Restablecer contraseña</a>
              <hr class="my-4"/>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</body>
</html>


