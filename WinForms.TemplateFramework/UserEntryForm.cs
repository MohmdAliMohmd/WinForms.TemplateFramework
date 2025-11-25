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
    public partial class UserEntryForm : BaseEditorForm // Inherit from our template
    {
        public UserEntryForm()
        {
            InitializeComponent();
        }
        // We override the method defined in the Base Form
        protected override void PerformSave()
        {
            // 1. Validation specific to this form
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter a name first!", "Validation Error");
                return;
            }

            // 2. The actual "Save" logic
            MessageBox.Show($"User '{txtName.Text}' has been saved to the database.", "Success");

            // 3. Optionally call the base method if you needed its logic too
            // base.PerformSave(); 
        }
    }
}
