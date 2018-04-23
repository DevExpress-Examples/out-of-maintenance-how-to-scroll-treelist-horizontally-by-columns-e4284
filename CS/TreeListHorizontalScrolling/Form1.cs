using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TreeListHorizontalScrolling {
    public partial class Form1 : Form {


        List<Customer> list = new List<Customer>();
        TreeListExtensionScrolling Extension;
        
        public Form1() {
            InitializeComponent();

            textEdit1.KeyDown += textEdit1_KeyDown;
            treeList1.LeftCoordChanged += treeList1_LeftCoordChanged;

            for (int i = 0; i < 10; i++)
                list.Add(new Customer() { Zero = "1", One = "1", Two = "1", Three = "1", Four = "1", Five = "1", Six = "1" });
            treeList1.DataSource = list;

            Extension = new TreeListExtensionScrolling(treeList1);

            treeList1.Columns[0].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            treeList1.Columns[3].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
        }

        void treeList1_LeftCoordChanged(object sender, EventArgs e) {
            textEdit1.Text = Extension.UpdateIndexInEditor().ToString();
        }

        void textEdit1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                if (textEdit1.Text != string.Empty)
                    Extension.ScrollTo(Convert.ToInt32(textEdit1.Text));
        }

        private void Form1_Load(object sender, EventArgs e) {
            Extension.UpdateFixedColumnsCount();
            textEdit1.Text = Extension.UpdateIndexInEditor().ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            Extension.ScrollBackward();
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            Extension.ScrollForward();
        }
    }
}