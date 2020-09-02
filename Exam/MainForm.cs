using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Exam
{
    public partial class MainForm : Form
    {
        // Расходы
        public List<Expense> expenses = new List<Expense>();

        // Сумма расходов
        public decimal sum = 0;

        // Пути к XML-файлам
        string fileName = "data.xml";
        string fileName2 = "clients.xml";
        string fileName3 = "categories.xml";

        // Список членов семьи
        public List<string> clients = new List<string>();
        // Список категорий
        public List<string> categories = new List<string>();

        public MainForm()
        {
            InitializeComponent();

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;

            listView1.View = View.Details;
            // программное создание столбцов
            listView1.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Название", 150, HorizontalAlignment.Right);
            listView1.Columns.Add("Сумма", 70, HorizontalAlignment.Right);
            listView1.Columns.Add("Дата/Время", 120, HorizontalAlignment.Center);
            listView1.Columns.Add("Клиент", 150, HorizontalAlignment.Right);
            listView1.Columns.Add("Категория", 150, HorizontalAlignment.Right);
            listView1.Columns.Add("Описание", 300, HorizontalAlignment.Right);

            // Список расходов
            if(File.Exists(fileName) && File.Exists(fileName2) && File.Exists(fileName3))
            {
                // Загрузка данныз их XML-файлов
                expenses = LoadXML();
                clients = LoadXMLClients();
                categories = LoadXMLCategories();

                // Привязка данных к ComboBox
                foreach (var client in clients)
                {
                    comboBox1.Items.Add(client);
                }
                comboBox1.Text = "Все";

                // Привязка данных к ComboBox
                foreach (var cat in categories)
                {
                    comboBox2.Items.Add(cat);
                }
                comboBox2.Text = "Все";

                // Добавление строк с расходом в основную форму
                foreach (var expense in expenses)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(expense.Id));
                    item.SubItems.Add(Convert.ToString(expense.Name));
                    item.SubItems.Add(Convert.ToString(expense.Sum));
                    item.SubItems.Add(Convert.ToString(expense.Date));
                    item.SubItems.Add(Convert.ToString(expense.Client));
                    item.SubItems.Add(Convert.ToString(expense.Category));
                    item.SubItems.Add(Convert.ToString(expense.Description));

                    listView1.Items.Add(item);

                    // Общая сумма
                    sum += expense.Sum;
                }
                label2.Text = Convert.ToString(sum);
            }
            else
            {
                MessageBox.Show("Файлы данных не найдены!");
            }
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddExpense form2 = new AddExpense();
            form2.Owner = this;
            form2.Show();

            // Привязка данных к ComboBox
            foreach (var client in clients)
            {
                form2.comboBox1.Items.Add(client);
                form2.comboBox1.Text = client;
            }

            // Привязка данных к ComboBox
            foreach (var cat in categories)
            {
                form2.comboBox2.Items.Add(cat);
                form2.comboBox2.Text = cat;
            }
        }

        // Выход с сохранением
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveXML(expenses);
            SaveXMLClients(clients);
            SaveXMLCategories(categories);
            this.Close();
        }

        // Редактирование расхода
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (expenses.Count > 0)
                {
                    int index = listView1.SelectedIndices[0];

                    EditExpense form3 = new EditExpense();
                    form3.Owner = this;
                    form3.Show();

                    form3.textBox1.Text = Convert.ToString(expenses[index].Name);
                    form3.textBox2.Text = Convert.ToString(expenses[index].Sum);
                    form3.comboBox1.Text = Convert.ToString(expenses[index].Client);
                    form3.comboBox2.Text = Convert.ToString(expenses[index].Category);
                    form3.textBox3.Text = Convert.ToString(expenses[index].Description);
                }
                else
                {
                    MessageBox.Show("А редактировать-то нечего!", "Внимание!", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Выберите строку расхода!");
            }
        }

        // Удаление расхода
        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (expenses.Count > 0)
                {
                    // Индекс нажатой строки
                    int index = listView1.SelectedIndices[0];

                    // Изменение общей суммы
                    sum -= expenses[index].Sum;
                    label2.Text = Convert.ToString(sum);

                    // Удаление строки по индексу
                    listView1.Items.RemoveAt(index);

                    // Удаление из List по индексу
                    expenses.RemoveAt(index);
                }
                else
                {
                    MessageBox.Show("А удалять-то нечего!", "Внимание!", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Выберите строку расхода!");
            }
        }

        // Форма для работы с членами семьи
        private void ClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients form4 = new Clients();
            form4.Owner = this;
            form4.Show();

            // Вывод членов семьи
            form4.listView1.View = View.Details;
            // программное создание столбцов
            form4.listView1.Columns.Add("Член семьи", 150, HorizontalAlignment.Left);

            foreach (var client in clients)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(client));

                form4.listView1.Items.Add(item);
            }
        }

        // Форма для работы с категориями
        private void CategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categories form5 = new Categories();
            form5.Owner = this;
            form5.Show();

            // Вывод категорий
            form5.listView1.View = View.Details;
            // программное создание столбцов
            form5.listView1.Columns.Add("Категория расхода", 150, HorizontalAlignment.Left);

            foreach (var category in categories)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(category));

                form5.listView1.Items.Add(item);
            }

        }

        // Удалить все
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Очищаем List
            expenses.Clear();
            //// Очищаем ListView
            listView1.Items.Clear();

            // Обнуляем сумму
            sum = 0;
            label2.Text = Convert.ToString(sum);
        }

        // Сохранение существующих расходов в XML-файл
        public void SaveXML(List<Expense> expenses)
        {
            // класс для записи XML
            XmlWriter writer;

            // настройки записи
            XmlWriterSettings settings = new XmlWriterSettings();

            // перенос на новые строки
            settings.Indent = true;

            // символы перехода на следующую строку
            settings.NewLineChars = "\r\n";

            // кодировка
            settings.Encoding = Encoding.Unicode;

            // переход на новую строку для атрибутов
            settings.NewLineOnAttributes = false;

            // создание нового файла
            writer = XmlWriter.Create(fileName, settings);

            // вывести заголовок XML
            writer.WriteStartDocument();

            // записать начальный тег элемента
            writer.WriteStartElement("expenses");

            foreach (var item1 in expenses)
            {
                writer.WriteStartElement("expense");

                writer.WriteElementString("id", Convert.ToString(item1.Id));
                writer.WriteElementString("sum", Convert.ToString(item1.Sum));
                writer.WriteElementString("date", Convert.ToString(item1.Date));
                writer.WriteElementString("name", Convert.ToString(item1.Name));
                writer.WriteElementString("client", Convert.ToString(item1.Client));
                writer.WriteElementString("category", Convert.ToString(item1.Category));
                writer.WriteElementString("description", Convert.ToString(item1.Description));

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            // закрытие записи всего документа
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        // Загрузка расходов из XML-файла
        public List<Expense> LoadXML()
        {
            var _expenses = new List<Expense>();
            string _id = "", _sum = "", _date = "", _name = "", _client = "", _category = "", _description = "";

            XmlDocument xml = new XmlDocument();
            xml.Load(fileName);
            foreach (XmlNode item in xml.SelectNodes("/expenses/expense"))
            {
                _id = item.SelectSingleNode("id").InnerText;
                _sum = item.SelectSingleNode("sum").InnerText;
                _date = item.SelectSingleNode("date").InnerText;
                _name = item.SelectSingleNode("name").InnerText;
                _client = item.SelectSingleNode("client").InnerText;
                _category = item.SelectSingleNode("category").InnerText;
                _description = item.SelectSingleNode("description").InnerText;

                _expenses.Add(new Expense(
                    Convert.ToDecimal(_sum),
                    Convert.ToDateTime(_date),
                    Convert.ToString(_name),
                    Convert.ToString(_client),
                    Convert.ToString(_category),
                    Convert.ToString(_description)));
            }
            return _expenses;
        }

        // Сохранение членов семьи в XML-файл
        public void SaveXMLClients(List<string> clients)
        {
            // класс для записи XML
            XmlWriter writer;

            // настройки записи
            XmlWriterSettings settings = new XmlWriterSettings();

            // перенос на новые строки
            settings.Indent = true;

            // символы перехода на следующую строку
            settings.NewLineChars = "\r\n";

            // кодировка
            settings.Encoding = Encoding.Unicode;

            // переход на новую строку для атрибутов
            settings.NewLineOnAttributes = false;

            // создание нового файла
            writer = XmlWriter.Create(fileName2, settings);

            // вывести заголовок XML
            writer.WriteStartDocument();

            // записать начальный тег элемента
            writer.WriteStartElement("clients");

            foreach (var item1 in clients)
            {
                writer.WriteStartElement("client");
                writer.WriteElementString("name", Convert.ToString(item1)); 
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            // закрытие записи всего документа
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        // Загрузка членов семьи из XML-файла
        public List<string> LoadXMLClients()
        {
            var _clients = new List<string>();
            string _client = "";

            XmlDocument xml = new XmlDocument();
            xml.Load(fileName2);
            foreach (XmlNode item in xml.SelectNodes("/clients/client"))
            {
                _client = item.SelectSingleNode("name").InnerText;

                _clients.Add(Convert.ToString(_client));
            }
            return _clients;
        }

        // Сохранение категорий в XML-файл
        public void SaveXMLCategories(List<string> clients)
        {
            // класс для записи XML
            XmlWriter writer;

            // настройки записи
            XmlWriterSettings settings = new XmlWriterSettings();

            // перенос на новые строки
            settings.Indent = true;

            // символы перехода на следующую строку
            settings.NewLineChars = "\r\n";

            // кодировка
            settings.Encoding = Encoding.Unicode;

            // переход на новую строку для атрибутов
            settings.NewLineOnAttributes = false;

            // создание нового файла
            writer = XmlWriter.Create(fileName3, settings);

            // вывести заголовок XML
            writer.WriteStartDocument();

            // записать начальный тег элемента
            writer.WriteStartElement("categories");

            foreach (var item1 in categories)
            {
                writer.WriteStartElement("category");
                writer.WriteElementString("name", Convert.ToString(item1));
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            // закрытие записи всего документа
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        // Загрузка категорий из XML-файла
        public List<string> LoadXMLCategories()
        {
            var _categories = new List<string>();
            string _category = "";

            XmlDocument xml = new XmlDocument();
            xml.Load(fileName3);
            foreach (XmlNode item in xml.SelectNodes("/categories/category"))
            {
                _category = item.SelectSingleNode("name").InnerText;

                _categories.Add(Convert.ToString(_category));
            }
            return _categories;
        }

        // Форма статистики за период
        private void StatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics form8 = new Statistics();
            form8.Owner = this;
            form8.Show();
        }

        // Фильтрация по членам семьи
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очищаем ListView
            listView1.Items.Clear();

            sum = 0;
            foreach (var expense in expenses)
            {
                // Выводим записи нужного члена семьи
                if (expense.Client == comboBox1.Text)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(expense.Id));
                    item.SubItems.Add(Convert.ToString(expense.Name));
                    item.SubItems.Add(Convert.ToString(expense.Sum));
                    item.SubItems.Add(Convert.ToString(expense.Date));
                    item.SubItems.Add(Convert.ToString(expense.Client));
                    item.SubItems.Add(Convert.ToString(expense.Category));
                    item.SubItems.Add(Convert.ToString(expense.Description));

                    listView1.Items.Add(item);

                    // Общая сумма
                    sum += expense.Sum;
                }
            }
            label2.Text = Convert.ToString(sum);
        }

        // Кнопка "Очистить"
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "Все";
            ClearAndFillListView();
        }

        // Фильтрация по категориям
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очищаем ListView
            listView1.Items.Clear();

            sum = 0;
            foreach (var expense in expenses)
            {
                // Выводим записи нужного члена семьи
                if (expense.Category == comboBox2.Text)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(expense.Id));
                    item.SubItems.Add(Convert.ToString(expense.Name));
                    item.SubItems.Add(Convert.ToString(expense.Sum));
                    item.SubItems.Add(Convert.ToString(expense.Date));
                    item.SubItems.Add(Convert.ToString(expense.Client));
                    item.SubItems.Add(Convert.ToString(expense.Category));
                    item.SubItems.Add(Convert.ToString(expense.Description));

                    listView1.Items.Add(item);

                    // Общая сумма
                    sum += expense.Sum;
                }
            }
            label2.Text = Convert.ToString(sum);
        }

        // Кнопка "Очистить"
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "Все";
            ClearAndFillListView();
        }

        // Поиск по id
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Очищаем ListView
                listView1.Items.Clear();

                sum = 0;
                foreach (var expense in expenses)
                {
                    // Выводим записи нужного члена семьи
                    if (expense.Id == Convert.ToInt32(textBox1.Text))
                    {
                        ListViewItem item = new ListViewItem(Convert.ToString(expense.Id));
                        item.SubItems.Add(Convert.ToString(expense.Name));
                        item.SubItems.Add(Convert.ToString(expense.Sum));
                        item.SubItems.Add(Convert.ToString(expense.Date));
                        item.SubItems.Add(Convert.ToString(expense.Client));
                        item.SubItems.Add(Convert.ToString(expense.Category));
                        item.SubItems.Add(Convert.ToString(expense.Description));

                        listView1.Items.Add(item);

                        // Общая сумма
                        sum += expense.Sum;
                    }
                    else
                    {
                        listView1.Items.Clear();
                    }
                }
                label2.Text = Convert.ToString(sum);
            }
            catch (Exception)
            {
                ClearAndFillListView();
                MessageBox.Show("Такого id нет!");
            }
        }

        // Поиск по дате
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Очищаем ListView
            listView1.Items.Clear();

            sum = 0;
            foreach (var expense in expenses)
            {
                // Выводим записи нужного члена семьи
                if (expense.Date.DayOfYear == dateTimePicker1.Value.DayOfYear)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(expense.Id));
                    item.SubItems.Add(Convert.ToString(expense.Name));
                    item.SubItems.Add(Convert.ToString(expense.Sum));
                    item.SubItems.Add(Convert.ToString(expense.Date));
                    item.SubItems.Add(Convert.ToString(expense.Client));
                    item.SubItems.Add(Convert.ToString(expense.Category));
                    item.SubItems.Add(Convert.ToString(expense.Description));

                    listView1.Items.Add(item);

                    // Общая сумма
                    sum += expense.Sum;
                }
            }
            label2.Text = Convert.ToString(sum);
        }

        // Кпопка "Очистить"
        private void button4_Click(object sender, EventArgs e)
        {
            ClearAndFillListView();
        }

        // Метод для очистки и нового заполнения ListView
        private void ClearAndFillListView()
        {
            sum = 0;

            // Очищаем ListView
            listView1.Items.Clear();

            // Выводим все записи
            foreach (var expense in expenses)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(expense.Id));
                item.SubItems.Add(Convert.ToString(expense.Name));
                item.SubItems.Add(Convert.ToString(expense.Sum));
                item.SubItems.Add(Convert.ToString(expense.Date));
                item.SubItems.Add(Convert.ToString(expense.Client));
                item.SubItems.Add(Convert.ToString(expense.Category));
                item.SubItems.Add(Convert.ToString(expense.Description));

                listView1.Items.Add(item);

                // Общая сумма
                sum += expense.Sum;
            }
            label2.Text = Convert.ToString(sum);
        }
    }
}
