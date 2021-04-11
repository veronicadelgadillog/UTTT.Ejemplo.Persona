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
    public partial class ContrasenaCorreo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCorreo_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            try
            {
                CtrlCorreo email = new CtrlCorreo();
                DataContext db = new DcGeneralDataContext();
                var persona = db.GetTable<Linq.Data.Entity.Persona>().FirstOrDefault(p => p.strEmail == this.txtCorreo.Text.Trim());
                if (persona == null)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "El correo ingresado no existe en nuestra base de datos";
                    return;
                }
                var usuario = db.GetTable<Usuario>().FirstOrDefault(u => u.idComPersona == persona.id);
                if (usuario == null)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "El correo ingresado no está asociado a un usuario";
                    return;
                }
                if (usuario.idEstado > 1)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "El usuario asignado a este correo no se encuentra activo";
                    return;
                }
                var token = CtrlEncrypt.GetSHA256(Guid.NewGuid().ToString());
                usuario.strTokenContrasena = token;
                db.SubmitChanges();
                if (email.recuperarContrasenaCorreo(persona.strEmail, persona.strNombre, token))
                {
                    Response.Redirect("~/CorreoEnviado.html", false);
                }
                else
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "Hubo un error al enviar el correo, intenta más tarde.";
                    return;
                }

            }
            catch (Exception ex)
            {
                CtrlCorreo email = new CtrlCorreo();
                email.enviarCorreo(ex.Message);
                this.Response.Redirect("~/PaginasErrores/Error.html", false);
            }
        }
    }
}