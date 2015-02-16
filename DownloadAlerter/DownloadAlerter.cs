using System;
using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.Web.Iis.Rewrite;

public class DownloadAlerter : IRewriteProvider, IProviderDescriptor
{
    private string recipient, sender, server;

    public void Initialize(IDictionary<string, string> settings, IRewriteContext rewriteContext)
    {
        if (!settings.TryGetValue("Recipient", out recipient) || string.IsNullOrEmpty(recipient))
            throw new ArgumentException("Recipient provider setting is required and cannot be empty");

        if (!settings.TryGetValue("Sender", out sender) || string.IsNullOrEmpty(sender))
            throw new ArgumentException("Sender provider setting is required and cannot be empty");

        if (!settings.TryGetValue("Server", out server) || string.IsNullOrEmpty(server))
            throw new ArgumentException("Server provider setting is required and cannot be empty");
    }

    public string Rewrite(string value)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress(sender);

        message.To.Add(new MailAddress(recipient));

        message.Subject = "Download Alert";
        message.Body = string.Format("The following URL was downloaded: '{0}'" + Environment.NewLine + "For details check your IIS logs", value);

        SmtpClient client = new SmtpClient();
        client.Host = server;
        client.Send(message);

        return value;
    }

    public IEnumerable<SettingDescriptor> GetSettings()
    {
        yield return new SettingDescriptor("Recipient", "Email address of the recipient");
        yield return new SettingDescriptor("Sender", "Email address of the sender");
        yield return new SettingDescriptor("Server", "The SMTP server to be used");
    }
}