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
    public partial class RecuperacionContrasena : System.Web.UI.Page
    {
        private String key;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataContext db = new DcGeneralDataContext();
            var key = Request.QueryString["key"] as String;
            if (key == null)
            {
                Response.Redirect("~/ContrasenaCorreo.aspx", false);
                return;
            }
            this.key = key;
            var user = db.GetTable<Usuario>().FirstOrDefault(u => u.strTokenContrasena == key);
            if (user == null)
            {
                Response.Redirect("~/ContrasenaCorreo.aspx", false);
                return;
            }
            else
            {
                var persona = db.GetTable<Linq.Data.Entity.Persona>().FirstOrDefault(p => p.id == user.idComPersona);
                this.txtNombreUsuario.Text = user.strNombreUsuario;
                this.txtCorreo.Text = persona.strEmail;
                this.txtPersona.Text = persona.NombreCompleto;
            }
        }

        protected void btnReestablecerContrasena_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            try
            {
                DataContext db = new DcGeneralDataContext();
                String mensaje = String.Empty;
                var user = db.GetTable<Usuario>().FirstOrDefault(u => u.strTokenContrasena == this.key);
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
                user.strContrasena = CtrlEncrypt.Encriptar(this.txtContrasena.Text.Trim());
                user.strTokenContrasena = null;
                db.SubmitChanges();
                Response.Redirect("~/PaginaPrincipal.aspx", false);
            }
            catch (Exception ex)
            {
                CtrlCorreo email = new CtrlCorreo();
                email.enviarCorreo(ex.Message);
                this.Response.Redirect("~/PaginasErrores/Error.html", false);
            }
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
        private bool validacion(ref String mensaje)
        {
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
            if (this.txtContrasena.Text.Trim().Length < 5)
            {
                mensaje = "El campo contraseña debe tener al menos 5 caracteres";
                return false;
            }
            if (txtContrasena2.Text.Trim().Length == 0)
            {
                mensaje = "El campo confirmar contraseña es requerido.";
                return false;
            }
            if (!txtContrasena2.Text.Trim().Equals(txtContrasena.Text.Trim()))
            {
                mensaje = "Las contraseñas ingresadas no son iguales";
                return false;
            }
            return true;
        }
        public bool sqlInjectionValida(ref String _mensaje)
        {
            CtrlValidacion valida = new CtrlValidacion();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtContrasena.Text.Trim(), ref _mensajeFuncion, "Contraseña", ref this.txtContrasena))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtContrasena2.Text.Trim(), ref _mensajeFuncion, "Confirmar contraseña", ref this.txtContrasena2))
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
            if (valida.htmlInjectionValida(this.txtContrasena.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtContrasena))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtContrasena2.Text.Trim(), ref mensajeFuncion, "Confirmar contraseña", ref this.txtContrasena2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }
    }
}