using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryMgmt
{
    public partial class Home : Form
    {
        private string connectionString = "Server=OMEN\\SQLEXPRESS;Database=LibraryMgmt;User Id=sa;Password=1234;";

        
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
        


        private void logoutButton_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddBook addBookForm = new AddBook();
            addBookForm.ShowDialog();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            Remove remove = new Remove();
            remove.ShowDialog();
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            Lend lend = new Lend();
            lend.Show();
            this.Hide();
           
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.BeginEdit(true);
            string editval = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            Edit editcell = new Edit(editval);
            if (editcell.ShowDialog() == DialogResult.OK)
            { 
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = editcell.newval;
            }

        }

        private void displayB_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT BookId, bookname, author, Category FROM Books";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
    
}
