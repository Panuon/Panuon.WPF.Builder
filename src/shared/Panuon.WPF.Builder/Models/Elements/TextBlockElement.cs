using Panuon.WPF.Builder.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Panuon.WPF.Builder.Elements
{
    internal class TextBlockElement
        : DocumentTextElement, ITextBlockElement
    {
        #region Ctor
        internal TextBlockElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(TextBlock);

        public string Text
        {
            get => GetValue(TextBlock.TextProperty) as string;
            set => SetValue(TextBlock.TextProperty, value);
        }
        #endregion

        #region Methods
        public ITextBlockElement CustomStyle(object foreground = null)
        {
            if (foreground != null)
            {
                SetValue(TextBlock.ForegroundProperty, foreground);
            }

            return this;
        }

        public ITextBlockElement AddRun(object text = null,
            object foreground = null)
        {
            var run = new Run();

            if (text != null)
            {
                SetValue(run, Run.TextProperty, text);
            }
            if (foreground != null)
            {
                SetValue(run, Run.ForegroundProperty, foreground);
            }

            var textBlock = ActualVisual as TextBlock;
            textBlock.Inlines.Add(run);

            return this;
        }

        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "text":
                    SetValue(TextBlock.TextProperty, value);
                    return true;
                case "foreground":
                    SetValue(TextBlock.ForegroundProperty, value);
                    return true;
                case "fontsize":
                    SetValue(TextBlock.FontSizeProperty, value);
                    return true;
                case "fontfamily":
                    SetValue(TextBlock.FontFamilyProperty, value);
                    return true;
                case "fontstyle":
                    SetValue(TextBlock.FontStyleProperty, value);
                    return true;
                case "fontweight":
                    SetValue(TextBlock.FontWeightProperty, value);
                    return true;
                case "fontstretch":
                    SetValue(TextBlock.FontStretchProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion

        #region Functions
        #endregion
    }

}
