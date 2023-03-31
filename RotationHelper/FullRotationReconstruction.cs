﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationModel
{
	public class FullRotationReconstruction
	{
		public Dictionary<int, List<RotationEvent>> Rotations { get; }
		HashSet<int> plateIds;

		public FullRotationReconstruction()
		{
			Rotations = new Dictionary<int, List<RotationEvent>>();
			plateIds = new HashSet<int>();
		}

		public void AddNewRotation(RotationEvent rotation)
		{
			if (rotation == null)
				throw new ArgumentNullException($"{nameof(rotation)} should never be null");
			if (rotation.PlateID == 0)
				throw new ArgumentException("PlateID 000 is used for other purposes, please do not use it!");

			if (plateIds.Contains(rotation.PlateID))
			{
				if (Rotations[rotation.PlateID].Contains(rotation))
					throw new ArgumentException("Same rotation is already added");
				else
					Rotations[rotation.PlateID].Add(rotation);
			}
			else
			{
				plateIds.Add(rotation.PlateID);
				Rotations[rotation.PlateID] = new List<RotationEvent>() { rotation };
			}
		}
	}
}
