using System.Net;
using System.Net.Mail;

namespace CityInfo.API.Services
{
    public class LocalMailService : ILocalMailService
    {
        private readonly string _mailFrom = string.Empty;
        private readonly string _mailTo = string.Empty;
        public LocalMailService(IConfiguration configuration)
        {
            _mailFrom = configuration["MailSetting:FromEmail"];
            _mailTo = configuration["MailSetting:ToEmail"];

        }


        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail from {_mailFrom} to {_mailTo},"
                + $"with {nameof(LocalMailService)},");
            Console.WriteLine($"subject {subject}");
            Console.WriteLine($"message {message}");
        }


        public static void Email(string subject, string to, string htmlstring)
        {
            try
            {
                string _mailFrom = "";

                var message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_mailFrom);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlstring;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("bsjbase@gmail.com", "******");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(message);


            }
            catch (Exception er)
            {

                throw;
            }
        }
    }
}
