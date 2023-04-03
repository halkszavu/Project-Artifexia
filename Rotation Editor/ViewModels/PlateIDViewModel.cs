using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor.ViewModel
{
	internal class PlateIDViewModel : ViewModelBase
	{
		public List<int> PlateIDs
		{
			get => new() { 1, 100, 200, 300, 400, 500 };// ParentWnd.Model.GetPlateIDs;
		}

		int _selected;
		public int SelectedIndex
		{
			get => _selected;
			set
			{
				if (_selected != value)
				{
					_selected = value;
					OnPropertyChanged();
				}
			}
		}

		public int SelectedPlateID => PlateIDs[_selected];
	}
}
