using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeTask7
{
    public partial class Form1 : Form
    {
        string variables = string.Empty;
        Label output_label = null, input_file_label = null;
        TextBox input_user_textbox = null;
        public Form1()
        {
            InitializeComponent();
        }
        void Read_FromFile() // Чтение из файла
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Открытие текстового файла";
            openFileDialog1.Filter = "Текстовые файлы|*.txt";
            openFileDialog1.InitialDirectory = "";
            string[] filelines = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                filelines = File.ReadAllLines(filename);
            }
            if (filelines != null)
            {
                if (filelines.Length == 1) // Строка в файле одна
                {
                    Remove_Elements(case_input: 0); // Очищение формы
                    if (Check_Input(filelines[0])) // Проверка корректного ввода
                    {
                        Input_Label.Text = "Вектор значений функции из файла:";
                        input_file_label = new Label()
                        {
                            Name = "Input_File_Label",
                            AutoSize = true,
                            Text = filelines[0],
                            Location = new Point(Input_Label.Location.X + Input_Label.Width + 5, Input_Label.Location.Y),
                            ForeColor = Color.Black,
                            BackColor = Color.Transparent,
                            Font = Input_Label.Font
                        };
                        Controls.Add(input_file_label); // Вывод вектора на экран
                        Check_Classes(filelines[0]); // Определение на принадлежность классам
                    }
                }
                else MessageBox.Show($"В файле содержится больше одной строки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool Check_Input(string vector) // Проверка ввода
        {
            bool is_correct = true;
            if (vector.All(ch => ch == '0' || ch == '1')) // Вектор из нулей и единиц
            {
                int count_of_variables = 0; // Количество аргументов функции
                double sum_of_chars = vector.Length; // Количество символов
                if (sum_of_chars > 0 && sum_of_chars <= 16) // Длина вектора от 1 до 16
                {
                    while (sum_of_chars > 1) // Пока количество символов больше 1
                    {
                        sum_of_chars /= 2; // Делим пополам
                        count_of_variables++; // Увеличиваем счетчик аргументов
                    }
                    if (sum_of_chars != 1) // Если не получено 1, то длина вектора не 2 в некоторой степени
                    {
                        is_correct = false;
                        MessageBox.Show("Вектор значений имеет некорректную длину!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    is_correct = false;
                    MessageBox.Show("Вектор значений должен иметь длину от 1 до 16 включительно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                is_correct = false;
                MessageBox.Show("Вектор значений должен имеет некорректные символы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return is_correct;
        }
        bool T0(string vector) // Класс Т0
        {
            return vector[0] == '0'; // 0 на нулевом наборе
        }
        bool T1(string vector) // Класс Т1
        {
            return vector[vector.Length - 1] == '1'; // 1 на единичном наборе
        }
        bool S(string vector) // Класс S
        {
            if (vector.Length > 1) // Длина вектора больше 1
            {
                for(int i = 0; i < vector.Length / 2; i++)
                {
                    if (vector[i] == vector[vector.Length - 1 - i]) return false; // Если одинаковые значения на противоположных концах, то не принадлежит S
                }
                return true;
            }
            else return false;
        }
        bool L(string vector) // Класс L
        {
            string[] pascal_triangle_lines = new string[vector.Length]; // Линии треугольника Паскаля
            string coeffs = string.Empty; // Коэффициенты полинома
            string multipl_coeffs = string.Empty; // Коэффициенты при произведении аргументов
            bool is_linear = true;
            for (int i = 0; i < vector.Length && is_linear; i++)
            {
                if (i == 0)
                {
                    pascal_triangle_lines[i] = vector; // Первая строка треугольника равна вектору
                    coeffs += vector[0]; // В коэффициенты записываем первое значение строки
                }
                else
                {
                    for (int j = 0; j < pascal_triangle_lines[i - 1].Length - 1 && is_linear; j++)
                    {
                        string mod_sum = (((int)Char.GetNumericValue(pascal_triangle_lines[i - 1][j]) + 
                            (int)Char.GetNumericValue(pascal_triangle_lines[i - 1][j + 1])) % 2).ToString(); // Вычисляем сумму по модулю 2
                        pascal_triangle_lines[i] += mod_sum; // Добавляем ее в текущую строку треугольника
                        if (j == 0) // Если это первое значение строки
                        {
                            coeffs += mod_sum; // Добавляем к коэффициентам
                            if (coeffs.Length > 1)
                            {
                                if ((int)Math.Log(coeffs.Length - 1, 2) != Math.Log(coeffs.Length - 1, 2)) // Если коэффициент не при слагаемом полинома вида x1, x2... xn
                                {
                                    if (coeffs[coeffs.Length - 1] == '1') is_linear = false; // Если коэффициент равен 1, в полиноме есть произведение аргументов, функция нелинейна
                                    multipl_coeffs += coeffs[coeffs.Length - 1];
                                }
                            }
                        }
                    }
                }
            }
            return is_linear;
        }
        bool M(string vector) // Класс М
        {
            List<List<int>> vectors = new List<List<int>>(); // Список из списков, где i-тый список содержит наборы, в которые можно попасть из i-того набора
            for(int i = 0; i < vector.Length - 1; i++)
            {
                vectors.Add(new List<int>()); // Заполняем список
            }
            for (int i = 0; i < vector.Length - 1; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < Math.Log(vector.Length, 2); j++)
                    {
                        vectors[i].Add((int)Math.Pow(2, j)); // Из нулевого набора можно попасть в наборы, содержающие 1 единицу среди аргументов
                    }
                }
                else if ((int)Math.Log(i, 2) == Math.Log(i, 2)) // Набор содержит 1 единицу
                {
                    for (int j = 0; j < vectors[0].Count; j++) // Перебираем все доступные числа вида 2 в некоторой степени
                    {
                        if (i != vectors[0][j])
                        {
                            int index = i + vectors[0][j];
                            vectors[i].Add(index); // Записываем в список сумму индекса текущего набора и числа вида 2 в некоторой степени
                        }
                    }
                }
                else
                {
                    Check_Sum(i, ref vectors); // Находим наборы, в которые можно попасть, прибавив к текущему i еще неприбавленное число вида 2 в некоторой степени
                }

            }
            for (int i = 0; i < vectors.Count; i++)
            {
                if (!Is_Monotonic(vector, vectors[i], i)) return false; // Проверка каждой цепи наборов на монотонность
            }
            return true;
        }
        bool Is_Monotonic(string vector, List<int> i_vector, int index)
        {
            bool is_monotonic = true;
            for (int i = 0; i < i_vector.Count; i++)
            {
                if (char.GetNumericValue(vector[index]) > char.GetNumericValue(vector[i_vector[i]])) // Если значение текущего набора больше, чем значение набора, в который можно попасть из текущего, то функция немонотонна
                {
                    is_monotonic = false;
                    break;
                }
            }
            return is_monotonic;
        }
        void Check_Sum(int sum, ref List<List<int>> vectors)
        {
            List<int> sum_indexes = new List<int>(); // Индексы, которые в сумме составляют sum
            List<int> except_indexes = new List<int>(); // Индексы, которые не входят в sum
            for (int i = vectors[0].Count - 1; i >= 0; i--)
            {
                if (sum_indexes.Sum() + vectors[0][i] <= sum) // Сумма текущего числа и суммы sum_indexes не превосходит sum
                {
                    sum_indexes.Add(vectors[0][i]); // Текущее число является слагаемым sum
                }
                else
                {
                    except_indexes.Add(sum + vectors[0][i]);
                    vectors[sum].Add(sum + vectors[0][i]); // Из набора sum можно попасть в набор sum + vectors[0][i]
                }
            }
        }
        public void Check_Classes(string vector)
        {
            output_label = new Label()
            {
                Name = "Output_Label",
                Text = "Функция относится к",
                Location = new Point(Input_Label.Location.X, Input_Label.Location.Y + Input_Label.Height + 20),
                AutoSize = true,
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Font = Input_Label.Font
            };
            if (T0(vector)) output_label.Text += " T0"; // Относится к Т0
            if (T1(vector)) output_label.Text += " T1"; // Относится к Т1
            if (S(vector)) output_label.Text += " S"; // Относится к S
            if (L(vector)) output_label.Text += " L"; // Относится к L
            if (M(vector)) output_label.Text += " M"; // Относится к M
            if (output_label.Text == "Функция относится к") output_label.Text = "Функция не относится ни к одному \n" + "из замкнутых классов";
            Controls.Add(output_label);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        void User_Input()
        {
            Remove_Elements(case_input: 1);
            Input_Label.Text = "Введите вектор значений функции: ";
            input_user_textbox = new TextBox()
            {
                Name = "Input_User_TextBox",
                Size = new Size(100, 50),
                Location = new Point(Input_Label.Location.X + Input_Label.Width + 5, Input_Label.Location.Y),
                ForeColor = Color.Black,
                BackColor = Color.FromArgb(255, 245, 248),
                Font = Input_Label.Font
            };
            Controls.Add(input_user_textbox); // Добавление текстбокса для ввода вектора вручную
            input_user_textbox.KeyDown += new KeyEventHandler(Input_User_TextBox_KeyDown);
            input_user_textbox.Focus();
        }
        void Input_User_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Нажат энтер
            {
                Remove_Output(); // Очистка результатов
                if (Check_Input((sender as TextBox).Text)) // Корректный ввод
                {
                    Check_Classes((sender as TextBox).Text); // Проверка на принадлежность классам
                }
            }
        }
        void Remove_Elements(int case_input)
        {
            Input_Label.Text = string.Empty;
            if (case_input == 0)
            {
                Remove_User_Input(); // Удаление пользовательского ввода
            }
            else Remove_File_Input(); // Удаление ввода из файла
            Remove_Output(); // Очистка результатов
        }
        void Remove_File_Input()
        {
            if (input_file_label != null) // Если еще не удалено
            {
                Controls.Remove(input_file_label); // Очистка
                input_file_label = null;
            }
        }
        void Remove_User_Input()
        {
            if (input_user_textbox != null) // Если еще не удалено
            {
                Controls.Remove(input_user_textbox); // Очистка
                input_user_textbox = null;
            }
        }
        void Remove_Output()
        {
            if (output_label != null) // Если еще не удалено
            {
                Controls.Remove(output_label); // Очистка
                output_label = null;
            }
        }
        private void вводФункцииВручнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove_Elements(case_input: 1);
            User_Input();
        }
        private void вводФункцииИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove_Elements(case_input: 0);
            Read_FromFile();
        }
    }
}
