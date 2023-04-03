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

		public RotationRecontstructionModel ReadFile(Stream fileStream)
		{
			using (var reader = new StreamReader(fileStream))
			{
				var reconstruction = ParseReconstruction(reader.ReadToEnd());
				return reconstruction;
			}
		}

		public void WriteToFile(Stream fileStream, IGetRotationsService rotationService)
		{
			using (StreamWriter writer = new StreamWriter(fileStream))
			{
				writer.Write(PrintFullReconstruction(rotationService));
			}
		}

		private static string PrintFullReconstruction(IGetRotationsService rotationService)
		{
			string print = string.Empty;

			foreach (RotationEvent rot in rotationService.GetRotations)
			{
				print += rot.ToString();
				print += Environment.NewLine;
			}

			return print;
		}

		private static RotationRecontstructionModel ParseReconstruction(string text)
		{
			RotationRecontstructionModel parsedReconstruction = new RotationRecontstructionModel();

			var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var line in lines)
			{
				RotationEvent rot = RotationEvent.Parse(line);
				parsedReconstruction.AddRotation(rot);
			}

			return parsedReconstruction;
		}
	}
}