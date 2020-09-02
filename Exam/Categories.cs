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
    // Категории
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
        }

        // Добавление
        private void button1_Click(object sender, EventArgs e)
        {
            MainForm f1 = (MainForm)this.Owner;

            // Добавление новой категории
            f1.categories.Add(Convert.ToString(textBox1.Text));
            listView1.Items.Add(Convert.ToString(textBox1.Text));
            textBox1.Clear();
        }

        // Удаление
        private void button2_Click(object sender, EventArgs e)
        {
            MainForm f1 = (MainForm)this.Owner;

            try
            {
                if (f1.categories.Count > 0)
                {
                    // Индекс нажатой строки
                    int index = listView1.SelectedIndices[0];

                    // Удаление строки по индексу
                    listView1.Items.RemoveAt(index);

                    // Удаление из List по индексу
                    f1.categories.RemoveAt(index);
                }
                else
                {
                    MessageBox.Show("А удалять-то нечего!", "Внимание!", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Выберите категорию!");
            }
        }

        // Редактирование
        private void button3_Click(object sender, EventArgs e)
        {
            MainForm f1 = (MainForm)this.Owner;

            try
            {
                if (f1.categories.Count > 0)
                {
                    int index = listView1.SelectedIndices[0];

                    EditCategory form7 = new EditCategory();
                    form7.Owner = this;
                    form7.Show();

                    form7.textBox1.Text = Convert.ToString(f1.categories[index]);
                }
                else
                {
                    MessageBox.Show("А редактировать-то нечего!", "Внимание!", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Выберите члена семьи!");
            }
        }

        // Закрытие
        private void button4_Click(object sender, EventArgs e)
        {
            MainForm f1 = (MainForm)this.Owner;
            // Очищаем список клиентов
            f1.categories.Clear();

            // Заполняем список клиентов новыми значениями
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                f1.categories.Add(listView1.Items[i].Text);
            }

            this.Close();
        }
    }
}
