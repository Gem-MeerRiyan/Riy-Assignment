using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Assignment.View.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        public string email { get; set; }
        public string password { get; set; }
        //public string error { get; set; }

        private string _error;
        public string error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged("error");

            }
        }



        private ICommand _loginclick;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public ICommand loginclick
        {
            get
            {
                return _loginclick ?? (_loginclick = new CommandHandler(() => logaction(), true));
            }
        }


        public void logaction()
        {

            string emailaddress = email;
            string pass = password;



            SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=Assignments;Integrated Security=True;");
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                string query = "select count(1) from users where email=@emailaddress and password=@pass";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Parameters.AddWithValue("@emailaddress", email);
                sqlcmd.Parameters.AddWithValue("@pass", password);
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {
                    

                    MainWindow main = new MainWindow();
                    main.Close();

                }
                else
                {

                    error = "email or password not correct";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;
            public CommandHandler(Action action, bool canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }


    }
}
