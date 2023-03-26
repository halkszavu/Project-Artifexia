using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor
{
	internal class NewPlateIDViewModel : ViewModelBase
	{
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

		public List<int> PlateIDs
		{
			get => ParentWnd.Model.GetPlateIDs;
		}
	}
}
