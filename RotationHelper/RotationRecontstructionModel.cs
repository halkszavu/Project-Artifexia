using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationModel
{
	public class RotationRecontstructionModel : IDriftcorrectionService, INewPlateService, IStartIndependentMoveService, IJoinIndependentService, IGetRotationsService
	{
		public IEnumerable<RotationEvent> GetRotations => Rotations;
		public double StartTime { get; private set; }

		HashSet<int> plateIds;
		List<RotationEvent> Rotations;

		public RotationRecontstructionModel()
		{
			Rotations = new();
			plateIds = new HashSet<int>();
			StartTime = 0.0D;
		}

		public void AddRotation(RotationEvent rotation)
		{
			if (rotation == null)
				throw new ArgumentNullException($"rotation should never be null");
			if (rotation.PlateID == 0)
				throw new ArgumentException("PlateID 000 is used for other purposes, please do not use it!");

			if (plateIds.Contains(rotation.PlateID))
			{
				if (Rotations.Contains(rotation))
					throw new ArgumentException("Same rotation is already added");
				else
				{
					Rotations.Add(rotation);
					if(rotation.TimeStamp > StartTime)
						StartTime = rotation.TimeStamp;
				}
			}
			else
			{
				plateIds.Add(rotation.PlateID);
				Rotations.Add(rotation);
				if (rotation.TimeStamp > StartTime)
					StartTime = rotation.TimeStamp;
			}
		}

		public void InsertRotation(int index, RotationEvent rotation)
		{
			if (rotation == null)
				throw new ArgumentNullException($"rotation should never be null");
			if (rotation.PlateID == 0)
				throw new ArgumentException("PlateID 000 is used for other purposes, please do not use it!");

			if (plateIds.Contains(rotation.PlateID))
			{
				if (Rotations.Contains(rotation))
					throw new ArgumentException("Same rotation is already added");
				else
				{
					Rotations.Insert(index, rotation);
					if (rotation.TimeStamp > StartTime)
						StartTime = rotation.TimeStamp;
				}
			}
			else
			{
				plateIds.Add(rotation.PlateID);
				Rotations.Insert(index, rotation);
				if(rotation.TimeStamp > StartTime)
					StartTime=rotation.TimeStamp;
			}
		}

		public void CreateDriftCorrection()
		{
			
		}

		public void NewPlateFirstStep(int newPlateid, double timeStamp)
		{
			
		}

		public void NewPlateSecondStep(Coordinates gotCoordinates)
		{
			
		}

		public void StartIndependentMove(int plateId, double timeStamp)
		{
			
		}

		public void JoinIndependentPlates(int firstPlateId, int secondPlateId, double timeStamp, Coordinates coords)
		{
			
		}
	}
}
