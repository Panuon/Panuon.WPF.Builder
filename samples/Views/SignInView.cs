using Panuon.WPF;
using Panuon.WPF.Builder;
using Panuon.WPF.UI;

namespace Samples.Views
{
    public class SignInView
        : View
    {
        protected override IElement OnCreate()
        {
            var builder = IoC.Get<IAppBuilder>();

            var account = builder.Observe("admin");
            var password = builder.Observe("");

            var errorText = builder.Observe("");
            var errorVisibility = builder.Observe((o) =>
            {
                return string.IsNullOrEmpty(errorText.Value);
            }, errorText);

            var formPanel = builder.CreateStackPanel(new IModule[]
            {
                builder.CreateTextBlock(text: "Sign In", fontSize: 20),
                builder.CreateTextBlock(margin: "0,20,0,0")
                    .AddRun("*", "red")
                    .AddRun("Account"),
                builder.CreateTextBox(style: "SignInTextBoxStyle", margin: "0,3,0,0", text: account),
                  builder.CreateTextBlock(margin: "0,10,0,0")
                    .AddRun("*", "red")
                    .AddRun("Password"),
                builder.CreatePasswordBox(style: "SignInPasswordBoxStyle", margin: "0,3,0,0", password: password),
                builder.CreateButton(style: "SignInButtonStyle", margin: "0,10,0,0", content: "Sign In")
                    .OnClick((s, e) =>
                    {
                        if (string.IsNullOrEmpty(account.Value))
                        {
                            errorText.Value = "Account can not be null";
                            e.Handled = true;
                            return;
                        }
                        else if (string.IsNullOrEmpty(password.Value))
                        {
                            errorText.Value = "Password can not be null";
                            e.Handled = true;
                            return;
                        }
                        errorText.Value = null;
                        TryClose(true);
                    }),
                builder.CreateTextBlock(margin: "0,3,0,0", text: errorText, visibility: errorVisibility, foreground: "red"),
            });

            return builder.CreateWindow(
                type: typeof(WindowX),
                content: formPanel.Set("Margin", 40),
                location: "centerOwner", width: 380, height: 420);
        }
    }
}
