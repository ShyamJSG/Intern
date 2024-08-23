using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryMgmt
{
    public partial class Lend : Form
    {
        private DataTable lendTable;
        public Lend()
        {
            InitializeComponent();
            InitializeLendTable();
        }

        private void InitializeLendTable()
        {
            lendTable = new DataTable();
            lendTable.Columns.Add("CustomerName", typeof(string));
            lendTable.Columns.Add("BookName", typeof(string));
            lendTable.Columns.Add("IssueDate", typeof(DateTime));
            lendTable.Columns.Add("Due?", typeof(bool));
            issueGrid.DataSource = lendTable;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void issueButton_Click(object sender, EventArgs e)
        {
            Issue issueBookForm = new Issue(lendTable);
            issueBookForm.ShowDialog();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            if (issueGrid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in issueGrid.SelectedRows)
                {
                    issueGrid.Rows.RemoveAt(row.Index);
                }
            }

            else
            {
                MessageBox.Show("Please select a row to remove.");
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
