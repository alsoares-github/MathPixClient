using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using MathPixClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;
using System;

namespace MathPixClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient client { get; set; }
        public MainWindow()
        {
            client = new HttpClient();
            InitializeHeader();
            
            InitializeComponent();
        }

        private void Credentials_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Show();
        }

        private void InitializeHeader()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("app_id", Settings.Default.app_id);
            client.DefaultRequestHeaders.Add("app_key", Settings.Default.app_key);
        }

        public void RefreshCredentials()
        {
            InitializeHeader();
        }

        private async void OCR_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestOCR();
        }

        private async Task<string> RequestOCR()
        {

            //string image = "data:image/png;base64,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";

            var image = GetImageFromClipboard();

            if (image == null) return null;

            var json = JsonConvert.SerializeObject(new Request { src = "data:image/png;base64," + image });

            var reply = await client.PostAsync(
                "https://api.mathpix.com/v3/text",
                new StringContent(json, Encoding.UTF8, "application/json")
                );

            var x = reply.Content.ReadAsAsync<Response>();

            return x.Result.text;
        }

        private string GetImageFromClipboard()
        {
            var x = Clipboard.GetImage();

            if (x == null) return null;

            var e = new PngBitmapEncoder();
            e.Frames.Add(BitmapFrame.Create(x));

            var ms = new MemoryStream();

            e.Save(ms);

            return Convert.ToBase64String(ms.ToArray());
        }
    }

    public class Response
    {
        public string text { get; set; }
    }

    public class Request
    {
        public string src { get; set; }
        public List<string> formats { get; set; } = new List<string> { "text" };
    }
}
