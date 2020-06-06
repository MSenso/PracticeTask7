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
        void Read_FromFile()
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
                if (filelines.Length == 1)
                {
                    Remove_Elements(case_input: 0);
                    if (Check_Input(filelines[0]))
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
                        Controls.Add(input_file_label);
                        Check_Classes(filelines[0]);
                    }
                }
                else MessageBox.Show($"В файле содержится больше одной строки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool Check_Input(string vector)
        {
            bool is_correct = true;
            if (vector.All(ch => ch == '0' || ch == '1'))
            {
                int count_of_variables = 0;
                double sum_of_chars = vector.Length;
                if (sum_of_chars > 0 && sum_of_chars <= 16)
                {
                    while (sum_of_chars > 1)
                    {
                        sum_of_chars /= 2;
                        count_of_variables++;
                    }
                    if (sum_of_chars != 1)
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
        bool T0(string vector)
        {
            return vector[0] == '0';
        }
        bool T1(string vector)
        {
            return vector[vector.Length - 1] == '1';
        }
        bool S(string vector)
        {
            if (vector.Length > 1)
            {
                for(int i = 0; i < vector.Length / 2; i++)
                {
                    if (vector[i] == vector[vector.Length - 1 - i]) return false;
                }
                return true;
            }
            else return false;
        }
        bool L(string vector)
        {
            string[] pascal_triangle_lines = new string[vector.Length];
            string coeffs = string.Empty;
            string multipl_coeffs = string.Empty;
            bool is_linear = true;
            for (int i = 0; i < vector.Length && is_linear; i++)
            {
                if (i == 0)
                {
                    pascal_triangle_lines[i] = vector;
                    coeffs += vector[0];
                }
                else
                {
                    for (int j = 0; j < pascal_triangle_lines[i - 1].Length - 1 && is_linear; j++)
                    {
                        string mod_sum = (((int)Char.GetNumericValue(pascal_triangle_lines[i - 1][j]) + (int)Char.GetNumericValue(pascal_triangle_lines[i - 1][j + 1])) % 2).ToString();
                        pascal_triangle_lines[i] += mod_sum;
                        if (j == 0)
                        {
                            coeffs += mod_sum;
                            if (coeffs.Length > 1)
                            {
                                if ((int)Math.Log(coeffs.Length - 1, 2) != Math.Log(coeffs.Length - 1, 2))
                                {
                                    if (coeffs[coeffs.Length - 1] == '1') is_linear = false;
                                    multipl_coeffs += coeffs[coeffs.Length - 1];
                                }
                            }
                        }
                    }
                }
            }
            return is_linear;
        }
        bool M(string vector)
        {
            List<List<int>> vectors = new List<List<int>>();
            List<int> pow_vector = new List<int>();
            for (int i = 0; i < Math.Log(vector.Length, 2); i++)
            {
                pow_vector.Add((int)Math.Pow(2, i));
            }
            for (int i = 0; i < vector.Length; i++)
            {
                if (i == 0)
                {
                    vectors.Add(new List<int>());
                    for (int j = 1; j < vector.Length; j++)
                    {
                        vectors[i].Add(j);
                        vectors.Add(new List<int>());
                    }
                }
                else if ((int)Math.Log(i, 2) == Math.Log(i, 2))
                {
                    for (int j = 0; j < pow_vector.Count; j++)
                    {
                        if (i != pow_vector[j])
                        {
                            int index = i + pow_vector[j];
                            vectors[i].Add(index);
                            vectors[index] = Check_Sum(index, pow_vector, ref vectors);
                            vectors[i].AddRange(vectors[index]);
                            vectors[i] = vectors[i].Distinct().ToList();
                        }
                    }
                }

            }
            for (int i = 0; i < vector.Length - 1; i++)
            {
                if (!monotonic(vector, vectors[i], i)) return false;
            }
            return true;
        }
        bool monotonic(string vector, List<int> i_vector, int index)
        {
            bool is_monotonic = true;
            for (int i = 0; i < i_vector.Count; i++)
            {
                if (char.GetNumericValue(vector[index]) > char.GetNumericValue(vector[i_vector[i]]))
                {
                    is_monotonic = false;
                    break;
                }
            }
            return is_monotonic;
        }
        List<int> Check_Sum(int sum, List<int> zero_vector, ref List<List<int>> vectors)
        {
            List<int> sum_indexes = new List<int>();
            List<int> except_indexes = new List<int>();
           for (int i = zero_vector.Count - 1; i >= 0; i--)
            {
                if (sum_indexes.Sum() + zero_vector[i] <= sum)
                {
                    sum_indexes.Add(zero_vector[i]);
                }
                else
                {
                    except_indexes.Add(sum + zero_vector[i]);
                    vectors[sum].Add(sum + zero_vector[i]);
                    vectors[sum].AddRange(Check_Sum(sum + zero_vector[i], zero_vector, ref vectors));
                    vectors[sum] = vectors[sum].Distinct().ToList();
                }
            }
            return except_indexes;
        }
        void Check_Classes(string vector)
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
            if (T0(vector)) output_label.Text += " T0";
            if (T1(vector)) output_label.Text += " T1";
            if (S(vector)) output_label.Text += " S";
            if (L(vector)) output_label.Text += " L";
            if (M(vector)) output_label.Text += " M";
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
            Controls.Add(input_user_textbox);
            input_user_textbox.KeyDown += new KeyEventHandler(Input_User_TextBox_KeyDown);
            input_user_textbox.Focus();
        }
        void Input_User_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Remove_Output();
                if (Check_Input((sender as TextBox).Text))
                {
                    Check_Classes((sender as TextBox).Text);
                }
            }
        }
        void Remove_Elements(int case_input)
        {
            Input_Label.Text = string.Empty;
            if (case_input == 0)
            {
                Remove_User_Input();
            }
            else Remove_File_Input();
            Remove_Output();
        }
        void Remove_File_Input()
        {
            if (input_file_label != null)
            {
                Controls.Remove(input_file_label);
                input_file_label = null;
            }
        }
        void Remove_User_Input()
        {
            if (input_user_textbox != null)
            {
                Controls.Remove(input_user_textbox);
                input_user_textbox = null;
            }
        }
        void Remove_Output()
        {
            if (output_label != null)
            {
                Controls.Remove(output_label);
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
