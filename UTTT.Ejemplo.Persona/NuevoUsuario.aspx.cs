using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        private Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private SessionManager session = new SessionManager();
        private int idUsuarios = 0;
        private int tipoAccion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idUsuarios = this.session.Parametros["idUsuario"] != null ?
                    int.Parse(this.session.Parametros["idUsuario"].ToString()) : 0;
                if (this.idUsuarios == 0)
                {
                    this.baseEntity = new Usuario();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Usuario>().First(u => u.id == this.idUsuarios);
                    this.tipoAccion = 2;
                }
                this.txtContrasena.Attributes.Add("type", "password");
                this.txtContrasena2.Attributes.Add("type", "password");
                if (!IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<Linq.Data.Entity.Persona> personas = this.dcGlobal.GetTable<Linq.Data.Entity.Persona>().ToList();
                    this.ddlPersona.DataTextField = "NombreCompleto";
                    this.ddlPersona.DataValueField = "id";
                    if (this.idUsuarios == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        this.ddlPersona.DataSource = personas;
                        this.ddlPersona.DataBind();
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtPersonaEditar.Visible = true;
                        this.ddlEstado.Visible = true;
                        this.ddlPersona.Visible = false;
                        this.txtFechaIngreso.Visible = false;
                        List<EstadoUsuario> estadosUsuario = dcGlobal.GetTable<EstadoUsuario>().ToList();
                        this.ddlEstado.DataTextField = "strValor";
                        this.ddlEstado.DataValueField = "id";
                        this.ddlEstado.DataSource = estadosUsuario;
                        this.ddlEstado.DataBind();
                        this.setItem(ref this.ddlEstado, baseEntity.EstadoUsuario.strValor);
                        this.txtPersonaEditar.Text = baseEntity.Persona.NombreCompleto;
                        this.txtNombreUsuario.Text = baseEntity.strNombreUsuario;
                        this.txtFechaIngreso.Text = baseEntity.dtFechaIngreso.ToString("dd/MM/yyyy");
                        this.txtContrasena.Text = CtrlEncrypt.DesEncriptar(baseEntity.strContrasena);
                        this.txtContrasena2.Text = CtrlEncrypt.DesEncriptar(baseEntity.strContrasena);
                    }
                }
                if (this.idUsuarios > 0)
                {
                    this.txtPersonaEditar.Text = baseEntity.Persona.NombreCompleto;
                    this.txtPersonaEditar.Visible = true;
                    this.txtFechaIngreso.Visible = false;
                }
            }
            catch (Exception ex)
            {
                this.Response.Redirect("~/PrincipalUsuarios.aspx", false);
            }
        }

        private void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            try
            {
                DataContext dcGuardar = new DcGeneralDataContext();
                Usuario usuario = new Usuario();
                DateTime fechaValida;
                if (this.idUsuarios == 0)
                {
                    usuario.idComPersona = int.Parse(this.ddlPersona.SelectedValue);
                    usuario.strNombreUsuario = this.txtNombreUsuario.Text.Trim();
                    usuario.strContrasena = CtrlEncrypt.Encriptar(this.txtContrasena.Text.Trim());
                    usuario.idEstado = 1;
                    if (DateTime.TryParseExact(this.txtFechaIngreso.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaValida))
                    {
                        usuario.dtFechaIngreso = fechaValida;
                    }
                    String mensaje = String.Empty;
                    if (!this.validacion(usuario, ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    if (!this.sqlInjectionValida(ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    if (!this.htmlInjectionValida(ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    if (!this.sqlValidaConsulta(usuario, ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    dcGuardar.GetTable<Usuario>().InsertOnSubmit(usuario);
                    dcGuardar.SubmitChanges();
                    this.Response.Redirect("~/PrincipalUsuarios.aspx", false);
                }
                if (this.idUsuarios > 0)
                {
                    usuario = dcGuardar.GetTable<Usuario>().First(u => u.id == this.idUsuarios);
                    usuario.strNombreUsuario = this.txtNombreUsuario.Text.Trim();
                    usuario.strContrasena = CtrlEncrypt.Encriptar(this.txtContrasena.Text.Trim());
                    usuario.idEstado = int.Parse(this.ddlEstado.SelectedValue);
                    String mensaje = String.Empty;
                    if (!this.validacion(usuario, ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    if (!this.sqlInjectionValida(ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    if (!this.htmlInjectionValida(ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    if (!this.sqlValidaConsultaEditar(usuario, ref mensaje))
                    {
                        this.lblError.Text = mensaje;
                        this.lblError.Visible = true;
                        return;
                    }
                    dcGuardar.SubmitChanges();
                    this.Response.Redirect("~/PrincipalUsuarios.aspx", false);
                }
            }
            catch (Exception ex)
            {
                CtrlCorreo email = new CtrlCorreo();
                email.enviarCorreo(ex.Message);
                this.Response.Redirect("~/PaginasErrores/Error.html", false);
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtNombreUsuario.Text.Contains("  ");
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtContrasena.Text.Contains("  ");
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtContrasena2.Text.Contains("  ");
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = this.txtContrasena.Text.Trim().Equals(this.txtContrasena2.Text.Trim());
        }

        // Validaciones 
        private bool validacion(Usuario _usuario, ref String mensaje)
        {
            DateTime fechaValida;
            if (_usuario.strNombreUsuario.Length == 0)
            {
                mensaje = "El campo nombre de usuario es obligatorio";
                return false;
            }
            if (_usuario.strNombreUsuario.Length > 20)
            {
                mensaje = "El campo nombre de usuario no puede ser tan grande";
                return false;
            }
            if (_usuario.strNombreUsuario.Length < 3)
            {
                mensaje = "El campo nombre de usuario debe tener al menos 3 letras";
                return false;
            }
            if (_usuario.strContrasena.Length == 0)
            {
                mensaje = "El campo contraseña es obligatorio";
                return false;
            }
            if (this.txtContrasena.Text.Trim().Length > 20)
            {
                mensaje = "El campo contraseña no puede ser tan grande";
                return false;
            }
            if (this.txtContrasena.Text.Trim().Length < 5)
            {
                mensaje = "El campo contraseña debe tener al menos 5 caracteres";
                return false;
            }
            if (!this.txtContrasena.Text.Trim().Equals(this.txtContrasena2.Text.Trim()))
            {
                mensaje = "Las contraseñas ingresadas no son iguales";
                return false;
            }
            if (this.txtFechaIngreso.Text.Trim().Length == 0 && this.idUsuarios == 0)
            {
                mensaje = "El campo fecha de ingreso es obligatorio";
                return false;
            }
            if ((!DateTime.TryParseExact(this.txtFechaIngreso.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaValida)) && this.idUsuarios == 0)
            {
                mensaje = "El campo fecha de ingreso no contiene una fecha válida, utiliza el formato dd/MM/yyyy";
                return false;
            }
            DateTime now = DateTime.Now;
            DateTime old = DateTime.Parse("01/01/1753");
            if ((_usuario.dtFechaIngreso >= now || _usuario.dtFechaIngreso <= old) && this.idUsuarios == 0)
            {
                mensaje = "La fecha ingresada está fuera de rango, el rango es de 01/01/1753 hasta el día de hoy.";
                return false;
            }
            return true;
        }

        public bool sqlInjectionValida(ref String _mensaje)
        {
            CtrlValidacion valida = new CtrlValidacion();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtNombreUsuario.Text.Trim(), ref _mensajeFuncion, "Nombre de usuario", ref this.txtNombreUsuario))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtContrasena.Text.Trim(), ref _mensajeFuncion, "Contraseña", ref this.txtContrasena))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtContrasena2.Text.Trim(), ref _mensajeFuncion, "Confirmar Contraseña", ref this.txtContrasena2))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (this.idUsuarios == 0)
            {
                return true;
            }
            if (valida.sqlInjectionValida(this.txtFechaIngreso.Text.Trim(), ref _mensajeFuncion, "Fecha de ingreso", ref this.txtFechaIngreso))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool htmlInjectionValida(ref String _mensaje)
        {
            CtrlValidacion valida = new CtrlValidacion();
            String mensajeFuncion = String.Empty;
            if (valida.htmlInjectionValida(this.txtNombreUsuario.Text.Trim(), ref mensajeFuncion, "Nombre de usuario", ref this.txtNombreUsuario))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtContrasena.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtContrasena))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtContrasena2.Text.Trim(), ref mensajeFuncion, "Confirmar Contraseña", ref this.txtContrasena2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (this.idUsuarios == 0)
            {
                return true;
            }
            if (valida.htmlInjectionValida(this.txtFechaIngreso.Text.Trim(), ref mensajeFuncion, "Fecha de ingreso", ref this.txtFechaIngreso))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool sqlValidaConsulta(Usuario usuario, ref String mensaje)
        {
            var user = dcGlobal.GetTable<Usuario>().FirstOrDefault(u => u.strNombreUsuario == usuario.strNombreUsuario);
            if (user != null)
            {
                mensaje = "El nombre de usuario ingresado ya está en uso, intenta con uno diferente";
                return false;
            }
            var userInPerson = dcGlobal.GetTable<Usuario>().FirstOrDefault(u => u.idComPersona == usuario.idComPersona);
            if (userInPerson != null)
            {
                mensaje = "La persona elegida ya se encuentra registrada con un usuario, intenta con una diferente";
                return false;
            }
            return true;
        }

        public bool sqlValidaConsultaEditar(Usuario usuario, ref String mensaje)
        {
            var userCount = dcGlobal.GetTable<Usuario>().Where(u => u.strNombreUsuario == usuario.strNombreUsuario && u.id != usuario.id).Count();
            if (userCount > 0)
            {
                mensaje = "El nombre de usuario ingresado ya está en uso, intenta con uno diferente";
                return false;
            }
            return true;
        }
    }
}