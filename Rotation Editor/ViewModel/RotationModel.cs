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
	}

	internal class ReconstructionModel
	{
		List<RotationModel> Rotations { get; set; }
	}
}
