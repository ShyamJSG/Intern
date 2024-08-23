using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LibraryMgmt
{
    public partial class AddBook : Form
    {
        private string connectionString = "Server=OMEN\\SQLEXPRESS;Database=LibraryMgmt;User Id=sa;Password=1234;";

        public AddBook()
        {
            InitializeComponent();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bookidtxt_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(bookidtxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                bookidtxt.Text = bookidtxt.Text.Remove(bookidtxt.Text.Length - 1);
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            

            string bookidtemp = bookidtxt.Text;
            string bookname = booknametxt.Text;
            string author = bookauthortxt.Text;
            string category = categoryBox.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(bookname) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(bookidtemp))
            {
                MessageBox.Show("Input Fields Cannot be Empty");
                return;
            }
            int bookid = int.Parse(bookidtemp);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Books (BookId, bookname,author,Category) VALUES (@BookId, @BookName,@Author,@Category)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookId",bookid );
                        command.Parameters.AddWithValue("@BookName",bookname);
                        command.Parameters.AddWithValue("@Author", author);
                        command.Parameters.AddWithValue("@Category",category);
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Book Successfully Added.");
                        }
                        else
                        {
                            MessageBox.Show("Entry Failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            this.Close();

            
        }
    }
}
