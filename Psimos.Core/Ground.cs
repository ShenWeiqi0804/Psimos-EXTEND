using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psimos.Core
{
    public class Ground : CheckArea
    {
        public Hold[] holds;
        public Tap[] taps;
        public GroundLine[] groundLines;

        public override List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            notes.AddRange(holds);
            notes.AddRange(taps);
            return notes;
        }
    }
}
