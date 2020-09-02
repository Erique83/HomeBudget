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
    // Обновление расхода
    public partial class EditExpense : Form
    {
        public EditExpense()
        {
            InitializeComponent();
        }

        // Кнопка "Обновить"
        private void button1_Click(object sender, EventArgs e)
        {
            MainForm f1 = (MainForm)this.Owner;

            int index = f1.listView1.SelectedIndices[0];

            decimal newCost = 0;

            try
            {
                newCost = Convert.ToDecimal(textBox2.Text);

                // Вычисление общей суммы в основной форме
                f1.sum += newCost - f1.expenses[index].Sum;

                // Вывод общей суммы в основной форме
                f1.label2.Text = Convert.ToString(f1.sum);

                // Обновление данных в List<Expense> и в ListView
                f1.expenses[index].Name = Convert.ToString(textBox1.Text);
                f1.listView1.Items[index].SubItems[1].Text = Convert.ToString(textBox1.Text);

                f1.expenses[index].Sum = newCost;
                f1.listView1.Items[index].SubItems[2].Text = Convert.ToString(textBox2.Text);

                f1.expenses[index].Client = Convert.ToString(comboBox1.Text);
                f1.listView1.Items[index].SubItems[4].Text = Convert.ToString(comboBox1.Text);

                f1.expenses[index].Category = Convert.ToString(comboBox2.Text);
                f1.listView1.Items[index].SubItems[5].Text = Convert.ToString(comboBox2.Text);

                f1.expenses[index].Description = Convert.ToString(textBox3.Text);
                f1.listView1.Items[index].SubItems[6].Text = Convert.ToString(textBox3.Text);

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
