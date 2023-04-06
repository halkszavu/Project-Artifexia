using Microsoft.Win32;
using RotationEditor.ViewModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RotationEditor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string FileName;
		public ReconstructionModel Model { get; private set; }

		public MainWindow()
		{
			InitializeComponent();
			Model = new ReconstructionModel();

			this.DataContext = Model;
		}		
	}
}