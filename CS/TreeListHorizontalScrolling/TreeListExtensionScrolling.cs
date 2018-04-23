using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;

namespace TreeListHorizontalScrolling {
    public class TreeListExtensionScrolling {

        TreeList treeList1;
        int FixedColumnsCount = 0;

        public TreeListExtensionScrolling(TreeList treeList) {
            treeList1 = treeList;
            treeList1.ColumnChanged += treeList1_ColumnChanged;
        }

        public void ScrollTo(int columnVisibleIndex) {
            int index = Math.Max(columnVisibleIndex, FixedColumnsCount);
            Scroll(index);
        }

        public void ScrollTo(TreeListColumn column) {
            int index = column.VisibleIndex;
            Scroll(index);
        }

        private void Scroll(int index) {
            int width = 0;
            for (int i = FixedColumnsCount; i < index; i++) {
                width += treeList1.GetColumnByVisibleIndex(i).Width;
            }
            treeList1.LeftCoord = width;
        }

        public void UpdateFixedColumnsCount() {
            FixedColumnsCount = 0;
            for (int i = 0; i < treeList1.Columns.Count; i++)
                if (treeList1.Columns[i].Fixed != FixedStyle.None)
                    FixedColumnsCount++;
        }

        public void ScrollForward() {
            int width = 0;
            for (int i = FixedColumnsCount; i < treeList1.Columns.Count; i++) {
                width += treeList1.GetColumnByVisibleIndex(i).Width;
                if (treeList1.LeftCoord < width) {
                    treeList1.LeftCoord = width;
                    break;
                }
                if (treeList1.LeftCoord == width && i + 1 != treeList1.Columns.Count) {
                    treeList1.LeftCoord += treeList1.GetColumnByVisibleIndex(i + 1).Width;
                    break;
                }
            }
        }

        public void ScrollBackward() {
            int width = 0;
            for (int i = FixedColumnsCount; i < treeList1.Columns.Count; i++) {
                width += treeList1.GetColumnByVisibleIndex(i).Width;
                if (treeList1.LeftCoord < width) {
                    treeList1.LeftCoord -= treeList1.LeftCoord - (width - treeList1.GetColumnByVisibleIndex(i).Width);
                    break;
                }
                if (treeList1.LeftCoord == width) {
                    treeList1.LeftCoord -= treeList1.GetColumnByVisibleIndex(i).Width;
                    break;
                }
            }
        }


        public int UpdateIndexInEditor() {
            int index = FixedColumnsCount;
            int width = 0;
            if (treeList1.LeftCoord != 0)
                for (int i = index; i < treeList1.Columns.Count; i++) {
                    width += treeList1.GetColumnByVisibleIndex(i).Width;
                    if (treeList1.LeftCoord < width) {
                        index = i;
                        break;
                    }
                    if (treeList1.LeftCoord == width) {
                        index = i + 1;
                        break;
                    }
                }
            return index;
        }

        private void treeList1_ColumnChanged(object sender, ColumnChangedEventArgs e) {
            UpdateFixedColumnsCount();
        }
    }
}
