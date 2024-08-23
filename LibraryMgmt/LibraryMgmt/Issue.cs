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
    public partial class Issue : Form
    {
        private DataTable lendtable;
        public Issue(DataTable lendtable)
        {
            InitializeComponent();
            this.lendtable = lendtable;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            DataRow newRow = lendtable.NewRow();
            newRow["CustomerName"] = cnametxt.Text; 
            newRow["BookName"] = bnametxt.Text;
            newRow["IssueDate"] = datepicker.Value; 
            newRow["Due?"] = false; 
            lendtable.Rows.Add(newRow);
            this.Close();
        }
    }

}

