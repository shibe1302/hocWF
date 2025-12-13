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
    public partial class Form2 : Form
    {
        public event Action<List<string>> PathsSaved;

        public Form2(List<string> initialPaths)
        {
            InitializeComponent();
            rTB_listPath.Text = string.Join(Environment.NewLine, initialPaths);
            BTN_savePath_RemoteOrLocal.Click += BTN_savePath_RemoteOrLocal_Click;
        }

        
        private void BTN_savePath_RemoteOrLocal_Click(object sender, EventArgs e)
        {
            List<string> listPaths = new List<string>();

            foreach (string line in rTB_listPath.Lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    listPaths.Add(line.Trim());
                }
            }

            
            PathsSaved?.Invoke(listPaths);

            this.Close();
        }
    }
}
