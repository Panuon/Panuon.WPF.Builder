using Panuon.WPF;
using Panuon.WPF.Builder;
using Panuon.WPF.UI;
using Samples.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Samples
{
    //启动位置位于App.xaml.cs
    public class HomeView
        : View
    {
        #region Fields
        private int _themeFlag = 0;
        #endregion

        #region Methods
        protected override IElement OnCreate()
        {
            var builder = IoC.Get<IAppBuilder>();

            var cardItems = builder.ObserveCollection<CardItem>(GetCardItems());

            //Viewer
            var itemsControl = builder.CreateItemsControl(
                horizontal: "left",
                itemsSource: cardItems,
                itemsPanel: builder.Create<UniformGrid>().Set(UniformGrid.RowsProperty, 2),
                itemTemplate: CreateItemTemplate());

            //Theme Switch Button
            var themeSwitchButton = builder.CreateButton("\ue9c8",
                fontFamily: "/Panuon.WPF.UI;component/Resources/#PanuonIcon",
                fontSize: 18)
                .OnClick((s, e) =>
                {
                    //关于如何配置主题，请查看App.xaml.cs
                    if (_themeFlag == 0)
                    {
                        AppBuilder.CurrentTheme = "dark";
                        _themeFlag = 1;
                    }
                    else
                    {
                        AppBuilder.CurrentTheme = "light";
                        _themeFlag = 0;
                    }
                });

            //Content grid
            var grid = builder.CreateGrid(new IModule[]
            {
                builder.CreateTextBlock(text: "Samples", fontSize: 30),
                builder.Create<ScrollViewer>()
                    .Set(ScrollViewer.ContentProperty, itemsControl.ActualVisual)
                    .Set(ScrollViewer.VerticalScrollBarVisibilityProperty, "disabled")
                    .Set(ScrollViewer.HorizontalScrollBarVisibilityProperty, "auto")
                    .Set(FrameworkElement.MarginProperty, "0,15,0,0")
                    .SetGridRow(1),
            },
            rows: new object[] { "auto", "*" },
            margin: "20,10,20,20");

            return builder.CreateWindow(
                content: grid,
                type: typeof(WindowX),
                location: "centerScreen",
                width: 1000, height: 600)
                .Set(WindowXCaption.ExtendControlProperty, themeSwitchButton.ActualVisual);
        }
        #endregion

        #region Functions
        private IEnumerable<CardItem> GetCardItems()
        {
            var cardItems = new List<CardItem>();
            var viewTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && typeof(View).IsAssignableFrom(t) && t != typeof(HomeView))
                .ToList();

            foreach (var type in viewTypes)
            {
                var view = (View)Activator.CreateInstance(type);
                var rootElement = view.RootElement;
                var window = rootElement.ActualVisual as WindowX;
                var content = window.Content;
                window.Content = null;

                var cardItem = new CardItem()
                {
                    DisplayName = type.Name,
                    Path = $@"Views/{type.Name}.cs",
                    ViewType = type,
                    Preview = CreatePreviewView(window, content),
                };
                cardItems.Add(cardItem);
            }
            return cardItems;
        }

        private IElement CreateItemTemplate()
        {
            var builder = IoC.Get<IAppBuilder>();

            var contentControl = builder.Create<ContentControl>()
                .Set(ContentControl.ContentProperty, builder.CreateBinding(nameof(CardItem.Preview)));

            var grid = builder.CreateGrid(new IModule[]
            {
                builder.Create<Viewbox>()
                    .Set("Child", contentControl.ActualVisual),
                builder.CreateTextBlock(text: builder.CreateBinding(nameof(CardItem.DisplayName)), fontWeight: "bold")
                    .SetGridRow(1),
                builder.CreateTextBlock(text: builder.CreateBinding(nameof(CardItem.Path)))
                    .SetGridRow(2),
                builder.Create<Border>().Set(Border.BackgroundProperty, Brushes.Transparent)
                    .SetGridRowSpan(3),
            },
            rows: new object[] { "*", "auto", "auto" },
            autoAllocate: false);

            return builder.Create<Card>(margin: "0,0,10,10", style: "SamplesCardStyle", width: 250)
                .Set(Card.ContentProperty, grid.ActualVisual)
                .AddHandle(Card.ClickEvent, new RoutedEventHandler((s, e) =>
                {
                    var card = s as Card;
                    var cardItem = card.DataContext as CardItem;
                    var view = (View)Activator.CreateInstance(cardItem.ViewType);
                    builder.ShowDialog(view,
                        owner: this);
                }));
        }

        private FrameworkElement CreatePreviewView(WindowX view, object content)
        {
            var contentControl = new ContentControl()
            {
                Foreground = view.Foreground,
                Content = content,
            };
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(view.DisableDragMove ? 0 : WindowXCaption.GetHeight(view)) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            grid.Children.Add(new Border()
            {
                Background = WindowXCaption.GetBackground(view),
                Child = new ContentControl()
                {
                    Content = view.DataContext is Window ? "" : view.DataContext,
                    ContentTemplate = WindowXCaption.GetHeaderTemplate(view),
                }
            });
            Grid.SetRow(contentControl, 1);
            grid.Children.Add(contentControl);
            var border = new Border()
            {
                Background = view.Background,
                BorderBrush = view.BorderBrush ?? Brushes.Gray,
                BorderThickness = view.BorderThickness.Left == 0 ? new Thickness(1) : view.BorderThickness,
                Width = view.Width,
                Height = view.Height,
                Child = grid,
            };
            return border;
        }

        #endregion
    }

    public class CardItem
    {
        public string DisplayName { get; set; }

        public string Path { get; set; }

        public Type ViewType { get; set; }

        public FrameworkElement Preview { get; set; }
    }
}
