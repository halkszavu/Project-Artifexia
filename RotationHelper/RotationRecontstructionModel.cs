using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static RotationModel.FileManipulationService;

namespace RotationModel
{
	public class RotationRecontstructionModel : IDriftcorrectionService, INewPlateService, IStartIndependentMoveService, IJoinIndependentService, IGetRotationsService, IGetPlateIDsService, IUpdateService
	{
		public double StartTime { get; private set; }
		public IEnumerable<RotationEvent> GetRotations => Rotations;
		public IEnumerable<int> GetPlateIDs => Rotations.Select(r => r.PlateID).Distinct();

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

		public void CreateDriftCorrection(string rotationFileName)
		{
			foreach (int plateID in plateIds)
			{
				if (plateID == 1)
					continue;

				var myRots = Rotations.Where(rot => rot.PlateID == plateID);
				var lastRotation = myRots.Where(rot => rot.TimeStamp > 1.0).OrderBy(x => x.TimeStamp).First();
				var rotation1 = myRots.FirstOrDefault(rot => rot.TimeStamp == 1.0);
				if (lastRotation != null)
				{
					if (lastRotation.TimeStamp == StartTime)
						continue;//if a plate has only one entry at simulation's start, leave it alone, it didn't move independently at all
					if (rotation1 == null)
					{
						//there is no already existing drift correcting rotation entry
						//let's create one:
						rotation1 = new(plateID, 1.0D, lastRotation.Coordinates, lastRotation.ConjugatePlateID, "Drift correction");
						int lastRotIndex = Rotations.IndexOf(lastRotation);
						InsertRotation(lastRotIndex, rotation1);
					}
					else
					{
						//there is a drift correction entry we need to update
						rotation1.Coordinates = lastRotation.Coordinates;
						rotation1.ConjugatePlateID = lastRotation.ConjugatePlateID;
						rotation1.Comment = "Drift correction";
					}
				}
			}

			WriteToFile(File.Open(rotationFileName,FileMode.Open), this);
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

		public void Update(string fileName)
		{
			//this migth need to relocate
			//or at least re-done
			plateIds = new();
			Rotations = new();
			StartTime = 0.0D;
			var tmp = ReadFile(File.Open(fileName,FileMode.Open));
			foreach (var rot in tmp.Rotations)
			{
				AddRotation(rot);
			}
		}
	}
}
