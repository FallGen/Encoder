using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int auto_key_0(int lenght_text)  //автоматическое определение размеров таблицы м0 // подбор ключа
        {
            int d;

            if (checkBox1.Checked)
                TB_output_text.Text += "\r\nавтоматическое определение ключа:";

            if (lenght_text <= 100) d = 10; else d = 50;

            while (d > 0)
            {
                if (lenght_text % d == 0)
                    break;
                d--;
            }

            TB_output_text.Text += "\r\nвременный ключ: " + d + "-" + lenght_text / d;
            TB_key.Text = d + "-" + lenght_text / d;
            return d;
        }

        public void auto_key_1(int lenght_text)  //автоматическое определение размеров таблицы м1 // подбор ключа

        {
            string[] temp_key = { "я", "уж", "йод", "ваза", "акция", "машина", "абонент", "абориген", "авторитет", "автовокзал" };
            int d = 0;

            if (checkBox1.Checked)
            {
                TB_output_text.Text += "\r\nавтоматическое определение ключа:";
                TB_output_text.Text += "\r\nAUTO_KEY ВЫПОЛНЯЕТСЯ:";
                TB_output_text.Text += "\r\nlenght_text: " + lenght_text;
                TB_output_text.Text += "\r\ntemp_key[1]: " + temp_key[2].Length;
                TB_output_text.Text += "\r\ntemp_key.lenght: " + temp_key.Length;
            }

            //if (CB_method.SelectedIndex == 1)
                for (int i = temp_key.Length - 1; i >= 0; i--)
                    if (lenght_text % temp_key[i].Length == 0)
                    {
                        TB_key.Text = temp_key[i];
                        TB_output_text.Text += "\r\nвременный ключ " + temp_key[i];
                        break;
                    }
        }
        public void auto_key_2(int lenght_text)
        {
            if (checkBox1.Checked)
                TB_output_text.Text += "\r\nавтоматическое определение ключа:";

            string temp_key = null;

            int d = auto_key_0(lenght_text);

            for (int i = d; i > 0; i--)
                temp_key += i;

            temp_key += "-";

            for (int i = 1; i <= lenght_text/d; i++)
                temp_key += i;

            TB_key.Text = temp_key;
            TB_output_text.Text += "\r\nвременный ключ " + temp_key;
        }
        public int sizing(int lenght_text)
        {
            try
            {
                if (checkBox1.Checked)
                    TB_output_text.Text += "\r\nвыполнение sizing";

                int d = 0;

                if (CB_method.SelectedIndex == 0)
                {
                    if (TB_key.Text != "")
                        d = Convert.ToInt32(TB_key.Text.ToString().Substring(0, TB_key.Text.IndexOf('-')));
                    else
                        d = auto_key_0(lenght_text);

                    TB_output_text.Text += "\r\nключ: " + TB_key.Text + "\r\n";
                }

                if (CB_method.SelectedIndex == 1)
                {
                    if (TB_key.Text == "")
                        auto_key_1(lenght_text);

                    d = TB_key.Text.Length;
                    TB_output_text.Text += "\r\nключ: " + TB_key.Text + "\r\n";
                }

                if (CB_method.SelectedIndex == 2)
                {
                    if (TB_key.Text == "")
                        auto_key_2(lenght_text);

                    d = TB_key.Text.ToString().Substring(0, TB_key.Text.IndexOf('-')).Length;
                    TB_output_text.Text += "\r\nключ: " + TB_key.Text + "\r\n";
                }

                if (checkBox1.Checked)
                {
                    TB_output_text.Text += "\r\nпроверка ключа в sizing-e: " + d + "\r\n";
                    TB_output_text.Text += "\r\nопределение размеров таблицы по ключу:";
                }

                return d;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return 0;
            }
        }

        public void text_transformation(string operation)
        {
            try
            {
                string text = TB_input_text.Text;

                if (checkBox1.Checked)
                {
                    TB_output_text.Text += "\r\nтекст:" + "\r\n" + text;
                    TB_output_text.Text += "\r\nдлина текста без форматирования: " + text.Length + "\r\n";
                }

                string new_text = text.Replace(" ", "").ToLower();
                int lenght_text = new_text.Length;

                if (checkBox1.Checked)
                {
                    TB_output_text.Text += "\r\nтекст с удаленными пробелами: " + "\r\n" + new_text;
                    TB_output_text.Text += "\r\nдлина текста с удалением пробелов: " + lenght_text + "\r\n";
                }

                int d = sizing(lenght_text);

                if (checkBox1.Checked)
                {
                    TB_output_text.Text += "\r\nвысота таблицы: " + d + "\r\n";
                    TB_output_text.Text += "ширина таблицы: " + lenght_text / d + "\r\n";
                }

                dataGridView1.RowCount = lenght_text / d;
                dataGridView1.ColumnCount = d;

                //заполнение таблицы логов
                if (checkBox1.Checked)
                    TB_output_text.Text += "\r\nзаполнение вкладки таблицы";

                int c = 0;
                if (operation == "encoder")
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                        for (int j = 0; j < dataGridView1.RowCount; j++)
                        {
                            dataGridView1[i, j].Value = new_text[c];
                            c++;
                        }
                }
                else if (operation == "decoding")
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            dataGridView1[j, i].Value = new_text[c];
                            c++;
                        }
                }

            }
            catch (Exception E)
            {
                //MessageBox.Show(E.Message);
            }
        }
        public void text_output(string operation)
        {
            int dgv_lenght_row = 0;
            int dgv_lenght_column = 0;

            if (CB_method.SelectedIndex == 1)
                dgv_lenght_row = 2;
            if (CB_method.SelectedIndex == 2)
            {
                dgv_lenght_row = 1;
                dgv_lenght_column = 1;
            }

            string new_text = null;

            //вывод текста из логов
            if (operation == "encoder")
            {
                for (int i = 0; i < dataGridView1.RowCount - dgv_lenght_row; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount - dgv_lenght_column; j++)
                        new_text += Convert.ToString(dataGridView1[j, i].Value);

            }
            else if (operation == "decoding")
            {
                for (int i = 0; i < dataGridView1.ColumnCount - dgv_lenght_column; i++)
                    for (int j = 0; j < dataGridView1.RowCount - dgv_lenght_row; j++)
                        new_text += Convert.ToString(dataGridView1[i, j].Value);
            }


            TB_output_text.Text += "\r\nзашифрованный текст: " + "\r\n" + new_text + "\r\n\r\n";
        }

        public void permutation_row()
        {
            if (checkBox1.Checked)
                TB_output_text.Text += "\r\nвыполнение перестановки строк";

            int prioritet = 1;
            int j = 0;

            while (true)
            {
                if (Convert.ToInt32(dataGridView1[dataGridView1.ColumnCount - 1, j].Value) == prioritet)
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        string temp = dataGridView1[i, j].Value.ToString();
                        dataGridView1[i, j].Value = dataGridView1[i, prioritet - 1].Value.ToString();
                        dataGridView1[i, prioritet - 1].Value = temp;
                    }
                    prioritet++;
                }

                j++;

                if (prioritet == dataGridView1.RowCount - 1) break;
                if (j > dataGridView1.RowCount - 1) j = 0;

            }
        }
        public void permutation_column(string operation)
        {
            if (checkBox1.Checked)
                TB_output_text.Text += "\r\nвыполнение перестановки столбцов";

            int prioritet = 1;
            int j = 0;

            if (operation == "encoder")
                while (true)
                {
                    if (Convert.ToInt32(dataGridView1[j, dataGridView1.RowCount - 1].Value) == prioritet)
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            string temp = dataGridView1[j, i].Value.ToString();
                            dataGridView1[j, i].Value = dataGridView1[prioritet - 1, i].Value.ToString();
                            dataGridView1[prioritet - 1, i].Value = temp;
                        }
                        prioritet++;
                    }

                    j++;

                    if (CB_method.SelectedIndex == 1)
                    {
                        if (prioritet == dataGridView1.ColumnCount) break;
                        if (j >= dataGridView1.ColumnCount) j = 0;
                    }

                    if (CB_method.SelectedIndex == 2)
                    {
                        if (prioritet == dataGridView1.ColumnCount - 1) break;
                        if (j > dataGridView1.ColumnCount - 1) j = 0;
                    }
                }
            else
            if (operation == "decoding")
            {
                for (int i = 0; i + 1 < dataGridView1.ColumnCount; ++i)
                    for (int c = 0; c + 1 < dataGridView1.RowCount - i; ++c)
                    {
                        if (Convert.ToInt32(dataGridView1[c + 1, dataGridView1.RowCount - 1].Value) > Convert.ToInt32(dataGridView1[c, dataGridView1.RowCount - 1].Value))
                        {
                            for (int p = 0; p < dataGridView1.RowCount; ++p)
                            {
                                string temp = Convert.ToString(dataGridView1[c, p].Value);
                                dataGridView1[c, p].Value = Convert.ToString(dataGridView1[c + 1, p].Value);
                                dataGridView1[c + 1, p].Value = temp;
                            }
                        }
                    }
            }
        }

        public void key_installation()
        {
            dataGridView1.RowCount += 2;

            //заполение ключа в таблицу
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                dataGridView1[i, dataGridView1.RowCount - 2].Value = TB_key.Text[i];

            //расстановка "приоритета" для формирование букв
            int prioritet = 0;
            for (char b = 'a'; b <= 'я'; b++) //цикл для алфавита
                for (int i = 0; i < dataGridView1.ColumnCount; i++) //цикл для букв ключа (корова)
                    if (Convert.ToChar(dataGridView1[i, dataGridView1.RowCount - 2].Value) == b)
                        dataGridView1[i, dataGridView1.RowCount - 1].Value = ++prioritet;
        }
        public void encoder_simple_permutation()
        {
            TB_output_text.Text += "шифрование простой перестановкой: " + "\r\n";

            text_transformation("encoder");
            text_output("encoder");
        }
        public void encoder_single_permutation_by_key()
        {
            TB_output_text.Text += "шифрование одиночной перестановка по ключу: " + "\r\n";
            text_transformation("encoder");

            try
            {
                if (checkBox1.Checked)
                {
                    TB_output_text.Text += "\r\n" + "\r\nключ шифрования: " + TB_key.Text + "\r\n";
                    TB_output_text.Text += "\r\nзаполнение ключа в таблицу";
                    TB_output_text.Text += "\r\nрасстановка приоритета для формирования букв";
                }

                key_installation();
                permutation_column("encoder");
                text_output("encoder");

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        public void encoder_double_transposition_encryption()
        {
            TB_output_text.Text += "шифрование двойной перестановкой: " + "\r\n";
            text_transformation("encoder");

            try
            {
                dataGridView1.RowCount += 1;
                dataGridView1.ColumnCount += 1;

                int r = TB_key.Text.IndexOf('-');

                if (checkBox1.Checked) TB_output_text.Text += "\r\nзаполнение таблицы ключами";

                //заполение логов ключами
                for (int i = 0; i < r; i++) //горизонталь
                    dataGridView1[i, dataGridView1.RowCount - 1].Value = TB_key.Text[i].ToString();

                int c = 0;
                for (int j = r + 1; j < TB_key.Text.Length; j++) //вертикаль
                {
                    dataGridView1[dataGridView1.ColumnCount - 1, c].Value = TB_key.Text[j].ToString();
                    c++;
                }

                permutation_column("encoder");
                permutation_row();
                text_output("encoder");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) //зашифровать
        {
            if(checkBox2.Checked) method_clear();

            switch (CB_method.SelectedIndex)
            {
                case 0:
                    encoder_simple_permutation();
                    break;
                case 1:
                    encoder_single_permutation_by_key();
                    break;
                case 2:
                    encoder_double_transposition_encryption();
                    break;
                default:
                    MessageBox.Show("не выбран метод");
                    break;
            }
        }

        public void decoding_simple_permutation()
        {
            TB_output_text.Text += "дешифрование простой перестановкой: " + "\r\n";

            text_transformation("decoding");
            text_output("decoding");

        }       
        public void decoding_single_permutation_by_key()
        {
            TB_output_text.Text += "дешифрование одиночной перестановкой по ключу: " + "\r\n";

            text_transformation("decoding");
            key_installation();
            permutation_column("decoding");
            text_output("decoding");

        }
        public void decoding_double_transposition_encryption()
        {

        }
        private void button2_Click(object sender, EventArgs e) //расшифровать
        {
            if (checkBox2.Checked) method_clear();

            switch (CB_method.SelectedIndex)
            {
                case 0:
                    decoding_simple_permutation();
                    break;
                case 1:
                    decoding_single_permutation_by_key();
                    break;
                case 2:
                    decoding_double_transposition_encryption();
                    break;
                default:
                    MessageBox.Show("не выбран метод");
                    break;
            }

        }


        public void method_clear()
        {
            //TB_input_text.Clear();
            TB_output_text.Clear();
            dataGridView1.RowCount = 0;
            dataGridView1.ColumnCount = 0;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            method_clear();
        }

        private void CB_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                TB_input_text.Text = "Прилетаю седьмого в полдень";
                if (CB_method.SelectedIndex == 0)
                    TB_key.Text = "6-4";
                if (CB_method.SelectedIndex == 1)
                    TB_key.Text = "корова";
                if (CB_method.SelectedIndex == 2)
                {
                    TB_input_text.Text = "Прилетаю седьмого";
                    TB_key.Text = "4132-3142";
                }
            }
        }

        private void TB_input_text_TextChanged(object sender, EventArgs e)
        {
            label5.Text = "символов без пробела: " + TB_input_text.Text.Replace(" ", "").ToLower().Length;
        }
    }
}
