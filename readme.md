QQ频道：https://pd.qq.com/s/fpap7qj2y


# Panuon.WPF.Builder

以一种全新的方式开发你的WPF应用。  
`Panuon.WPF.Builder` 使你能完全使用C#代码来构建WPF页面——无需MVVM，也无需编写绑定或 `Command` 。后续还将提供使用C#代码构建主题文件和样式的功能。  

## 此项目仍在开发中！

它是从 `Orionid` 商业项目上诞生的衍生品。编写它花了不少时间，这也是OD频繁跳票的原因之一。  
`Panuon.WPF.Builder`可以在 `Orionid` 这种商业客户端上很好地工作，因此是时候将它放出来了，虽然仅仅只是 `alpha` 版。随着时间的推移，`Panuon.WPF.Builder` 会进一步尝试简化WPF的开发流程。  
如果有任何建议或者问题反馈，欢迎提issue，或加入我们的QQ频道讨论。  

## 一些新鲜事

### 创建UI对象和绑定

`Panuon.WPF.Builder` 使用 `Observer` 对象来替代绑定（尽管内部还是会将其转换成绑定）。你可以使用 `AppBuilder` 来操控任何内容，例如创建控件、显示页面、创建变量或是绑定。  
这是一段创建输入框，并将 `userName` 对象绑定到 `Text` 属性上的代码：  
```CSharp
var builder = new AppBuilder();
var userName = builder.Observe(""); //创建变量
var textBox = builder.CreateTextBox(text: userName, margin: "5")
    .OnTextChanged((s, e) =>
    {
        //处理内容变化事件 userName.Value
    });
```
想和你现有的代码一起工作？没问题，你可以通过 `textBox.ActualVisual` 属性来获得实际创建的 `TextBox` 控件，并添加到你的UI中。  
```CSharp
bdrMain.Child = textBox.ActualVisual;
```

### 类型转换

相信你已经发现，在上面的示例中， `margin` 参数的值是 `"5"` 而不是 `new Thickness(5)` 。`Panuon.WPF.Builder` 内置了数值转换，大多数参数都可以接受 `object` 类型的对象。  
例如上面的 `margin` 参数，它可以接受以下值：  
```CSharp
builder.CreateTextBox(margin: 5); //使用int
builder.CreateTextBox(margin: 5d); //使用double
builder.CreateTextBox(margin: "5,5,5,5"); //使用字符串
builder.CreateTextBox(margin: new Thickness(5,5,5,5)); //使用Thickness
builder.CreateTextBox(margin: builder.Observe(new Thickness(5))); //使用自定义变量（绑定）
```  
这是一个 `Brush` 类型的例子：  
```CSharp
builder.CreateTextBox(foreground: "red"); //使用已知的色彩名称（不区分大小写）
builder.CreateTextBox(foreground: "#FF0000"); //使用HEX颜色
builder.CreateTextBox(foreground: "ErrorBrush"); //使用自定义的资源键
```  

### 关于Observer的更多事情
`Observer` 对象可以设置 `mode` 和 `updateSourceTrigger` 参数。默认情况下，它使用 `BindingMode.Default` 和 `UpdateSourceTrigger.PropertyChanged` 。
`Observer` 对象支持监听其他 `Observer` 对象。被监听的对象发生变化时，会触发数值更新逻辑。这用于取代WPF中的 `Converter 转换器` 。

```CSharp
var text = builder.Observer("");
var isTextEmpty = builder.Observer((o) =>
{
    return string.IsNullOrEmpty(text.Value);
}, text); //isTextEmpty.Value的值为 true
text.Value = "123"; //isTextEmpty.Value的值现在为false
```

另外，`Observer`  对象也支持双向监听。
```CSharp
var isVisible = builder.Observe(false);
var visibility = builder.Observe((o) =>
{
    return isVisible ? Visibility.Visible : Visibility.Collapsed;
}, (o) =>
{
    return o.Value == Visibility.Visible;
}, isVisible);
```

### 给属性赋值
通常情况下， `Create()` 方法中提供了一些常用的参数，至于样式相关的属性，建议你将其封装成 `Style`并放置在资源字典中。在之后的版本中，`Panuon.WPF.Builder` 还会提供通过C#代码创建样式和主题的方法。
对于没有提供参数的属性，可以使用 `Set()` 方法：  
```CSharp
var button = builder.CreateButton(content: "ClickMe");
builder.CreateBorder(borderBrush: "red")
    .Set(Border.BackgroundProperty, "blue") //直接使用依赖属性，性能最好
    .Set("borderThickness", "1") //使用依赖属性的名称，不区分大小写
    .Set("child", button.ActualVisual) //非依赖属性可以直接使用属性名称，不区分大小写
```

### 事件处理
部分控件提供了常用的事件处理扩展方法（后续会慢慢扩充）。如果没有，可以调用 `AddHandler()` 方法：
```CSharp
var button = builder.CreateButton(content: "ClickMe")
    .OnClick((s, e) =>
    {
        //点击事件
    }) 
    .AddHandle(Button.ToolTipOpeningEvent, new ToolTipEventHandler((s, e) =>
    {
        //添加路由事件处理
    })) 
    .AddHandle("Initialized", new EventHandler((s, e) =>
    {
        //添加C#常规事件处理(event)
    })); 
```

### 将你的页面拆分成多个类
`Panuon.WPF.Builder` 中不区分 `用户控件` 和 `页面` ，仅提供了 `View` 类。任何窗体或分页都可以是 `View` ，即使只有一个控件。  
另外，在 `Panuon.WPF.Builder` 中，每个控件都派生自 `IElement` 接口。要将WPF控件或你的自定义控件转换为 `IElement` ，只需要使用 `appBuilder.Create<TControl>()` 方法：
```CSharp
public class MyView
  : View
{
    protected override IElement OnCreate()
    {
        var builder = new AppBuilder();

        return builder.Create<MyUserControl>(margin: "10", width: 300, height: 300);
    }
}
```
要显示这个页面，需要使用 `appBuilder.Show<MyView>()` 方法。你可以将这个调用写在 `App.xaml.cs` 的 `OnStartup()` 方法中，用于运行WPF程序时打开启动窗体。  
如果你十分确定这个页面只会是一个窗体，则可以在 `OnCreate()` 方法中返回一个 `WindowElement` ，这样就不需要在每次调用 `Show` 方法时设置大小等属性 ：
```CSharp
var userControl = builder.Create<MyUserControl>(margin: "10");
return builder.CreateWindow(content: userControl, location: "centerScreen", width: 300, height: 300);
```
如果你想和 `Panuon.WPF.UI` 一起使用，也可以创建一个 `WindowXElement` ：
```
return builder.CreateWindow(type: typeof(WindowX));
```