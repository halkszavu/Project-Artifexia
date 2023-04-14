using System.Collections.Generic;

namespace RotationModel
{
	public interface IDriftcorrectionService
	{
		void CreateDriftCorrection();
	}

	public interface INewPlateService
	{
		void NewPlateFirstStep(int newPlateId, int parentPlateId, double timeStamp);
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

	public interface IUpdateService
	{
		void Update(string fileName);
	}

	public interface ISaveService
	{
		void Save();
		void Save(string fileName);
	}
}