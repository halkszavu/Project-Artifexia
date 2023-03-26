using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor
{
	internal class TwoPlateIDViewModel : ViewModelBase
	{
		public List<int> PlateIDs
		{
			get => new() { 1, 100, 200, 300, 400, 500 }; //ParentWnd.Model.GetPlateIDs;
		}

		int _1selected;
		public int FirstSelectedIndex
		{
			get => _1selected;
			set
			{
				if (_1selected != value)
				{
					_1selected = value;
					OnPropertyChanged();
				}
			}
		}
		int _2selected;
		public int SecondSelectedIndex
		{
			get => _2selected;
			set
			{
				if (_2selected != value)
				{
					_2selected = value;
					OnPropertyChanged();
				}
			}
		}
	}
}
