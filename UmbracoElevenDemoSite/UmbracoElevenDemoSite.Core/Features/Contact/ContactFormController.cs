using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Website.Controllers;

namespace UmbracoTenDemoSite.Core.Features.Contact
{
    public class ContactFormController : SurfaceController
    {
        private readonly IOptionsMonitor<GlobalSettings> _globalSettings;
        private readonly ContactMailOptions _mailOptions;
        public ContactFormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IOptionsMonitor<GlobalSettings> globalSettings, IOptions<ContactMailOptions> mailOptions) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _globalSettings = globalSettings;
            _mailOptions = mailOptions.Value;
        }
        /// <summary>
        /// route: /umbraco/surface/{controllername}/{action}/{id}
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateUmbracoFormRouteString]
        public IActionResult HandleSubmit(ContactFormModel formData)
        {
            if (formData.DataConsent && formData.DataConsentText == null)
            {
                ModelState.Remove("DataConsentText");
            }
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            HandleContactData(formData);

            if (CurrentPage?.Children != null && CurrentPage.Children.Any())
            {
                return RedirectToUmbracoPage(CurrentPage.Children.First());
            }

            return RedirectToCurrentUmbracoPage();
        }

        private void HandleContactData(ContactFormModel formData)
        {
            if (string.IsNullOrWhiteSpace(_mailOptions.To))
            {
                throw new Exception("No recipient configured. Check appSettings ContactMail:To");
            }
            var smtpSettings = _globalSettings.CurrentValue.Smtp;
            if (smtpSettings == null || string.IsNullOrEmpty(smtpSettings.Host))
            {
                throw new Exception("No smtp configured. Check appSettings Umbraco:CMS:Global:Smtp");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpSettings.From, smtpSettings.From));
            message.To.Add(new MailboxAddress(_mailOptions.ToName ?? _mailOptions.To, _mailOptions.To));
            message.Subject = $"Contact from {Request.Host}";

            message.Body = new TextPart("plain")
            {
                Text = $"Contact\n\nName:\n{formData.FirstAndLastName}\n\nEmail:\n{formData.Email}\n\nSubject:\n{formData.Subject}\n\nMessage:\n{formData.Message}\n\nConsent:\n{formData.DataConsent}"
            };

            using (var client = new SmtpClient())
            {
                if (smtpSettings.Port > 0)
                {
                    var useSsl = smtpSettings.SecureSocketOptions.ToString() == SecureSocketOptions.SslOnConnect.ToString();
                    client.Connect(smtpSettings.Host, smtpSettings.Port, useSsl);
                }
                else
                {
                    client.Connect(smtpSettings.Host);
                }

                if (!string.IsNullOrWhiteSpace(smtpSettings.Username))
                {
                    client.Authenticate(smtpSettings.Username, smtpSettings.Password);
                }

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
