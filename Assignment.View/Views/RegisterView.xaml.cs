using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace Assignment.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {

        public RegisterView()
        {
            InitializeComponent();

        }



        private void Cancel_Register(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtGender.Text = "";
           
            txtEmailAddress.Text = "";
            txtPassword.Text = "";
            txtconfirmPassword.Text = "";

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://learn.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo("https://www.google.com/"));
            e.Handled = true;
        }

        private void Hyperlink_RequestNavigate_1(object sender, RequestNavigateEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {

            MainWindow form2 = new MainWindow();
            form2.Show();
            this.Close();

        }

    }
}
