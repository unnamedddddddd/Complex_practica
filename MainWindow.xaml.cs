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
            string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..","..",".."));
            string directoryPath = System.IO.Path.Combine(sourcePath, "Test");
            Directory.CreateDirectory(directoryPath);
            string filePath = System.IO.Path.Combine(directoryPath, "data.txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

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
                string data = File.ReadAllText(path.Text).Trim();
                TextFile.Text = data;            

            }
        }
        private void to_know_Click(object sender, RoutedEventArgs e)
        {
            string selectedFilePath = path.Text;
            bool krasnukovChecked = Krasnukov.IsChecked ?? false;
            bool kamaldinovChecked = Sequence.IsChecked ?? false;
            bool proskuryakovChecked = Proskuryakov.IsChecked ?? false;
            bool poryvaevChecked = Poryvaev.IsChecked ?? false;

            StringBuilder selectedItems = new StringBuilder();
            foreach (var child in PanelBox.Children)
            {
                if (child is CheckBox checkBox && checkBox.IsChecked == true)
                {
                    selectedItems.AppendLine($"{checkBox.Content} выбран");
                }
            }
            if (path.Text != "" && selectedItems.Length > 0)
            {
                if (kamaldinovChecked)
                {
                    Kamaldinov kamal = new Kamaldinov();
                    kamal.CreateDataFile(selectedFilePath);

                }
            }
            else if(path.Text == "")
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   "Введите путь",
                   "ЗАКРЫТЬ",
                   () => { /*Можно добавить действие(можете подумать что)*/});
            }
            else
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   "Выберите действие",
                   "ЗАКРЫТЬ",
                   () => { /*Можно добавить действие(можете подумать что)*/});
            }
           
        }
        private void SaveFile(object sender, RoutedEventArgs e)
        {

            string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            string directoryPath = System.IO.Path.Combine(sourcePath, "Test");
            string filePath = System.IO.Path.Combine(directoryPath, "data.txt");
            File.WriteAllText(filePath,TextFile.Text.ToString());



        }

    }
}
