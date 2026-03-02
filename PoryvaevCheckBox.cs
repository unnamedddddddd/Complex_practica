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
        public string CheckDigitParity(List<int> digits)
        {
            try
            {
                if (digits == null || digits.Count == 0)
                    return "Ошибка: нет данных";

                bool allEven = true;
                bool allOdd = true;

                foreach (int digit in digits)
                {
                    if (digit % 2 == 0)
                        allOdd = false;
                    else
                        allEven = false;
                }

                if (allEven)
                    return "Четность: все цифры четные\n";
                else if (allOdd)
                    return "Четность: все цифры нечетные\n";
                else
                    return "Четность: цифры разной четности\n";
            }
            catch (Exception)
            {
                return "Ошибка при проверке четности\n";
            }
            
        }
    }
}
