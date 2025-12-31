using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Resources.Service
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _Localizer;
        public LocalizationService(IStringLocalizerFactory factory)
        {
            var AssemblyNmae= typeof(SharedResources).Assembly.GetName().Name;
            _Localizer = factory.Create("SharedResources", AssemblyNmae!);
        }

        public string Get(string key)
        {
            return _Localizer[key];
        }

    }
}
