using RotationModel.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotationModel.Services
{
	public interface IRotModelService
	{
		IEnumerable<RotationEvent> GetRotations { get; }
		IEnumerable<int> GetPlateIDs { get; }
		void CreateDriftCorrection();
		void NewPlateFirstStep(int newPlateId, int parentPlateId, double timeStamp);
		void NewPlateSecondStep(Coordinates gotCoordinates);
		void StartIndependentMove(int plateId, double timeStamp);
		void JoinIndependentPlates(int firstPlateId, int secondPlateId, double timeStamp, Coordinates coords);
		void Validate();
		void ResetModel();
		void AddCraton(int cratonId, string cratonName);
		void SetStartTime(double startTime);
	}
}
