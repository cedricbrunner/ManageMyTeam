using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyTeam.Models
{
    public class WeekOfYearModelBinderProvider : IModelBinderProvider
    {
        internal static readonly DateTimeStyles SupportedStyles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AllowWhiteSpaces;
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var modelType = context.Metadata.UnderlyingOrModelType;
            if (modelType == typeof(DateTime))
            {
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
                return new WeekOfYearAwareDateTimeModelBinder(SupportedStyles, loggerFactory);
            }

            return null;
        }
    }
}
