using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RotationHelper;

namespace Rotation_Editor.Tools
{
	internal static class FileManipulationTool
	{
		internal static readonly string DefaultExtension = ".rot";

		internal static FullRotationReconstruction ReadFile(Uri source)
		{
			using (StreamReader sr = File.OpenText(source.AbsolutePath))
			{
				var reconstruction = Parser.ParseReconstruction(sr.ReadToEnd());
				return reconstruction;
			}
		}

		internal static void WriteFile(Uri destination, FullRotationReconstruction reconstruction)
		{
			using (StreamWriter sw = File.CreateText(destination.AbsolutePath))
			{
				sw.Write(Parser.PrintFullReconstruction(reconstruction));
			}
		}
	}
}
