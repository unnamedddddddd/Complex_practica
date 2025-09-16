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
            string filePath = System.IO.Path.Combine(sourcePath, "data.txt");
            MessageBox.Show(filePath);
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

            }
        }

        private void to_know_Click(object sender, RoutedEventArgs e)
        {
            if (path.Text != "")
            {
                string selectedFilePath = path.Text;
                bool krasnukovChecked = Krasnukov.IsChecked ?? false;
                bool kamaldinovChecked = Sequence.IsChecked ?? false;
                bool proskuryakovChecked = Proskuryakov.IsChecked ?? false;
                bool poryvaevChecked = Poryvaev.IsChecked ?? false;

                if (kamaldinovChecked)  
                {
                    Kamaldinov kamal = new Kamaldinov();
                    kamal.CreateDataFile(selectedFilePath);

                }
            }
            else
            {
                NotificationSnackbar.MessageQueue.Enqueue(
                   "Введи путь",
                   "ЗАКРЫТЬ",
                   () => { /*Можно добавить действие(можете подумать что)*/});
            }
        }
    }
}
