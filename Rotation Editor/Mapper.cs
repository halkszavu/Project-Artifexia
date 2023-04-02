using RotationModel;
using System.Collections.Generic;

namespace RotationEditor.ViewModel
{
	internal static class Mapper
	{
		public static global::RotationModel.RotationRecontstructionModel MapToData(ReconstructionModel model)
		{
			global::RotationModel.RotationRecontstructionModel reconstruction = new();
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

		public static ReconstructionModel MapToModel(global::RotationModel.RotationRecontstructionModel reconstruction)
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
