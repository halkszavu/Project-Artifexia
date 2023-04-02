using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor.ViewModel
{
	internal class NewPlateIDViewModel : ViewModelBase
	{
		public List<int> PlateIDs
		{
			get => new() { 1, 100, 200, 300, 400, 500 }; //ParentWnd.Model.GetPlateIDs;
		}

		int _newPlate;
		public int NewPlate
		{
			get => _newPlate;
			set
			{
				if (_newPlate != value)
				{
					_newPlate = value;
					OnPropertyChanged();
				}
			}
		}
		int _select;
		public int SelectedPlateIndex
		{
			get => _select;
			set
			{
				if (value != _select)
				{
					_select = value;
					OnPropertyChanged();
				}
			}
		}
	}
}