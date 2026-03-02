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
    class Krasnukov
    {
        public string CheckMaxMin(List<int> digits)
        {
            string cifri = "";
            int maxDigit = -1;
            int minDigit = 10;
            int maxPosition = -1;
            int minPosition = -1;

            for (int i = 0; i < digits.Count; i++)
            {

                if (digits[i] > maxDigit)
                {
                    maxDigit = digits[i];
                }

                if (digits[i] < minDigit)
                {
                    minDigit = digits[i];
                }

            }

            for (int i = 0; i < digits.Count; i++)
            {
                if (digits[i] == maxDigit)
                {
                    cifri += ($"Максимальное число: {maxDigit}, на позиции {i + 1}\n");
                }

                if (digits[i] == minDigit)
                {
                    cifri += ($"Минимальное число: {minDigit}, на позиции: {i + 1}\n");
                }
            }
            return cifri;

        }
    }
}


