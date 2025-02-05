﻿using System;
using System.Collections.Generic;
using System.Text;

using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDF_ToolBox.Extensions
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).Assembly);

            return imageSource;
        }
    }
}
