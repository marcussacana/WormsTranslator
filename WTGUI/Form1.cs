using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WormsTranslator;

namespace WTGUI {
    public partial class WTGUI : Form {
        public WTGUI() {
            InitializeComponent();
        }

        BinEditor Editor;
        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog fd = new OpenFileDialog();
            var rst = fd.ShowDialog();
            if (rst != DialogResult.OK)
                return;
            Editor = new BinEditor(File.ReadAllBytes(fd.FileName));
            string[] Strings = Editor.Import();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Strings);

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog fd = new SaveFileDialog();
            var rst = fd.ShowDialog();
            if (rst != DialogResult.OK)
                return;
            byte[] Data = Editor.Export(listBox1.Items.Cast<string>().ToArray());
            File.WriteAllBytes(fd.FileName, Data);
            MessageBox.Show("File Saved");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\n' || e.KeyChar == '\r') {
                try {
                    listBox1.Items[listBox1.SelectedIndex] = textBox1.Text;
                } catch {

                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            } catch { }
        }

    }
}
