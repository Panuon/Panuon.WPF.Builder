using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal abstract class PanelElement
        : Element, IPanelElement
    {
        #region Fields
        private readonly List<IModule> _children = 
            new List<IModule>();
        #endregion

        #region Ctor
        internal PanelElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public IEnumerable<IModule> Children => _children;
        #endregion

        #region Methods
        public void AddChild(IModule module)
        {
            _children.Add(module);
            if (IsInitalized)
            {
                (Visual as Panel).Children.Add(module.Visual);
            }
        }

        public void InsertChild(IModule module,
            int index)
        {
            _children.Remove(module);
            if (IsInitalized)
            {
                (Visual as Panel).Children.Insert(index, module.Visual);
            }
        }

        public void RemoveChild(IModule module)
        {
            _children.Remove(module);
            if (IsInitalized)
            {
                (Visual as Panel).Children.Remove(module.Visual);
            }
        }

        public void RemoveChildAt(int index)
        {
            _children.RemoveAt(index);
            if (IsInitalized)
            {
                (Visual as Panel).Children.RemoveAt(index);
            }
        }
        #endregion

        #region Overrides
        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "children":
                    if (value is IEnumerable<IModule> children)
                    {
                        var panel = Visual as Panel;
                        foreach (var child in children)
                        {
                            _children.Add(child);
                            panel.Children.Add(child.ActualVisual);
                        }
                    }
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}