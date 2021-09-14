using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagementDB;
using MySql.Data.MySqlClient;

namespace loginusermanagement
{
    public partial class loginform : Form
    {
        public loginform()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection("localhost", "usermanagement", "csharp", "password");

            if(db.OpenConnection())
            {
                //get list of users 
                
                UsersDB usersDB = new UsersDB();
                
                List<User> userList = usersDB.GetUsers(db);

                bool validCredentials = false;
                bool userFound = false;

                //check email exists 
                foreach(User user in userList)
                {
                    if(this.textBoxEmail.Text == user.Email)
                    {
                        userFound = true;

                        if(this.textBoxPassword.Text == user.Password)
                        {
                            validCredentials = true;
                        }
                        else
                        {
                            MessageBox.Show("Inavlid Password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        break;


                    }
                        
                        
                }

                if(userFound == false)
                {
                    MessageBox.Show($"User: {this.textBoxEmail.Text} does not exist. Please try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                if(validCredentials == true)
                {
                    this.Visible = false;

                    ViewUsersForm viewUsersForm = new ViewUsersForm();
                    viewUsersForm.ShowDialog();

                    this.Visible = true;

                    
                }

                //Email not found
            }




            this.Visible = false;

            //ViewUsersForm viewUsersForm = new ViewUsersForm();
            //viewUsersForm.ShowDialog();

            Application.Exit();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
