using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //сохранение результата\ключа
        public void saveFile_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "Text";
            s.Filter = "Text Documents (.txt)|*.txt*";
            Nullable<bool> result = s.ShowDialog();
            if (result == true)
            {
                StreamWriter streamWriter = new StreamWriter(s.FileName);
                streamWriter.WriteLine(finalText.Text);
                streamWriter.Close();
            }
        } 
        //загрузка файла с тектом
        public void uploadFile_Click(object sender, System.EventArgs e)
        {
            string filePath;
            string fileContent;
            OpenFileDialog o = new OpenFileDialog();
            Nullable<bool> result = o.ShowDialog();
            if (result == true)
            {
                filePath = o.FileName;
                var fileStream = o.OpenFile();
                StreamReader reader = new StreamReader(fileStream);
                fileContent = reader.ReadToEnd();
                startText.Text = fileContent;
            }
        }
        //шифрование текста
        public void encrypt_Click(object sender, System.EventArgs e)
        {
            string s = (startText.Text).ToUpper();
            string k = (Key.Text).ToUpper();
            bool show = true;
            Error err = new Error();
            Cipher c = new Cipher("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");
            for (int i=0; i<k.Length;i++)
            {

                var a = c.letters.IndexOf(k[i]);
                if (a < 0)
                {
                    err.Show();
                    show = false;
                    break;
                }
            }
            if (show)
            {

            var text = c.Encrypt(s, k);
            finalText.Text = text;
            }
        }
        //дешифрование текста
        public void decrypt_Click(object sender, System.EventArgs e)
        {
            string s = (startText.Text).ToUpper();
            string k = (Key.Text).ToUpper();
            bool show = true;
            Error err = new Error();
            Cipher c = new Cipher("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");
            for (int i = 0; i < k.Length; i++)
            {
                var a = c.letters.IndexOf(k[i]);
                if (a < 0)
                {
                    err.Show();
                    show = false;
                    break;
                }
            }
            if (show)
            {

                var text = c.Decrypt(s, k);
                finalText.Text = text;
            }
        }
    }
}