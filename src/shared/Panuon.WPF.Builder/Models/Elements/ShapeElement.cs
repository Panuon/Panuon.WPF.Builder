using System.Collections.Generic;
using System.Windows.Shapes;

namespace Panuon.WPF.Builder.Elements
{
    internal abstract class ShapeElement
        : Element, IShapeElement
    {
        #region Ctor
        internal ShapeElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "fill":
                    SetValue(Shape.FillProperty, value);
                    return true;
                case "stroke":
                    SetValue(Shape.StrokeProperty, value);
                    return true;
                case "strokethickness":
                    SetValue(Shape.StrokeThicknessProperty, value);
                    return true;
                case "strokedasharray":
                    SetValue(Shape.StrokeDashArrayProperty, value);
                    return true;
                case "strokedashcap":
                    SetValue(Shape.StrokeDashCapProperty, value);
                    return true;
                case "strokedashoffset":
                    SetValue(Shape.StrokeDashOffsetProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}
