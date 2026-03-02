using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Practica
{
    class ChernyakovCheckBox
    {
        public string CheckPalindrome(List<int> digits)
        {
            try
            {
                if (digits == null || digits.Count == 0)
                    return "Ошибка: нет данных";

                List<int> reversedDigits = new List<int>(digits);
                reversedDigits.Reverse();

                bool isPalindrome = true;
                for (int i = 0; i < digits.Count; i++)
                {
                    if (digits[i] != reversedDigits[i])
                    {
                        isPalindrome = false;
                        break;
                    }
                }

                if (isPalindrome)
                    return "Палиндром: да\n";
                else
                    return "Палиндром: нет\n";
            }
            catch (Exception)
            {
                return "Ошибка при проверке палиндрома\n";
            }
        }
    }
}
