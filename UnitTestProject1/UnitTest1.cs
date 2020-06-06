using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTask7;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
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
            string first_half = vector.Substring(0, vector.Length / 2);
            string second_half = vector.Substring(vector.Length / 2, vector.Length / 2);
            string anti_first_half = string.Empty;
            for (int i = 0; i < first_half.Length; i++)
            {
                if (first_half[i] == '0') anti_first_half += '1';
                else anti_first_half += '0';
            }
            return (anti_first_half == second_half);
        }
        bool L(string vector)
        {
            string[] pascal_triangle_lines = new string[vector.Length];
            string coeffs = string.Empty;
            string multipl_coeffs = string.Empty;
            for (int i = 0; i < vector.Length; i++)
            {
                if (i == 0)
                {
                    pascal_triangle_lines[i] = vector;
                    coeffs += vector[0];
                }
                else
                {
                    for (int j = 0; j < pascal_triangle_lines[i - 1].Length - 1; j++)
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
                                    multipl_coeffs += coeffs[coeffs.Length - 1];
                                }
                            }
                        }
                    }
                }
            }
            return multipl_coeffs.All(ch => ch == '0');
        }
        bool M(string vector)
        {
            string[] vectors = new string[vector.Length];
            string pow_vector = string.Empty;
            for (int i = 0; i < Math.Log(vector.Length, 2); i++)
            {
                pow_vector += Math.Pow(2, i);
            }
            for (int i = 0; i < vector.Length; i++)
            {
                if (i == 0)
                {
                    for (int j = 1; j < vector.Length; j++)
                    {
                        vectors[i] += j;
                    }
                }
                else if ((int)Math.Log(i, 2) == Math.Log(i, 2))
                {
                    for (int j = 0; j < pow_vector.Length; j++)
                    {
                        if (i != char.GetNumericValue(pow_vector[j]))
                        {
                            int index = i + (int)char.GetNumericValue(pow_vector[j]);
                            vectors[i] += index;
                            vectors[index] = Check_Sum(index, pow_vector);
                            vectors[i] += vectors[index];
                            vectors[i] = string.Join("", vectors[i].Distinct());
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
        bool monotonic(string vector, string i_vector, int index)
        {
            bool is_monotonic = true;
            for (int i = 0; i < i_vector.Length; i++)
            {
                if (char.GetNumericValue(vector[index]) > char.GetNumericValue(vector[(int)char.GetNumericValue(i_vector[i])]))
                {
                    is_monotonic = false;
                    break;
                }
            }
            return is_monotonic;
        }
        string Check_Sum(int sum, string zero_vector)
        {
            List<int> sum_indexes = new List<int>();
            string except_indexes = string.Empty;
            for (int i = zero_vector.Length - 1; i >= 0; i--)
            {
                if (sum_indexes.Sum() + char.GetNumericValue(zero_vector[i]) <= sum)
                {
                    sum_indexes.Add((int)char.GetNumericValue(zero_vector[i]));
                }
                else
                {
                    except_indexes += (sum + char.GetNumericValue(zero_vector[i]));
                }
            }
            return except_indexes;
        }
        [TestMethod]
        public void TestMethod1()
        {
            string vector = "01011011";
            bool a = T0(vector);
            a = T1(vector);
            a = S(vector);
            a = L(vector);
            a = M(vector);
        }
    }
}
