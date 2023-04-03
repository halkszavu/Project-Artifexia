using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationModel
{
	public interface IDriftcorrectionService
	{
		void CreateDriftCorrection(string rotationFileName);
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
		void JoinIndependentPlates(int firstPlateId, int secondPlateId, double timeStamp, Coordinates coords);
	}

	public interface IValidateService
	{
		void Validate();
	}

	public interface IGetRotationsService
	{
		IEnumerable<RotationEvent> GetRotations { get; }
	}

	public interface IGetPlateIDsService
	{
		IEnumerable<int> GetPlateIDs { get; }
	}
}