using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TE_TOOL
{
    public partial class FORM_FTU : Form
    {
        public event Action<string> DataSaved;
        public FORM_FTU()
        {
            InitializeComponent();
        }

        private void FORM_FTU_Load(object sender, EventArgs e)
        {

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            string logContent = RTB_LOG_FUNTION.Text;
            DataSaved?.Invoke(logContent);
        }
    }
}
