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
    public partial class BookViewerWindow : Form
    {
        BookEntity _book;

        public BookViewerWindow(BookEntity book)
        {
            InitializeComponent();
            _book = book;
        }

        private void BookViewerWindow_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = $"Id: {_book.Id} | Author: {_book.Author}\r\n\r\n{_book.Contents}";
        }
    }
}
