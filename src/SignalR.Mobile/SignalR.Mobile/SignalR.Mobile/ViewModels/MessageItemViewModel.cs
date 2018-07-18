namespace SignalR.Mobile.ViewModels
{
    class MessageItemViewModel : ViewModelBase
    {
        private string _user;
        private string _message;

        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}
