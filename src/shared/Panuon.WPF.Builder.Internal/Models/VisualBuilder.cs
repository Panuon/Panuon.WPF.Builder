using Panuon.WPF.Builder.Internal.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Panuon.WPF.Builder.Internal.Models
{
    internal class VisualBuilder
    {
        #region Fields
        private readonly Dictionary<DependencyProperty, object> _propertyValues =
            new Dictionary<DependencyProperty, object>();
        private FrameworkElement _visual;

        private readonly List<object> _children =
            new List<object>();

        private IModule _parentModule;
        #endregion

        #region Ctor

        public VisualBuilder(IModule module)
        {
            _parentModule = module;
        }
        #endregion

        #region Methods

        #region SetValue
        public void SetValue(DependencyProperty property,
            object value)
        {
            _propertyValues[property] = value;
            if (_visual != null)
            {
                SetVisualValue(_visual, property, value);
            }
        }
        #endregion

        #region AddChild
        public void AddChild(object child)
        {
            _children.Add(child);
            if (_visual != null
                && child is IModule module)
            {
                if (child is View view)
                {
                    view.AppBuilder = _parentModule.AppBuilder;
                }
                if (_visual is Panel panel)
                {
                    panel.Children.Add(module.Visual);
                }
                else if (_visual is Decorator decorator)
                {
                    decorator.Child = module.Visual;
                }
            }
        }

        public void InsertChild(object child,
            int index)
        {
            _children.Insert(index, child);
            if (_visual != null
                && child is IModule module)
            {
                if (child is View view)
                {
                    view.AppBuilder = _parentModule.AppBuilder;
                }
                if (_visual is Panel panel)
                {
                    panel.Children.Insert(index, module.Visual);
                }
                else if (_visual is Decorator decorator)
                {
                    decorator.Child = module.Visual;
                }
            }
        }

        public void RemoveChild(object child)
        {
            _children.Remove(child);
            if (_visual != null
                && child is IModule module)
            {
                if (_visual is Panel panel)
                {
                    panel.Children.Remove(module.Visual);
                }
                else if (_visual is Decorator decorator)
                {
                    decorator.Child = null;
                }
            }
        }

        public void RemoveChildAt(int index)
        {
            var child = _children[index];
            _children.Remove(child);
            if (_visual != null
                && child is IModule module)
            {
                if (_visual is Panel panel)
                {
                    panel.Children.Remove(module.Visual);
                }
                else if (_visual is Decorator decorator)
                {
                    decorator.Child = null;
                }
            }
        }
        #endregion

        #region SetVisual
        public void SetVisual(FrameworkElement visual)
        {
            _visual = visual;
            foreach (var propertyValue in _propertyValues)
            {
                SetVisualValue(visual, propertyValue.Key, propertyValue.Value);
            }
            foreach (var child in _children)
            {
                if (child is IModule module)
                {
                    if (visual is Panel panel)
                    {
                        panel.Children.Add(module.Visual);
                    }
                    else if (visual is Decorator decorator)
                    {
                        decorator.Child = module.Visual;
                    }
                }
            }
        }
        #endregion

        #region CreateFactory
        public FrameworkElementFactory CreateFactory()
        {
            var factory = new FrameworkElementFactory(_parentModule.VisualType);
            foreach (var propertyValue in _propertyValues)
            {
                SetFactoryValue(factory, propertyValue.Key, propertyValue.Value);
            }
            foreach (var child in _children)
            {
                if (child is IModule module)
                {
                    factory.AppendChild(module.VisualFactory);
                }
                else if (child is FrameworkElementFactory childFactory)
                {
                    factory.AppendChild(childFactory);
                }
            }
            return factory;
        }
        #endregion
        #endregion

        #region Functions
        private void SetVisualValue(FrameworkElement visual,
            DependencyProperty property,
            object value)
        {
            if (value is Binding bindingValue)
            {
                visual.SetBinding(property, bindingValue);
                return;
            }

            var finalValue = SerializeUtil.SerializeValue(_parentModule.AppBuilder, property.PropertyType, value);

            if (finalValue is Module module)
            {
                visual.SetValue(property, module.Visual);
            }
            else if (finalValue is BindingBase binding)
            {
                visual.SetBinding(property, binding);
            }
            else
            {
                visual.SetValue(property, finalValue);
            }
        }

        private void SetFactoryValue(FrameworkElementFactory factory,
            DependencyProperty property,
            object value)
        {
            if (value is Binding bindingValue)
            {
                factory.SetBinding(property, bindingValue);
                return;
            }

            var finalValue = SerializeUtil.SerializeValue(_parentModule.AppBuilder, property.PropertyType, value);

            if (finalValue is Module module)
            {
                factory.SetValue(property, module.VisualFactory);
            }
            else if (finalValue is BindingBase binding)
            {
                factory.SetBinding(property, binding);
            }
            else
            {
                factory.SetValue(property, finalValue);
            }
        }


        #endregion

    }
}