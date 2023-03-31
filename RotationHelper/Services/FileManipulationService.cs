using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RotationModel
{
	public class FileManipulationService
	{
		public string DefaultExtension { get => ".rot"; }

		public RotationModel ReadFile(Stream fileStream)
		{
			using (var reader = new StreamReader(fileStream))
			{
				var reconstruction = ParseReconstruction(reader.ReadToEnd());
				return reconstruction;
			}
		}

		public void WriteToFile(Stream fileStream, RotationModel reconstruction)
		{
			using (StreamWriter writer = new StreamWriter(fileStream))
			{
				writer.Write(PrintFullReconstruction(reconstruction));
			}
		}

		private static string PrintFullReconstruction(RotationModel reconstruction)
		{
			var firstOrdered = reconstruction.Rotations.OrderBy(o => o.Key).SelectMany(o => o.Value);
			var rawRotations = firstOrdered.OrderByDescending(rot => rot.TimeStamp).ToList();

			string print = string.Empty;

			foreach (RotationEvent rot in rawRotations)
			{
				print += rot.ToString();
				print += Environment.NewLine;
			}

			return print;
		}

		private static RotationModel ParseReconstruction(string text)
		{
			RotationModel parsedReconstruction = new RotationModel();

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