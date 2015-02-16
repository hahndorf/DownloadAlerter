# DownloadAlerter
An IIS URL Rewrite custom provider to send an email when a specific url is requested.

Someone on [Serverfault](http://serverfault.com/questions/667999/iis-how-to-receive-an-email-notification-when-file-is-downloaded-using-a-direct/668051 "Link to article") asked about how to send an email when someone requested a specific URL on your IIS server.

One solution is to use the [IIS Url Rewrite module ](http://www.iis.net/downloads/microsoft/url-rewrite) and write a custom Rewrite Provider.

An [article on IIS.net](http://www.iis.net/learn/extensions/url-rewrite-module/developing-a-custom-rewrite-provider-for-url-rewrite-module) describes how to do this in general.

This repository has a Visual Studio 2013 project that creates such a provider.

After you built the project you have to register the file: `Hahndorf.IIS.DownloadAlertProvider.dll` in the GAC, this is a requirement for the Rewrite Module Provider.

Next you have to add the provider to your site and add a rule. See the `web.config` file for an example of this.

Make sure you IIS Application pool runs .Net 4