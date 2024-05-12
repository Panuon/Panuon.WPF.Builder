using System;
using System.Windows;
using System.Windows.Threading;

namespace Panuon.WPF.Builder
{
    public abstract class View
        : Module, IView
    {
        #region Ctor
        public View()
            : base(null)
        {

        }
        #endregion

        #region Properties
        public IElement RootElement 
        {
            get
            {
                if(_rootElement == null)
                {
                    _rootElement = OnCreate();
                    _rootElement.Initialize();
                    OnCreated();
                }
                return _rootElement;
            }
        }
        private IElement _rootElement;

        public Dispatcher UIDispatcher => Visual.Dispatcher;

        public override FrameworkElementFactory VisualFactory => RootElement.VisualFactory;

        public override Type VisualType => RootElement?.VisualType;
        #endregion

        #region Methods
        public void TryClose(bool? dialogResult = null)
        {
            try
            {
                Close(dialogResult);
            }
            catch { }
        }

        public void Close(bool? dialogResult = null)
        {
            var window = Window.GetWindow(Visual);
            if(dialogResult == null)
            {
                window.Close();
            }
            else
            {
                window.DialogResult = dialogResult;
            }
        }
       
        #endregion

        #region Abstract Methods
        protected abstract IElement OnCreate();

        protected virtual void OnCreated() { }

        protected override void OnInitializing()
        {
            base.OnInitializing();

            if (_rootElement == null)
            {
                _rootElement = OnCreate();
                OnCreated();
            }
        }
        #endregion

        #region Internal Methods
        protected override FrameworkElement OnCreatingVisual()
        {
            return RootElement.Visual;
        }
        #endregion
    }
}
