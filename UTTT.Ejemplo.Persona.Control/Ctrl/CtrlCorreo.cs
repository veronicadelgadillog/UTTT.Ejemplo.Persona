using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlCorreo
    {
        public void enviarCorreo(String message)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                mailMessage.From = new MailAddress("veronicadelgadillogomez51@gmail.com");
                mailMessage.To.Add(new MailAddress("veronicadelgadillogomez51@gmail.com"));
                mailMessage.Subject = "Error en el sistema";
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = message;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("veronicadelgadillogomez51@gmail.com", "your-password");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
            catch (Exception _e)
            {
                Console.WriteLine(_e.Message);
            }
        }

        public bool recuperarContrasenaCorreo(String correo, String nombreUsuario, String token)
        {
            try
            {
                return true;
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                mailMessage.From = new MailAddress("veronicadelgadillogomez51@gmail.com");
                mailMessage.To.Add(new MailAddress(correo));
                mailMessage.Subject = "Restaurar contraseña";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = this.crearMensajeRecuperacion(nombreUsuario, token);
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("veronicadelgadillogomez51@gmail.com", "your-password");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }
        private String crearMensajeRecuperacion(String nombreUsuario, String key)
        {
            // String url = "http://localhost:36683/RecuperacionContrasena.aspx?key="+key;
            String url = "http://gomezveronica-001-site1.itempurl.com/RecuperacionContrasena.aspx?key=" + key;
            String body = "<h2>Qué tal " + nombreUsuario + "</h2><br>" +
                "<p>Este correo es para que puedas cambiar tu contraseña para ello" +
                " debes dar clic en el siguiente enlace</p>" +
                "<a href='" + url + "'>Restaurar mi contraseña</a>";
            return body;
        }
    }
}
