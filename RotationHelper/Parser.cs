using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RotationHelper
{
	static public class Parser
	{//text -> Rotations and Rotations -> text
		public static string PrintFullReconstruction(FullRotationReconstruction reconstruction)
		{
			var rawRotations = reconstruction.Rotations.OrderBy(o => o.Key).SelectMany(rot => rot.Value).ToList();

			string print = string.Empty;

			foreach (RotationEvent rot in rawRotations)
			{
				print += rot.ToString();
				print += Environment.NewLine;
			}

			return print;
		}

		public static FullRotationReconstruction ParseReconstruction(string text)
		{
			FullRotationReconstruction parsedReconstruction = new FullRotationReconstruction();

			var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var line in lines)
			{
				RotationEvent rot = RotationEvent.Parse(line);
				parsedReconstruction.AddNewRotation(rot);
			}

			return parsedReconstruction;
		}
	}
}
