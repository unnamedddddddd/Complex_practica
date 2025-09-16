using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Practica
{
    class ProskuryakovCheckBox
    {
        public static void HandleProskuryakov(bool isChecked, string filename = "output.txt")
        {
            using (StreamWriter file = File.AppendText(filename))
            {
                file.WriteLine($"Proskuryakov: {(isChecked ? "checked" : "unchecked")}");
            }
        }
    }
}
