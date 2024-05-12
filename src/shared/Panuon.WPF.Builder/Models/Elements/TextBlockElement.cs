using Panuon.WPF.Builder.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Panuon.WPF.Builder.Elements
{
    internal class TextBlockElement
        : DocumentTextElement, ITextBlockElement
    {
        #region Ctor
        internal TextBlockElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
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
        public ITextBlockElement AddRun(object text = null,
            object toolTip = null,
            object foreground = null)
        {
            var runFactory = new FrameworkElementFactory(typeof(Run));
            if (text != null)
            {
                SetFactoryValue(runFactory, Run.TextProperty, text);
            }
            if (foreground != null)
            {
                SetFactoryValue(runFactory, Run.ForegroundProperty, foreground);
            }
            if (toolTip != null)
            {
                SetFactoryValue(runFactory, Run.ToolTipProperty, toolTip);
            }
            _visualBuilder.AddChild(runFactory);

            var run = new Run();
            if (text != null)
            {
                SetValue(run, Run.TextProperty, text);
            }
            if (foreground != null)
            {
                SetValue(run, Run.ForegroundProperty, foreground);
            }
            if (toolTip != null)
            {
                SetValue(run, Run.ToolTipProperty, toolTip);
            }

            var textBlock = Visual as TextBlock;
            textBlock.Inlines.Add(run);

            return this;
        }

        override public void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "text":
                    SetValue(TextBlock.TextProperty, value);
                    break;
                case "foreground":
                    SetValue(TextBlock.ForegroundProperty, value);
                    break;
                case "fontsize":
                    SetValue(TextBlock.FontSizeProperty, value);
                    break;
                case "fontfamily":
                    SetValue(TextBlock.FontFamilyProperty, value);
                    break;
                case "fontstyle":
                    SetValue(TextBlock.FontStyleProperty, value);
                    break;
                case "fontweight":
                    SetValue(TextBlock.FontWeightProperty, value);
                    break;
                case "fontstretch":
                    SetValue(TextBlock.FontStretchProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }   
        #endregion

        #region Functions
        #endregion
    }

}
