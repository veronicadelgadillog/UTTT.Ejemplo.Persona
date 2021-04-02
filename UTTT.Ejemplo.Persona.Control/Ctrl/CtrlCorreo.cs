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
                mailMessage.From = new MailAddress("el correo de donde lo vas a enviar");
                mailMessage.To.Add(new MailAddress("tucorreo"));
                mailMessage.Subject = "Error en el sistema";
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = message;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("your-email-address-bro", "your-password");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
            catch (Exception _e)
            {
                Console.WriteLine(_e.Message);
            }
        }
    }
}
