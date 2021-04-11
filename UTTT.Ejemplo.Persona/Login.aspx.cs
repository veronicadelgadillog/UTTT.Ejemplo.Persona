using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            try
            {
                DataContext db = new DcGeneralDataContext();
                String mensaje = String.Empty;
                if (!this.validacion(ref mensaje))
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
                var dbUser = db.GetTable<Usuario>().FirstOrDefault(u => u.strNombreUsuario == this.txtNombreUsuario.Text.Trim());
                if (dbUser == null)
                {
                    this.lblError.Text = "Nombre de usuario o contraseña incorrectos";
                    this.lblError.Visible = true;
                    return;
                }
                if (dbUser.idEstado != 1)
                {
                    this.lblError.Text = "Este usuario no está activo por lo cual no puede iniciar sesión";
                    this.lblError.Visible = true;
                    return;
                }
                var passDec = CtrlEncrypt.DesEncriptar(dbUser.strContrasena);
                if (!passDec.Equals(this.txtContrasena.Text.Trim()))
                {
                    this.lblError.Text = "Nombre de usuario o contraseña incorrectos";
                    this.lblError.Visible = true;
                    return;
                }
                this.Response.Redirect("~/PaginaPrincipal.aspx", false);
            }
            catch (Exception ex)
            {
                CtrlCorreo email = new CtrlCorreo();
                email.enviarCorreo(ex.Message);
                this.Response.Redirect("~/PaginasErrores/Error.html", false);
            }
        }
        private bool validacion(ref String mensaje)
        {
            if (txtNombreUsuario.Text.Trim().Length == 0)
            {
                mensaje = "El campo nombre de usuario es obligatorio";
                return false;
            }
            if (txtNombreUsuario.Text.Trim().Length > 20)
            {
                mensaje = "El campo nombre de usuario no puede ser tan grande";
                return false;
            }
            if (txtContrasena.Text.Trim().Length == 0)
            {
                mensaje = "El campo contraseña es obligatorio";
                return false;
            }
            if (this.txtContrasena.Text.Trim().Length > 20)
            {
                mensaje = "El campo contraseña no puede ser tan grande";
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
            return true;
        }
    }
}