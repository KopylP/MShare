using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace MShare.Framework.System.Localization
{
	public static class WebApplicationExtentions
	{
		public static IApplicationBuilder UseSystemLocalization(this IApplicationBuilder builder)
		{
			builder.UseRequestLocalization(opt =>
			{
				var supportedCultures = new string[] { "uk", "pl", "en" };
				opt.AddSupportedCultures(supportedCultures);
				opt.AddSupportedUICultures(supportedCultures);
                opt.DefaultRequestCulture = new RequestCulture("en");
				opt.RequestCultureProviders.Clear();
				opt.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
			});

			return builder;
        }
	}
}

