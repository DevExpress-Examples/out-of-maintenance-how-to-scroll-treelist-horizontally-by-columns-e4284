Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms

Namespace TreeListHorizontalScrolling
	Partial Public Class Form1
		Inherits Form


		Private list As New List(Of Customer)()
		Private Extension As TreeListExtensionScrolling

		Public Sub New()
			InitializeComponent()

			AddHandler textEdit1.KeyDown, AddressOf textEdit1_KeyDown
			AddHandler treeList1.LeftCoordChanged, AddressOf treeList1_LeftCoordChanged

			For i As Integer = 0 To 9
				list.Add(New Customer() With {.Zero = "1", .One = "1", .Two = "1", .Three = "1", .Four = "1", .Five = "1", .Six = "1"})
			Next i
			treeList1.DataSource = list

			Extension = New TreeListExtensionScrolling(treeList1)

			treeList1.Columns(0).Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left
			treeList1.Columns(3).Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left
		End Sub

		Private Sub treeList1_LeftCoordChanged(ByVal sender As Object, ByVal e As EventArgs)
			textEdit1.Text = Extension.UpdateIndexInEditor().ToString()
		End Sub

		Private Sub textEdit1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
			If e.KeyCode = Keys.Enter Then
				If textEdit1.Text <> String.Empty Then
					Extension.ScrollTo(Convert.ToInt32(textEdit1.Text))
				End If
			End If
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Extension.UpdateFixedColumnsCount()
			textEdit1.Text = Extension.UpdateIndexInEditor().ToString()
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			Extension.ScrollBackward()
		End Sub

		Private Sub simpleButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
			Extension.ScrollForward()
		End Sub
	End Class
End Namespace