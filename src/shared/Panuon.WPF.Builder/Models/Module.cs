using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Panuon.WPF.Builder
{
    public abstract class Module
        : IModule
    {
        #region Fields
        internal readonly Dictionary<object, PresetPropertyValue> _presetPropertyValues =
            new Dictionary<object, PresetPropertyValue>();

        private static TypeConverter _gridLengthConverter = 
            TypeDescriptor.GetConverter(typeof(GridLength));

        private static IDictionary<string, Color> _colorNames;
        private static IDictionary<string, Brush> _brushNames;
        private static IDictionary<string, FontWeight> _fontWeightNames;
        private static IDictionary<string, FontStretch> _fontStretchNames;
        private static IDictionary<string, FontStyle> _fontStyleNames;
        #endregion

        #region Ctor
        static Module()
        {
            _colorNames = GetStaticValues<Color>(typeof(Colors));
            _brushNames = GetStaticValues<Brush>(typeof(Brushes));
            _fontWeightNames = GetStaticValues<FontWeight>(typeof(FontWeights));
            _fontStretchNames = GetStaticValues<FontStretch>(typeof(FontStretches));
            _fontStyleNames = GetStaticValues<FontStyle>(typeof(FontStyles));
        }
        #endregion

        #region Properties
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

        public bool IsInitalized { get; private set; }

        public FrameworkElement ActualVisual
        {
            get
            {
                var sourceElement = Visual;
                return _rootVisual ?? sourceElement;
            }
        }
        private FrameworkElement _rootVisual;
        #endregion

        #region Methods

        #region GetValue
        public object GetValue(string propertyName)
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
        public object GetValue(DependencyProperty property)
        {
            if(_visual != null)
            {
                return _visual.GetValue(property);
            }
            return null;
        }
        #endregion

        #region SetValue
        public void SetValue(string propertyNameOrKey,
          object value,
          bool updateActualVisual = false)
        {
            if (value is Observer)
            {
                throw new InvalidOperationException($"This method does not support an 'Observer' value as parameter. Please call the 'SetValue(DependencyProperty property, object value)' method instead.");
            }
            if (IsInitalized)
            {
                SetElementValue(updateActualVisual ? ActualVisual : Visual,
                    propertyNameOrKey,
                    value);
            }
            else
            {
                _presetPropertyValues[propertyNameOrKey] = new PresetPropertyValue()
                {
                    Value = value,
                    IsActualVisual = updateActualVisual
                };
            }
        }
        #endregion

        #region SetValue
        public void SetValue(DependencyProperty property,
            object value,
            bool updateActualVisual = false)
        {
            if (IsInitalized)
            {
                SetElementValue(updateActualVisual ? ActualVisual : Visual,
                    property,
                    value);
            }
            else
            {
                _presetPropertyValues[property] = new PresetPropertyValue()
                {
                    Value = value,
                    IsActualVisual = updateActualVisual
                };
            }
        }
        #endregion

        #endregion

        #region Internal Methods
        internal FrameworkElement InternalCreatingElement()
        {
            return OnCreatingVisual();
        }
        #endregion

        #region Protected Methods
        protected abstract FrameworkElement OnCreatingVisual();

        protected virtual FrameworkElement OnCreatingActualVisual(FrameworkElement visual)
        {
            return visual;
        }

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
            return (T)SerializeValue(typeof(T), value);
        }
          
        protected object SerializeValue(Type type,
            object value)
        {
            object finalValue = null;

            if (type == typeof(string))
            {
                finalValue = GetStringValue(value);
            }
            else if (type.IsEnum)
            {
                finalValue = GetEnumValue(type, value);
            }
            else if (type == typeof(bool)
                || type == typeof(bool?))
            {
                finalValue = GetBoolValue(value);
            }
            else if (type == typeof(int)
                || type == typeof(int?))
            {
                finalValue = GetIntValue(value);
            }
            else if (type == typeof(double)
                || type == typeof(double?))
            {
                finalValue = GetDoubleValue(value);
            }
            else if (type == typeof(FontFamily))
            {
                finalValue = GetFontFamilyValue(value);
            }
            else if (type == typeof(Thickness)
                || type == typeof(Thickness?))
            {
                finalValue = GetThicknessValue(value);
            }
            else if (type == typeof(CornerRadius)
                || type == typeof(CornerRadius?))
            {
                finalValue = GetCornerRadiusValue(value);
            }
            else if(type == typeof(GridLength)
                || type == typeof(GridLength?))
            {
                finalValue = GetGridLengthValue(value);
            }
            else if (type == typeof(FontWeight)
                || type == typeof(FontWeight?))
            {
                finalValue = GetFontWeightValue(value);
            }
            else if (type == typeof(FontStyle)
                || type == typeof(FontStyle?))
            {
                finalValue = GetFontStyleValue(value);
            }
            else if (type == typeof(FontStretch)
                || type == typeof(FontStretch?))
            {
                finalValue = GetFontStretchValue(value);
            }
            else if (type == typeof(Color)
                || type == typeof(Color?))
            {
                finalValue = GetColorValue(value);
            }
            else if (type == typeof(SolidColorBrush))
            {
                finalValue = GetSolidColorBrushValue(value);
            }
            else if (type == typeof(Brush))
            {
                finalValue = GetBrushValue(value);
            }
            else if (type == typeof(Style))
            {
                finalValue = GetStyleValue(value);
            }
            else if (type == typeof(DataTemplate))
            {
                finalValue = GetDataTemplateValue(value);
            }
            else if(type == typeof(ItemsPanelTemplate))
            {
                finalValue = GetItemsPanelTemplateValue(value);
            }
            else if (type == typeof(object))
            {
                finalValue = GetObjectValue(value);
            }
            //IEnumeable<T>
            else if (GetIEnumerableType(type) is Type enumerableType)
            {
                finalValue = GetIEnumableValue(enumerableType, value);
            }
            else
            {
                return value;
            }

            return finalValue;
        }

        protected virtual bool SetPropertyValue(string propertyKey,
            object value)
        {
            return false; 
        }
        #endregion

        #region Internal Properties

        private void SetElementValue(FrameworkElement element,
            DependencyProperty property,
            object value)
        {
            if (value is Binding bindingValue)
            {
                element.SetBinding(property, bindingValue);
                return;
            }

            var finalValue = SerializeValue(property.PropertyType, value);

            if (finalValue is BindingBase binding)
            {
                element.SetBinding(property, binding);
            }
            else
            {
                element.SetValue(property, finalValue);
            }
        }

        private void SetElementValue(FrameworkElement element,
            string propertyNameOrKey,
            object value)
        {
            if(SetPropertyValue(propertyNameOrKey, value))
            {
                return;
            }

            var elementType = element.GetType();
            var propertyInfo = elementType.GetProperty(propertyNameOrKey, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                var dpName = GetDependencyPropertyName(elementType, propertyNameOrKey);
                if (string.IsNullOrEmpty(dpName))
                {
                    throw new InvalidOperationException($"Can not find property named '{propertyNameOrKey}' in type '{elementType}'.");
                }
                var dpDescriptor = DependencyPropertyDescriptor.FromName(dpName.Remove(dpName.Length - 8, 8), elementType, elementType);
                var property = dpDescriptor.DependencyProperty;
                SetElementValue(element, property, value);
                return;
            }
            var finalValue = SerializeValue(propertyInfo.PropertyType, value);
            propertyInfo.SetValue(element, finalValue);
        }

        public void Initialize()
        {
            if (IsInitalized)
            {
                return;
            }

            _visual = OnCreatingVisual();
            var container = OnCreatingActualVisual(_visual);
            if(container == null)
            {
                throw new Exception("ActualVisual can not be null.");
            }
            if (container != _visual)
            {
                _rootVisual = container;
            }

            OnInitializing();

            foreach (var propertyValue in _presetPropertyValues)
            {
                var updateActualVisual = propertyValue.Value.IsActualVisual;
                var value = propertyValue.Value.Value;

                if (propertyValue.Key is DependencyProperty property)
                {
                    SetElementValue(updateActualVisual ? ActualVisual : Visual,
                        property,
                        value);
                }
                else if (propertyValue.Key is string propertyName)
                {
                    SetElementValue(updateActualVisual ? ActualVisual : Visual, 
                        propertyName,
                        value);
                }
            }

            IsInitalized = true;
        }
        #endregion

        #region GetValue
        private object GetStringValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is string stringValue)
            {
                return stringValue;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                return value.ToString();
            }
        }

        private object GetObjectValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                return value;
            }
        }

        private object GetIEnumableValue(Type elementType,
            object value)
        {
            if (value != null)
            {
                var valueElementType = GetIEnumerableType(value.GetType());
                if (valueElementType == elementType)
                {
                    return value;
                }

                var listType = typeof(List<>).MakeGenericType(elementType);
                var list = (IList)Activator.CreateInstance(listType);
                foreach (var obj in (IEnumerable)value)
                {
                    list.Add(SerializeValue(elementType, obj));
                }
                return list;
            }
            return null;
        }


        private object GetEnumValue(Type enumType,
            object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (enumType == typeof(Visibility)
                && (value is bool
                    || (value is Observer obv && obv.ValueType == typeof(bool))))
            {
                if (value is bool)
                {
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    var observerValue = value as Observer;
                    if (observerValue.BindingMode == BindingMode.TwoWay)
                    {
                        return GetBinding(new Observer<Visibility>(() =>
                        {
                            return (bool)observerValue.Value ? Visibility.Visible : Visibility.Collapsed;
                        }, () =>
                        {
                            return (Visibility)observerValue.Value == Visibility.Visible;
                        }, observerValue)
                        {
                            BindingMode = observerValue.BindingMode,
                            UpdateSourceTrigger = observerValue.UpdateSourceTrigger,
                        });
                    }
                    else
                    {
                        return GetBinding(new Observer<Visibility>(() =>
                        {
                            return (bool)observerValue.Value ? Visibility.Visible : Visibility.Collapsed;
                        }, observerValue)
                        {
                            BindingMode = observerValue.BindingMode,
                            UpdateSourceTrigger = observerValue.UpdateSourceTrigger,
                        });
                    }
                }
            }
            else if (enumType.IsInstanceOfType(value))
            {
                return value;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else if (value is string stringValue)
            {
                try
                {
                    return Enum.Parse(enumType, stringValue, true);
                }
                catch { }
                return Visual.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{enumType}'.");
        }
            private object GetBoolValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is bool boolValue)
            {
                return boolValue;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else if (bool.TryParse(value.ToString(), out bool bValue))
            {
                return (bool)bValue;
            }
            else if (value is string stringValue)
            {
                return Visual.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(int)}'.");
        }

        private object GetIntValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is int intValue)
            {
                return intValue;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else if (double.TryParse(value.ToString(), out double dValue))
            {
                return (int)dValue;
            }
            else if (value is string stringValue)
            {
                return Visual.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(int)}'.");
        }

        private object GetDoubleValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is double doubleValue)
            {
                return doubleValue;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else if (double.TryParse(value.ToString(), out double dValue))
            {
                return dValue;
            }
            else if(value is string stringValue)
            {
                return Visual.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(double)}'.");
        }

        private object GetThicknessValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is int intValue)
            {
                return new Thickness(intValue);
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else if (value is Thickness thickness)
            {
                return thickness;
            }
            else if (value is string stringValue)
            {
                double[] doubleSplits = null;
                if (stringValue.Contains(" "))
                {
                    var textSplits = stringValue.Split(' ');
                    doubleSplits = textSplits.Select(t => double.Parse(t))
                        .ToArray();
                }
                else if (stringValue.Contains(","))
                {
                    var textSplits = stringValue.Split(',');
                    doubleSplits = textSplits.Select(t => double.Parse(t))
                        .ToArray();
                }
                if (doubleSplits != null)
                {
                    if (doubleSplits.Length == 2)
                    {
                        return new Thickness(doubleSplits[0], doubleSplits[1], doubleSplits[0], doubleSplits[1]);
                    }
                    else if (doubleSplits.Length == 4)
                    {
                        return new Thickness(doubleSplits[0], doubleSplits[1], doubleSplits[2], doubleSplits[3]);
                    }
                }
                return Visual.FindResource(stringValue);
            }
            else if (double.TryParse(value.ToString(), out double dValue))
            {
                return new Thickness(dValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(Thickness)}'.");
        }

        private object GetFontFamilyValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is FontFamily fontValue)
            {
                return fontValue;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else if (value is string stringValue)
            {
                if (Uri.IsWellFormedUriString(stringValue, UriKind.Absolute))
                {
                    return Fonts.GetFontFamilies(new Uri(stringValue, UriKind.RelativeOrAbsolute)).FirstOrDefault();
                }
                else if (stringValue.StartsWith("/")
                    && stringValue.Contains(";component/"))
                {
                    return Fonts.GetFontFamilies(new Uri("pack://application:,,," + stringValue, UriKind.RelativeOrAbsolute)).FirstOrDefault();
                }
                else if (System.IO.Path.IsPathRooted(stringValue)
                    && stringValue.IndexOfAny(System.IO.Path.GetInvalidPathChars()) == -1)
                {
                    return Fonts.GetFontFamilies(new Uri(stringValue, UriKind.RelativeOrAbsolute)).FirstOrDefault();
                }
                return Visual.FindResource(stringValue);
            }
            else if (double.TryParse(value.ToString(), out double dValue))
            {
                return new CornerRadius(dValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(FontFamily)}'.");
        }

        private object GetCornerRadiusValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is int intValue)
            {
                return intValue;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else if (value is string stringValue)
            {
                double[] doubleSplits = null;
                if (stringValue.Contains(" "))
                {
                    var textSplits = stringValue.Split(' ');
                    doubleSplits = textSplits.Select(t => double.Parse(t))
                        .ToArray();
                }
                else if (stringValue.Contains(","))
                {
                    var textSplits = stringValue.Split(',');
                    doubleSplits = textSplits.Select(t => double.Parse(t))
                        .ToArray();
                }
                else if (double.TryParse(value.ToString(), out double dValue))
                {
                    return new CornerRadius(dValue);
                }
                if (doubleSplits != null)
                {
                    if (doubleSplits.Length == 2)
                    {
                        return new CornerRadius(doubleSplits[0], doubleSplits[1], doubleSplits[0], doubleSplits[1]);
                    }
                    else if (doubleSplits.Length == 4)
                    {
                        return new CornerRadius(doubleSplits[0], doubleSplits[1], doubleSplits[2], doubleSplits[3]);
                    }
                }
                return Visual.FindResource(stringValue);
            }
          
            throw new Exception($"Can not convert '{value}' to type '{typeof(CornerRadius)}'.");
        }

        private object GetGridLengthValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            try
            {
                return (GridLength)_gridLengthConverter.ConvertFromString(value.ToString());
            }
            catch { }

            throw new Exception($"Can not convert '{value}' to type '{typeof(GridLength)}'.");
        }

        private object GetFontWeightValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is FontWeight fontWeight)
            {
                return fontWeight;
            }
            else if (value is int intValue)
            {
                return FontWeight.FromOpenTypeWeight(intValue);
            }
            else if (value is string stringValue)
            {
                if (_fontWeightNames.ContainsKey(stringValue))
                {
                    return _fontWeightNames[stringValue];
                }
                return Visual.FindResource(value);
            }

            throw new Exception($"Can not convert '{value}' to type '{typeof(FontWeight)}'.");
        }

        private object GetFontStretchValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is FontStretch fontStretch)
            {
                return fontStretch;
            }
            else if (value is int intValue)
            {
                return FontStretch.FromOpenTypeStretch(intValue);
            }
            else if (value is string stringValue)
            {
                if (_fontStretchNames.ContainsKey(stringValue))
                {
                    return _fontStretchNames[stringValue];
                }
                return Visual.FindResource(value);
            }

            throw new Exception($"Can not convert '{value}' to type '{typeof(FontStretch)}'.");
        }

        private object GetFontStyleValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is FontStyle fontStyle)
            {
                return fontStyle;
            }
            else if (value is string stringValue)
            {
                if (_fontStyleNames.ContainsKey(stringValue))
                {
                    return _fontStyleNames[stringValue];
                }
                return Visual.FindResource(value);
            }

            throw new Exception($"Can not convert '{value}' to type '{typeof(FontStyle)}'.");
        }

        private object GetColorValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is string hexText
                && hexText.StartsWith("#"))
            {
                return (Color)ColorConverter.ConvertFromString(hexText);
            }
            else if (value is string colorText
                && _colorNames.ContainsKey(colorText.ToLower()))
            {
                return _colorNames[colorText];
            }
            else if (value is Brush brush)
            {
                return brush;
            }
            else if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                return Visual.FindResource(value);
            }
        }

        private object GetBrushValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is string hexText
                && hexText.StartsWith("#"))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexText));
            }
            else if (value is string brushText
                && _brushNames.ContainsKey(brushText.ToLower()))
            {
                return _brushNames[brushText];
            }
            else if (value is Brush brush)
            {
                return brush;
            }
            else if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                return Visual.FindResource(value);
            }
        }

        private object GetSolidColorBrushValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is string text
                && text.StartsWith("#"))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(text));
            }
            else if (value is string brushText
                && _brushNames.ContainsKey(brushText.ToLower()))
            {
                return _brushNames[brushText];
            }
            else if (value is SolidColorBrush brush)
            {
                return brush;
            }
            else if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                return Visual.FindResource(value);
            }
        }

        private object GetStyleValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is Style style)
            {
                return style;
            }
            else if (value is ControlTemplate template)
            {
                style = new Style();
                style.Setters.Add(new Setter()
                {
                    Property = FrameworkElement.StyleProperty,
                    Value = template,
                });
                return style;
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                return Visual.FindResource(value);
            }
        }

        private object GetDataTemplateValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is DataTemplate dataTemplate)
            {
                return dataTemplate;
            }
            else if(value is FrameworkElementFactory factory)
            {
                return new DataTemplate()
                {
                    VisualTree = factory,
                };
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                if(value is IModule module)
                {
                    value = module.ActualVisual;
                }
                var generator = new FrameworkElementFactory(typeof(TemplateGeneratorControl));
                generator.SetValue(TemplateGeneratorControl.FactoryProperty, new Func<object>(() =>
                {
                    return value;
                }));
                return new DataTemplate()
                {
                    VisualTree = generator,
                };
            }


            throw new Exception($"Can not convert '{value}' to type '{typeof(Style)}'.");
        }

        private object GetItemsPanelTemplateValue(object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is ItemsPanelTemplate panelTemplate)
            {
                return panelTemplate;
            }
            else if (value is FrameworkElementFactory factory)
            {
                return new DataTemplate()
                {
                    VisualTree = factory,
                };
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                if (value is IModule module)
                {
                    value = module.ActualVisual;
                }
                if (value is Panel panel)
                {
                    var panelType = panel.GetType();
                    var generator = new FrameworkElementFactory(panelType);
                    var properties = GetDependencyProperties(panelType);
                    foreach (var property in properties)
                    {
                        var valueSource = DependencyPropertyHelper.GetValueSource(panel, property);
                        if (valueSource.BaseValueSource != BaseValueSource.Default)
                        {
                            if (panel.GetBindingExpression(property) is BindingExpression expression)
                            {
                                generator.SetBinding(property, expression.ParentBinding);
                            }
                            else
                            {
                                var propertyValue = panel.GetValue(property);
                                generator.SetValue(property, propertyValue);
                            }
                        }
                    }
                    return new ItemsPanelTemplate()
                    {
                        VisualTree = generator,
                    };
                }
            }


            throw new Exception($"Can not convert '{value}' to type '{typeof(Style)}'.");
        }

        #endregion

        #region Functions
        private static IDictionary<string, T> GetStaticValues<T>(Type classType)
        {
            var dictionary = new Dictionary<string, T>();
            foreach (var propertyInfo in classType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                dictionary.Add(propertyInfo.Name.ToLower(), (T)propertyInfo.GetValue(null, null));
            }
            return dictionary;
        }

        private IEnumerable<DependencyProperty> GetDependencyProperties(Type elementType)
        {
            return elementType.GetFields(BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)
                .Where(f => f.FieldType == typeof(DependencyProperty))
                .Select(f => (DependencyProperty)f.GetValue(elementType))
                .Where(dp => !dp.ReadOnly);
        }

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

        private Type GetIEnumerableType(Type type)
        {
            if(type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetGenericArguments()[0];
            }
            if(type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) is Type interfaceType)
            {
                return interfaceType.GetGenericArguments()[0];
            }
            return null;
        }

        private Binding GetBinding(Observer observer)
        {
            return new Binding()
            {
                Path = new PropertyPath(nameof(ObserverObject.Value)),
                Source = observer.ObserverObject,
                Mode = observer.BindingMode,
                UpdateSourceTrigger = observer.UpdateSourceTrigger,
            };
        }
        #endregion
    }

    internal class PresetPropertyValue
    {
        public object Value { get; set; }

        public bool IsActualVisual { get; set; }
    }
}
