using Kurs.App;
using Kurs.Core.MiniORM;

namespace Kurs
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Kostil.DB = new DbConn("storage.sqlite");
            Kostil.DB.ensure_created<BookEntity>();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}