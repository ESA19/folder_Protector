using folder_Protector;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.Run(new LoginForm());

    }
    
}       
    


