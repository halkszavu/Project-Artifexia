using Microsoft.Win32;
using RotationEditor.ViewModel;
using RotationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor.Commands
{
	public class ExitCommand : CommandBase
	{
		public override void Execute(object? parameter) => App.Current.Shutdown();
	}

	public class SaveCommand : CommandBase
	{
		private readonly ISaveService saveService;

		public SaveCommand(ISaveService saveService)
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
		private readonly ISaveService saveService;
		private readonly MainViewModel mainViewModel;

		public SaveAsCommand(ISaveService saveService, MainViewModel mainViewModel)
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
		private readonly IUpdateService updateService;
		private readonly IGetRotationsService getRotationsService;
		private readonly MainViewModel mainViewModel;

		public OpenCommand(IUpdateService updateService, IGetRotationsService getRotationsService, MainViewModel mainViewModel) : base()
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

				mainViewModel.UpdateRotations(getRotationsService.GetRotations.Select(rotEvent=> 
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
		public override void Execute(object? parameter)
		{

		}
	}

	public class AboutCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{

		}
	}

	public class RefreshCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{

		}
	}
}