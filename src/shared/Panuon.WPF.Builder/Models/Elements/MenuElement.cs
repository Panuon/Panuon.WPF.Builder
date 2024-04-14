using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Panuon.WPF.Builder.Elements
{
    internal class MenuElement
        : ItemsControlElement, IMenuElement
    {
        #region Ctor
        internal MenuElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Menu);
        #endregion

        #region Overrids
        protected override void OnInitializing()
        {
            base.OnInitializing();

            var menu = Visual as Menu;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var menu = Visual as Menu;
        }
        #endregion

        #region Methods
        public IMenuElement OnItemClick(RoutedEventHandler handler)
        {
            //if (IsInitalized)
            //{
            //    var menu = Visual as Menu;
            //    menu.AddHandler(MenuItem.ClickEvent, handler);

            //}
            //else
            //{
                AddRoutedEventHandler(MenuItem.ClickEvent, handler);
            //}

            return (IMenuElement)this;
        }
        #endregion

        #region Event Handlers
        #endregion
    }
}
