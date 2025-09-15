using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace Complex_Practica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists("C:\\Users\\denis\\source\\data.txt"))
            {
                File.Create("C:\\Users\\denis\\source\\data.txt").Close();
            }

        }
        public void CreateDataFile(string file = "C:\\Users\\denis\\source\\data.txt")
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

                Sequence.Text = data;
                CheckSequence(digits);

            }

        }
        public void InputResFile(string DataFile)
        {
            string IntFile = "C:\\Users\\denis\\source\\InputData.txt";
            
             File.WriteAllText(IntFile, DataFile.ToString());
            
            
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

                if (Up && Down)
                {
                    Sequence.Text = ("Постоянная");

                }
                else if (Up)
                {
                    Sequence.Text = ("Возврастающая");
                }
                else if (Down)
                {
                    Sequence.Text = ("Убывающая");
                }
                else
                {
                    Sequence.Text = ("Никакая");
                }

                string data = Sequence.Text;
                InputResFile(data);

            }
        }
        public void Initialization(string path = "C:\\Users\\denis\\source\\data.txt")
        {
            CreateDataFile(path);
        }

        private void Open_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Все файлы (*.*)|*.*|Текстовые файлы (*.txt)|*.txt|Документы (*.docx;*.pdf)|*.docx;*.pdf";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                path.Text = selectedFilePath;


            }
        }

        private void to_know_Click(object sender, RoutedEventArgs e)
        {
            if (path.Text != "")
            {
                string selectedFilePath = path.Text;
                Initialization(selectedFilePath);
            }
            else
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   "Введи путь",
                   "ЗАКРЫТЬ",
                   () => { /* Действие при нажатии на кнопку */ });
            }
        }
    }
}
