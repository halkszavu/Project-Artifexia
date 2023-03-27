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

		public FullRotationReconstruction ReadFile(Stream fileStream)
		{
			using(var reader = new StreamReader(fileStream))
			{
				var reconstruction = ParserService.ParseReconstruction(reader.ReadToEnd());
				return reconstruction;
			}
		}

		public void WriteToFile(Stream fileStream, FullRotationReconstruction reconstruction)
		{
			using(StreamWriter writer = new StreamWriter(fileStream))
			{
				writer.Write(ParserService.PrintFullReconstruction(reconstruction));
			}
		}
	}
}
