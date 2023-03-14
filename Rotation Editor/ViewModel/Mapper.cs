using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotationHelper;

namespace Rotation_Editor.ViewModel
{
	internal static class Mapper
	{
		public static FullRotationReconstruction MapToData(ReconstructionModel model)
		{
			FullRotationReconstruction reconstruction = new();
			foreach (var item in model.Rotations)
			{
				Coordinates coordinates = new Coordinates(item.Latitude, item.Longitude, item.Angle);
				reconstruction.AddNewRotation(new RotationEvent(item.PlateID)
				{
					Coordinates = coordinates,
					ConjugatePlateID = item.ConjugateID,
					TimeStamp = item.TimeStamp,
					Comment = item.Comment,
				});
			}

			return reconstruction;
		}

		public static ReconstructionModel MapToModel(FullRotationReconstruction reconstruction)
		{
			ReconstructionModel model = new();
			foreach (KeyValuePair<int, List<RotationEvent>> kvp in reconstruction.Rotations)
			{
				foreach (var item in kvp.Value)
				{
					model.AddRotation((RotationModel)item);
				}
			}
			return model;
		}
	}
}
