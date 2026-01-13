using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationModel
{
	public class RotationRecontstructionModel : IDriftcorrectionService, INewPlateService, IStartIndependentMoveService, IJoinIndependentService, IGetRotationsService, IGetPlateIDsService, IUpdateService, ISaveService, ICratonService
	{
		public double StartTime { get; private set; }
		public IEnumerable<RotationEvent> GetRotations => Rotations;
		public IEnumerable<int> GetPlateIDs => Rotations.Select(r => r.PlateID).Distinct();

		HashSet<int> plateIds;
		List<RotationEvent> Rotations;
		string rotationFileName;

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
					if (rotation.TimeStamp > StartTime)
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
				if (rotation.TimeStamp > StartTime)
					StartTime = rotation.TimeStamp;
			}
		}

		public void CreateDriftCorrection()
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

			SaveModel();
		}

		(int newPlateId, int parentPlateId, double timeStamp, int parentEntryIndex) newPlateData;
		public void NewPlateFirstStep(int newPlateId, int parentPlateId, double timeStamp)
		{			
			newPlateData = (newPlateId, parentPlateId, timeStamp, 0);
			RotationEvent plateMovingIndependently = new(newPlateId, timeStamp, Coordinates.Default, 0, $"{newPlateId} starts moving independently");
			RotationEvent plateAtEnd = new(newPlateId, 0.0D, Coordinates.Default, 0, $"{newPlateId} at the end");

			var parentLastEntry = Rotations.First(x => (x.PlateID == parentPlateId && x.TimeStamp == StartTime));
			int parentEntryIndex = Rotations.IndexOf(parentLastEntry);
			newPlateData.parentEntryIndex = parentEntryIndex;

			InsertRotation(parentEntryIndex + 1, plateMovingIndependently);
			InsertRotation(parentEntryIndex + 1, plateAtEnd);

			SaveModel();
		}

		public void NewPlateSecondStep(Coordinates gotCoordinates)
		{			
			RotationEvent plateEndFollowingParent = new(newPlateData.newPlateId, newPlateData.timeStamp, gotCoordinates, newPlateData.parentPlateId, $"{newPlateData.newPlateId} end following {newPlateData.parentPlateId} parent");
			RotationEvent plateAtStart = new(newPlateData.newPlateId, StartTime, gotCoordinates, newPlateData.parentPlateId, $"{newPlateData.newPlateId} at start");

			InsertRotation(newPlateData.parentEntryIndex + 3, plateEndFollowingParent);
			InsertRotation(newPlateData.parentEntryIndex + 4, plateAtStart);

			SaveModel();
		}

		public void StartIndependentMove(int plateId, double timeStamp)
		{
			var lastEntry = Rotations.First(rot => rot.PlateID == plateId && rot.TimeStamp > 1.0D);
			int originalConjugateID = lastEntry.ConjugatePlateID;
			int lastEntryIndex = Rotations.IndexOf(lastEntry);

			RotationEvent endFollowing = new(plateId, timeStamp, lastEntry.Coordinates, lastEntry.ConjugatePlateID, $"End following {originalConjugateID}");
			Coordinates conjugateCoords = GetCoordinatesOfIDAtTimestep(lastEntry.ConjugatePlateID, timeStamp);
			RotationEvent startIndependent = new(plateId, timeStamp, conjugateCoords, 0, "Start moving independently");

			InsertRotation(lastEntryIndex, endFollowing);
			InsertRotation(lastEntryIndex, startIndependent);

			SaveModel();
		}

		public void JoinIndependentPlates(int firstPlateId, int secondPlateId, double timeStamp, Coordinates coords)
		{
			int parentPlateId = firstPlateId >= secondPlateId ? firstPlateId : secondPlateId;
			int childPlateId = secondPlateId >= firstPlateId ? firstPlateId : secondPlateId;

			RotationEvent joiningEvent = new(childPlateId, timeStamp, coords, parentPlateId, $"{childPlateId} start following {parentPlateId}");

			var x = Rotations.First(o => o.PlateID == childPlateId && o.TimeStamp == timeStamp);
			int index = Rotations.IndexOf(x);

			InsertRotation(index, joiningEvent);

			var first = Rotations.First(o => o.PlateID == childPlateId && o.TimeStamp == 0.0D);
			first.ConjugatePlateID = parentPlateId;

			SaveModel();
		}

		public void Update(string fileName)
		{
			//this migth need to relocate
			//or at least re-done
			plateIds = new();
			Rotations = new();
			StartTime = 0.0D;
			rotationFileName = fileName;
			var tmp = FileManipulationService.ReadFile(File.Open(fileName, FileMode.Open));
			foreach (var rot in tmp.Rotations)
			{
				AddRotation(rot);
			}
		}

		public void Save()
		{
			SaveModel();
		}

		public void Save(string fileName)
		{
			rotationFileName = fileName;
			SaveModel();
		}

		void SaveModel()
		{
			if (string.IsNullOrEmpty(rotationFileName))
				throw new Exception();
			FileManipulationService.WriteToFile(File.Open(rotationFileName, FileMode.OpenOrCreate), this);
		}

		Coordinates GetCoordinatesOfIDAtTimestep(int plateId, double timeStamp, bool isUpper = true)
		{
			var plateRotations = Rotations.Where(r => r.PlateID == plateId).Where(r => r.TimeStamp == timeStamp).ToList();
			if (plateRotations.Any())
			{
				if (plateRotations.Count == 1)
					return plateRotations[0].Coordinates;
				else
				{
					if (isUpper)
						return plateRotations[0].Coordinates;
					else
						return plateRotations[1].Coordinates;
				}
			}
			else
				throw new ArgumentException("There is no Coordinates for this ID at this Timestamp");
		}

		public void ResetModel()
		{
			plateIds = new();
			Rotations = new();
			StartTime = 0.0D;
			rotationFileName = string.Empty;
		}

		public void AddCraton(int cratonId, string cratonName)
		{
			Rotations.Add(new RotationEvent(cratonId, 0.0D, Coordinates.Default, 0, cratonName));
			Rotations.Add(new RotationEvent(cratonId, StartTime, Coordinates.Default, 0, cratonName));
		}

		public void SetStartTime(double startTime)
		{
			StartTime = startTime;
		}
	}
}