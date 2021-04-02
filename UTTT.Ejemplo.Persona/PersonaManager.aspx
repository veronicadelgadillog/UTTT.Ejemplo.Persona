<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaManager" debug=false%>
<% @ Register Assembly = "AjaxControlToolkit" Namespace = "AjaxControlToolkit" TagPrefix = "asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.14.0/css/all.css" integrity="sha384-HzLeBuhoNPvSl5KYnjx0BT+WB0QEEqLprO+NBkkk5gbc67FTaL7XIGa2w1L0Xbgc" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-12">

            
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="font-family: Arial; font-size: medium; font-weight: bold">
     <h1 class="text-center">Persona</h1>
    </div>
        <div class="card">
           <div class="card-header">
          <div>
              <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
        </div>
               </div>
        <div class="card-body">
            <asp:Label ID="lblErrorValidacion" runat="server" ForeColor="Red" Visible="False" ></asp:Label>
            <div class="mb-3">
             <label class="form-label">Sexo:</label> 
            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select" 
                onselectedindexchanged="ddlSexo_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddlSexo" EnableClientScript="False" ErrorMessage="Debes seleccionar un tipo de sexo" OnServerValidate="ValidarSexo"></asp:CustomValidator>
            </div>
        <div class="mb-3"> 
            <label class="form-label">Clave Unica:</label>
            <asp:TextBox ID="txtClaveUnica" runat="server" 
                 ViewStateMode="Disabled" MaxLength="3" CssClass="form-control">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClaveUnica" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClaveUnica" EnableClientScript="False" ErrorMessage="Este campo debe contener solo números" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
        </div>
        <div class="mb-3">
            <label class="form-label">Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="15" ViewStateMode="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtNombre" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="ValidarEspaciosNombre"></asp:CustomValidator>
        </div>
        <div class="mb-3"> 
            <label class="form-label">A Paterno:</label>
            <asp:TextBox 
                ID="txtAPaterno" runat="server" MaxLength="15" ViewStateMode="Disabled" CssClass="form-control">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAPaterno" EnableClientScript="False" ErrorMessage="Es obligatorio llenar este campo"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtAPaterno" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="ValidarEspaciosAPaterno"></asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label class="form-label">A Materno:</label>
            <asp:TextBox ID="txtAMaterno" runat="server" 
                ViewStateMode="Disabled" MaxLength="15" CssClass="form-control">
            </asp:TextBox>
            <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txtAMaterno" EnableClientScript="False" ErrorMessage="Este campo no puede tener más de 2 espacios seguidos!!" OnServerValidate="ValidarEspaciosAMaterno"></asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label class="form-label">Feha de nacimiento</label>
            <asp:TextBox ID="txtFechaNaci" runat="server" CssClass="form-control"></asp:TextBox>
            <button class="btn btn-white mt-1" type="button" id="btnCalendar"><i class="far fa-calendar-alt"></i></button>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaNaci" PopupButtonID="btnCalendar" />
        </div>
        <div class="mb-3">
            <label class="form-label">Número de hermanos</label>
            <asp:TextBox ID="txtHermanos" runat="server" MaxLength="2" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Rfv1" runat="server" ControlToValidate="txtHermanos" EnableClientScript="false" ErrorMessage="Es obligatorio llenar este campo"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtHermanos" EnableClientScript="False" ErrorMessage="Este campo solo admite números" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtHermanos" EnableClientScript="False" ErrorMessage="Por favor ingrese un número entre 0 y 10" MaximumValue="10" MinimumValue="0" Type="Integer"></asp:RangeValidator>
        </div>
        <div class="mb-3">
            <label class="form-label">Correo electrónico</label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCorreo" EnableClientScript="false" ErrorMessage="Es obligatorio llenar este campo"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCorreo" EnableClientScript="False" ErrorMessage="El formato del correo no es correcto, ingrese uno válido, ejemplo: vero@test.com" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </div>
        <div class="mb-3">
            <label class="form-label">Código Postal</label>
            <asp:TextBox ID="txtCodigoP" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCodigoP" EnableClientScript="false" ErrorMessage="Es obligatorio llenar este campo"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCodigoP" EnableClientScript="False" ErrorMessage="Este campo solo admite números" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCodigoP" EnableClientScript="False" ErrorMessage="Este campo no es válido; Los códigos postales válidos en México se encuentran entre 01000 y 52999" ValidationExpression="^(?:0?[1-9]|[1-4]\d|5[0-2])\d{3}$"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="CustomValidator6" runat="server" ControlToValidate="txtCodigoP" EnableClientScript="False" ErrorMessage="El códgio postal debe tener 5 caracteres" OnServerValidate="ValidarCodigoPostal"></asp:CustomValidator>
        </div>
        <div class="mb-3">
            <label class="form-label">RFC</label>
            <asp:TextBox ID="txtRFC" runat="server" MaxLength="13" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRFC" EnableClientScript="false" ErrorMessage="Es obligatorio llenar este campo"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtRFC" EnableClientScript="False" ErrorMessage="El RFC no es válido" ValidationExpression="^([aA-zZñÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([aA-zZ\d]{3})?$"></asp:RegularExpressionValidator>
        </div>
        <div class="mb-3">
            <label class="form-label">Estado Civil</label>
            <asp:DropDownList ID="ddlEstadoCivil" CssClass="form-select" runat="server"></asp:DropDownList>
            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="ddlEstadoCivil" EnableClientScript="False" ErrorMessage="Debes seleccionar un estado civil" OnServerValidate="ValidarEstadoCivil"></asp:CustomValidator>
        </div>
   </div>
   <div class="card-footer">
       <div class="mb-3">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" onclick="btnAceptar_Click" ViewStateMode="Disabled" CssClass="btn btn-primary" OnClientClick="return validar();"/>
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" onclick="btnCancelar_Click" ViewStateMode="Disabled" CssClass="btn btn-primary" />
        </div>
   </div>
            </div>
    </form>
        </div>
            </div>
        </div>
    <script type="text/javascript" src="javaScripFile.js"></script>
</body>
</html>
