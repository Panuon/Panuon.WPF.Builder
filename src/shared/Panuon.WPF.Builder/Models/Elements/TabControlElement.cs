using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    internal class TabControlElement
        : SelectorElement, ITabControlElement
    {
        #region Fields
        private readonly List<TabControlElementItem> _items =
            new List<TabControlElementItem>();
        #endregion

        #region Ctor
        internal TabControlElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }

        #endregion

        #region Properties
        public override Type VisualType => typeof(TabControl);
        #endregion

        #region Methods
        public ITabControlElement AddItem(object header,
            object content)
        {
            content = content is IModule module
                    ? module.Visual
                    : content;
            if (IsInitalized)
            {
                var tabControl = Visual as TabControl;
                tabControl.Items.Add(new TabItem()
                {
                    Header = header,
                    Content = content,
                });
            }
            else
            {
                _items.Add(new TabControlElementItem()
                {
                    Header = header,
                    Content = content,
                });
            }
            return this;
        }

        protected override void OnInitializing()
        {
            base.OnInitializing();

            var tabControl = Visual as TabControl;

            foreach (var item in _items)
            {
                tabControl.Items.Add(new TabItem()
                {
                    Header = item.Header,
                    Content = item.Content,
                });
            }
        }
        #endregion
    }

    internal class TabControlElementItem
    {
        public object Header { get; set; }

        public object Content { get; set; }
    }
}
