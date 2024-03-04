using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace CalculatorApp
{
    public partial class MainPage : ContentPage
    {
        public CalculatorViewModel ViewModel { get; } = new CalculatorViewModel();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }

    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string displayText;

        public string DisplayText
        {
            get { return displayText; }
            set
            {
                if (displayText != value)
                {
                    displayText = value;
                    OnPropertyChanged(nameof(DisplayText));
                }
            }
        }

        public ICommand NumberCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand EqualsCommand { get; }

        public CalculatorViewModel()
        {
            NumberCommand = new Command<string>(HandleNumberCommand);
            OperationCommand = new Command<string>(HandleOperationCommand);
            EqualsCommand = new Command(HandleEqualsCommand);
        }

        private void HandleNumberCommand(string number)
        {
            DisplayText += number;
        }

        private void HandleOperationCommand(string operation)
        {
            DisplayText += operation;
        }

        private void HandleEqualsCommand()
        {
            if (!string.IsNullOrEmpty(DisplayText))
            {
                var result = new System.Data.DataTable().Compute(DisplayText, null);
                DisplayText = result.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
