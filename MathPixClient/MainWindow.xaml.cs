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
using System.ComponentModel;

namespace MathPixClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static HttpClient client { get; set; }

        private string _rawOcr;
        private string _ocrResult;
        
        public string ocrResult
        {
            get { return _ocrResult; }
            set
            {
                _rawOcr = value;
                _ocrResult = Process(_rawOcr);
                NotifyPropertyChanged(nameof(ocrResult));
            }
        }
        public MainWindow()
        {
            client = new HttpClient();

            ocrResult = "None";
            
            InitializeHeader();
            
            InitializeComponent();
        }

        private string Process(string input)
        {
            string result = input;

            App app = Application.Current as App;
            var Substitutions = app.Cfg.ActiveSubstitutions;

            foreach (var s in Substitutions)
            {
                result = result.Replace(s.From, s.To);
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Credentials_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
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
            try
            {
                ocrResult = await RequestOCR();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<string> RequestOCR()
        {

            var image = GetImageFromClipboard();

            if (image == null) return "No image on clipboard";

            var json = JsonConvert.SerializeObject(new Request { src = "data:image/png;base64," + image });

            var reply = await client.PostAsync(
                "https://api.mathpix.com/v3/text",
                new StringContent(json, Encoding.UTF8, "application/json")
                );

            if (reply.IsSuccessStatusCode)
            {
                var x = await reply.Content.ReadAsAsync<Response>();
                return x.text;
            }
            else
            {
                throw new Exception(reply.ReasonPhrase);
            }
            
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

        private void GroupButtonOnClick(object sender, RoutedEventArgs e)
        {
            SubstitutionsWindow wd = new SubstitutionsWindow();
            wd.ShowDialog();
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
