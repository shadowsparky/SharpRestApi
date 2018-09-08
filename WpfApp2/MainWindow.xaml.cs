using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static WpfApp2.RequestFactory<WpfApp2.User>;

namespace WpfApp2
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Testic();
        }

        public void Testic()
        {
            Response response = new User();
            RequestFactory<User> Requester = new RequestFactory<User>();
            RequestCallback callback = new RequestCallback(RequestHandler);
            new Task(() =>
                Requester.GetRequest("https://jsonplaceholder.typicode.com/posts", callback))
                .Start();
        }

        public void RequestHandler(List<User> users)
        {
            if (users == null)
            {
                MessageBox.Show("");
            }
        }
    }
}
