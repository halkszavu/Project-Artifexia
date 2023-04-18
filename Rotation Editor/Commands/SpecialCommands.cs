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
	/// <summary>
	/// Command for testing purposes. This command can have various effects according to what needs to be tested
	/// </summary>
	public class TestingCommand : CommandBase
	{
		/// <summary>
		/// This is where the test should go.
		/// </summary>
		public override void Execute(object? parameter)
		{

		}
	}

	/// <summary>
	/// Command for validating the opened .rot database for format.
	/// </summary>
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

	/// <summary>
	/// Command for generating the drift correction.
	/// </summary>
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
			driftcorrectionService.CreateDriftCorrection();

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

	/// <summary>
	/// Command for generating a new plate.
	/// </summary>
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
					newPlateService.NewPlateFirstStep(newPlateVM.NewPlate, newPlateVM.SelectedPlateId, timeStampVM.DesiredTimestamp);

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

	/// <summary>
	/// Command for starting an independent movement of a given plate.
	/// </summary>
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

	/// <summary>
	/// Command for joining a plate to another plate at a given point in time.
	/// </summary>
	public class JoinPlateCommand : CommandBase
	{
		private readonly IJoinIndependentService joinPlateService;
		private readonly IGetPlateIDsService plateIDsService;

		public JoinPlateCommand(IJoinIndependentService joinPlateService, IGetPlateIDsService plateIDsService) : base()
		{
			this.joinPlateService = joinPlateService;
			this.plateIDsService = plateIDsService;
		}

		public override void Execute(object? parameter)
		{
			var plateIDsVM = new TwoPlateIDViewModel(plateIDsService);
			var timestampVM = new TimeStampViewModel();

			var plateIDsView = new TwoPlateID() { DataContext = plateIDsVM };
			var timestampView = new TimeStamp() { DataContext = timestampVM };

			if (plateIDsView.ShowDialog() == true)
			{
				if (timestampView.ShowDialog() == true)
				{
					var coordsVM = new CoordinateViewModel()
					{
						HelpText = "",
					};
					var coordsView = new Coordinate() { DataContext = coordsVM };

					if(coordsView.ShowDialog() == true)
					joinPlateService.JoinIndependentPlates(plateIDsVM.FirstPlateID, plateIDsVM.SecondPlateID, timestampVM.DesiredTimestamp, coordsVM.GetCoordinates);
				}
			}
		}
	}
}