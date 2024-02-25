using System.Windows;

namespace Panuon.WPF.Builder
{
    public abstract class View
        : Module, IView
    {
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
            var window = Window.GetWindow(ActualVisual);
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
            return RootElement.ActualVisual;
        }
        #endregion
    }
}
