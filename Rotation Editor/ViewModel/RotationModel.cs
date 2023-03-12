using RotationHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Rotation_Editor.ViewModel
{
	public class RotationModel
	{
		public int PlateID { get; set; }
		public double TimeStamp { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public double Angle { get; set; }
		public int ConjugateID { get; set; }
		public string Comment { get; set; }

		public static explicit operator RotationModel(RotationEvent rotEvent) => new RotationModel()
		{
			PlateID = rotEvent.PlateID,
			TimeStamp = rotEvent.TimeStamp,
			ConjugateID = rotEvent.ConjugatePlateID,
			Comment = rotEvent.Comment,
			Longitude = rotEvent.Coordinates.Longitude,
			Latitude = rotEvent.Coordinates.Latitude,
			Angle = rotEvent.Coordinates.Angle,
		};
	}

	public class ReconstructionModel : INotifyPropertyChanged
	{
		private ObservableCollection<RotationModel> _rotations;
		public ObservableCollection<RotationModel> Rotations
		{
			get => this._rotations;
			set
			{
				if (value != this._rotations)
				{
					this._rotations = value;
					OnPropertyChanged();
				}
			}
		}

		HashSet<int> PlateIds;
		public List<int> GetPlateIDs => PlateIds.ToList();

		public ReconstructionModel()
		{
			Rotations = new();
			PlateIds = new();
		}

		internal void AddRotation(RotationModel model)
		{
			if (!PlateIds.Contains(model.PlateID))
				PlateIds.Add(model.PlateID);
			Rotations.Add(model);

			OnPropertyChanged(nameof(Rotations));
		}
		internal void InsertRotation(int position, RotationModel model)
		{
			Rotations.Insert(position, model);

			OnPropertyChanged(nameof(Rotations));
		}

		public (double latitude, double longitude, double angle) GetCoordinatesOfIDAtTimestep(int plateId, double timeStamp, bool isUpper = true)
		{
			var plateRotations = Rotations.Where(r => r.PlateID == plateId).Where(r =>r.TimeStamp == timeStamp).ToList();
			if (plateRotations.Any())
			{
				if(plateRotations.Count == 1)
					return ( plateRotations[0].Latitude, plateRotations[0].Longitude, plateRotations[0].Angle);
				else
				{
					if (isUpper)
						return (plateRotations[0].Latitude, plateRotations[0].Longitude, plateRotations[0].Angle);
					else
						return (plateRotations[1].Latitude, plateRotations[1].Longitude, plateRotations[1].Angle);
				}
			}
			else
				throw new ArgumentException("There is no Coordinates for this ID at this Timestamp");
		}    
		public bool IsIDInUse(int id)
		{
			if (id == 0)
				return true;
			else
				return PlateIds.Contains(id);
		}
		public int GeneratePlateID(int parentID = 1)
		{
			int gen = parentID;
			while (IsIDInUse(gen))
			{
				gen++;
			}
			return gen;
		}
    
		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}