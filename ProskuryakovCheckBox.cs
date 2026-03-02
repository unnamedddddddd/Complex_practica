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
                return "Ошибка: список цифр пуст";
            }

            bool hasEven = false; 
            bool hasOdd = false;  

            foreach (int digit in digits)
            {
                if (digit % 2 == 0)
                    hasEven = true;
                else
                    hasOdd = true;

                if (hasEven && hasOdd)
                    break;
            }

            if (hasEven && !hasOdd)
                return "Все цифры четные";
            if (!hasEven && hasOdd)
                return "Все цифры нечетные";
            return "Четность: " + "Цифры разной четности\n";
        }
    }
}
