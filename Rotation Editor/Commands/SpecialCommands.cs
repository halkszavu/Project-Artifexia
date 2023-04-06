using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotationEditor.ViewModel;
using RotationEditor.Views;
using RotationEditor.Resources;
using RotationModel;

namespace RotationEditor.Commands
{
	public class TestingCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{

		}
	}

	public class ValidateCommand : CommandBase
	{
		private readonly IValidateService validateService;

		public ValidateCommand(IValidateService validateService) : base()
		{
			this.validateService = validateService;
		}

		//Temporary:
		public ValidateCommand() { }
		public override bool CanExecute(object? parameter) => false;

		public override void Execute(object? parameter)
		{

		}
	}

	public class DriftCorrectionCommand : CommandBase
	{
		private readonly IDriftcorrectionService driftcorrectionService;
		private readonly IGetRotationsService getRotationsService;
		private readonly MainViewModel mainViewModel;

		public DriftCorrectionCommand(IDriftcorrectionService driftcorrectionService, IGetRotationsService getRotationsService, MainViewModel mainViewModel) : base()
		{
			this.driftcorrectionService = driftcorrectionService;
			this.getRotationsService = getRotationsService;
			this.mainViewModel = mainViewModel;
		}

		public override void Execute(object? parameter)
		{
			driftcorrectionService.CreateDriftCorrection(mainViewModel.FileName);

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

	public class NewPlateCommand : CommandBase
	{
		private readonly INewPlateService newPlateService;
		private readonly IGetPlateIDsService plateIDsService;

		public NewPlateCommand(INewPlateService newPlateService, IGetPlateIDsService plateIDsService) : base()
		{
			this.newPlateService = newPlateService;
			this.plateIDsService = plateIDsService;
		}

		public override void Execute(object? parameter)
		{
			var newPlateVM = new NewPlateIDViewModel(plateIDsService);
			var timeStampVM = new TimeStampViewModel();
			var newPlateView = new NewPlateID() { DataContext = newPlateVM };
			var timeStampView = new TimeStamp() { DataContext = timeStampVM };
			if (newPlateView.ShowDialog() == true)
			{
				if (timeStampView.ShowDialog() == true)
				{
					newPlateService.NewPlateFirstStep(newPlateVM.NewPlate, timeStampVM.DesiredTimestamp);

					var coordsVM = new CoordinateViewModel() 
					{
						HelpText = "" 
					};
					var coordsView = new Coordinate() { DataContext = coordsVM };
					if (coordsView.ShowDialog() == true)
					{
						newPlateService.NewPlateSecondStep(coordsVM.GetCoordinates);
					}
				}
			}
		}
	}

	public class IndependentMoveCommand : CommandBase
	{
		private readonly IStartIndependentMoveService independentMoveService;
		private readonly IGetPlateIDsService plateIDsService;

		public IndependentMoveCommand(IStartIndependentMoveService independetMoveService, IGetPlateIDsService plateIDsService) : base()
		{
			this.independentMoveService = independetMoveService;
			this.plateIDsService = plateIDsService;
		}

		public override void Execute(object? parameter)
		{
			var plateIDVM = new PlateIDViewModel(plateIDsService);
			var timestampVM = new TimeStampViewModel();
			var plateIDView = new PlateID() { DataContext = plateIDVM };
			var timestampView = new TimeStamp() { DataContext = timestampVM };

			if (plateIDView.ShowDialog() == true)
			{
				if (timestampView.ShowDialog() == true)
				{
					independentMoveService.StartIndependentMove(plateIDVM.SelectedPlateID, timestampVM.DesiredTimestamp);
				}
			}
		}
	}

	public class JoinPlateCommand : CommandBase
	{
		private readonly IJoinIndependentService joinPlateService;
		private readonly IGetPlateIDsService plateIDsService;
		private readonly MainViewModel mainViewModel;

		public JoinPlateCommand(IJoinIndependentService joinPlateService, IGetPlateIDsService plateIDsService, MainViewModel mainViewModel) : base()
		{
			this.joinPlateService = joinPlateService;
			this.plateIDsService = plateIDsService;
			this.mainViewModel = mainViewModel;
		}

		public override void Execute(object? parameter)
		{
			var plateIDsVM = new TwoPlateIDViewModel(plateIDsService);
			var timestampVM = new TimeStampViewModel();

			var plateIDView = new PlateID() { DataContext = plateIDsVM };
			var timestampView = new TimeStamp() { DataContext = timestampVM };

			if (plateIDView.ShowDialog() == true)
			{
				if (timestampView.ShowDialog() == true)
				{
					var coordsVM = new CoordinateViewModel()
					{
						HelpText = "",
					};
					var coordsView = new Coordinate() { DataContext = coordsVM };

					if(coordsView.ShowDialog() == true)
					joinPlateService.JoinIndependentPlates(plateIDsVM.FirstPlateID, plateIDsVM.SecondPlateID, timestampVM.DesiredTimestamp, coordsVM.GetCoordinates, mainViewModel.FileName);
				}
			}
		}
	}
}