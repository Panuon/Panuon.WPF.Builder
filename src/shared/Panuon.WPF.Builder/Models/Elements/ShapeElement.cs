using System.Collections.Generic;
using System.Windows.Shapes;

namespace Panuon.WPF.Builder.Elements
{
    internal abstract class ShapeElement
        : Element, IShapeElement
    {
        #region Ctor
        internal ShapeElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "fill":
                    SetValue(Shape.FillProperty, value);
                    break;
                case "stroke":
                    SetValue(Shape.StrokeProperty, value);
                    break;
                case "strokethickness":
                    SetValue(Shape.StrokeThicknessProperty, value);
                    break;
                case "strokedasharray":
                    SetValue(Shape.StrokeDashArrayProperty, value);
                    break;
                case "strokedashcap":
                    SetValue(Shape.StrokeDashCapProperty, value);
                    break;
                case "strokedashoffset":
                    SetValue(Shape.StrokeDashOffsetProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}
