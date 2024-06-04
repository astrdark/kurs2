using Kurs.App;

namespace Kurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddNewBookWindow().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SearchBooksWindow().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (long.TryParse(textBox1.Text, out var id))
            {
                var list = Kostil.DB.select_all<BookEntity>();
                foreach(BookEntity book in list)
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
