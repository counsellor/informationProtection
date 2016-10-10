﻿using System;
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
using Microsoft.Win32;

namespace WpfInformProtection
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

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private static int smesh = (int)'а';
        private static int alphLength = 32;
        public string inputText;
        public static string Encrypt (string text, string keyWord)
        {
            var ans = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                if (!text[i].Equals(' '))
                {
                    var num = ((text[i] + keyWord[i%keyWord.Length])%alphLength);
                    var c = (char) (num + smesh);
                    ans.Append(c);
                }
                else
                {
                    ans.Append(' ');
                }
            }
            return ans.ToString();
        }

        public static string Decode(string text, string keyWord)
        {
            var ans = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                if (!text[i].Equals(' '))
                {
                    var num = ((text[i] - keyWord[i%keyWord.Length] + alphLength)%alphLength);
                    var c = (char) (num + smesh);
                    ans.Append(c);
                }
                else
                {
                    ans.Append(' ');
                }
            }
            return ans.ToString();
        }

        private void bGo_Click(object sender, RoutedEventArgs e)
        {
            if (rbEncrypt.IsChecked != null && rbEncrypt.IsChecked.Value)
            {
                tbOutput.Text = Encrypt(tbInput.Text, "ключ");
            }
            else
            {
                if (rbDecrypt.IsChecked != null && rbDecrypt.IsChecked.Value)
                {
                    tbOutput.Text = Decode(tbInput.Text, "ключ");
                }
            }
           
        }

        private void bFileIn_Click(object sender, RoutedEventArgs e)
        {
            var myDialog = new OpenFileDialog();
            myDialog.Filter = "Все файлы (*)|*.txt*";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;

            if (myDialog.ShowDialog() == true)
            {
                tbInput.Text = myDialog.FileName.ToString().Split('\\').Last();
                Stream myStream = null;

                if ((myStream = myDialog.OpenFile()) != null)
                {
                    var file = new StreamReader(myStream);
                    var counter = 0;
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        counter++;
                        if (counter > 1)
                        {
                            tbInput.Text = "";
                            MessageBox.Show("Error. Should be one string");
                            return;
                        }
                        inputText = line;
                    }
                }
                myStream.Close();
            }
        }
    }
}
