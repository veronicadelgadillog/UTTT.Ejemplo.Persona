#region Using

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
using System.Globalization;
using System.Text.RegularExpressions;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = false;
                    this.ddlEstadoCivil.AutoPostBack = false;
                    List<EstadoCivil> estadoCivil = dcGlobal.GetTable<EstadoCivil>().ToList();
                    this.ddlEstadoCivil.DataTextField = "strValor";
                    this.ddlEstadoCivil.DataValueField = "id";
                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataTextField = "strValor";
                        this.ddlSexo.DataValueField = "id";
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        EstadoCivil estadoCivilTemp = new EstadoCivil();
                        estadoCivilTemp.Id = -1;
                        estadoCivilTemp.strValor = "Seleccionar";
                        estadoCivil.Insert(0, estadoCivilTemp);
                        this.ddlEstadoCivil.DataSource = estadoCivil;
                        this.ddlEstadoCivil.DataBind();
                    }
                    else
                    {
                        catTemp.id = baseEntity.CatSexo.strValor == "Masculino" ? 1 : 2;
                        catTemp.strValor = baseEntity.CatSexo.strValor;
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataTextField = "strValor";
                        this.ddlSexo.DataValueField = "id";
                        lista.RemoveAt(0);
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        this.txtHermanos.Text = this.baseEntity.intNumHermanos.ToString();
                        DateTime? fechaNacimiento = this.baseEntity.dtFechaNacimiento;
                        this.txtCorreo.Text = this.baseEntity.strEmail;
                        this.txtCodigoP.Text = this.baseEntity.strCP;
                        this.txtRFC.Text = this.baseEntity.strRFC;
                        if (fechaNacimiento != null)
                        {
                            this.txtFechaNaci.Text = fechaNacimiento.Value.Date.ToString("dd/MM/yyyy");
                        }
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);
                        if (baseEntity.EstadoCivil == null)
                        {
                            EstadoCivil ecTemp = new EstadoCivil();
                            ecTemp.Id = -1;
                            ecTemp.strValor = "Seleccionar";
                            estadoCivil.Insert(0, ecTemp);
                        }
                        this.ddlEstadoCivil.DataSource = estadoCivil;
                        this.ddlEstadoCivil.DataBind();
                        if (baseEntity.EstadoCivil != null)
                        {
                            this.setItem(ref this.ddlEstadoCivil, baseEntity.EstadoCivil.strValor);
                        }
                    }                
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValid)
                {
                    return;
                }
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                int i = 0;
                DateTime dateValidate = DateTime.Now;
                if (this.idPersona == 0)
                {
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    persona.dtFechaNacimiento = (!DateTime.TryParseExact(this.txtFechaNaci.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValidate)) ? (DateTime?)null : dateValidate;
                    persona.intNumHermanos = this.txtHermanos.Text.Trim().Length > 0 ? (int.TryParse(this.txtHermanos.Text.Trim(), out i) ? int.Parse(this.txtHermanos.Text.Trim()) : 0) : 0;
                    persona.strEmail = this.txtCorreo.Text.Trim();
                    persona.strCP = this.txtCodigoP.Text.Trim();
                    persona.strRFC = this.txtRFC.Text.Trim();
                    persona.idEstadoCivil = int.Parse(this.ddlEstadoCivil.Text);
                    String mensaje = String.Empty;
                    if (!this.validacion(persona, ref mensaje))
                    {
                        this.lblErrorValidacion.Text = mensaje;
                        this.lblErrorValidacion.Visible = true;
                        return;
                    }
                    if (!this.sqlInjectionValida(ref mensaje))
                    {
                        this.lblErrorValidacion.Text = mensaje;
                        this.lblErrorValidacion.Visible = true;
                        return;
                    }
                    if (!this.htmlInjectionValida(ref mensaje))
                    {
                        this.lblErrorValidacion.Text = mensaje;
                        this.lblErrorValidacion.Visible = true;
                        return;
                    }
                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                    
                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == idPersona);
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    persona.dtFechaNacimiento = (!DateTime.TryParseExact(this.txtFechaNaci.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValidate)) ? (DateTime?)null : dateValidate;
                    persona.intNumHermanos = this.txtHermanos.Text.Trim().Length > 0 ? (int.TryParse(this.txtHermanos.Text.Trim(), out i) ? int.Parse(this.txtHermanos.Text.Trim()) : 0) : 0;
                    persona.strEmail = this.txtCorreo.Text.Trim();
                    persona.strCP = this.txtCodigoP.Text.Trim();
                    persona.strRFC = this.txtRFC.Text.Trim();
                    persona.idEstadoCivil = int.Parse(this.ddlEstadoCivil.Text);
                    String mensaje = String.Empty;
                    if (!this.validacion(persona, ref mensaje))
                    {
                        this.lblErrorValidacion.Text = mensaje;
                        this.lblErrorValidacion.Visible = true;
                        return;
                    }
                    if (!this.sqlInjectionValida(ref mensaje))
                    {
                        this.lblErrorValidacion.Text = mensaje;
                        this.lblErrorValidacion.Visible = true;
                        return;
                    }
                    if (!this.htmlInjectionValida(ref mensaje))
                    {
                        this.lblErrorValidacion.Text = mensaje;
                        this.lblErrorValidacion.Visible = true;
                        return;
                    }
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                }
            }
            catch (Exception _e)
            {
                CtrlCorreo correo = new CtrlCorreo();
                correo.enviarCorreo(_e.Message);
                this.Response.Redirect("~/PaginasErrores/Error.html", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {              
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();            
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

        public void setItem(ref DropDownList _control, String _value)
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

        #endregion

        protected void ValidarSexo(object source, ServerValidateEventArgs args)
        {
            int sexIndex = int.Parse(this.ddlSexo.SelectedValue);
            args.IsValid = sexIndex > 0;
        }

        protected void ValidarEspaciosNombre(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtNombre.Text.Contains("  ");
        }

        protected void ValidarEspaciosAPaterno(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtAPaterno.Text.Contains("  ");
        }

        protected void ValidarEspaciosAMaterno(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtAMaterno.Text.Contains("  ");
        }

        protected void ValidarEstadoCivil(object source, ServerValidateEventArgs args)
        {
            int sexIndex = int.Parse(this.ddlEstadoCivil.SelectedValue);
            args.IsValid = sexIndex > 0;
        }

        protected void ValidarCodigoPostal(object source, ServerValidateEventArgs args)
        {
            args.IsValid = this.txtCodigoP.Text.Trim().Length == 5;
        }

        //
        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona, ref String _mensaje)
        {
            Regex emailRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            Regex rfcRegex = new Regex(@"^([aA-zZñÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([aA-zZ\d]{3})?$");
            Regex onlyLetters = new Regex(@"^[a-zA-ZÀ-ÿ\s\u00f1\u00d1]+$");
            int i = 0;
            if (_persona.idCatSexo < 0)
            {
                _mensaje = "El campo sexo es obligatorio.";
                return false;
            }
            if (_persona.strClaveUnica.Equals(String.Empty))
            {
                _mensaje = "El campo clave única es obligatorio.";
                return false;
            }
            if (!int.TryParse(_persona.strClaveUnica, out i))
            {
                _mensaje = "La clave única solo puede contener números.";
                return false;
            }
            if (int.Parse(_persona.strClaveUnica) < 100 || int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "La clave única debe estar en un rango de 100 a 999.";
                return false;
            }
            if (_persona.strClaveUnica.Length > 3 || _persona.strClaveUnica.Length < 3)
            {
                _mensaje = "La clave única debe contar con 3 caracteres.";
                return false;
            }
            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "El campo nombre es obligatorio.";
                return false;
            }
            if (_persona.strNombre.Length > 50)
            {
                _mensaje = "El campo nombre no puede ser tan grande.";
                return false;
            }
            if (_persona.strNombre.Length < 3)
            {
                _mensaje = "El campo nombre debe tener al menos de 3 letras.";
                return false;
            }
            if (!onlyLetters.IsMatch(_persona.strNombre))
            {
                _mensaje = "El campo nombre debe contener solo letras.";
                return false;
            }
            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "El campo apellido paterno es obligatorio.";
                return false;
            }
            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = "El campo apellido paterno no puede ser tan grande.";
                return false;
            }
            if (_persona.strAPaterno.Length < 3)
            {
                _mensaje = "El campo apellido paterno debe tener al menos de 3 letras";
                return false;
            }
            if (!onlyLetters.IsMatch(_persona.strAPaterno))
            {
                _mensaje = "El campo apellido paterno debe contener solo letras.";
                return false;
            }
            if (_persona.strAMaterno.Length > 50)
            {
                _mensaje = "El campo apellido materno no puede ser tan grande.";
                return false;
            }
            if (_persona.strAMaterno.Length > 0 && _persona.strAMaterno.Length < 3)
            {
                _mensaje = "El campo apellido materno debe tener al menos 3 letras.";
                return false;
            }
            if (!onlyLetters.IsMatch(_persona.strAMaterno) && _persona.strAMaterno.Length > 0)
            {
                _mensaje = "El campo apellido materno debe contener solo letras.";
                return false;
            }
            if (this.txtFechaNaci.Text.Equals(String.Empty))
            {
                _mensaje = "La fecha de nacimiento es obligatoria.";
                return false;
            }
            if (_persona.dtFechaNacimiento == null)
            {
                _mensaje = "La fecha ingresada no es correcta, por favor, sigue el formato dd/MM/yyyy.";
                return false;
            }
            var time = DateTime.Now - _persona.dtFechaNacimiento.Value.Date;
            if (time.Days < 6570)
            {
                _mensaje = "Debes ser mayor de edad para poder registrarte.";
                return false;
            }
            if (_persona.intNumHermanos.ToString().Length > 2)
            {
                _mensaje = "El campo número de hermanos no puede ser tan grande.";
                return false;
            }
            if (!emailRegex.IsMatch(_persona.strEmail))
            {
                _mensaje = "El correo electrónico no es válido.";
                return false;
            }
            if (_persona.strEmail.Length > 100)
            {
                _mensaje = "El correo electrónico no puede ser tan grande.";
                return false;
            }
            if (_persona.strCP.Length != 5)
            {
                _mensaje = "El código postal debe tener 5 números.";
                return false;
            }
            if (!rfcRegex.IsMatch(_persona.strRFC))
            {
                _mensaje = "El formato del campo RFC no es válido";
                return false;
            }
            if (_persona.strRFC.Length > 13)
            {
                _mensaje = "La longitud del RFC debe estar entre 12 y 13 caracteres.";
                return false;
            }
            return true;
        }

        public bool htmlInjectionValida(ref String _mensaje)
        {
            CtrlValidacion valida = new CtrlValidacion();
            String mensajeFuncion = String.Empty;
            if (valida.htmlInjectionValida(this.txtClaveUnica.Text.Trim(), ref mensajeFuncion, "Clave Única", ref this.txtClaveUnica))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "Apellido Paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "Apellido Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtHermanos.Text.Trim(), ref mensajeFuncion, "Número de Hermanos", ref this.txtHermanos))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtCorreo.Text.Trim(), ref mensajeFuncion, "Email", ref this.txtCorreo))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtCodigoP.Text.Trim(), ref mensajeFuncion, "Código postal", ref this.txtCodigoP))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtRFC.Text.Trim(), ref mensajeFuncion, "RFC", ref this.txtRFC))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool sqlInjectionValida (ref String _mensaje)
        {
            CtrlValidacion valida = new CtrlValidacion();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtClaveUnica.Text.Trim(), ref _mensajeFuncion, "Clave Única", ref this.txtClaveUnica))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(txtNombre.Text.Trim(), ref _mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtAPaterno.Text.Trim(), ref _mensajeFuncion, "Apellido Paterno", ref this.txtAPaterno))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtAMaterno.Text.Trim(), ref _mensajeFuncion, "Apellido Materno", ref this.txtAMaterno))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtHermanos.Text.Trim(), ref _mensajeFuncion, "Numero de hermanos", ref this.txtHermanos))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtCorreo.Text.Trim(), ref _mensajeFuncion, "Email", ref this.txtCorreo))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtCodigoP.Text.Trim(), ref _mensajeFuncion, "Código Postal", ref this.txtCodigoP))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtRFC.Text.Trim(), ref _mensajeFuncion, "RFC", ref this.txtRFC))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            return true;
        }

    }
}