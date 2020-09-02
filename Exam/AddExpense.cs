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
    // Добавление расхода
    public partial class AddExpense : Form
    {
        public AddExpense()
        {
            InitializeComponent();
        }

        // Кнопка "Добавить"
        private void button1_Click(object sender, EventArgs e)
        {
            MainForm f1 = (MainForm)this.Owner;

            decimal cost = 0; ;
            try
            {
                cost = Convert.ToDecimal(textBox2.Text);
            
            // Добавление расхода из формы 2 в List
            f1.expenses.Add(new Expense(cost, DateTime.Now, Convert.ToString(textBox1.Text), Convert.ToString(comboBox1.Text), Convert.ToString(comboBox2.Text), Convert.ToString(textBox3.Text)));

            // Добавление строки с расходом в основную форму
            ListViewItem item = new ListViewItem(Convert.ToString(Expense.nextId));
            item.SubItems.Add(Convert.ToString(textBox1.Text));
            item.SubItems.Add(Convert.ToString(textBox2.Text));
            item.SubItems.Add(Convert.ToString(DateTime.Now));
            item.SubItems.Add(Convert.ToString(comboBox1.Text));
            item.SubItems.Add(Convert.ToString(comboBox2.Text));
            item.SubItems.Add(Convert.ToString(textBox3.Text));

            f1.listView1.Items.Add(item);
            
            // Вычисление общей суммы в основной форме
            f1.sum += cost;

            // Вывод общей суммы в основной форме
            f1.label2.Text = Convert.ToString(f1.sum);

            this.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Введите правильные данные в поле Сумма");
            }
        }

        // Кнопка "Отменить"
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
