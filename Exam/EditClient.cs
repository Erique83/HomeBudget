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
    // Редактирование члена семьи
    public partial class EditClient : Form
    {
        public EditClient()
        {
            InitializeComponent();
        }

        // Кнопка "ОК"
        private void button1_Click(object sender, EventArgs e)
        {
            Clients f4 = (Clients)this.Owner;

            int index = f4.listView1.SelectedIndices[0];

            f4.listView1.Items[index].SubItems[0].Text = Convert.ToString(textBox1.Text);
            this.Close();
        }
    }
}
