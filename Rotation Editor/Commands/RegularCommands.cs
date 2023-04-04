using Microsoft.Win32;
using RotationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor.Commands
{
	public class ExitCommand : CommandBase
	{
		public override void Execute(object? parameter) => App.Current.Shutdown();
	}

	public class SaveCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{
			
		}
	}

	public class SaveAsCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{
			
		}
	}

	public class OpenCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{
			OpenFileDialog odlg = new()
			{
				DefaultExt = FileManipulationService.DefaultExtension,
			};
		}
	}

	public class NewCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{

		}
	}

	public class AboutCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{

		}
	}

	public class RefreshCommand : CommandBase
	{
		public override void Execute(object? parameter)
		{

		}
	}
}