using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationModel
{
	public interface IDriftcorrectionService
	{
		void CreateDriftCorrection();
	}

	public interface INewPlateService
	{
		void NewPlateFirstStep(int newPlateid, double timeStamp);
		void NewPlateSecondStep(Coordinates gotCoordinates);
	}

	public interface IStartIndependentMoveService
	{
		void StartIndependentMove(int plateId, double timeStamp);
	}

	public interface IJoinIndependentService
	{

	}

	public interface IValidateService
	{
		void Validate();
	}

	public interface IGetRotationsService
	{
		IEnumerable<RotationEvent> GetRotations { get; }
	}
}