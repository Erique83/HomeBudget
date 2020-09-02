using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    // Статистика
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
        }

        // Кнопка "Посчитать"
        private void button1_Click(object sender, EventArgs e)
        {
            MainForm f1 = (MainForm)this.Owner;

            decimal sum = 0;
            foreach (var item in f1.expenses)
            {
                if (item.Date.DayOfYear >= dateTimePicker1.Value.DayOfYear && item.Date.DayOfYear <= dateTimePicker2.Value.DayOfYear)
                {
                    sum += item.Sum;
                }
            }
            label4.Text = Convert.ToString(sum) + " руб.";
        }
    }
}
