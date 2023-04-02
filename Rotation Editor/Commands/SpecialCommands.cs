using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotationEditor.ViewModel;
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



			newPlateService.NewPlateFirstStep(newPlateVM.NewPlate, timeStampVM.DesiredTimestamp);

			var coordsVM = new CoordinateViewModel();
			var gotCoords = new Coordinates(coordsVM.Latitude, coordsVM.Longitude, coordsVM.Angle);

			newPlateService.NewPlateSecondStep(gotCoords);
		}
	}

	public class IndependentMoveCommand : CommandBase
	{
		private readonly IStartIndependentMoveService independetMoveService;

		public IndependentMoveCommand(IStartIndependentMoveService independetMoveService) : base()
		{
			this.independetMoveService = independetMoveService;
		}

		public override void Execute(object? parameter)
		{

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

		}
	}
}