using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalSubtitles
{
    class Subtitles
    {
        public TimeSpan start;
        public TimeSpan stop;
        public string ST;

        //Constructeur
        public Subtitles(TimeSpan start, TimeSpan stop, string ST)
        {
            this.start = start;
            this.stop = stop;
            this.ST = ST;
        }
    }
}
