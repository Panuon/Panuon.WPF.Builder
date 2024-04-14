using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Panuon.WPF.Builder
{
    public interface IAppBuilder
    {
        #region Observe
        Observer<T> Observe<T>(T defaultValue,
            BindingMode mode = BindingMode.Default,
            UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged);

        Observer<T> Observe<T>(Func<Observer, T> onSourceUpdate,
            Observer source,
            BindingMode mode = BindingMode.Default,
            UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged);

        Observer<T> Observe<T>(Func<Observer, T> onSourceUpdate,
            Func<Observer, object> onUpdateSource,
            Observer source,
            BindingMode mode = BindingMode.Default,
            UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged);

        Observer<T> Observe<T>(Func<Observer, T> onSourceUpdate,
            Observer[] sources,
            BindingMode mode = BindingMode.Default,
            UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged);

        Observer<T> Observe<T>(Func<Observer, T> onSourceUpdate,
            Func<Observer, object> onUpdateSource,
            Observer[] sources,
            BindingMode mode = BindingMode.Default,
            UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged);
        #endregion

        #region ObservableCollectionX
        ObservableCollectionX<T> ObserveCollection<T>();

        ObservableCollectionX<T> ObserveCollection<T>(IEnumerable<T> items);
        #endregion

        #region CreateBinding
        Binding CreateBinding(string path
            , string stringFormat = null
            , IValueConverter converter = null);

        Binding CreateBinding(DependencyProperty property
            , object source
            , string stringFormat = null
            , IValueConverter converter = null
            , BindingMode mode = BindingMode.Default
            , UpdateSourceTrigger trigger = UpdateSourceTrigger.Default);

        Binding CreateBinding(DependencyProperty property
            , RelativeSource relativeSource
            , string stringFormat = null
            , IValueConverter converter = null
            , BindingMode mode = BindingMode.Default
            , UpdateSourceTrigger trigger = UpdateSourceTrigger.Default);
        #endregion

        #region Show Window
        void Show<TView>(TView view = default,
            object owner = null,
            Type type = null,
            object title = null,
            object icon = null,
            object style = null,
            object width = null,
            object height = null,
            object location = null)
            where TView : IView;

        bool? ShowDialog<TView>(TView view = default,
            object owner = null,
            Type type = null,
            object title = null,
            object icon = null,
            object style = null,
            object width = null,
            object height = null,
            object location = null)
            where TView : IView;
        #endregion
    }
}
