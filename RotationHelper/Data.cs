using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

namespace RotationHelper
{
	[DebuggerDisplay("Lat: {Latitude} Lon: {Longitude} Ang: {Angle}")]
	public struct Coordinates : IEquatable<Coordinates>
	{
		public double Latitude;
		public double Longitude;
		public double Angle;

		public Coordinates(double latitude, double longitude, double angle)
		{
			Latitude = latitude;
			Longitude = longitude;
			Angle = angle;
		}

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
			if (Latitude != other.Latitude)
				return false;
			else if (Longitude != other.Longitude)
				return false;
			else if (Angle != other.Angle)
				return false;
			else
				return true;
		}

		public static bool operator ==(Coordinates lhs, Coordinates rhs) => lhs.Equals(rhs);

		public static bool operator !=(Coordinates lhs, Coordinates rhs) => !lhs.Equals(rhs);

		public override string ToString() => $"{Latitude} {Longitude} {Angle}";

		public override int GetHashCode() => Latitude.GetHashCode() ^ Longitude.GetHashCode() ^ Angle.GetHashCode();
	}

	[DebuggerDisplay("{PlateID} {TimeStamp} Coordinates {ConjugatePlateID} ! {Comment}")]
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
			PlateID = plateId;
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
			else if (this.TimeStamp != other.TimeStamp)
				return false;
			else if (this.Coordinates != other.Coordinates)
				return false;
			else
				return true;
		}

		public override string ToString()
		{
			if (ConjugatePlateID == 0)
				return $"{PlateID:D3} {TimeStamp:F1} {Coordinates} 000 ! {Comment}";
			else
				return $"{PlateID:D3} {TimeStamp:F1} {Coordinates} {ConjugatePlateID:D3} ! {Comment}";
		}

		public override int GetHashCode() => ToString().GetHashCode();

		public static RotationEvent Parse(string txt)
		{
			var rough = txt.Split('!');

			var contents = rough[0].Split(' ').Select(x => x.Trim()).Where(r => !string.IsNullOrEmpty(r)).ToArray();

			RotationEvent rotation = new RotationEvent(int.Parse(contents[0]))
			{
				TimeStamp = double.Parse(contents[1]),
				Coordinates = new Coordinates() 
				{
					Latitude = double.Parse(contents[2]),
					Longitude = double.Parse(contents[3]),
					Angle = double.Parse(contents[4]),
				},
				ConjugatePlateID = int.Parse(contents[5]),
				Comment = rough[1].Trim(),
			};

			return rotation;
		}
	}

	public class FullRotationReconstruction
	{
		public Dictionary<int, List<RotationEvent>> Rotations { get; private set; }

		public FullRotationReconstruction()
		{
			Rotations = new Dictionary<int, List<RotationEvent>>();
		}

		public void AddNewRotation(RotationEvent rotation)
		{
			if (rotation == null)
				throw new ArgumentNullException($"{nameof(rotation)} should never be null");
			if (rotation.PlateID == 0)
				throw new ArgumentException("PlateID 000 is used for other purposes, please do not use it!");

			if (Rotations.ContainsKey(rotation.PlateID))
			{
				if (Rotations[rotation.PlateID].Contains(rotation))
					throw new ArgumentException("Same rotation is already added");
				else
					Rotations[rotation.PlateID].Add(rotation);
			}
			else
				Rotations[rotation.PlateID] = new List<RotationEvent>() { rotation };
		}
	}
}