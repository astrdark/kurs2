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

        public SearchBooksWindow(string regex = "")
        {
            InitializeComponent();
            _books = Kostil.DB.select_all<BookEntity>();

            search_impl(regex);
        }

        private void search_impl(string regex)
        {
            richTextBox1.Text = "";
            try
            {
                Regex r = new Regex(regex);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            search_impl(textBox1.Text);
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
