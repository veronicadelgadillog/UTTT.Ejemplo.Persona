const txtClaveUnica = document.querySelector('#txtClaveUnica');
const ddlSexo = document.querySelector('#ddlSexo');
const txtNombre = document.querySelector('#txtNombre');
const txtAPaterno = document.querySelector('#txtAPaterno');
const txtAMaterno = document.querySelector('#txtAMaterno');
const txtNumHermanos = document.querySelector('#txtHermanos');
const txtEmail = document.querySelector('#txtCorreo');
const txtCP = document.querySelector('#txtCodigoP');
const txtRFC = document.querySelector('#txtRFC');
const lblAction = document.querySelector('#lblAccion');
const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const rfcRegex = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/;
const onlyStringsRegex = /^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$/;
const spaceRegex = /\s\s+/;
let errorMessage = '';

function validar() {
    if (validarFormulario()) {
        return true;
    }
    // mensaje en pantasha
    if (errorMessage.length > 0) {
        alert(errorMessage);
    }
    return false;
}

function validarFormulario() {
    if (Number.parseInt(ddlSexo.value) < 0) {
        errorMessage = 'El campo sexo es obligatorio.';
        return false;
    }
    if (txtClaveUnica.value.trim().length === 0) {
        errorMessage = 'El campo clave única es obligatorio.';
        return false;
    }
    if (!(/^([0-9])*$/.test(txtClaveUnica.value)) && txtClaveUnica.value.trim().length > 0) {
        errorMessage = 'El campo clave única solo acepta números.';
        return false;
    }
    if (txtClaveUnica.value.trim().length !== 3) {
        errorMessage = 'El campo clave única debe contener 3 leras.';
        return false;
    }
    if (txtNombre.value.trim().length === 0) {
        errorMessage = 'El campo nombre es obligatorio.';
        return false;
    }
    if (txtNombre.value.trim().length < 3 || txtNombre.value.trim().length > 50) {
        errorMessage = 'El campo nombre debe tener 3 letras como mínimo y 20 como máximo';
        return false;
    }
    if (spaceRegex.test(txtNombre.value)) {
        errorMessage = 'El campo nombre no puede contener más de 1 espacio seguido.';
        return false;
    }
    if (!onlyStringsRegex.test(txtNombre.value)) {
        errorMessage = 'El campo nombre solo acepta letras.';
        return false;
    }
    if (txtAPaterno.value.trim().length === 0) {
        errorMessage = 'El campo apellido paterno es obligatorio.';
        return false;
    }
    if (txtAPaterno.value.length < 3 || txtAPaterno.value.length > 50) {
        errorMessage = 'El campo apellido paterno debe tener 3 letras como mínimo y 20 como máximo.';
        return false;
    }
    if (spaceRegex.test(txtAPaterno.value)) {
        errorMessage = 'El campo apellido paterno no puede contener más de 1 espacio seguido.';
        return false;
    }
    if (!onlyStringsRegex.test(txtAPaterno.value)) {
        errorMessage = 'El campo apellido paterno solo acepta letras.';
        return false;
    }
    if (spaceRegex.test(txtAMaterno.value)) {
        errorMessage = 'El campo apellido materno no puede contener más de 1 espacio seguido.';
        return false;
    }
    if (txtAMaterno.value.trim().length > 0 && txtAMaterno.value.trim().length < 3) {
        errorMessage = 'El campo apellido materno debe tener 3 letras como mínimo y 20 como máximo.';
        return false;
    }
    if (!onlyStringsRegex.test(txtAMaterno.value) && txtAMaterno.value.trim().length > 1) {
        errorMessage = 'El campo apellido materno solo acepta letras.';
        return false;
    }
    if (txtNumHermanos.value.trim().length === 0) {
        errorMessage = 'El campo número de hermanos es obligatorio.';
        return false;
    }
    if (!(/^([0-9])*$/.test(txtNumHermanos.value.length > 0 ? txtNumHermanos.value : 'abcd'))) {
        errorMessage = 'El campo número de hermanos solo acepta números.';
        return false;
    }
    if (txtNumHermanos.value.length > 1 && txtNumHermanos.value[0] === "0") {
        errorMessage = 'El valor del campo número de hermanos debe estar entre 0 y 10.';
        return false;
    }
    if (txtEmail.value.trim().length === 0) {
        errorMessage = 'El campo correo electrónico es obligatorio.';
        return false;
    }
    if (!emailRegex.test(txtEmail.value.toLowerCase())) {
        errorMessage = 'El formato del correo electrónico no es válido.';
        return false;
    }
    if (txtCP.value.trim().length === 0) {
        errorMessage = 'El campo código postal es obligatorio.';
        return false;
    }
    if (!(/^([0-9])*$/.test(txtCP.value.length > 0 ? txtCP.value : 'abcd'))) {
        errorMessage = 'El campo código postal solo acepta números.';
        return false;
    }
    if (txtCP.value.trim().length !== 5) {
        errorMessage = 'El campo código postal debe tener 5 caracteres de longitud.';
        return false;
    }
    if (txtRFC.value.trim().length === 0) {
        errorMessage = 'El campo RFC es obligatorio.';
        return false;
    }
    if (!rfcRegex.test(txtRFC.value.toUpperCase())) {
        errorMessage = 'El formato del campo RFC no es válido.';
        return false;
    }
    return true;
}