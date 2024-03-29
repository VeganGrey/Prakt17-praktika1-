﻿using PrimerBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Prakt17_praktika1_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WorkerCashEntities3 db = WorkerCashEntities3.GetContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.EmployeeCashes.Load();
            DataGrid1.ItemsSource = db.EmployeeCashes.Local.ToBindingList();
        }

        private void AddEmployee_Button(object sender, RoutedEventArgs e)
        {
            AddEmployee add = new AddEmployee();
            add.ShowDialog();
            DataGrid1.Focus();
        }

        private void EditEmployee_Button(object sender, RoutedEventArgs e)
        {
            int indexRow = DataGrid1.SelectedIndex;
            if (indexRow != -1)
            {
                EmployeeCash row = (EmployeeCash)DataGrid1.Items[indexRow];
                Data.Id = row.Id;
                EditEmployee edit = new EditEmployee();
                edit.ShowDialog();
                DataGrid1.Items.Refresh();
                DataGrid1.Focus();
            }
            else MessageBox.Show("Не выбрано");
        }

        private void Spravka(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сведения о месячной зарплате рабочих. База данных должна содержать следующую\n"+
                "информацию: фамилию, имя, отчество рабочего, название цеха, в котором он работает, дату поступления на работу. По заработной плате необходимо хранить информацию о ее\n"+
                "размере, стаже работника, его разряде и должности.","Задание");
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteEmployee_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Удалить запись???", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(res == MessageBoxResult.Yes)
            {
                try
                {
                    EmployeeCash row = (EmployeeCash)DataGrid1.SelectedItems[0];
                    db.EmployeeCashes.Remove(row);
                    db.SaveChanges();
                    DataGrid1.Items.Refresh();
                }
                catch(ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберите запись");
                }
            }
        }

        private void FindEmployee_Button(object sender, RoutedEventArgs e)
        {
            if (txtFind.Text.Length == 0)
            {
                MessageBox.Show("Введите слово");
                return;
            }
            for(int i=0;i<DataGrid1.Items.Count;i++)
            {
                var row = (EmployeeCash)DataGrid1.Items[i];
                string findContent = row.LastName;
                try
                {
                    if(findContent != null && findContent.Contains(txtFind.Text))
                    {
                        object item = DataGrid1.Items[i];
                        DataGrid1.SelectedItem = item;
                        DataGrid1.ScrollIntoView(item);
                        DataGrid1.Focus();
                        break;
                    }
                }
                catch
                {
                    MessageBox.Show("Не найдено совпадений");
                }
            }
        }

        List<EmployeeCash> _employee;

        private void FilterEmployee_Button(object sender, RoutedEventArgs e)
        {
            if (txtFind.Text.Length == 0)
            {
                MessageBox.Show("Введите слово");
                return;
            }
            _employee = db.EmployeeCashes.ToList();
            var filtered = _employee.Where(_employee => _employee.LastName == txtFiltered.Text);
            DataGrid1.ItemsSource = filtered;
        }

        private void ClearFilteredEmployee_Button(object sender, RoutedEventArgs e)
        {
            DataGrid1.ItemsSource = db.EmployeeCashes.Local.ToBindingList();
        }

        private void ReFindEmployee_Button(object sender, RoutedEventArgs e)
        {
            if (txtFind.Text.Length == 0)
            {
                MessageBox.Show("Введите слово");
                return;
            }
            for (int i = 0; i < DataGrid1.Items.Count; i++)
            {
                var row = (EmployeeCash)DataGrid1.Items[i];
                string findContent = row.LastName;
                try
                {
                    if (findContent != null && findContent.Contains(txtFind.Text))
                    {
                        object item = DataGrid1.Items[i];
                        DataGrid1.SelectedItem = item;
                        DataGrid1.ScrollIntoView(item);
                        row.LastName = txtRefind.Text;
                        db.SaveChanges();
                        DataGrid1.Items.Refresh();
                        DataGrid1.Focus();
                        break;
                    }
                }
                catch
                {
                    MessageBox.Show("Не найдено совпадений");
                }
            }
        }
    }
}
namespace PrimerBD
{
    public static class Data
    {
        public static int Id;
    }
}
