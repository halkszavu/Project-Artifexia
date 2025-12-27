using RotationEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor.Commands
{
	public class GenerateNewPlateIDCommand : CommandBase
	{
		private readonly NewPlateIDViewModel baseViewModel;

		public GenerateNewPlateIDCommand(NewPlateIDViewModel baseViewModel) : base()
		{
			this.baseViewModel = baseViewModel;
		}

		public override void Execute(object? parameter)
		{
			int x = baseViewModel.SelectedPlateId;
			do
			{
				x++;
			} while (baseViewModel.PlateIDs.Contains(x));

			baseViewModel.NewPlate = x;
		}
	}

	public class GenerateCratonsCommand : CommandBase
	{
		private readonly CratonCreationViewModel cratonCreationViewModel;

		public GenerateCratonsCommand(CratonCreationViewModel cratonCreationViewModel) : base()
		{
			this.cratonCreationViewModel = cratonCreationViewModel;
		}

		public override void Execute(object? parameter)
		{
			cratonCreationViewModel.GenerateCratons();
		}
	}
}
