using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Complex_Practica
{
    public class Kamaldinov 
    {   
        public string CheckSequence(List<int> digits)
        {
            bool Up = true;
            bool Down = true;
            string data;
            for (int i = 1; i < digits.Count; i++)
            {
                if (digits[i] > digits[i - 1])
                {
                    Down = false;
                }
                else if (digits[i] < digits[i - 1])
                {
                    Up = false;
                }
            }
            if (Up && Down)
            {
                data = "Постоянная\n";

            }
            else if (Up)
            {
                data = "Возрастающая\n";
            }
            else if (Down)
            {
                data = "Убывающая\n";
            }
            else
            {
                data = "Неупорядоченная\n";
            }
            return "Последовательность: " + data;
        }
    }
}
