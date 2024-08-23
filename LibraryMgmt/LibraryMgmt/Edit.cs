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
    public partial class Edit : Form
    {
        private string connectionString = "Server=OMEN\\SQLEXPRESS;Database=LibraryMgmt;User Id=sa;Password=1234;";
        public string newval { get; private set; }
        private int recordId;
        public Edit(string editval)
        {
            InitializeComponent();
            currenttxt.Text = editval;
            this.recordId = recordId;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            newval = newvaltxt.Text;
            //UpdateDatabase();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE YourTable SET YourColumn = @NewValue WHERE Id = @RecordId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewValue", newval);
                        command.Parameters.AddWithValue("@RecordId", recordId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Update successful.");
                        }
                        else
                        {
                            MessageBox.Show("No record found to update.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}