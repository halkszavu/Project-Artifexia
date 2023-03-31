using RotationModel;
using System.IO;

namespace RotationEditor.Tools
{
	internal static class FileManipulationTool
	{
		internal static readonly string DefaultExtension = ".rot";

		internal static RotationModel.RotationModel ReadFile(Stream stream)
		{
			//using (StreamReader sr = new StreamReader(stream))
			//{
			//	var reconstruction = Parser.ParseReconstruction(sr.ReadToEnd());
			//	return reconstruction;
			//}
		}

		internal static void WriteFile(string destination, RotationModel.RotationModel reconstruction)
		{
			//using (StreamWriter sw = File.CreateText(destination))
			//{
			//	sw.Write(Parser.PrintFullReconstruction(reconstruction));
			//}
		}
	}
}
