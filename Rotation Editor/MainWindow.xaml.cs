using Microsoft.Win32;
using Rotation_Editor.Tools;
using Rotation_Editor.ViewModel;
using RotationHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rotation_Editor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ReconstructionModel model;

		public MainWindow()
		{
			InitializeComponent();
			model = new ReconstructionModel();
			List<RotationModel> rotations = new()
			{
				new RotationModel()
				{
					PlateID = 1,
					TimeStamp = 2000,
					Latitude = 90,
					Longitude = 0,
					Angle = 0,
					ConjugateID = 0,
					Comment = "comment"
				},
				new RotationModel()
				{
					PlateID = 100,
					TimeStamp = 0,
					Latitude = 90,
					Longitude = 0,
					Angle = 0,
					ConjugateID = 0,
					Comment = "Plate A at the end"
				},
				new RotationModel()
				{
					PlateID = 100,
					TimeStamp = 2000,
					Latitude = 90,
					Longitude = 0,
					Angle = 0,
					ConjugateID = 0,
					Comment = "Plate A at the start"
				}
			};

			this.DataContext = model;

			foreach (var item in rotations)
			{
				model.AddRotation(item);
			}
		}

		//Drift correction
		private void btnDriftCorrection_Click(object sender, RoutedEventArgs e)
		{

		}

		//Create new container
		private void btnNewPlate_Click(object sender, RoutedEventArgs e)
		{

		}

		//Container starts moving independently
		private void btnStartIndependent_Click(object sender, RoutedEventArgs e)
		{

		}

		//Container joins exsisting container
		private void btnJoinIndependent_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btnValidate_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
		private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e) => Application.Current.Shutdown();
		private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
		private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog dlg = new()
			{
				DefaultExt = FileManipulationTool.DefaultExtension,
				Filter = "Rotation files (*.rot)|*.rot|All files (*.*)|*.*",
			};
			
			
			if(dlg.ShowDialog() == true)
			{
				var m = Mapper.MapToModel(FileManipulationTool.ReadFile(dlg.OpenFile()));
				model.Rotations.Clear();
				foreach (var item in m.Rotations)
				{
					model.Rotations.Add(item);
				}
			}
		}
		private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{

		}
		private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}
		private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
		private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}

		//Actions:
		//Parse existing file
		//Refresh file, as it has been modified by GPlates
		//Save edits to the existing file/to new file
	}
}