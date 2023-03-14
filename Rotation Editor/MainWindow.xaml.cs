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
			RotationModel plateAtEnd, plateMovingIndependently, plateEndFollowingParent, plateAtStart;
			var newPlateIdForm = new NewPlateID();
			if (newPlateIdForm.ShowDialog() == true)
			{
				int newPlateId = newPlateIdForm.NewPlate;
				int parentPlateId = Model.GetPlateIDs[newPlateIdForm.SelectedPlateIndex];
				plateAtEnd = new()
				{
					PlateID = newPlateId,
					TimeStamp = 0.0D,
					Latitude = 90.0D,
					Longitude = 0.0D,
					Angle = 0.0D,
					ConjugateID = 0,
					Comment = $"{newPlateId} at the end",
				};
				var timeStampForm = new TimeStamp();
				if (timeStampForm.ShowDialog() == true)
				{
					double newTimeStamp = timeStampForm.DesiredTimestamp;
					plateMovingIndependently = new()
					{
						PlateID = newPlateId,
						TimeStamp = newTimeStamp,
						Latitude = 90.0D,
						Longitude = 0.0D,
						Angle = 0.0D,
						ConjugateID = 0,
						Comment = $"{newPlateId} start moving independently",
					};

					var parentLastEntry = Model.Rotations.First(x => (x.PlateID == parentPlateId && x.TimeStamp == Model.SimulationStart));
					int parentEntryIndex = Model.Rotations.IndexOf(parentLastEntry);

					Model.InsertRotation(parentEntryIndex, plateMovingIndependently);
					Model.InsertRotation(parentEntryIndex, plateAtEnd);

					SaveModelToFile(FileName);
					//now prompt the user to reload the .rot to GPlates, and then get the Coordinates of the new plate relative to the Parent plate
					MessageBox.Show("The Rotation is now saved, please reload it in GPlates. Use Ctrl+M.", "Reload", MessageBoxButton.OK, MessageBoxImage.Information);

					var coordinateForm = new Coordinate
					{
						HelpText = "1. In GPlates: Specify Anchored Plate ID (Ctrl+D)-specify the parent plate ID\n2. In GPlates: Total Reconstruction Poles (Ctrl+P)-get Equivalent Rotations Relative to Anchored Plate ID, and fetch the coordinates"
					};
					if(coordinateForm.ShowDialog() == true)
					{
						plateEndFollowingParent = new()
						{
							PlateID = newPlateId,
							TimeStamp = newTimeStamp,
							Longitude = coordinateForm.Longitude,
							Latitude = coordinateForm.Latitude,
							Angle = coordinateForm.Angle,
							ConjugateID = parentPlateId,
							Comment = $"{newPlateId} end following {parentPlateId} parent",
						};

						plateAtStart = new()
						{
							PlateID = newPlateId,
							TimeStamp = Model.SimulationStart,
							Longitude = coordinateForm.Longitude,
							Latitude = coordinateForm.Latitude,
							Angle = coordinateForm.Angle,
							ConjugateID= parentPlateId,
							Comment = $"{newPlateId} at start"
						};

						Model.InsertRotation(parentEntryIndex + 3, plateEndFollowingParent);
						Model.InsertRotation(parentEntryIndex + 4, plateAtStart);

						SaveModelToFile(FileName);

						MessageBox.Show("The full Rotation is now saved. Reload it in GPlates and then Specify Anchored Plate (Ctrl+D) to 000, to reset the simulation", "New Plate ID added", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
			}
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
			//Parse existing file
			OpenFileDialog dlg = new()
			{
				DefaultExt = FileManipulationTool.DefaultExtension,
				Filter = "Rotation files (*.rot)|*.rot|All files (*.*)|*.*",
			};

			if (dlg.ShowDialog() == true)
			{
				FileName = dlg.FileName;

				ReconstructionModel m = Mapper.MapToModel(FileManipulationTool.ReadFile(new FileStream(FileName, FileMode.Open)));

				Model.Clear();
				foreach (var item in m.Rotations)
				{
					Model.AddRotation(item);
				}
			}
		}
		private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (Model.Rotations == null || Model.Rotations.Count == 0)
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

			if (dlg.ShowDialog() == true)
				SaveModelToFile(dlg.FileName);
		}
		private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
		private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
		{

		}

		private void TestingCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			//various testing behaviour
			//var formToShow = new TimeStamp();
			//formToShow.HelpText = "A very very very long helptext for the form.\n Still going on\n Still on and on...";
			//formToShow.ShowDialog();
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