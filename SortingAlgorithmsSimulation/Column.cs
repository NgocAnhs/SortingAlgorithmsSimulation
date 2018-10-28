using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingAlgorithmsSimulation
{
    public partial class Column : UserControl
    {
        public Column()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return lblValue.Text; }
            set { lblValue.Text = value; }
        }

    }
}
