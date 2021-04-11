<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrincipalUsuarios.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PrincipalUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Principal Usuarios</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.14.0/css/all.css" integrity="sha384-HzLeBuhoNPvSl5KYnjx0BT+WB0QEEqLprO+NBkkk5gbc67FTaL7XIGa2w1L0Xbgc" crossorigin="anonymous" />
</head>
<body>
   <div class="container-fluid">
    <form id="form1" runat="server">
    
    <div style="color: #000000; font-size: medium; font-family: Arial; font-weight: bold"><h1 class="text-center">Usuarios</h1></div>
    <div>
    <p>
        <%--<asp:TextBox ID="txtNombre" runat="server" Width="174px" 
            ViewStateMode="Disabled"></asp:TextBox>--%>
        
        <%--<asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
            onclick="btnBuscar_Click" ViewStateMode="Disabled" />
        
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
            onclick="btnAgregar_Click" ViewStateMode="Disabled" />--%>
    </p>
    </div>
    <div class="row g-3">
        <%--<div class="col-1">
            <label class="col-form-label">Nombre</label>
        </div>--%>
        <div class="col-md-2">
            <label class="col-form-label">Nombre de Usuario</label>
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
        <%--<div class="col-1">
            <label class="col-form-label">Sexo</label>
        </div>--%>
        <div class="col-md-2">
            <label class="col-form-label">Estado</label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select"></asp:DropDownList>
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
    <div>
        <%--<asp:DropDownList ID="ddlSexo" runat="server" Height="22px" Width="177px">
        </asp:DropDownList>--%>
    </div>
    <div style="font-weight: bold"><h4 class="text-center">Detalle</h4></div>
        <div class="row mt-3">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:UpdatePanel ID="panelGrid" runat="server">
                    <ContentTemplate>
             <asp:GridView ID="dgvUsuario" runat="server" 
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourceUsuario" 
                Width="1500px" CellPadding="3" GridLines="Horizontal" 
                onrowcommand="dgvUsuario_RowCommand" BackColor="White" 
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                ViewStateMode="Disabled">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="strNombreUsuario" HeaderText="Nombre de Usuario" 
                        ReadOnly="True" SortExpression="strNombreUsuario" />
                    <asp:BoundField DataField="dtFechaIngreso" HeaderText="Fecha de ingreso" ReadOnly="True" 
                        SortExpression="dtFechaIngreso" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="EstadoUsuario.strValor" HeaderText="Estado" ReadOnly="True" 
                        SortExpression="EstadoUsuarios.strValor" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                    
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Eliminar" Visible="True">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')"/>
                            </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
             </div>
         </div>
        </div>
        <asp:LinqDataSource ID="DataSourceUsuario" runat="server"
            onselecting="DataSourceUsuario_Selecting" 
            Select="new (strNombreUsuario, dtFechaIngreso, id, EstadoUsuario)"
            TableName="Usuario" EntityTypeName="" ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext">
        </asp:LinqDataSource>
    </form>
    <div class="row mt-3">
        <div class="col-md-12">
            <a href="PaginaPrincipal.aspx">Regresar</a>
        </div>
    </div>
    </div>
    <script type="text/javascript">
        document.querySelector('#txtNombre').addEventListener('keyup', () => {
            document.querySelector('#botonBusquedaXD').click();
        });
    </script>
</body>
</html>
