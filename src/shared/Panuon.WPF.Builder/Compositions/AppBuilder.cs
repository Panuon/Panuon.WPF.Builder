using Panuon.WPF.Builder.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Panuon.WPF.Builder
{
    public class AppBuilder
        : IAppBuilder
    {
        #region Fields
        private static Dictionary<object, Uri> _themeUris
            = new Dictionary<object, Uri>();
        #endregion

        #region Methods

        #region AddResourceDictionary
        public static void AddResourceDictionary(string uri)
        {
            AddResourceDictionary(new Uri(uri));
        }

        public static void AddResourceDictionary(Uri uri)
        {
            Application.Current.Resources.MergedDictionaries.Add(new SharedResourceDictionary()
            {
                Source = uri,
            });
        }

        public static void AddThemeResourceDictionary(object themeKey,
            string uri)
        {
            AddThemeResourceDictionary(themeKey,
                new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static void AddThemeResourceDictionary(object themeKey,
            Uri uri)
        {
            _themeUris[themeKey] = uri;
            if (CurrentTheme == null)
            {
                CurrentTheme = themeKey;
            }
        }

        public static object CurrentTheme
        {
            get
            {
                return _currentTheme;
            }
            set
            {
                SwitchTheme(value);
                _currentTheme = value;
            }
        }
        private static object _currentTheme;
        #endregion

        #region Observe
        public Observer<T> Observe<T>(T defaultValue,
            BindingMode mode,
            UpdateSourceTrigger trigger)
        {
            return new Observer<T>(defaultValue)
            {
                BindingMode = mode,
                UpdateSourceTrigger = trigger
            };
        }

        public Observer<T> Observe<T>(Func<Observer, T> onSourceUpdated,
            Observer source,
            BindingMode mode,
            UpdateSourceTrigger trigger)
        {
            return new Observer<T>(onSourceUpdated,
                source)
            {
                BindingMode = mode,
                UpdateSourceTrigger = trigger
            };
        }

        public Observer<T> Observe<T>(Func<Observer, T> onSourceUpdated,
            Func<Observer, object> onUpdateSource,
            Observer source,
            BindingMode mode,
            UpdateSourceTrigger trigger)
        {
            return new Observer<T>(onSourceUpdated,
                onUpdateSource,
                source)
            {
                BindingMode = mode,
                UpdateSourceTrigger = trigger
            };
        }

        public Observer<T> Observe<T>(Func<Observer, T> onSourceUpdated,
            Observer[] sources,
            BindingMode mode,
            UpdateSourceTrigger trigger)
        {
            return new Observer<T>(onSourceUpdated,
                sources)
            {
                BindingMode = mode,
                UpdateSourceTrigger = trigger
            };
        }

        public Observer<T> Observe<T>(Func<Observer, T> onSourceUpdated,
            Func<Observer, object> onUpdateSource,
            Observer[] sources,
            BindingMode mode,
            UpdateSourceTrigger trigger)
        {
            return new Observer<T>(onSourceUpdated,
                onUpdateSource,
                sources)
            {
                BindingMode = mode,
                UpdateSourceTrigger = trigger
            };
        }
        #endregion

        #region ObserveCollection
        public ObservableCollectionX<T> ObserveCollection<T>()
        {
            return new ObservableCollectionX<T>();
        }

        public ObservableCollectionX<T> ObserveCollection<T>(IEnumerable<T> items)
        {
            return new ObservableCollectionX<T>(items);
        }
        #endregion

        #region Binding
        public Binding CreateBinding(string path
            , string stringFormat = null
            , IValueConverter converter = null)
        {
            return new Binding()
            {
                Path = new PropertyPath(path),
                StringFormat = stringFormat,
                Converter = converter,
            };
        }

        public Binding CreateBinding(DependencyProperty property
            , object source
            , string stringFormat = null
            , IValueConverter converter = null
            , BindingMode mode = BindingMode.Default
            , UpdateSourceTrigger trigger = UpdateSourceTrigger.Default)
        {
            return new Binding()
            {
                Path = new PropertyPath(property),
                Source = source,
                Mode = mode,
                UpdateSourceTrigger = trigger,
                StringFormat = stringFormat,
                Converter = converter,
            };
        }

        public Binding CreateBinding(DependencyProperty property
            , RelativeSource relativeSource
            , string stringFormat = null
            , IValueConverter converter = null
            , BindingMode mode = BindingMode.Default
            , UpdateSourceTrigger trigger = UpdateSourceTrigger.Default)
        {
            return new Binding()
            {
                Path = new PropertyPath(property),
                RelativeSource = relativeSource,
                Mode = mode,
                UpdateSourceTrigger = trigger,
                StringFormat = stringFormat,
                Converter = converter,
            };
        }
        #endregion

        #region Show
        public void Show<TView>(TView view = default,
            object owner = null)
            where TView : IView
        {
            var targetView = view == null
                ? (TView)Activator.CreateInstance(typeof(TView))
                : view;
            if (targetView.RootElement is WindowElement windowElement)
            {
                windowElement.SetOwner(owner);
                windowElement.Show();
            }
            else
            {
                windowElement = new WindowElement(null, new Dictionary<string, object>()
                {
                    { "content", targetView },
                    { "owner", owner },
                });
                windowElement.Show();
            }
        }

        public bool? ShowDialog<TView>(TView view = default,
            object owner = null)
            where TView : IView
        {
            var targetView = view == null
                ? (TView)Activator.CreateInstance(typeof(TView))
                : view;
            if(targetView.RootElement is WindowElement windowElement)
            {
                windowElement.SetOwner(owner);
                return windowElement.ShowDialog();
            }
            else
            {
                windowElement = new WindowElement(null, new Dictionary<string, object>()
                {
                    { "content", targetView },
                    { "owner", owner },
                });
                return windowElement.ShowDialog();
            }
        }
        #endregion

        #endregion

        #region Functions
        private static void SwitchTheme(object themeKey)
        {
            var uri = _themeUris[themeKey];
            var dictionaries = Application.Current.Resources.MergedDictionaries.ToList();

            for (int i = dictionaries.Count - 1; i >= 0; i--)
            {
                var dictioanry = dictionaries[i];
                if (dictioanry.Source == uri)
                {
                    dictionaries.Remove(dictioanry);
                    continue;
                }
            }

            AddResourceDictionary(uri);
        }


        #endregion
    }
}