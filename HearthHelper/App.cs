using System;
using System.CodeDom.Compiler;
using System.Windows;

namespace HearthHelper;

public class App : Application
{
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
        base.StartupUri = new Uri("./HearthHelper/MainWindow.xaml", UriKind.Relative);
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [STAThread]
    public static void Main()
    {
        App app = new App();
        app.InitializeComponent();
        app.Run();
    }
}
