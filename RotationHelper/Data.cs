using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RotationHelper
{
	public struct Coordinates : IEquatable<Coordinates>
	{
		public double Latitude;
		public double Longitude;
		public double Angle;

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (obj == null)
				return false;
			else if (obj.GetType() != typeof(Coordinates))
				return false;
			else
				return Equals((Coordinates)obj);
		}

		public bool Equals(Coordinates other)
		{
			if(Latitude != other.Latitude)
				return false;
			else if(Longitude != other.Longitude)
				return false;
			else if (Angle != other.Angle)
				return false;
			else
				return true;
		}

		public static bool operator ==(Coordinates lhs, Coordinates rhs) => lhs.Equals(rhs);

		public static bool operator !=(Coordinates lhs, Coordinates rhs) => !lhs.Equals(rhs);

		public override string ToString() => $"{Latitude} {Longitude} {Angle}";
	}

	public class RotationEvent : IEquatable<RotationEvent>
	{
		//400 1900.0   12.0868  -83.5756  -27.0759  000 ! Starts moving independently
		//ID timestamp coordinates1 2 3 conjugatePlateId ! comment

		public int PlateID { get; private set; }
		public double TimeStamp { get; set; }
		public Coordinates Coordinates { get; set; }
		public int ConjugatePlateID { get; set; }
		public string? Comment { get; set; }

		public RotationEvent(int plateId)
		{
			PlateID	= plateId;
		}

		public override bool Equals(object? obj)
		{
			if (obj == null)
				return false;
			else if (ReferenceEquals(this, obj))
				return true;
			else if (obj.GetType() != typeof(RotationEvent))
				return false;
			else
				return Equals((RotationEvent)obj);
		}

		public bool Equals(RotationEvent? other)
		{
			if (other == null)
				return false;
			else if (this.PlateID != other.PlateID)
				return false;
			else if (this.ConjugatePlateID != other.ConjugatePlateID)
				return false;
			else if (other.TimeStamp != other.TimeStamp)
				return false;
			else if (other.Coordinates != other.Coordinates)
				return false;
			else
				return true;
		}

		public override string ToString()
		{
			if (ConjugatePlateID == 0)
				return $"{PlateID} {TimeStamp} {Coordinates} 000 ! {Comment}";
			else
				return $"{PlateID} {TimeStamp} {Coordinates} {ConjugatePlateID} ! {Comment}";
		}
	}

	public class FullRotationReconstruction
	{
		Dictionary<int, List<RotationEvent>> rotations = new Dictionary<int, List<RotationEvent>>();

		public void AddNewRotation(RotationEvent rotation)
		{
			if (rotation == null)
				throw new ArgumentNullException("Rotation should never be null");
			if (rotation.PlateID == 0)
				throw new ArgumentException("PlateID 000 is used for other purposes, please do not use it!");

			if (rotations.ContainsKey(rotation.PlateID))
			{
				if (rotations[rotation.PlateID].Contains(rotation))
					throw new ArgumentException("Same rotation is already added");
				else
					rotations[rotation.PlateID].Add(rotation);
			}
			else
				rotations[rotation.PlateID] = new List<RotationEvent>() { rotation };
		}
	}
}