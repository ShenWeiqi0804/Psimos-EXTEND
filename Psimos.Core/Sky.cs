using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psimos.Core
{
    public class Sky : CheckArea
    {
        public AIRTAP[] AIRTAPs;
        public SLIDE[] SLIDEs;
        public Launcher[] Launchers;

        public override List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            notes.AddRange(AIRTAPs);
            notes.AddRange(SLIDEs);
            return notes;
        }
    }
}
