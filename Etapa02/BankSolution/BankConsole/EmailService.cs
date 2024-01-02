using MailKit.Net.Smtp;
using MimeKit;

namespace BankConsole;

public static class EmailService    {
    public static void SendMail()   {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress ("John Doe", "johndoe@gmail.com"));
        message.To.Add(new MailboxAddress ("Admin", "admin123@gmail.com"));
        message.Subject = "BankConsole: Usuarios nuevos";

        message.Body = new TextPart("plain")    {
            Text = GetEmailText()
        };

        using (var Client = new SmtpClient ())  {
            Client.Connect("smtp.gmail.com", 587, false);
            Client.Authenticate("johndoe@gmail.com", "1234987654191625");
            Client.Send(message);
            Client.Disconnect(true);
        }
    }

    private static string GetEmailText()    {
        List<User> newUsers = Storage.GetNewUsers();

        if(newUsers.Count == 0)
            return "No nuevos usuarios";
        
        string emailText = "Usuarios agregados hoy: \n";

        foreach (User user in newUsers)
            emailText += "\t+ " + user.ShowData() + "\n";
        
        return emailText;
    }
}