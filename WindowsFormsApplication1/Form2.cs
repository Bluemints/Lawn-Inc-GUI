using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {

                textBox7.Text = null;
                return;
            }

            textBox7.Text = "$" + (Convert.ToDouble(textBox4.Text) * 1000).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox8.Text = null;
                return;
            }
            textBox8.Text = "$" + (50 + ((Convert.ToDouble(textBox1.Text) - 1) * 10)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
