using Microsoft.Win32;
using Rotation_Editor.Tools;
using Rotation_Editor.ViewModel;
using RotationHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
		private string FileName;
		public ReconstructionModel Model { get; private set; }

		public MainWindow()
		{
			InitializeComponent();
			Model = new ReconstructionModel();
			
			this.DataContext = Model;
						
		}

		//Drift correction
		private void btnDriftCorrection_Click(object sender, RoutedEventArgs e)
		{

		}

		//Create new container
		private void btnNewPlate_Click(object sender, RoutedEventArgs e)
		{
			var form = new NewPlateID();
			if (form.ShowDialog() == true)
			{
				
			}			
		}

		//Container starts moving independently
		private void btnStartIndependent_Click(object sender, RoutedEventArgs e)
		{
			PlateID plateIdForm = new();
			if (plateIdForm.ShowDialog() == true)
			{
				TimeStamp timeStampForm = new();
				if(timeStampForm.ShowDialog() == true)
				{
					int plateId = Model.GetPlateIDs[plateIdForm.SelectedIndex];

					var lastEntry = Model.Rotations.First(rot => rot.PlateID == plateId && rot.TimeStamp > 1.0D);
					int originalConjugateID = lastEntry.ConjugateID;
					int lastEntryIndex = Model.Rotations.IndexOf(lastEntry);

					RotationModel endFollowing = new()
					{
						PlateID = plateId,
						TimeStamp = timeStampForm.DesiredTimestamp,
						Latitude = lastEntry.Latitude,
						Longitude = lastEntry.Longitude,
						Angle = lastEntry.Angle,
						ConjugateID = originalConjugateID,
						Comment = $"End following {originalConjugateID}",
					};

					var conjugateCoordinates = Model.GetCoordinatesOfIDAtTimestep(originalConjugateID, timeStampForm.DesiredTimestamp);

					RotationModel startIndependent = new()
					{
						PlateID = plateId,
						TimeStamp = timeStampForm.DesiredTimestamp,
						Latitude = conjugateCoordinates.latitude,
						Longitude = conjugateCoordinates.longitude,
						Angle = conjugateCoordinates.angle,
						ConjugateID = 0,
						Comment = "Start moving independently",
					};

					Model.InsertRotation(lastEntryIndex, endFollowing);
					Model.InsertRotation(lastEntryIndex, startIndependent);

					SaveModelToFile(FileName);
				}
			}
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
			//Parse existing file
			OpenFileDialog dlg = new()
			{
				DefaultExt = FileManipulationTool.DefaultExtension,
				Filter = "Rotation files (*.rot)|*.rot|All files (*.*)|*.*",
			};			
			
			if(dlg.ShowDialog() == true)
			{
				FileName = dlg.FileName;

				var m = Mapper.MapToModel(FileManipulationTool.ReadFile(new FileStream(FileName, FileMode.Open)));
				Model.Rotations.Clear();
				foreach (var item in m.Rotations)
				{
					Model.Rotations.Add(item);
				}
			}
		}
		private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if(Model.Rotations == null || Model.Rotations.Count == 0)
				e.CanExecute = false;
			else
				e.CanExecute = true;
		}
		private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e) => SaveModelToFile(FileName);
		private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SaveFileDialog dlg = new()
			{
				DefaultExt = FileManipulationTool.DefaultExtension,
				Filter = "Rotation files (*.rot)|*.rot|All files (*.*)|*.*",
			};

			if( dlg.ShowDialog() == true)
				SaveModelToFile(dlg.FileName);
		}
		private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
		private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}

		//Actions:
		//Refresh file, as it has been modified by GPlates

		private void SaveModelToFile(string fileName)
		{
			//Save edits to the existing file/to new file
			var rot = Mapper.MapToData(Model);
			FileManipulationTool.WriteFile(FileName, rot);
		}
	}
}