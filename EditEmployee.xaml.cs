﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using PrimerBD;

namespace Prakt17_praktika1_
{
    /// <summary>
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        public EditEmployee()
        {
            InitializeComponent();
        }

        WorkerCashEntities3 db = WorkerCashEntities3.GetContext();
        EmployeeCash employee;
        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditEmployee_Button(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (LastName.Text.Length == 0) errors.Append("Введите фамилию");
            if (DataPicker1.Text.Length == 0) errors.Append("Введите дату");
            if (CashSumm.Text.Length == 0 || Int32.TryParse(CashSumm.Text, out int cash) == false) errors.AppendLine("Введите зарплату");
            if (Experience.Text.Length == 0 || Int32.TryParse(Experience.Text, out int experience) == false) errors.AppendLine("Введите Стаж");
            if (Category.Text.Length == 0 || Int32.TryParse(Category.Text, out int category) == false) errors.AppendLine("Введите разряд");
            if (Post.Text.Length == 0) errors.AppendLine("Введите должность");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            employee.LastName = LastName.Text;
            employee.FirstName = FirstName.Text;
            employee.SecondName = SecondName.Text;
            employee.PodrazdName = PodrazdName.Text;
            employee.DateWork = (DateTime)DataPicker1.SelectedDate;
            employee.CashSumm = Convert.ToInt32(CashSumm.Text);
            employee.Experience = Convert.ToInt32(Experience.Text);
            employee.Category = Convert.ToInt32(Category.Text); ;
            employee.Post = Post.Text;

            try
            {
                db.SaveChanges();
                MessageBox.Show("Успешно изменено");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            employee = db.EmployeeCashes.Find(Data.Id);
            LastName.Text = employee.LastName;
            FirstName.Text = employee.FirstName;
            SecondName.Text = employee.SecondName;
            PodrazdName.Text = employee.PodrazdName;
            DataPicker1.SelectedDate = (DateTime)employee.DateWork;
            CashSumm.Text = Convert.ToString(employee.CashSumm);
            Experience.Text = Convert.ToString(employee.Experience);
            Category.Text= Convert.ToString(employee.Category); ;
            Post.Text = employee.Post ;
        }
    }
}
