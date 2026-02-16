using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RotationModel.Data
{
	[DebuggerDisplay("Lat: {Latitude} Lon: {Longitude} Ang: {Angle}")]
	public struct Coordinates : IEquatable<Coordinates>
	{
		public double Latitude;
		public double Longitude;
		public double Angle;

		public static Coordinates Default => new Coordinates(90.0D, 0.0D, 0.0D);

		public Coordinates(double latitude, double longitude, double angle)
		{
			Latitude = latitude;
			Longitude = longitude;
			Angle = angle;
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if(obj == null)
				return false;
			else if(obj.GetType() != typeof(Coordinates))
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
			else if(Angle != other.Angle)
				return false;
			else
				return true;
		}

		public static bool operator ==(Coordinates lhs, Coordinates rhs) => lhs.Equals(rhs);

		public static bool operator !=(Coordinates lhs, Coordinates rhs) => !lhs.Equals(rhs);

		public override string ToString() => $"{Latitude} {Longitude} {Angle}";

		public override int GetHashCode() => HashCode.Combine(Latitude, Longitude, Angle);
	}
}