using Microsoft.Win32;
using Rotation_Editor.Views;
using RotationEditor.ViewModel;
using RotationModel;
using RotationModel.Services;
using System.Linq;
using System.Windows;

namespace RotationEditor.Commands
{
	public class ExitCommand : CommandBase
	{
		public override void Execute(object? parameter) => App.Current.Shutdown();
	}

	public class SaveCommand : CommandBase
	{
		private readonly IRotFileService saveService;

		public SaveCommand(IRotFileService saveService)
		{
			this.saveService = saveService;
		}

		public override void Execute(object? parameter)
		{
			saveService.Save();
		}
	}

	public class SaveAsCommand : CommandBase
	{
		private readonly IRotFileService saveService;
		private readonly MainViewModel mainViewModel;

		public SaveAsCommand(IRotFileService saveService, MainViewModel mainViewModel)
		{
			this.saveService = saveService;
			this.mainViewModel = mainViewModel;
		}

		public override void Execute(object? parameter)
		{
			var sdlg = new SaveFileDialog()
			{
				DefaultExt = FileManipulationService.DefaultExtension,
				Filter = "Rotation files (*.rot)|*.rot|All files (*.*)|*.*",
			};

			if(sdlg.ShowDialog() == true)
			{
				mainViewModel.FileName = sdlg.FileName;
				saveService.Save(mainViewModel.FileName);
			}
		}
	}

	public class OpenCommand : CommandBase
	{
		private readonly IRotFileService updateService;
		private readonly IRotModelService getRotationsService;
		private readonly MainViewModel mainViewModel;

		public OpenCommand(IRotFileService updateService, IRotModelService getRotationsService, MainViewModel mainViewModel) : base()
		{
			this.updateService = updateService;
			this.getRotationsService = getRotationsService;
			this.mainViewModel = mainViewModel;
		}

		public override void Execute(object? parameter)
		{
			OpenFileDialog odlg = new()
			{
				DefaultExt = FileManipulationService.DefaultExtension,
				Filter = "Rotation files (*.rot)|*.rot|All files (*.*)|*.*",
			};

			if(odlg.ShowDialog() == true)
			{
				mainViewModel.FileName = odlg.FileName;
				updateService.Update(mainViewModel.FileName);

				mainViewModel.UpdateRotations(getRotationsService.GetRotations.Select(rotEvent =>
				new RotationViewModel(rotEvent.PlateID)
				{
					TimeStamp = rotEvent.TimeStamp,
					Latitude = rotEvent.Coordinates.Latitude,
					Longitude = rotEvent.Coordinates.Longitude,
					Angle = rotEvent.Coordinates.Angle,
					ConjugateID = rotEvent.ConjugatePlateID,
					Comment = rotEvent.Comment,
				}));
			}
		}
	}

	public class NewCommand : CommandBase
	{
		private readonly IRotModelService rotModelService;
		private readonly MainViewModel mainViewModel;

		public NewCommand(IRotModelService rotModelService, MainViewModel mainViewModel)
		{
			this.rotModelService = rotModelService;
			this.mainViewModel = mainViewModel;
		}

		public override void Execute(object? parameter)
		{
			// Generate the necessary cratons with the default rotation events
			mainViewModel.ResetViewModel();
			var cratonVM = new CratonCreationViewModel();
			var cratonGenerationDialog = new CratonCreation()
			{
				DataContext = cratonVM
			};

			if(cratonGenerationDialog.ShowDialog() == true)
			{
				rotModelService.ResetModel();
				rotModelService.SetStartTime(cratonVM.StartTime);
				// We have the craton data in cratonVM
				foreach(var craton in cratonVM.Cratons)
				{
					rotModelService.AddCraton(craton.ID, craton.Name);
				}
			}

			mainViewModel.UpdateRotations(rotModelService.GetRotations.Select(rotEvent =>
				new RotationViewModel(rotEvent.PlateID)
				{
					TimeStamp = rotEvent.TimeStamp,
					Latitude = rotEvent.Coordinates.Latitude,
					Longitude = rotEvent.Coordinates.Longitude,
					Angle = rotEvent.Coordinates.Angle,
					ConjugateID = rotEvent.ConjugatePlateID,
					Comment = rotEvent.Comment,
				}));
		}
	}

	public class AboutCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{
			MessageBox.Show(
				@"This program is for manipulating the .rot file associated with GPlates.
Created by: Lewis Callman",
				"About Rotation Editor", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}

	public class RefreshCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{

		}
	}
}