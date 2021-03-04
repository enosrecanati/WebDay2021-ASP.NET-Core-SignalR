using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;

namespace WebDay2021.SignalR.SimpleMultiplatformChat.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _hubConnection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                StatusMessagetextBlock.Text = "Connessione in corso...";

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:44315/chat")
                    .Build();

                _hubConnection.On<string, string>("MessageReceived", (username, message) => {
                    DiscussionListBox.Items?.Add($"{username}: {message}");
                });

                await _hubConnection.StartAsync();

                ConnectButton.IsEnabled = false;
                DisconnectButton.IsEnabled = true;
                MessageTextBox.IsEnabled = true;
                SendMessageButton.IsEnabled = true;

                StatusMessagetextBlock.Text = "Connesso...";
            }
            else
            {
                StatusMessagetextBlock.Text = "Specificare uno username per connettersi!";
            }
        }

        private async void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            await _hubConnection.StopAsync();

            ConnectButton.IsEnabled = true;
            DisconnectButton.IsEnabled = false;
            MessageTextBox.IsEnabled = false;
            SendMessageButton.IsEnabled = false;

            StatusMessagetextBlock.Text = "Disconnesso.";
        }

        private async void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageTextBox.Text))
            {
                StatusMessagetextBlock.Text = "Specificare un messaggio per inviarlo!";
            }
            else
            {
                await _hubConnection.InvokeAsync("SendMessage", UsernameTextBox.Text, MessageTextBox.Text);

                DiscussionListBox.Items?.Add($"{UsernameTextBox.Text}: {MessageTextBox.Text}");

                MessageTextBox.Text = "";
            }
        }
    }
}
