using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotationEditor.ViewModel;
using RotationEditor.Views;
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

		public DriftCorrectionCommand(IDriftcorrectionService driftcorrectionService) : base()
		{
			this.driftcorrectionService = driftcorrectionService;
		}

		public override void Execute(object? parameter)
		{
			driftcorrectionService.CreateDriftCorrection();
		}
	}

	public class NewPlateCommand : CommandBase
	{
		private readonly INewPlateService newPlateService;

		public NewPlateCommand(INewPlateService newPlateService) : base()
		{
			this.newPlateService = newPlateService;
		}

		public override void Execute(object? parameter)
		{
			var newPlateVM = new NewPlateIDViewModel();
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

		public IndependentMoveCommand(IStartIndependentMoveService independetMoveService) : base()
		{
			this.independentMoveService = independetMoveService;
		}

		public override void Execute(object? parameter)
		{
			var plateIDVM = new PlateIDViewModel();
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

		public JoinPlateCommand(IJoinIndependentService joinPlateService) : base()
		{
			this.joinPlateService = joinPlateService;
		}

		public override void Execute(object? parameter)
		{
			var plateIDsVM = new TwoPlateIDViewModel();
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
					joinPlateService.JoinIndependentPlates(plateIDsVM.FirstPlateID, plateIDsVM.SecondPlateID, timestampVM.DesiredTimestamp, coordsVM.GetCoordinates);
				}
			}
		}
	}
}