using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Azure.Devices;
using System.Runtime.InteropServices.WindowsRuntime;


namespace LightController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=jackiepi.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0jaBCaJOtdxuQwns8J/SWwFpvZLBehGqm6KKRh4Mv/g=";

        public MainWindow()
        {
            InitializeComponent();

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        private async Task TurnLight(bool isOn)
        {
            await SendCloudToDeviceMessageAsync(isOn);
        }

        private static async Task SendCloudToDeviceMessageAsync(bool isOn)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(isOn ? "on" : "off"));
            await serviceClient.SendAsync("1", commandMessage);
        }

        private async void BtnTurnOn_OnClick(object sender, RoutedEventArgs e)
        {
            await TurnLight(true);
        }

        private async void BtnTurnOff_OnClick(object sender, RoutedEventArgs e)
        {
            await TurnLight(false);
        }
    }
}
