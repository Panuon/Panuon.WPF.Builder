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

namespace Panuon.WPF.Builder.Internal.Utils
{
    internal  static class SerializeUtil
    {
        #region Fields
        private static TypeConverter _gridLengthConverter =
           TypeDescriptor.GetConverter(typeof(GridLength));

        private static IDictionary<string, Color> _colorNames;
        private static IDictionary<string, Brush> _brushNames;
        private static IDictionary<string, FontWeight> _fontWeightNames;
        private static IDictionary<string, FontStretch> _fontStretchNames;
        private static IDictionary<string, FontStyle> _fontStyleNames;
        private static IDictionary<Type, Tuple<Enum, Enum>> _boolDefaultValues = new Dictionary<Type, Tuple<Enum, Enum>>()
        {
            { typeof(Visibility), new Tuple<Enum, Enum>(Visibility.Visible, Visibility.Collapsed) },
            { typeof(TextWrapping), new Tuple<Enum, Enum>(TextWrapping.Wrap, TextWrapping.NoWrap) },
        };
        #endregion

        #region Ctor
        static SerializeUtil()
        {
            _colorNames = GetStaticValues<Color>(typeof(Colors));
            _brushNames = GetStaticValues<Brush>(typeof(Brushes));
            _fontWeightNames = GetStaticValues<FontWeight>(typeof(FontWeights));
            _fontStretchNames = GetStaticValues<FontStretch>(typeof(FontStretches));
            _fontStyleNames = GetStaticValues<FontStyle>(typeof(FontStyles));
        }

        #endregion

        #region Methods
        public static object SerializeValue(IAppBuilder appBuilder,
            Type type,
            object value)
        {
            object finalValue = null;

            if (type == typeof(string))
            {
                finalValue = GetStringValue(appBuilder, value);
            }
            else if (type.IsEnum)
            {
                finalValue = GetEnumValue(appBuilder, type, value);
            }
            else if (type == typeof(bool)
                || type == typeof(bool?))
            {
                finalValue = GetBoolValue(appBuilder, value);
            }
            else if (type == typeof(int)
                || type == typeof(int?))
            {
                finalValue = GetIntValue(appBuilder, value);
            }
            else if (type == typeof(double)
                || type == typeof(double?))
            {
                finalValue = GetDoubleValue(appBuilder, value);
            }
            else if (type == typeof(FontFamily))
            {
                finalValue = GetFontFamilyValue(appBuilder, value);
            }
            else if (type == typeof(Thickness)
                || type == typeof(Thickness?))
            {
                finalValue = GetThicknessValue(appBuilder, value);
            }
            else if (type == typeof(CornerRadius)
                || type == typeof(CornerRadius?))
            {
                finalValue = GetCornerRadiusValue(appBuilder, value);
            }
            else if (type == typeof(GridLength)
                || type == typeof(GridLength?))
            {
                finalValue = GetGridLengthValue(appBuilder, value);
            }
            else if (type == typeof(FontWeight)
                || type == typeof(FontWeight?))
            {
                finalValue = GetFontWeightValue(appBuilder, value);
            }
            else if (type == typeof(FontStyle)
                || type == typeof(FontStyle?))
            {
                finalValue = GetFontStyleValue(appBuilder, value);
            }
            else if (type == typeof(FontStretch)
                || type == typeof(FontStretch?))
            {
                finalValue = GetFontStretchValue(appBuilder, value);
            }
            else if (type == typeof(Color)
                || type == typeof(Color?))
            {
                finalValue = GetColorValue(appBuilder, value);
            }
            else if (type == typeof(SolidColorBrush))
            {
                finalValue = GetSolidColorBrushValue(appBuilder, value);
            }
            else if (type == typeof(Brush))
            {
                finalValue = GetBrushValue(appBuilder, value);
            }
            else if (type == typeof(Style))
            {
                finalValue = GetStyleValue(appBuilder, value);
            }
            else if (type == typeof(DataTemplate))
            {
                finalValue = GetDataTemplateValue(appBuilder, value);
            }
            else if (type == typeof(ItemsPanelTemplate))
            {
                finalValue = GetItemsPanelTemplateValue(appBuilder, value);
            }
            else if (type == typeof(Geometry))
            {
                finalValue = GetGeometryValue(appBuilder, value);
            }
            else if (type == typeof(object))
            {
                finalValue = GetObjectValue(appBuilder, value);
            }
            //IEnumeable<T>
            else if (GetIEnumerableType(type) is Type enumerableType)
            {
                finalValue = GetIEnumableValue(appBuilder, enumerableType, value);
            }
            else
            {
                return value;
            }

            return finalValue;
        }
        #endregion

        #region Functions

        #region GetValue
        private static object GetStringValue(IAppBuilder appBuilder, object value)
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

        private static object GetObjectValue(IAppBuilder appBuilder, object value)
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

        private static object GetIEnumableValue(IAppBuilder appBuilder, Type elementType,
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
                    list.Add(SerializeValue(appBuilder, elementType, obj));
                }
                return list;
            }
            return null;
        }


        private static object GetEnumValue(IAppBuilder appBuilder, 
            Type enumType,
            object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (_boolDefaultValues.ContainsKey(enumType)
                && (value is bool
                    || (value is Observer visibilityObv && visibilityObv.ValueType == typeof(bool))))
            {
                return GetBoolToEnumValue(enumType, value, _boolDefaultValues[enumType].Item1, _boolDefaultValues[enumType].Item2);
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
                return appBuilder.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{enumType}'.");
        }

        private static object GetBoolToEnumValue(Type enumType,
            object value,
            Enum trueValue,
            Enum falseValue)
        {
            if (value is bool)
            {
                return (bool)value ? trueValue : falseValue;
            }
            else
            {
                var observerValue = value as Observer;
                if (observerValue.BindingMode == BindingMode.TwoWay)
                {
                    return GetBinding(new Observer(enumType, () =>
                    {
                        return (bool)observerValue.Value ? trueValue : falseValue;
                    }, () =>
                    {
                        return observerValue.Value == trueValue;
                    }, observerValue)
                    {
                        BindingMode = observerValue.BindingMode,
                        UpdateSourceTrigger = observerValue.UpdateSourceTrigger,
                    });
                }
                else
                {
                    return GetBinding(new Observer(enumType, () =>
                    {
                        return (bool)observerValue.Value ? trueValue : falseValue;
                    }, observerValue)
                    {
                        BindingMode = observerValue.BindingMode,
                        UpdateSourceTrigger = observerValue.UpdateSourceTrigger,
                    });
                }
            }
        }

        private static object GetBoolValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(int)}'.");
        }

        private static object GetIntValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(int)}'.");
        }

        private static object GetDoubleValue(IAppBuilder appBuilder, object value)
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
            else if (value is string stringValue)
            {
                return appBuilder.FindResource(stringValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(double)}'.");
        }

        private static object GetThicknessValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(stringValue);
            }
            else if (double.TryParse(value.ToString(), out double dValue))
            {
                return new Thickness(dValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(Thickness)}'.");
        }

        private static object GetFontFamilyValue(IAppBuilder appBuilder, object value)
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
                string uriString = null;
                if (Uri.IsWellFormedUriString(stringValue, UriKind.Absolute))
                {
                    uriString = stringValue;
                }
                else if ((stringValue.StartsWith("/")
                        && stringValue.Contains(";component/"))
                    || (System.IO.Path.IsPathRooted(stringValue)
                        && stringValue.IndexOfAny(System.IO.Path.GetInvalidPathChars()) == -1))
                {
                    uriString = "pack://application:,,," + stringValue;
                }
                if (!string.IsNullOrEmpty(uriString))
                {
                    int lastIndex = uriString.LastIndexOf('/');
                    return new FontFamily(new Uri(uriString.Substring(0, lastIndex + 1)),
                        $"./{uriString.Substring(lastIndex + 1)}");
                }
                return appBuilder.FindResource(stringValue);
            }
            else if (double.TryParse(value.ToString(), out double dValue))
            {
                return new CornerRadius(dValue);
            }
            throw new Exception($"Can not convert '{value}' to type '{typeof(FontFamily)}'.");
        }

        private static object GetCornerRadiusValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(stringValue);
            }

            throw new Exception($"Can not convert '{value}' to type '{typeof(CornerRadius)}'.");
        }

        private static object GetGridLengthValue(IAppBuilder appBuilder, object value)
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

        private static object GetFontWeightValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(value);
            }

            throw new Exception($"Can not convert '{value}' to type '{typeof(FontWeight)}'.");
        }

        private static object GetFontStretchValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(value);
            }

            throw new Exception($"Can not convert '{value}' to type '{typeof(FontStretch)}'.");
        }

        private static object GetFontStyleValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(value);
            }

            throw new Exception($"Can not convert '{value}' to type '{typeof(FontStyle)}'.");
        }

        private static object GetColorValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(value);
            }
        }

        private static object GetBrushValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(value);
            }
        }

        private static object GetSolidColorBrushValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(value);
            }
        }

        private static object GetStyleValue(IAppBuilder appBuilder, object value)
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
                return appBuilder.FindResource(value);
            }
        }

        private static object GetDataTemplateValue(IAppBuilder appBuilder, object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is Module module)
            {
                return new DataTemplate()
                {
                    VisualTree = module.VisualFactory,
                };
            }
            else if (value is DataTemplate dataTemplate)
            {
                return dataTemplate;
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
                return appBuilder.FindResource(value) as DataTemplate;
            }
        }

        private static object GetGeometryValue(IAppBuilder appBuilder, object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is Geometry geometry)
            {
                return geometry;
            }
            else if (value is string stringValue)
            {
                try
                {
                    return Geometry.Parse(stringValue);
                }
                catch { }

                return appBuilder.FindResource(value);
            }
            else if (value is Observer observer)
            {
                return GetBinding(observer);
            }
            else
            {
                return appBuilder.FindResource(value);
            }
        }

        private static object GetItemsPanelTemplateValue(IAppBuilder appBuilder, object value)
        {
            if (value is null)
            {
                return null;
            }
            else if (value is Module module)
            {
                return new DataTemplate()
                {
                    VisualTree = module.VisualFactory,
                };
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

            throw new Exception($"Can not convert '{value}' to type '{typeof(Style)}'.");
        }


        #endregion

        private static IEnumerable<DependencyProperty> GetDependencyProperties(Type elementType)
        {
            return elementType.GetFields(BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)
                .Where(f => f.FieldType == typeof(DependencyProperty))
                .Select(f => (DependencyProperty)f.GetValue(elementType))
                .Where(dp => !dp.ReadOnly);
        }

        private static Type GetIEnumerableType(Type type)
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetGenericArguments()[0];
            }
            if (type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) is Type interfaceType)
            {
                return interfaceType.GetGenericArguments()[0];
            }
            return null;
        }

        private static Binding GetBinding(Observer observer)
        {
            return new Binding()
            {
                Path = new PropertyPath(nameof(ObserverObject.Value)),
                Source = observer.ObserverObject,
                Mode = observer.BindingMode,
                UpdateSourceTrigger = observer.UpdateSourceTrigger,
            };
        }


        private static IDictionary<string, T> GetStaticValues<T>(Type classType)
        {
            var dictionary = new Dictionary<string, T>();
            foreach (var propertyInfo in classType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                dictionary.Add(propertyInfo.Name.ToLower(), (T)propertyInfo.GetValue(null, null));
            }
            return dictionary;
        }


        #endregion
    }
}
