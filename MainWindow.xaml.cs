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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            string testDirectoryPath = System.IO.Path.Combine(sourcePath, "Test");
            Directory.CreateDirectory(testDirectoryPath);

            string filePath = System.IO.Path.Combine(testDirectoryPath, "data.txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            CreateTestFolders(testDirectoryPath);      
        }

        private void CreateTestFolders(string testDirectoryPath)
        {
            string[] studentFolders = { "Камальдинов", "Краснюков", "Порываев", "Проскуряков", "Черняков" };

            foreach (string student in studentFolders)
            {
                string studentPath = System.IO.Path.Combine(testDirectoryPath, student);
                Directory.CreateDirectory(studentPath);

                string studentDataPath = System.IO.Path.Combine(studentPath, "data.txt");
                string studentInputDataPath = System.IO.Path.Combine(studentPath, "InputData.txt");

                if (!File.Exists(studentDataPath))
                {
                    File.Create(studentDataPath).Close();
                }
                if (!File.Exists(studentInputDataPath))
                {
                    File.Create(studentInputDataPath).Close();
                }
            }
        }

        private void Open_file_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Выберите файл";
                openFileDialog.Filter = "Все файлы (*.*)|*.*|Текстовые файлы (*.txt)|*.txt|Документы (*.docx;*.pdf)|*.docx;*.pdf";
                openFileDialog.FilterIndex = 2; 
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    path.Text = selectedFilePath;

                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    if (fileInfo.Length > 1048576) 
                    {
                        NotificationSnackbar.MessageQueue.Enqueue(
                           "Файл слишком большой (макс. 1 МБ)",
                           "ОК",
                           () => { });
                        return;
                    }

                    string data = File.ReadAllText(path.Text).Trim();

                    if (!System.Text.RegularExpressions.Regex.IsMatch(data, @"^\d+$"))
                    {
                        NotificationSnackbar.MessageQueue.Enqueue(
                           "Файл должен содержать только цифры",
                           "ОК",
                           () => { });
                        return;
                    }

                    if (data.Length > 3)
                    {
                        NotificationSnackbar.MessageQueue.Enqueue(
                           "Число слишком длинное (макс. 100 цифр)",
                           "ОК",
                           () => { });
                        return;
                    }

                    TextFile.Text = data;
                }
            }
            catch (Exception ex)
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   $"Ошибка при открытии файла: {ex.Message}",
                   "ЗАКРЫТЬ",
                   () => { });
            }
        }

        private void to_know_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedFilePath = path.Text;

                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    NotificationSnackbar.MessageQueue.Enqueue(
                       "Сначала выберите файл",
                       "ЗАКРЫТЬ",
                       () => { });
                    return;
                }

                if (!File.Exists(selectedFilePath))
                {
                    NotificationSnackbar.MessageQueue.Enqueue(
                       "Файл не существует",
                       "ЗАКРЫТЬ",
                       () => { });
                    return;
                }

                bool krasnukovChecked = Krasnukov.IsChecked ?? false;
                bool kamaldinovChecked = Kamaldinov.IsChecked ?? false;
                bool proskuryakovChecked = Proskuryakov.IsChecked ?? false;
                bool poryvaevChecked = Poryvaev.IsChecked ?? false;
                bool chernyakovChecked = Chernyakov.IsChecked ?? false;

                if (!krasnukovChecked && !kamaldinovChecked && !proskuryakovChecked &&
                    !poryvaevChecked && !chernyakovChecked)
                {
                    NotificationSnackbar.MessageQueue.Enqueue(
                       "Выберите хотя бы одну функцию",
                       "ЗАКРЫТЬ",
                       () => { });
                    return;
                }

                string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                string testDirectoryPath = System.IO.Path.Combine(sourcePath, "Test");

                string generalInputFilePath = System.IO.Path.Combine(testDirectoryPath, "InputData.txt");
                File.WriteAllText(generalInputFilePath, string.Empty);
                TextInputFile.Text = string.Empty;

                List<int> digits = OutputDataFile(selectedFilePath);

                if (digits.Count == 0)
                {
                    NotificationSnackbar.MessageQueue.Enqueue(
                       "Файл не содержит цифр",
                       "ЗАКРЫТЬ",
                       () => { });
                    return;
                }

                StringBuilder selectedItems = new StringBuilder();
                int processedCount = 0;

                foreach (var child in PanelBox.Children)
                {
                    if (child is CheckBox checkBox && checkBox.IsChecked == true)
                    {
                        selectedItems.AppendLine($"{checkBox.Content} выбран");
                    }
                }

                string generalDataPath = System.IO.Path.Combine(testDirectoryPath, "data.txt");
                File.WriteAllText(generalDataPath, TextFile.Text);

                if (kamaldinovChecked)
                {
                    try
                    {
                        Kamaldinov kamal = new Kamaldinov();
                        string data = kamal.CheckSequence(digits);
                        SaveToTestFolder(testDirectoryPath, "Камальдинов", TextFile.Text, data);
                        InputResFile(data, "Камальдинов");
                        processedCount++;
                    }
                    catch (Exception ex)
                    {
                        NotificationSnackbar.MessageQueue.Enqueue(
                           $"Ошибка в функции Камальдинова: {ex.Message}",
                           "ОК",
                           () => { });
                    }
                }

                if (krasnukovChecked)
                {
                    try
                    {
                        Krasnukov kras = new Krasnukov();
                        string data = kras.CheckMaxMin(digits);
                        SaveToTestFolder(testDirectoryPath, "Краснюков", TextFile.Text, data);
                        InputResFile(data, "Краснюков");
                        processedCount++;
                    }
                    catch (Exception ex)
                    {
                        NotificationSnackbar.MessageQueue.Enqueue(
                           $"Ошибка в функции Краснюкова: {ex.Message}",
                           "ОК",
                           () => { });
                    }
                }

                if (proskuryakovChecked)
                {
                    try
                    {
                        ProskuryakovCheckBox prosk = new ProskuryakovCheckBox();
                        string data = prosk.CheckParity(digits);
                        SaveToTestFolder(testDirectoryPath, "Проскуряков", TextFile.Text, data);
                        InputResFile(data, "Проскуряков");
                        processedCount++;
                        
                    }
                    catch (Exception ex)
                    {
                        NotificationSnackbar.MessageQueue.Enqueue(
                           $"Ошибка в функции Проскурякова: {ex.Message}",
                           "ОК",
                           () => { });
                    }
                }

                if (poryvaevChecked)
                {
                    NotificationSnackbar.MessageQueue.Enqueue(
                       "Функция Порываева еще не реализована",
                       "ОК",
                       () => { });
                }

                if (chernyakovChecked)
                {
                    NotificationSnackbar.MessageQueue.Enqueue(
                       "Функция Чернякова еще не реализована",
                       "ОК",
                       () => { });
                }

            }
            catch (Exception ex)
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   $"Общая ошибка: {ex.Message}",
                   "ЗАКРЫТЬ",
                   () => { });
            }
        }

        private void SaveToTestFolder(string testDirectoryPath, string studentName, string inputData, string resultData)
        {
            try
            {
                string studentPath = System.IO.Path.Combine(testDirectoryPath, studentName);
                Directory.CreateDirectory(studentPath);

                string studentDataPath = System.IO.Path.Combine(studentPath, "data.txt");
                File.WriteAllText(studentDataPath, inputData);

                string studentResultPath = System.IO.Path.Combine(studentPath, "InputData.txt");
                File.WriteAllText(studentResultPath, resultData);
            }
            catch (Exception ex)
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   $"Ошибка при сохранении в папку {studentName}: {ex.Message}",
                   "ОК",
                   () => { });
            }
        }

        public List<int> OutputDataFile(string filePath)
        {
            List<int> digits = new List<int>();
            try
            {
                string data = File.ReadAllText(filePath).Trim();
                foreach (char c in data)
                {
                    if (char.IsDigit(c))
                    {
                        digits.Add(int.Parse(c.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   $"Ошибка при чтении файла: {ex.Message}",
                   "ОК",
                   () => { });
            }
            return digits;
        }

        public void InputResFile(string DataFile, string functionName)
        {
            try
            {
                string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                string directoryPath = System.IO.Path.Combine(sourcePath, "Test");
                string filePath = System.IO.Path.Combine(directoryPath, "InputData.txt");

                File.AppendAllText(filePath, DataFile + Environment.NewLine);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        ((MainWindow)window).TextInputFile.Text += DataFile + Environment.NewLine;
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   $"Ошибка при записи результата: {ex.Message}",
                   "ОК",
                   () => { });
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextFile.Text))
                {
                    NotificationSnackbar.MessageQueue.Enqueue(
                       "Нет данных для сохранения",
                       "ОК",
                       () => { });
                    return;
                }

                string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                string directoryPath = System.IO.Path.Combine(sourcePath, "Test");
                string filePath = System.IO.Path.Combine(directoryPath, "data.txt");

                File.WriteAllText(filePath, TextFile.Text.ToString());

                NotificationSnackbar.MessageQueue.Enqueue(
                   "Файл успешно сохранен",
                   "ОК",
                   () => { });
            }
            catch (Exception ex)
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   $"Ошибка при сохранении: {ex.Message}",
                   "ЗАКРЫТЬ",
                   () => { });
            }
        }

        private void TextInputFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextInputFile != null)
            {
                TextInputFile.ScrollToEnd();
            }
        }
    }
}