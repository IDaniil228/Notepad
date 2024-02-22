using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace Notepad
{
    public partial class MainForm : Form
    {
        private string fileName;

        public MainForm()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    richTextBox1.LoadFile(dialog.FileName);
                    fileName = dialog.FileName;
                }
                catch 
                {
                    MessageBox.Show("Недопустимый тип файла", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }           
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = color.Color;
            }
        }

        private void курсивToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font.Italic == false)
            {
                //Font.Italic = true;
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                fileName = sf.FileName;
                richTextBox1.SaveFile(fileName);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == null)
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
            else
            {
                richTextBox1.SaveFile(fileName);
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            fileName = null;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

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
        private void PrintPageH(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, 0, 0);
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog.Font;
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
            richTextBox1.SelectedText = "";
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

    }
}
