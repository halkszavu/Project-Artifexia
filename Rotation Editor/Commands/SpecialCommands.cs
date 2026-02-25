using RotationEditor.ViewModel;
using RotationEditor.Views;
using RotationModel;
using RotationModel.Services;
using System.Linq;

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
		private readonly IRotModelService validateService;

		public ValidateCommand(IRotModelService validateService) : base()
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
		private readonly IRotModelService rotModelService;
		private readonly MainViewModel mainViewModel;

		public DriftCorrectionCommand(IRotModelService driftcorrectionService, MainViewModel mainViewModel) : base()
		{
			this.rotModelService = driftcorrectionService;
			this.mainViewModel = mainViewModel;
		}

		public override void Execute(object? parameter)
		{
			rotModelService.CreateDriftCorrection();

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

	public class NewPlateCommand : CommandBase
	{
		private readonly IRotModelService rotModelService;

		public NewPlateCommand(IRotModelService newPlateService) : base()
		{
			this.rotModelService = newPlateService;
		}

		public override void Execute(object? parameter)
		{
			var newPlateVM = new NewPlateIDViewModel(rotModelService);
			var timeStampVM = new TimeStampViewModel();
			var newPlateView = new NewPlateID() { DataContext = newPlateVM };
			var timeStampView = new TimeStamp() { DataContext = timeStampVM };
			if(newPlateView.ShowDialog() == true)
			{
				if(timeStampView.ShowDialog() == true)
				{
					rotModelService.NewPlateFirstStep(newPlateVM.NewPlate, newPlateVM.SelectedPlateId, timeStampVM.DesiredTimestamp);

					var coordsVM = new CoordinateViewModel()
					{
						HelpText = ""
					};
					var coordsView = new Coordinate() { DataContext = coordsVM };
					if(coordsView.ShowDialog() == true)
					{
						rotModelService.NewPlateSecondStep(coordsVM.GetCoordinates);
					}
				}
			}
		}
	}

	public class IndependentMoveCommand : CommandBase
	{
		private readonly IRotModelService rotModelService;

		public IndependentMoveCommand(IRotModelService independetMoveService) : base()
		{
			this.rotModelService = independetMoveService;
		}

		public override void Execute(object? parameter)
		{
			var plateIDVM = new PlateIDViewModel(rotModelService);
			var timestampVM = new TimeStampViewModel();
			var plateIDView = new PlateID() { DataContext = plateIDVM };
			var timestampView = new TimeStamp() { DataContext = timestampVM };

			if(plateIDView.ShowDialog() == true)
			{
				if(timestampView.ShowDialog() == true)
				{
					rotModelService.StartIndependentMove(plateIDVM.SelectedPlateID, timestampVM.DesiredTimestamp);
				}
			}
		}
	}

	public class JoinPlateCommand : CommandBase
	{
		private readonly IRotModelService rotModelService;

		public JoinPlateCommand(IRotModelService joinPlateService) : base()
		{
			this.rotModelService = joinPlateService;
		}

		public override void Execute(object? parameter)
		{
			var plateIDsVM = new TwoPlateIDViewModel(rotModelService);
			var timestampVM = new TimeStampViewModel();

			var plateIDsView = new TwoPlateID() { DataContext = plateIDsVM };
			var timestampView = new TimeStamp() { DataContext = timestampVM };

			if(plateIDsView.ShowDialog() == true)
			{
				if(timestampView.ShowDialog() == true)
				{
					var coordsVM = new CoordinateViewModel()
					{
						HelpText = "",
					};
					var coordsView = new Coordinate() { DataContext = coordsVM };

					if(coordsView.ShowDialog() == true)
						rotModelService.JoinIndependentPlates(plateIDsVM.FirstPlateID, plateIDsVM.SecondPlateID, timestampVM.DesiredTimestamp, coordsVM.GetCoordinates);
				}
			}
		}
	}
}