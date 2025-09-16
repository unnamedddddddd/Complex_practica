using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Complex_Practica
{
    class PoryvaevCheckBox
    {
        public static void HandlePoryvaev(bool isChecked, string filename = "output.txt")
        {
            using (StreamWriter file = File.AppendText(filename))
            {
                file.WriteLine($"Poryvaev: {(isChecked ? "checked" : "unchecked")}");
            }
        }
        
    }
}
