using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Assignment.View.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string email { get; set; }
        public string password { get; set; }

        public string confirmpassword { get; set; }

        private DateTime _dateofbirth = DateTime.Now;
        public DateTime dateofbirth
        {
            get { return _dateofbirth; }
            set
            {
                _dateofbirth = value;
                OnPropertyChanged("dateofbirth");

            }
        }



        public string gender { get; set; }

        public DateTime? SelectedDate { get; set; }


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




        private ICommand _registerclick;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public ICommand registerclick
        {
            get
            {
                return _registerclick ?? (_registerclick = new CommandHandler(() => regaction(), true));
            }
        }


        public void regaction()
        {
            bool auth = false;

            string emailaddress = email;
            string pass = password;
            string copass = confirmpassword;
            string fname = firstname;
            string lname = lastname;
            string gen = gender;

            var date = SelectedDate;

            DateTime dob = dateofbirth;



            if (fname == null)
            {
                error = "Enter First Name";

            }
            else if (lname == null)
            {
                error = "Enter Last Name";
            }

            else if (emailaddress == null)
            {
                error = "Enter Email";
            }
            else if (!Regex.IsMatch(emailaddress, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                error = "Enter a valid email.";
            }
            else if (gen == null)
            {
                error = "Enter Gender";
            }
            else if (pass == null)
            {
                error = "Enter Password";
            }
            else if (pass != copass)
            {
                error = "Password does not match";
            }
            else
            {
                auth = true;
            }




            if (auth == true)
            {


                SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=Assignments;Integrated Security=True;");
                try
                {
                    if (sqlcon.State == ConnectionState.Closed)
                        sqlcon.Open();
                    string query = "insert into users (Firstname, LastName, Gender, Email,dob, Password)" +
                        " values (@fname,@lname,@gen,@emailaddress,@dob,@pass)";

                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@emailaddress", email);
                    sqlcmd.Parameters.AddWithValue("@pass", password);
                    sqlcmd.Parameters.AddWithValue("@fname", firstname);
                    sqlcmd.Parameters.AddWithValue("@lname", lastname);
                    sqlcmd.Parameters.AddWithValue("@gen", gender);
                    sqlcmd.Parameters.AddWithValue("@dob", dateofbirth);
                    sqlcmd.ExecuteScalar();
                    error = "Registered";



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

