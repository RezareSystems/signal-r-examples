using System;
using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Service
{
    public partial class SignalRLogger : ServiceBase
    {
        HubConnection _connection;

        public SignalRLogger()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            try
            {
                EventLog.WriteEntry("Connecting...", EventLogEntryType.Information);

                _connection = CreateSignalRConnection();
                RegisterSignalRListeners(_connection);

                await _connection.StartAsync();

                EventLog.WriteEntry("Connected", EventLogEntryType.Information);
            } catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                Stop();
            }
        }

        private HubConnection CreateSignalRConnection()
        {
            const string url = "http://localhost:50091/chatHub";

            var connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            return connection;
        }


        private void RegisterSignalRListeners(HubConnection connection)
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var logMessage = $"User: {user} - Message: {message}";

                EventLog.WriteEntry(logMessage, EventLogEntryType.Information);
            });
        }

        protected override void OnStop()
        {            
            _connection = null;
        }
    }
}
