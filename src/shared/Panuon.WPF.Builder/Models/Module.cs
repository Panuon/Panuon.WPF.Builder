using Panuon.WPF.Builder.Internal.Models;
using Panuon.WPF.Builder.Internal.Utils;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace Panuon.WPF.Builder
{
    public abstract class Module
        : IModule
    {
        #region Fields
        internal VisualBuilder _visualBuilder;
        #endregion

        #region Ctor
        internal Module(IAppBuilder appBuilder)
        {
            _visualBuilder = new VisualBuilder(this);
            AppBuilder = appBuilder;
        }
        #endregion

        #region Properties

        #region Factory
        public virtual FrameworkElementFactory VisualFactory => _visualBuilder.CreateFactory();
        #endregion

        public abstract Type VisualType { get; }

        #region Visual
        public FrameworkElement Visual
        {
            get
            {
                if (_visual == null)
                {
                    Initialize();
                }
                return _visual;
            }
        }
        private FrameworkElement _visual;
        #endregion

        #region IsInitailized
        public bool IsInitalized { get; private set; }
        #endregion

        #region Factory
        public Window ParentWindow => Window.GetWindow(Visual);
        #endregion

        #region AppBuilder
        public IAppBuilder AppBuilder { get; internal set; }
        #endregion

        #endregion

        #region Methods

        #region GetValue
        public virtual object GetValue(string propertyName)
        {
            var elementType = Visual.GetType();
            var propertyInfo = elementType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                var dpName = GetDependencyPropertyName(elementType, propertyName);
                if (string.IsNullOrEmpty(dpName))
                {
                    throw new InvalidOperationException($"Can not find property named '{propertyName}' in type '{elementType}'.");
                }
                var dpDescriptor = DependencyPropertyDescriptor.FromName(dpName.Remove(dpName.Length - 8, 8), elementType, elementType);
                var property = dpDescriptor.DependencyProperty;
                return Visual.GetValue(property);
            }
            return propertyInfo.GetValue(Visual);
        }
        #endregion

        #region GetValue
        public virtual object GetValue(DependencyProperty property)
        {
            return Visual.GetValue(property);
        }
        #endregion

        #region SetValue
        public virtual void SetValue(string propertyNameOrKey,
          object value)
        {
            SetElementValue(propertyNameOrKey,
                value);
        }
        #endregion

        #region SetValue
        public virtual void SetValue(DependencyProperty property,
            object value)
        {
            SetElementValue(property,
                value);
        }
        #endregion

        #endregion

        #region Protected Methods
        protected abstract FrameworkElement OnCreatingVisual();

        internal void InternalInitialize()
        {
            OnInitializing();
        }

        internal void InternalInitialized()
        {
            OnInitialized();
        }

        protected virtual void OnInitializing() 
        { }

        protected virtual void OnInitialized() { }

        protected T SerializeValue<T>(object value)
        {
            return (T)SerializeUtil.SerializeValue(AppBuilder, typeof(T), value);
        }
        #endregion

        #region Internal Properties

        #region SetElementValue
        internal void SetElementValue(DependencyProperty property,
            object value)
        {
            if (value is Binding bindingValue)
            {
                _visualBuilder.SetValue(property, bindingValue);
            }
            else
            {
                var finalValue = SerializeUtil.SerializeValue(AppBuilder, property.PropertyType, value);
                _visualBuilder.SetValue(property, finalValue);
            }
        }

        internal void SetElementValue(string propertyNameOrKey,
            object value)
        {
            propertyNameOrKey = propertyNameOrKey.ToLower();

            var elementType = VisualType;
            var dpName = GetDependencyPropertyName(elementType, propertyNameOrKey);
            if (string.IsNullOrEmpty(dpName))
            {
                throw new InvalidOperationException($"Can not find property named '{propertyNameOrKey}' in type '{elementType}'.");
            }
            var dpDescriptor = DependencyPropertyDescriptor.FromName(dpName.Remove(dpName.Length - 8, 8), elementType, elementType);
            var property = dpDescriptor.DependencyProperty;

            SetElementValue(property,
                value);
        }
        #endregion

        #region Initialize
        public void Initialize()
        {
            if (IsInitalized)
            {
                return;
            }

            _visual = OnCreatingVisual();
            _visualBuilder.SetVisual(_visual);

            OnInitializing();

            IsInitalized = true;
        }
        #endregion

        #endregion

        #region Functions
       
        private string GetDependencyPropertyName(Type elementType, string name)
        {
            if(!name.EndsWith("property"))
            {
                name += "property";
            }
            name = name.ToLower();

            return elementType.GetFields(BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)
                .Where(f => f.FieldType == typeof(DependencyProperty))
                .Select(f => f.Name)
                .FirstOrDefault(n => n.ToLower() == name);
        }

        #endregion

    }

}
