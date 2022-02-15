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

namespace Inventory.wpf.Controls
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private bool _IsPasswordChanging;

        public BindablePasswordBox()
        {
            InitializeComponent();

            Placeholder.Visibility = Visibility.Visible;
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), 
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _IsPasswordChanging = true;
            Password = PasswordBox.Password;
            Placeholder.Visibility = Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
            _IsPasswordChanging = false;
        }

        private void UpdatePassword()
        {
            if (!_IsPasswordChanging)
            {
                PasswordBox.Password = Password;
                Placeholder.Visibility = Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
