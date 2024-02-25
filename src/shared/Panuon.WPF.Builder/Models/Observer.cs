using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace Panuon.WPF.Builder
{
    public class Observer
    {

        #region Fields
        private bool _isObserveUpdating = false;

        private readonly List<ObserverReference> _references =
            new List<ObserverReference>();
        #endregion

        #region Ctor
        internal Observer()
        {

        }

        internal Observer(object value)
        {
            Value = value;
        }
        #endregion

        #region Properties
        public object Value
        {
            get
            {
                return ObserverObject.Value;
            }
            set
            {
                ObserverObject.Value = value;
            }
        }

        public virtual Type ValueType { get; } = typeof(object);

        public BindingMode BindingMode { get; internal set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; internal set; }


        internal ObserverObject ObserverObject
        {
            get
            {
                if (_observerObject == null)
                {
                    _observerObject = new ObserverObject();
                    _observerObject.PropertyChanged += ObserverObject_PropertyChanged;
                }
                return _observerObject;
            }
        }


        private ObserverObject _observerObject;
        #endregion

        #region Methods
        internal void BeginObserve()
        {
            _isObserveUpdating = true;
        }

        internal void EndObserve()
        {
            _isObserveUpdating = false;
        }

        internal void AddReference<TValue>(Observer source,
            Func<TValue> func,
            bool isBack)
        {
            var reference = new ObserverReference<TValue>(source, func, isBack);
            _references.Add(reference);
        }

        internal void AddReference<TValue>(Observer source,
            Func<Observer, TValue> func,
            bool isBack)
        {
            var reference = new ObserverReference<TValue>(source, func, isBack);
            _references.Add(reference);
        }

        internal void InvokeReferences()
        {
            foreach (var reference in _references)
            {
                reference.Invoke(this);
            }
        }

        internal void InvokeReferences(Observer observer)
        {
            foreach (var reference in _references.Where(x => x.Observer.TryGetTarget(out Observer target) && target == observer))
            {
                reference.Invoke(this);
            }
        }
        #endregion

        #region Event Handlers

        private void ObserverObject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_isObserveUpdating)
            {
                return;
            }

            if (e.PropertyName == nameof(ObserverObject.Value))
            {
                InvokeReferences();
            }
        }
        #endregion
    }

    public class Observer<T>
        : Observer
    {
        #region Ctor
        public Observer(T defaultValue)
            : base(defaultValue)
        {
        }

        public Observer(Func<T> onSourceUpdated,
            Observer source)
        {
            if (source == null)
            {
                throw new NullReferenceException("Parameter source can not be null.");
            }
            source.AddReference(this, onSourceUpdated, false);
            source.InvokeReferences(this);
        }

        public Observer(Func<T> onSourceUpdated,
            Func<object> onUpdateSource,
            Observer source)
            : this(onSourceUpdated, source)
        {
            AddReference(source, onUpdateSource, true);
        }


        public Observer(Func<Observer, T> onSourceUpdated,
            params Observer[] sources)
        {
            if (sources == null)
            {
                throw new NullReferenceException("Parameter sources can not be null.");
            }
            foreach (var source in sources)
            {
                source.AddReference(this, onSourceUpdated, false);
                source.InvokeReferences(this);
            }
        }

        public Observer(Func<Observer, T> onSourceUpdated,
            Func<Observer, object> onUpdateSource,
            params Observer[] sources)
            : this(onSourceUpdated, sources)
        {
            foreach (var source in sources)
            {
                AddReference(source, onUpdateSource, true);
            }
        }
        #endregion

        #region Properties

        public new T Value
        {
            get
            {
                return (T)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public override Type ValueType => typeof(T);
        #endregion

    }

    internal abstract class ObserverReference
    {
        public ObserverReference(Observer observer)
        {
            Observer = new WeakReference<Observer>(observer);
        }

        public WeakReference<Observer> Observer { get; }

        public abstract void Invoke(Observer source);
    }

    internal class ObserverObject
        : NotifyPropertyChangedBase
    {
        public object Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }
        private object _value;
    }

    internal class ObserverReference<T>
        : ObserverReference
    {
        private bool _isBack;

        public ObserverReference(Observer observer,
            Func<Observer, T> func,
            bool isBack)
            : base(observer)
        {
            _isBack = isBack;
            ObserverFunc = func;
        }

        public ObserverReference(Observer observer,
            Func<T> func,
            bool isBack)
            : base(observer)
        {
            _isBack = isBack;
            Func = func;
        }

        public Func<Observer, T> ObserverFunc { get; }

        public Func<T> Func { get; }


        public override void Invoke(Observer source)
        {
            if (Observer.TryGetTarget(out Observer observer))
            {
                if (_isBack)
                {
                    observer.BeginObserve();
                }
                try
                {
                    if (ObserverFunc != null)
                    {
                        observer.Value = ObserverFunc.Invoke(source);
                    }
                    else
                    {
                        observer.Value = Func.Invoke();
                    }
                }
                finally
                {
                    if (_isBack)
                    {
                        observer.EndObserve();
                    }
                }
            }
        }
    }
}