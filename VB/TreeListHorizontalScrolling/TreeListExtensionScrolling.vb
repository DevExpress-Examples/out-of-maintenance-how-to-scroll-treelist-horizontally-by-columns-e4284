Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList.Columns

Namespace TreeListHorizontalScrolling
	Public Class TreeListExtensionScrolling

		Private treeList1 As TreeList
		Private FixedColumnsCount As Integer = 0

		Public Sub New(ByVal treeList As TreeList)
			treeList1 = treeList
			AddHandler treeList1.ColumnChanged, AddressOf treeList1_ColumnChanged
		End Sub

		Public Sub ScrollTo(ByVal columnVisibleIndex As Integer)
			Dim index As Integer = Math.Max(columnVisibleIndex, FixedColumnsCount)
			Scroll(index)
		End Sub

		Public Sub ScrollTo(ByVal column As TreeListColumn)
			Dim index As Integer = column.VisibleIndex
			Scroll(index)
		End Sub

		Private Sub Scroll(ByVal index As Integer)
			Dim width As Integer = 0
			For i As Integer = FixedColumnsCount To index - 1
				width += treeList1.GetColumnByVisibleIndex(i).Width
			Next i
			treeList1.LeftCoord = width
		End Sub

		Public Sub UpdateFixedColumnsCount()
			FixedColumnsCount = 0
			For i As Integer = 0 To treeList1.Columns.Count - 1
				If treeList1.Columns(i).Fixed <> FixedStyle.None Then
					FixedColumnsCount += 1
				End If
			Next i
		End Sub

		Public Sub ScrollForward()
			Dim width As Integer = 0
			For i As Integer = FixedColumnsCount To treeList1.Columns.Count - 1
				width += treeList1.GetColumnByVisibleIndex(i).Width
				If treeList1.LeftCoord < width Then
					treeList1.LeftCoord = width
					Exit For
				End If
				If treeList1.LeftCoord = width AndAlso i + 1 <> treeList1.Columns.Count Then
					treeList1.LeftCoord += treeList1.GetColumnByVisibleIndex(i + 1).Width
					Exit For
				End If
			Next i
		End Sub

		Public Sub ScrollBackward()
			Dim width As Integer = 0
			For i As Integer = FixedColumnsCount To treeList1.Columns.Count - 1
				width += treeList1.GetColumnByVisibleIndex(i).Width
				If treeList1.LeftCoord < width Then
					treeList1.LeftCoord -= treeList1.LeftCoord - (width - treeList1.GetColumnByVisibleIndex(i).Width)
					Exit For
				End If
				If treeList1.LeftCoord = width Then
					treeList1.LeftCoord -= treeList1.GetColumnByVisibleIndex(i).Width
					Exit For
				End If
			Next i
		End Sub


		Public Function UpdateIndexInEditor() As Integer
			Dim index As Integer = FixedColumnsCount
			Dim width As Integer = 0
			If treeList1.LeftCoord <> 0 Then
				For i As Integer = index To treeList1.Columns.Count - 1
					width += treeList1.GetColumnByVisibleIndex(i).Width
					If treeList1.LeftCoord < width Then
						index = i
						Exit For
					End If
					If treeList1.LeftCoord = width Then
						index = i + 1
						Exit For
					End If
				Next i
			End If
			Return index
		End Function

		Private Sub treeList1_ColumnChanged(ByVal sender As Object, ByVal e As ColumnChangedEventArgs)
			UpdateFixedColumnsCount()
		End Sub
	End Class
End Namespace
