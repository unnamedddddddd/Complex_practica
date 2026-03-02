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
        public string CheckParity(List<int> digits)
        {
            if (digits == null || digits.Count == 0)
            {
                return "Ошибка: список цифр пуст\n";
            }

            foreach (int digit in digits)
            {
                if (digit % 2 == 0)
                {
                    return "Произведение цифр: четное\n";
                }
            }

            return "Произведение цифр: нечетное\n";
        }
    }
}
