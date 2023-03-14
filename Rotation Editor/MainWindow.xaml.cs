using Microsoft.Win32;
using Rotation_Editor.Tools;
using Rotation_Editor.ViewModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
			foreach (int plateId in Model.GetPlateIDs)
			{
				if (plateId == 1)
					continue;//leave plateID 1 alone, as it is used for other purposes
				var myRots = Model.Rotations.Where(rot => rot.PlateID == plateId);
				var lastRotation = myRots.Where(rot => rot.TimeStamp > 1.0).OrderBy(x => x.TimeStamp).First();
				var rotation1 = myRots.FirstOrDefault(rot => rot.TimeStamp == 1.0);
				if (lastRotation != null)
				{
					if (lastRotation.TimeStamp == Model.SimulationStart)
						continue;//if a plate has only one entry at simulation's start, leave it alone, it didn't move independently at all
					if (rotation1 == null)
					{
						//there is no already existing drift correcting rotation entry
						//let's create one:
						rotation1 = new()
						{
							PlateID = plateId,
							TimeStamp = 1.0,
							Latitude = lastRotation.Latitude,
							Longitude = lastRotation.Longitude,
							Angle = lastRotation.Angle,
							ConjugateID = lastRotation.ConjugateID,
							Comment = "Drift correction",
						};
						int lastRotIndex = Model.Rotations.IndexOf(lastRotation);
						Model.InsertRotation(lastRotIndex, rotation1);
					}
					else
					{
						//there is a drift correction entry we need to update
						rotation1.Latitude = lastRotation.Latitude;
						rotation1.Longitude = lastRotation.Longitude;
						rotation1.Angle = lastRotation.Angle;
						rotation1.ConjugateID = lastRotation.ConjugateID;
						rotation1.Comment = "Drift correction";
					}
				}
			}

			SaveModelToFile(FileName);
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
					if (coordinateForm.ShowDialog() == true)
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
							ConjugateID = parentPlateId,
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
			PlateID plateIdForm = new();
			if (plateIdForm.ShowDialog() == true)
			{
				TimeStamp timeStampForm = new();
				if (timeStampForm.ShowDialog() == true)
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
			TwoPlateID twoPlateForm = new();
			if (twoPlateForm.ShowDialog() == true)
			{
				int parentPlateId = Model.GetPlateIDs[twoPlateForm.FirstSelectedIndex];
				int childPlateId = Model.GetPlateIDs[twoPlateForm.SecondSelectedIndex];

				if (childPlateId < parentPlateId)
					(childPlateId, parentPlateId) = (parentPlateId, childPlateId);

				TimeStamp timeStampForm = new();
				if (timeStampForm.ShowDialog() == true)
				{
					double joinTimeStamp = timeStampForm.DesiredTimestamp;

					Coordinate coordForm = new()
					{
						HelpText = $"1. In GPlates: Specify Anchored Plate ID (Ctrl+D): set the parent ID {parentPlateId}.\n2. In GPlates: Total Reconstruction Poles(Ctrl + P): Equivalent Rotations relative to Anchored Plate, fetch the child's ({childPlateId}) coordinates.",
					};

					if (coordForm.ShowDialog() == true)
					{
						RotationModel rot = new()
						{
							PlateID = childPlateId,
							TimeStamp = joinTimeStamp,
							ConjugateID = parentPlateId,
							Comment = $"{childPlateId} start following {parentPlateId}",
							Latitude = coordForm.Latitude,
							Longitude = coordForm.Longitude,
							Angle = coordForm.Angle,
						};

						var x = Model.Rotations.First(o => o.PlateID == childPlateId && o.TimeStamp == joinTimeStamp);
						int index = Model.Rotations.IndexOf(x);

						Model.InsertRotation(index, rot);

						var first = Model.Rotations.First(o => o.PlateID == childPlateId && o.TimeStamp == 0.0D);
						first.ConjugateID = parentPlateId;

						SaveModelToFile(FileName);
					}
				}
			}
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