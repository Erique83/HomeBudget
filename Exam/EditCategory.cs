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
    // Редактирование категории
    public partial class EditCategory : Form
    {
        public EditCategory()
        {
            InitializeComponent();
        }

        // Кнопка "ОК"
        private void button1_Click(object sender, EventArgs e)
        {
            Categories f5 = (Categories)this.Owner;

            int index = f5.listView1.SelectedIndices[0];

            f5.listView1.Items[index].SubItems[0].Text = Convert.ToString(textBox1.Text);
            this.Close();
        }
    }
}
