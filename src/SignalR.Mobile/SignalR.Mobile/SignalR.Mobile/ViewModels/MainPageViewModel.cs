using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;

namespace SignalR.Mobile.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private string _serverAddress;
        private string _user;
        private string _messageToSend;
        private bool _isConnected;
        private Command _connectionCommand;
        private Command _sendCommand;
        private ObservableCollection<MessageItemViewModel> _allMessages;

        private HubConnection _connection;

        public MainPageViewModel()
        {
            ConnectionCommand = new Command(ConnectionAction);
            SendCommand = new Command(SendMessage, () => IsConnected);
            AllMessages = new ObservableCollection<MessageItemViewModel>();
        }

        public string ServerAddress
        {
            get => _serverAddress;
            set => SetProperty(ref _serverAddress, value);
        }

        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public string MessageToSend
        {
            get => _messageToSend;
            set => SetProperty(ref _messageToSend, value);
        }

        public Command ConnectionCommand
        {
            get => _connectionCommand;
            private set => SetProperty(ref _connectionCommand, value);
        }

        public bool IsConnected
        {
            get => _isConnected;
            private set
            {
                SetProperty(ref _isConnected, value);
                SendCommand.ChangeCanExecute();
            } 
        }

        public ObservableCollection<MessageItemViewModel> AllMessages
        {
            get => _allMessages;
            private set => SetProperty(ref _allMessages, value);
        }

        public Command SendCommand
        {
            get => _sendCommand;
            private set => SetProperty(ref _sendCommand, value);
        }
        
        private async void ConnectionAction()
        {
            if (IsConnected)
            {
                await DisconnectFromSignalRConnection();
            }
            else
            {
                await ConnectToSignalRHub();
            }
        }

        private async Task ConnectToSignalRHub()
        {
            try
            {
                AddSystemMessage($"Connecting to {ServerAddress}...");

                _connection = CreateSignalRConnection();

                RegisterSignalRListeners(_connection);

                await _connection.StartAsync();
                IsConnected = true;

                AddSystemMessage("Connected");
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
            }
        }

        private HubConnection CreateSignalRConnection()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(ServerAddress)
                .Build();

            return connection;
        }

        private void RegisterSignalRListeners(HubConnection connection)
        {
            connection.On<string, string>("ReceiveMessage", ReceiveMessage);
        }

        private void ReceiveMessage(string user, string message)
        {
            AddMessage(user, message);
        }

        private async Task DisconnectFromSignalRConnection()
        {
            try
            {
                AddSystemMessage("Disconnecting...");

                await _connection.StopAsync();
                await _connection.DisposeAsync();
                IsConnected = false;
                _connection = null;

                AddSystemMessage("Disconnected");
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
            }
        }

        private async void SendMessage()
        {
            try
            {
                await _connection.InvokeAsync("SendMessage", User, MessageToSend);
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
            }
        }

        private void AddSystemMessage(string message)
        {
            AddMessage("SYSTEM", message);
        }

        private void AddErrorMessage(string message)
        {
            AddMessage("ERROR", message);
        }

        private void AddMessage(string user, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AllMessages.Insert(0, new MessageItemViewModel
                {
                    User = user,
                    Message = message
                });
            });
        }
    }
}
