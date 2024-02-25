using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class GridElement
        : PanelElement, IGridElement
    {
        #region Ctor
        internal GridElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Grid);

        #region Row
        public static int GetRow(IModule module)
        {
            return (int)module.GetValue(Grid.RowProperty);
        }

        public static void SetRow(IModule module, int value)
        {
            module.SetValue(Grid.RowProperty, value, true);
        }
        #endregion

        #region RowSpan
        public static int GetRowSpan(IModule module)
        {
            return (int)module.GetValue(Grid.RowSpanProperty);
        }

        public static void SetRowSpan(IModule module, int value)
        {
            module.SetValue(Grid.RowSpanProperty, value, true);
        }
        #endregion

        #region Column
        public static int GetColumn(IModule module)
        {
            return (int)module.GetValue(Grid.ColumnProperty);
        }

        public static void SetColumn(IModule module, int value)
        {
            module.SetValue(Grid.ColumnProperty, value, true);
        }
        #endregion

        #region ColumnSpan
        public static int GetColumnSpan(IModule module)
        {
            return (int)module.GetValue(Grid.ColumnSpanProperty);
        }

        public static void SetColumnSpan(IModule module, int value)
        {
            module.SetValue(Grid.ColumnSpanProperty, value, true);
        }
        #endregion

        #endregion

        #region Methods
        protected override FrameworkElement OnCreatingActualVisual(FrameworkElement element)
        { 
            return new Border()
            {
                Child = element,
            };
        }
        #endregion

        #region Overrides
        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            var grid = Visual as Grid;

            switch (propertyKey)
            {
                case "rows":
                    var rows = SerializeValue<IEnumerable<GridLength>>(value);
                    grid.RowDefinitions.Clear();
                    foreach (var row in rows)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = row });
                    }
                    return true;
                case "columns":
                    var columns = SerializeValue<IEnumerable<GridLength>>(value);
                    grid.ColumnDefinitions.Clear();
                    foreach (var column in columns)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = column });
                    }
                    return true;
                case "autoallocate":
                    var autoAllocate = SerializeValue<bool>(value);
                    if (autoAllocate
                        && Children != null
                        && ((grid.RowDefinitions.Any() && !grid.ColumnDefinitions.Any()) || (!grid.RowDefinitions.Any() && grid.ColumnDefinitions.Any())))
                    {
                        var index = 0;
                        foreach (Module child in Children)
                        {
                            if (grid.RowDefinitions.Any())
                            {
                                if (!child._presetPropertyValues.ContainsKey(Grid.RowProperty))
                                {
                                    child.SetValue(Grid.RowProperty, index);
                                }
                            }
                            else if (grid.ColumnDefinitions.Any())
                            {
                                if (!child._presetPropertyValues.ContainsKey(Grid.ColumnProperty))
                                {
                                    child.SetValue(Grid.ColumnProperty, index);
                                }
                            }
                            index++;
                        }
                    }
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion

        #region Functions
        #endregion
    }
}
