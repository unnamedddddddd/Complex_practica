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
        public void CreateDataFile(string file)
        {
            if (!File.Exists(file))

            {
                File.Create(file).Close();
                OutputDataFile(file);
            }
            else
            {
                OutputDataFile(file);
            }
        }
        public void OutputDataFile(string file)
        {
            using (FileStream fl = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                string data = File.ReadAllText(file).Trim();

                List<int> digits = new List<int>();

                foreach (char c in data)
                {
                    if (char.IsDigit(c))
                    {
                        digits.Add(c - '0');
                    }
                }


                CheckSequence(digits);

            }

        }
        public void InputResFile(string DataFile)
        {
            string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            string directoryPath = System.IO.Path.Combine(sourcePath, "Test");
            string filePath = System.IO.Path.Combine(directoryPath, "InputData.txt");
            File.WriteAllText(filePath, DataFile.ToString());

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    ((MainWindow)window).TextInputFile.Text = DataFile;
                }
            }
            
        }
        public void CheckSequence(List<int> digits)
        {
            bool Up = true;
            bool Down = true;

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

                string data;
                if (Up && Down)
                {
                    data = "Постоянная";

                }
                else if (Up)
                {
                    data = "Возврастающая";
                }
                else if (Down)
                {
                    data = "Убывающая";
                }
                else
                {
                    data = "Никакая";
                }

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        ((MainWindow)window).TextSequence.Text = data;
                    }
                }

                InputResFile(data);
            }
        }
    }
}
