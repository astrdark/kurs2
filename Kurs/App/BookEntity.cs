using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs.App
{
    public class BookEntity
    {
        public long Id { get; set; }
        public long Hits { get; set; }

        public string Author { get; set; }
        public string Title { get; set; }

        public string Contents { get; set; }
    }
}
