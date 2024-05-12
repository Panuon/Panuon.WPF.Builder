using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class GridElement
        : PanelElement, IGridElement
    {
        #region Ctor
        internal GridElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Grid);

        #region Row
        public static object GetRow(IModule module)
        {
            return (object)module.GetValue(Grid.RowProperty);
        }

        public static void SetRow(IModule module, object value)
        {
            module.SetValue(Grid.RowProperty, value);
        }
        #endregion

        #region RowSpan
        public static object GetRowSpan(IModule module)
        {
            return (object)module.GetValue(Grid.RowSpanProperty);
        }

        public static void SetRowSpan(IModule module, object value)
        {
            module.SetValue(Grid.RowSpanProperty, value);
        }
        #endregion

        #region Column
        public static object GetColumn(IModule module)
        {
            return (object)module.GetValue(Grid.ColumnProperty);
        }

        public static void SetColumn(IModule module, object value)
        {
            module.SetValue(Grid.ColumnProperty, value);
        }
        #endregion

        #region ColumnSpan
        public static object GetColumnSpan(IModule module)
        {
            return (object)module.GetValue(Grid.ColumnSpanProperty);
        }

        public static void SetColumnSpan(IModule module, object value)
        {
            module.SetValue(Grid.ColumnSpanProperty, value);
        }
        #endregion

        #endregion

        #region Methods
        #endregion

        #region Overrides
        public override void SetValue(string propertyNameOrKey, object value)
        {
            var grid = Visual as Grid;
            switch (propertyNameOrKey)
            {
                case "rows":
                    var rows = SerializeValue<IEnumerable<GridLength>>(value);
                    foreach (var row in rows)
                    {
                        var rowDefinition = new FrameworkElementFactory(typeof(RowDefinition));
                        rowDefinition.SetValue(RowDefinition.HeightProperty, row);
                        _visualBuilder.AddChild(rowDefinition);
                    }

                    grid.RowDefinitions.Clear();
                    foreach (var row in rows)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = row });
                    }
                    break;
                case "columns":
                    var columns = SerializeValue<IEnumerable<GridLength>>(value);
                    foreach (var column in columns)
                    {
                        var columnDefinition = new FrameworkElementFactory(typeof(ColumnDefinition));
                        columnDefinition.SetValue(ColumnDefinition.WidthProperty, column);
                        _visualBuilder.AddChild(columnDefinition);
                    }

                    grid.ColumnDefinitions.Clear();
                    foreach (var column in columns)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = column });
                    }
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion

        #region Functions
        #endregion
    }
}
