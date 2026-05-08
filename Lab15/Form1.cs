using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab15
{
    public partial class Form1 : Form
    {
        private const double EPS = 1e-10;

        public Form1()
        {
            InitializeComponent();

            Text = "Лабораторна робота 15. Варіант 20";
            ClientSize = new Size(1032, 520);
            StartPosition = FormStartPosition.CenterScreen;

            tabControl1.Dock = DockStyle.Fill;

            ClearTabs();

            BuildTask1();
            BuildTask2();
            BuildTask3();
            BuildTask4();
            BuildTask5();
            BuildTask6();
            BuildTask7();
        }

        private void ClearTabs()
        {
            foreach (TabPage page in tabControl1.TabPages)
            {
                page.Controls.Clear();
                page.AutoScroll = true;
                page.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            }
        }

        // ---------------- Завдання 1 ----------------

        private void BuildTask1()
        {
            TabPage page = tabPage1;

            AddTitle(page, "Завдання 1. Обчислити значення виразу");
            AddFormula(page, "z = (1 + sin(√(x + 1))) / cos(12y - 4)");

            TextBox xBox = AddInput(page, "x:", 40, 120, "0");
            TextBox yBox = AddInput(page, "y:", 40, 165, "0");

            AddNumberFilter(xBox, yBox);

            Label result = AddResultLabel(page, 40, 260, 900, 110);
            Button button = AddButton(page, "Розрахувати", 40, 215);

            button.Click += delegate
            {
                double x, y;

                if (!TryReadDouble(xBox, "x", result, out x)) return;
                if (!TryReadDouble(yBox, "y", result, out y)) return;

                if (x < -1)
                {
                    result.Text = "Помилка: x + 1 не може бути меншим за 0.";
                    xBox.Focus();
                    return;
                }

                double denominator = Math.Cos(12 * y - 4);

                if (Math.Abs(denominator) < EPS)
                {
                    result.Text = "Помилка: знаменник cos(12y - 4) дорівнює нулю.";
                    yBox.Focus();
                    return;
                }

                double z = (1 + Math.Sin(Math.Sqrt(x + 1))) / denominator;

                result.Text = "Результат: z = " + FormatDouble(z);
            };
        }

        // ---------------- Завдання 2 ----------------

        private void BuildTask2()
        {
            TabPage page = tabPage2;

            AddTitle(page, "Завдання 2. Обчислити відстань між двома точками");

            TextBox x1Box = AddInput(page, "x1:", 40, 100, "0");
            TextBox y1Box = AddInput(page, "y1:", 340, 100, "0");

            TextBox x2Box = AddInput(page, "x2:", 40, 145, "0");
            TextBox y2Box = AddInput(page, "y2:", 340, 145, "0");

            AddNumberFilter(x1Box, y1Box, x2Box, y2Box);

            Label result = AddResultLabel(page, 40, 250, 900, 110);
            Button button = AddButton(page, "Розрахувати", 40, 200);

            button.Click += delegate
            {
                double x1, y1, x2, y2;

                if (!TryReadDouble(x1Box, "x1", result, out x1)) return;
                if (!TryReadDouble(y1Box, "y1", result, out y1)) return;
                if (!TryReadDouble(x2Box, "x2", result, out x2)) return;
                if (!TryReadDouble(y2Box, "y2", result, out y2)) return;

                double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                result.Text = "Відстань між точками: " + FormatDouble(distance);
            };
        }

        // ---------------- Завдання 3 ----------------

        private void BuildTask3()
        {
            TabPage page = tabPage3;

            AddTitle(page, "Завдання 3. Перевірити висловлення");
            AddFormula(page, "Числа c і d є відповідно квадратом і кубом числа a");

            TextBox aBox = AddInput(page, "a:", 40, 120, "0");
            TextBox cBox = AddInput(page, "c:", 40, 165, "0");
            TextBox dBox = AddInput(page, "d:", 40, 210, "0");

            AddNumberFilter(aBox, cBox, dBox);

            Label result = AddResultLabel(page, 40, 315, 900, 120);
            Button button = AddButton(page, "Перевірити", 40, 260);

            button.Click += delegate
            {
                double a, c, d;

                if (!TryReadDouble(aBox, "a", result, out a)) return;
                if (!TryReadDouble(cBox, "c", result, out c)) return;
                if (!TryReadDouble(dBox, "d", result, out d)) return;

                bool cIsSquare = Math.Abs(c - a * a) < EPS;
                bool dIsCube = Math.Abs(d - a * a * a) < EPS;
                bool answer = cIsSquare && dIsCube;

                result.Text =
                    "c = a²: " + cIsSquare.ToString() + Environment.NewLine +
                    "d = a³: " + dIsCube.ToString() + Environment.NewLine +
                    "Відповідь: " + answer.ToString();
            };
        }

        // ---------------- Завдання 4 ----------------

        private void BuildTask4()
        {
            TabPage page = tabPage4;

            AddTitle(page, "Завдання 4. Перевірити, чи точки A, B, C лежать на одній прямій");
            AddFormula(page, "Якщо точки не лежать на одній прямій, обчислити площу трикутника ABC");

            TextBox x1Box = AddInput(page, "x1:", 40, 120, "0");
            TextBox y1Box = AddInput(page, "y1:", 340, 120, "0");

            TextBox x2Box = AddInput(page, "x2:", 40, 165, "0");
            TextBox y2Box = AddInput(page, "y2:", 340, 165, "0");

            TextBox x3Box = AddInput(page, "x3:", 40, 210, "0");
            TextBox y3Box = AddInput(page, "y3:", 340, 210, "0");

            AddNumberFilter(x1Box, y1Box, x2Box, y2Box, x3Box, y3Box);

            Label result = AddResultLabel(page, 40, 315, 900, 120);
            Button button = AddButton(page, "Розрахувати", 40, 260);

            button.Click += delegate
            {
                double x1, y1, x2, y2, x3, y3;

                if (!TryReadDouble(x1Box, "x1", result, out x1)) return;
                if (!TryReadDouble(y1Box, "y1", result, out y1)) return;
                if (!TryReadDouble(x2Box, "x2", result, out x2)) return;
                if (!TryReadDouble(y2Box, "y2", result, out y2)) return;
                if (!TryReadDouble(x3Box, "x3", result, out x3)) return;
                if (!TryReadDouble(y3Box, "y3", result, out y3)) return;

                double determinant = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                if (Math.Abs(determinant) < EPS)
                {
                    result.Text = "Точки A, B і C розташовані на одній прямій.";
                }
                else
                {
                    double area = Math.Abs(determinant) / 2.0;

                    result.Text =
                        "Точки A, B і C не лежать на одній прямій." + Environment.NewLine +
                        "Площа трикутника ABC: " + FormatDouble(area);
                }
            };
        }

        // ---------------- Завдання 5 ----------------

        private void BuildTask5()
        {
            TabPage page = tabPage5;

            AddTitle(page, "Завдання 5. Знайти номер будинку Петра і номер будинку школи");

            Label info = new Label();
            info.Left = 40;
            info.Top = 90;
            info.Width = 900;
            info.Height = 80;
            info.Text =
                "Суми номерів будинків у трьох кварталах дорівнюють 99, 117 і 235." + Environment.NewLine +
                "Номери будинків на одній стороні вулиці змінюються через 2.";
            page.Controls.Add(info);

            Label result = AddResultLabel(page, 40, 230, 900, 170);
            Button button = AddButton(page, "Знайти", 40, 180);

            button.Click += delegate
            {
                for (int home = 1; home <= 99; home++)
                {
                    for (int n1 = 1; n1 <= 50; n1++)
                    {
                        if (SumHouseNumbers(home, n1) != 99) continue;

                        int start2 = home + 2 * n1;

                        for (int n2 = 1; n2 <= 50; n2++)
                        {
                            if (SumHouseNumbers(start2, n2) != 117) continue;

                            int start3 = start2 + 2 * n2;

                            for (int n3 = 1; n3 <= 50; n3++)
                            {
                                if (SumHouseNumbers(start3, n3) != 235) continue;

                                int school = start3 + 2 * (n3 - 1);

                                result.Text =
                                    "Номер будинку Петра: " + home + Environment.NewLine +
                                    "Номер будинку школи: " + school + Environment.NewLine +
                                    "Перевірка:" + Environment.NewLine +
                                    BuildHouseSequence(home, n1) + " = 99" + Environment.NewLine +
                                    BuildHouseSequence(start2, n2) + " = 117" + Environment.NewLine +
                                    BuildHouseSequence(start3, n3) + " = 235";

                                return;
                            }
                        }
                    }
                }

                result.Text = "Розв’язок не знайдено.";
            };
        }

        private int SumHouseNumbers(int firstHouse, int count)
        {
            return count * (firstHouse + count - 1);
        }

        private string BuildHouseSequence(int firstHouse, int count)
        {
            List<string> numbers = new List<string>();

            for (int i = 0; i < count; i++)
            {
                numbers.Add((firstHouse + 2 * i).ToString());
            }

            return string.Join(" + ", numbers.ToArray());
        }

        // ---------------- Завдання 6 ----------------

        private void BuildTask6()
        {
            TabPage page = tabPage6;

            AddTitle(page, "Завдання 6. Замінити перший максимальний елемент масиву нулем");

            Label inputLabel = AddLabel(page, "Введіть масив дійсних чисел:", 40, 100, 250);

            TextBox arrayBox = new TextBox();
            arrayBox.Left = 300;
            arrayBox.Top = 95;
            arrayBox.Width = 600;
            arrayBox.Height = 60;
            arrayBox.Multiline = true;
            arrayBox.Text = "2 10 434 1 6 8";
            page.Controls.Add(arrayBox);

            Label result = AddResultLabel(page, 40, 240, 900, 140);
            Button button = AddButton(page, "Обробити", 40, 180);

            button.Click += delegate
            {
                List<double> numbers = ParseDoubleArray(arrayBox.Text);

                if (numbers.Count == 0)
                {
                    result.Text = "Помилка: введіть хоча б одне число.";
                    arrayBox.Focus();
                    return;
                }

                int maxIndex = 0;

                for (int i = 1; i < numbers.Count; i++)
                {
                    if (numbers[i] > numbers[maxIndex])
                    {
                        maxIndex = i;
                    }
                }

                double maxValue = numbers[maxIndex];
                numbers[maxIndex] = 0;

                result.Text =
                    "Перший максимальний елемент: " + FormatDouble(maxValue) + Environment.NewLine +
                    "Позиція елемента в масиві: " + (maxIndex + 1) + Environment.NewLine +
                    "Масив після заміни: " + string.Join("; ", numbers.Select(FormatDouble).ToArray());
            };
        }

        // ---------------- Завдання 7 ----------------

        private void BuildTask7()
        {
            TabPage page = tabPage7;

            AddTitle(page, "Завдання 7. Вилучити частину рядка, взяту в дужки");

            Label inputLabel = AddLabel(page, "Введіть рядок:", 40, 100, 250);

            TextBox sourceBox = new TextBox();
            sourceBox.Left = 300;
            sourceBox.Top = 95;
            sourceBox.Width = 600;
            sourceBox.Height = 60;
            sourceBox.Multiline = true;
            sourceBox.Text = "Це рядок (цей фрагмент треба вилучити) після дужок.";
            page.Controls.Add(sourceBox);

            Label result = AddResultLabel(page, 40, 240, 900, 140);
            Button button = AddButton(page, "Вилучити", 40, 180);

            button.Click += delegate
            {
                string text = sourceBox.Text;

                int start = text.IndexOf('(');

                if (start == -1)
                {
                    result.Text = "Помилка: у рядку немає відкривальної дужки.";
                    sourceBox.Focus();
                    return;
                }

                int end = text.IndexOf(')', start + 1);

                if (end == -1)
                {
                    result.Text = "Помилка: у рядку немає закривальної дужки після відкривальної.";
                    sourceBox.Focus();
                    return;
                }

                string newText = text.Remove(start, end - start + 1);

                result.Text =
                    "Початковий рядок:" + Environment.NewLine +
                    text + Environment.NewLine + Environment.NewLine +
                    "Результат:" + Environment.NewLine +
                    newText;
            };
        }

        // ---------------- Допоміжні методи інтерфейсу ----------------

        private void AddTitle(Control parent, string text)
        {
            Label label = new Label();
            label.Left = 40;
            label.Top = 35;
            label.Width = 900;
            label.Height = 30;
            label.Text = text;
            label.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            parent.Controls.Add(label);
        }

        private void AddFormula(Control parent, string text)
        {
            Label label = new Label();
            label.Left = 40;
            label.Top = 70;
            label.Width = 900;
            label.Height = 30;
            label.Text = text;
            parent.Controls.Add(label);
        }

        private Label AddLabel(Control parent, string text, int x, int y, int width)
        {
            Label label = new Label();
            label.Left = x;
            label.Top = y;
            label.Width = width;
            label.Height = 25;
            label.Text = text;
            parent.Controls.Add(label);
            return label;
        }

        private TextBox AddInput(Control parent, string labelText, int x, int y, string defaultValue)
        {
            AddLabel(parent, labelText, x, y, 80);

            TextBox textBox = new TextBox();
            textBox.Left = x + 90;
            textBox.Top = y - 3;
            textBox.Width = 160;
            textBox.Text = defaultValue;
            parent.Controls.Add(textBox);

            return textBox;
        }

        private Button AddButton(Control parent, string text, int x, int y)
        {
            Button button = new Button();
            button.Left = x;
            button.Top = y;
            button.Width = 180;
            button.Height = 35;
            button.Text = text;
            parent.Controls.Add(button);
            return button;
        }

        private Label AddResultLabel(Control parent, int x, int y, int width, int height)
        {
            Label label = new Label();
            label.Left = x;
            label.Top = y;
            label.Width = width;
            label.Height = height;
            label.Text = "";
            label.BorderStyle = BorderStyle.FixedSingle;
            label.BackColor = Color.White;
            label.Padding = new Padding(8);
            parent.Controls.Add(label);
            return label;
        }

        private void AddNumberFilter(params TextBox[] boxes)
        {
            foreach (TextBox box in boxes)
            {
                box.KeyPress += NumberTextBox_KeyPress;
            }
        }

        private void NumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox == null) return;

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                return;
            }

            if ((e.KeyChar == ',' || e.KeyChar == '.') &&
                textBox.Text.IndexOf(',') == -1 &&
                textBox.Text.IndexOf('.') == -1)
            {
                e.KeyChar = ',';
                return;
            }

            if (e.KeyChar == '-' &&
                textBox.SelectionStart == 0 &&
                !textBox.Text.Contains("-"))
            {
                return;
            }

            e.Handled = true;
        }

        // ---------------- Допоміжні методи обчислень ----------------

        private bool TryReadDouble(TextBox sourceBox, string variableName, Label resultLabel, out double value)
        {
            value = 0;

            string text = NormalizeNumber(sourceBox.Text);

            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                resultLabel.Text = "Помилка: введіть коректне число для змінної " + variableName + ".";
                sourceBox.Focus();
                return false;
            }

            return true;
        }

        private string NormalizeNumber(string text)
        {
            return text.Trim().Replace(',', '.');
        }

        private string FormatDouble(double value)
        {
            return value.ToString("G15", CultureInfo.CurrentCulture);
        }

        private List<double> ParseDoubleArray(string text)
        {
            List<double> result = new List<double>();

            MatchCollection matches = Regex.Matches(text, @"[-+]?\d+(?:[.,]\d+)?");

            foreach (Match match in matches)
            {
                double value;

                if (double.TryParse(
                    NormalizeNumber(match.Value),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out value))
                {
                    result.Add(value);
                }
            }

            return result;
        }

        // Потрібно, бо у Form1.Designer.cs стара кнопка має прив’язку до цього методу.
        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}