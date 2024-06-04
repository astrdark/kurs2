using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kurs.App
{
    public partial class SearchBooksWindow : Form
    {
        private Core.ADT.Vec<BookEntity> _books;

        public SearchBooksWindow()
        {
            InitializeComponent();
            _books = Kostil.DB.select_all<BookEntity>();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            try
            {
                Regex r = new Regex(textBox1.Text);
                foreach (BookEntity book in _books)
                {
                    if (r.IsMatch(book.Contents))
                    {
                        richTextBox1.Text += $"{book.Id} | {book.Author} -- {book.Title}\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = $"Search failed! Ex:\r\n{ex.ToString()}";
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (long.TryParse(textBox2.Text, out var id))
            {
                var list = Kostil.DB.select_all<BookEntity>();
                foreach (BookEntity book in list)
                {
                    if (book.Id == id)
                    {
                        new BookViewerWindow(book).ShowDialog();
                        return;
                    }
                }
            }
        }
    }
}
