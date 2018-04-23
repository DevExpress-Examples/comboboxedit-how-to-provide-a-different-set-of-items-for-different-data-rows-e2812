Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Data
Imports DevExpress.Xpf.Grid

Namespace DynamicComboBoxItemsSource
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			Dim list As New List(Of TestData)()
			For i As Integer = 0 To 9
				list.Add(New TestData() With {.Text = "Row" & i, .Number = i})
			Next i
			grid.ItemsSource = list
		End Sub
	End Class

	Public Class TestData
		Private privateText As String
		Public Property Text() As String
			Get
				Return privateText
			End Get
			Set(ByVal value As String)
				privateText = value
			End Set
		End Property
		Private privateNumber As Integer
		Public Property Number() As Integer
			Get
				Return privateNumber
			End Get
			Set(ByVal value As Integer)
				privateNumber = value
			End Set
		End Property
	End Class

	Public Class TextToItemsSourceConverter
		Implements IValueConverter
		#Region "IValueConverter Members"
		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
			Dim list As New List(Of TestData)(New TestData() {New TestData() With {.Text = "Text0", .Number = 0}, New TestData() With {.Text = "Text1", .Number = 1}, New TestData() With {.Text = "Text2", .Number = 2}, New TestData() With {.Text = "Text3", .Number = 3}, New TestData() With {.Text = "Text4", .Number = 4}, New TestData() With {.Text = "Text5", .Number = 5}, New TestData() With {.Text = "Text6", .Number = 6}, New TestData() With {.Text = "Text7", .Number = 7}, New TestData() With {.Text = "Text8", .Number = 8}, New TestData() With {.Text = "Text9", .Number = 9}, New TestData() With {.Text = "Text10", .Number = 10}})
			Dim rowValueConverter As New RowPropertyValueConverter()
			Dim text As String = (TryCast(rowValueConverter, IValueConverter)).Convert(value, Nothing, "Text", Nothing).ToString()
			If String.IsNullOrEmpty(text) Then
				Return list
			End If
			If text = "Row0" Then
				list.RemoveRange(1, 2)
			End If
			If text = "Row3" Then
				list.RemoveRange(4, 3)
			End If
			Return list
		End Function
		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
		#End Region
	End Class
End Namespace
