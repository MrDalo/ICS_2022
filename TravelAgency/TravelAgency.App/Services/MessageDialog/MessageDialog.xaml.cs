using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.App.Services.MessageDialog
{
    
    public partial class MessageDialog : Window
    {
        private MessageDialogResult _result;

        public MessageDialog(string title, string text, MessageDialogResult defaultResult,
            MessageDialogButtonConfiguration buttonConfiguration)
        {
            InitializeComponent();
            Title = title;
            textBlock.Text = text;
            _result = defaultResult;
            InitializeButtons(buttonConfiguration);
        }

        private void InitializeButtons(MessageDialogButtonConfiguration buttonConfiguration)
        {
            var buttons = GetButtonsFromConfiguration(buttonConfiguration);

            foreach (var button in buttons)
            {
                var btn = new Button { Content = button, Tag = button };
                ButtonsPanel.Children.Add(btn);
                btn.Click += ButtonClick;
            }
        }

        private static IEnumerable<MessageDialogResult> GetButtonsFromConfiguration(
            MessageDialogButtonConfiguration buttonConfiguration) =>
            buttonConfiguration switch
            {
                MessageDialogButtonConfiguration.Ok => new[] { MessageDialogResult.Ok },
                MessageDialogButtonConfiguration.OkZatvoriť => new[] { MessageDialogResult.Ok, MessageDialogResult.Zatvoriť },
                MessageDialogButtonConfiguration.ÁnoNieZatvoriť => new[] { MessageDialogResult.Áno, MessageDialogResult.Nie, MessageDialogResult.Zatvoriť },
                MessageDialogButtonConfiguration.ÁnoNie => new[] { MessageDialogResult.Ok, MessageDialogResult.Nie },
                _ => new[] { MessageDialogResult.Ok }
            };

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(e.Source is Button button)) return;

            _result = (MessageDialogResult)button.Tag;
            Close();
        }

        public new MessageDialogResult ShowDialog()
        {
            base.ShowDialog();
            return _result;
        }
    }
}
