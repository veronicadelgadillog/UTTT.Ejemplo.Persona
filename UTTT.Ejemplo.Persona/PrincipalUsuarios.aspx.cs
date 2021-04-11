using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class PrincipalUsuarios : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new DcGeneralDataContext();
                if (!this.IsPostBack)
                {
                    List<EstadoUsuario> estadoUsuarios = dcTemp.GetTable<EstadoUsuario>().ToList();
                    EstadoUsuario estadoTemp = new EstadoUsuario();
                    estadoTemp.id = -1;
                    estadoTemp.strValor = "Todos";
                    estadoUsuarios.Insert(0, estadoTemp);
                    this.ddlEstado.DataTextField = "strValor";
                    this.ddlEstado.DataValueField = "id";
                    this.ddlEstado.DataSource = estadoUsuarios;
                    this.ddlEstado.DataBind();
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourceUsuario.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.session.Pantalla = "~/NuevoUsuario.aspx";
            Hashtable parametrosRagion = new Hashtable();
            parametrosRagion.Add("idUsuario", "0");
            this.session.Parametros = parametrosRagion;
            this.Session["SessionManager"] = this.session;
            this.Response.Redirect(this.session.Pantalla, false);
        }

        protected void botonBusquedaXD_Click(object sender, EventArgs e)
        {
            this.DataSourceUsuario.RaiseViewChanged();
        }

        protected void DataSourceUsuario_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                bool estadoBool = false;
                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlEstado.Text != "-1")
                {
                    estadoBool = true;
                }

                Expression<Func<Usuario, bool>>
                    predicate =
                    (c =>
                    ((estadoBool) ? c.idEstado == int.Parse(this.ddlEstado.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombreUsuario.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<Usuario> usersList = dcConsulta.GetTable<Usuario>().Where(predicate).ToList();
                e.Result = usersList;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }
        protected void dgvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idUser = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idUser);
                        break;
                    case "Eliminar":
                        this.eliminar(idUser);
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void editar(int idUser)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idUsuario", idUser.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/NuevoUsuario.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }
        private void eliminar(int idUser)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                Usuario user = dcDelete.GetTable<Usuario>().First(
                    c => c.id == idUser);
                dcDelete.GetTable<Usuario>().DeleteOnSubmit(user);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se agrego correctamente.");
                this.DataSourceUsuario.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

    }
}