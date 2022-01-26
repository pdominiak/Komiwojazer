using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komiwojażer
{
    public partial class Crossroad : UserControl
    {
        private int id;
        private static int number_of_crossroads = 0;
        public Crossroad()
        {
            InitializeComponent();
            this.id = number_of_crossroads++;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                checkBox1.BackColor = Color.Green;
            }
            else
            {
                checkBox1.BackColor = Color.Red;
            }
        }
    }
}
