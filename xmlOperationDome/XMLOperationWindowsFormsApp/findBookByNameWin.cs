using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMLOperationWindowsFormsApp;

namespace XMLOperationWindowsFormsApp
{
    public partial class findBookByNameWin : Form
    {
        public findBookByNameWin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            /*FormMain formMain = (FormMain)this.Owner;
            formMain.BookName = inputBookName.Text;
            this.Hide();*/
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            FormMain formMain = (FormMain)this.Owner;
            String bookName = inputBookName.Text;
            if ("".Equals(bookName))
            {
                MessageBox.Show("bookName不能为空，请重新输入", "查询条件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                formMain.ChangeText(inputBookName.Text);
                Close();
            }
        }
    }
}
