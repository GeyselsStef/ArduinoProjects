using DDDSoft.Windows.Winforms.Extensions;
using DDDSoft.Windows.Winforms.Hosting;
using SerialCommunication;
using System;

namespace StepperMotor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinformsHostApplicationBuilder applicationBuilder=WinformsHost.CreateWinformsApplicationBuilder(args:null);


            applicationBuilder.FormNavigator.AddMainForm<Form1>();
            applicationBuilder.FormNavigator.AddForms(new[] { typeof(Program).Assembly }, null);

            applicationBuilder.ApplicationConfiguration.SetEnableVisualStyles();
            applicationBuilder.ApplicationConfiguration.SetCompatibleTextRenderingDefault(false);
            applicationBuilder.ApplicationConfiguration.AddUnhandledException(OnUnhandledException);

            applicationBuilder.Services.AddSerialPortFactory();

            var host = applicationBuilder.Build();
            host.Run<Form1>();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }
    }
}
