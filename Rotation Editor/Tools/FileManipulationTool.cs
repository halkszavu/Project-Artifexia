using RotationHelper;
using System.IO;

namespace Rotation_Editor.Tools
{
	internal static class FileManipulationTool
	{
		internal static readonly string DefaultExtension = ".rot";

		internal static FullRotationReconstruction ReadFile(Stream stream)
		{
			using (StreamReader sr = new StreamReader(stream))
			{
				var reconstruction = Parser.ParseReconstruction(sr.ReadToEnd());
				return reconstruction;
			}
		}

		internal static void WriteFile(string destination, FullRotationReconstruction reconstruction)
		{
			using (StreamWriter sw = File.CreateText(destination))
			{
				sw.Write(Parser.PrintFullReconstruction(reconstruction));
			}
		}
	}
}
