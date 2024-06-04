using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs.App
{
    public partial class AddNewBookWindow : Form
    {
        public AddNewBookWindow()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* const */ string author = textBox1.Text;
            /* const */ string title = textBox2.Text;
            /* const */ string contents = richTextBox1.Text;

            Kostil.DB.insert(new BookEntity { Author = author, Title = title, Contents = contents });
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
