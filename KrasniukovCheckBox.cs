using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Practica
{
    class KrasniukovCheckBox
    {
        public static void HandleKrasnukov(bool isChecked, string filename = "output.txt")
        {
            using (StreamWriter file = File.AppendText(filename))
            {
                file.WriteLine($"Krasnukov: {(isChecked ? "checked" : "unchecked")}");
            }
        }
    }
}
