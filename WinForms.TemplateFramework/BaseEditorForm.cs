using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormInheritanceDemo
{
    public partial class BaseEditorForm : Form
    {
        public BaseEditorForm()
        {
            InitializeComponent();
        }


        // 1. The Cancel logic is standard for ALL forms. 
        // We do not mark it virtual, so children cannot easily change it.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 2. The Save button triggers a function that Children MUST override.
        private void btnSave_Click(object sender, EventArgs e)
        {
            PerformSave();
        }

        // 3. VIRTUAL: This means "I have a basic version, but you can replace it."
        protected virtual void PerformSave()
        {
            // Default implementation (optional)
            MessageBox.Show("Base save functionality invoked.");
        }
    }
}
