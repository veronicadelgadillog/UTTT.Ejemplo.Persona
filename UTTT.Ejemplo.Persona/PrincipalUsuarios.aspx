<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrincipalUsuarios.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PrincipalUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.14.0/css/all.css" integrity="sha384-HzLeBuhoNPvSl5KYnjx0BT+WB0QEEqLprO+NBkkk5gbc67FTaL7XIGa2w1L0Xbgc" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="color: #000000; font-size: medium; font-family: Arial; font-weight: bold"><h1 class="text-center">Usuarios</h1></div>
    <div>
            <br />
             <div class="col-md-2">
            <label class="col-form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" ViewStateMode="Disabled" CssClass="form-control"></asp:TextBox>
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="panel1" runat="server">
                <ContentTemplate>
                    <asp:Button style="display: none;" ID="botonBusquedaXD" runat="server" OnClick="botonBusquedaXD_Click"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
            <div class="row g-3 mt-3">
        <div class="col-auto">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" onclick="btnBuscar_Click" ViewStateMode="Disabled" CssClass="btn btn-primary"/>
        </div>
        <div class="col-auto">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" onclick="btnAgregar_Click" ViewStateMode="Disabled" CssClass="btn btn-primary"/>
        </div>
    </div>
            <div class="text-center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="LinqDataSource1" Width="796px">
                <Columns>
                    <asp:BoundField DataField="strNombre" HeaderText="Nombre Usuario" ReadOnly="True" SortExpression="strNombre" />
                    <asp:BoundField DataField="strCorreo" HeaderText="Correo" ReadOnly="True" SortExpression="strCorreo" />
                    <asp:BoundField DataField="strContrasena" HeaderText="Contrasena" ReadOnly="True" SortExpression="strContrasena" />
                    <asp:BoundField DataField="strConfContra" HeaderText="ConfContra" ReadOnly="True" SortExpression="strConfContra" />
                    <asp:ButtonField ButtonType="Image" HeaderText="Editar" Text="Editar" />
                    <asp:ButtonField ButtonType="Image" HeaderText="Eliminar" Text="Eliminar" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            </div>
            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" EntityTypeName="" OnSelecting="LinqDataSource1_Selecting" Select="new (strNombre, strCorreo, strContrasena, strConfContra)" TableName="Usuarios">
            </asp:LinqDataSource>
        </div>
    </form>
</body>
</html>
