using System.Collections.Generic;

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
        internal PanelElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public IEnumerable<IModule> Children => _children;
        #endregion

        #region Methods
        public void AddChild(IModule module)
        {
            _visualBuilder.AddChild(module);
            _children.Add(module);
        }

        public void InsertChild(IModule module,
            int index)
        {
            _visualBuilder.AddChild(module);
            _children.Remove(module);
        }

        public void RemoveChild(IModule module)
        {
            _visualBuilder.RemoveChild(module);
            _children.Remove(module);
        }

        public void RemoveChildAt(int index)
        {
            _visualBuilder.RemoveChildAt(index);
            _children.RemoveAt(index);
        }
        #endregion

        #region Overrides
        public override void SetValue(string propertyNameOrKey, object value)
        {
            var s = this;
            switch (propertyNameOrKey)
            {
                case "children":
                    if (value is IEnumerable<IModule> children)
                    {
                        foreach (var child in children)
                        {
                            _visualBuilder.AddChild(child);
                        }
                    }
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}