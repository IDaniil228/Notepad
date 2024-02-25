using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MainForm : Form
    {
        private string fileName;
        private bool save = false;
        public MainForm()
        {
            InitializeComponent();
        }

        #region Файл
        /// <summary>
        /// Метод, который обрабатывает кнопку "Открыть"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save == false)
            {
                DialogResult res = MessageBox.Show("Хотите сохранить файл?", "Сохранить", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(sender, e);
                }
                else if (res == DialogResult.Cancel)
                {
                    return;
                }
            }
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(dialog.FileName);
                    fileName = dialog.FileName;
                    Text = Path.GetFileNameWithoutExtension(fileName);
                    save = false;
                }
                catch
                {
                    MessageBox.Show("Недопустимый тип файла", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save == false)
            {
                DialogResult res = MessageBox.Show("Хотите сохранить файл?", "Сохранить", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(sender, e);
                }
                else if (res == DialogResult.Cancel)
                {
                    return;
                }
            }
            Text = "Блокнот";
            save = false;
            richTextBox1.Text = "";
            fileName = null;
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Сохранить как"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                fileName = sf.FileName;
                save = true;
                richTextBox1.SaveFile(fileName);
                Text = Path.GetFileNameWithoutExtension(sf.FileName);
            }
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Открыть как"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == null)
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
            else
            {
                save = true;
                richTextBox1.SaveFile(fileName);
            }
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Печать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pDocument = new PrintDocument();
            pDocument.PrintPage += PrintPageH;
            PrintDialog pDialog = new PrintDialog();
            pDialog.Document = pDocument;
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                pDialog.Document.Print();
            }
        }
        /// <summary>
        /// Метод, который делает так, чтобы печать работала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPageH(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, 0, 0);
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Выход"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Шрифт
        /// <summary>
        /// Метод, который обрабатывает кнопку "Цвет"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = color.Color;
            }
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Шрифт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog.Font;
            }
        }

        /// <summary>
        /// Метод, который обрабатывает кнопку "цветВсегоТекста"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void цветВсегоТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = color.Color;
            }
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "шрифтВсегоТекста"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void шрифтВсегоТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog.Font;
            }
        }
        #endregion

        #region Справка
        /// <summary>
        /// Метод, который обрабатывает кнопку "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }
        #endregion

        #region Правка
        /// <summary>
        /// Метод, который обрабатывает кнопку "Вырезать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length != 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Копировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length != 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }
        /// <summary>
        /// Метод, который обрабатывает кнопку "Вставить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }
        #endregion
    }
}
