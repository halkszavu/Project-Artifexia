using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor
{
	internal class PlateIDViewModel : ViewModelBase
	{
		public List<int> PlateIDs
		{
			get => ParentWnd.Model.GetPlateIDs;
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
	}
}
