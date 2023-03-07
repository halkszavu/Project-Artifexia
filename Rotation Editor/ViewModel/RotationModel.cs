using RotationHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor.ViewModel
{
	internal class RotationModel
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

	internal class ReconstructionModel
	{
		internal List<RotationModel> Rotations { get; set; }

		internal void AddRotation(RotationModel model)
		{

		}
	}
}
