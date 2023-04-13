using RotationModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor.ViewModel
{
	internal class TwoPlateIDViewModel : ViewModelBase
	{
		public ObservableCollection<int> PlateIDs { get; }

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

		public int FirstPlateID => PlateIDs[FirstSelectedIndex];
		public int SecondPlateID => PlateIDs[SecondSelectedIndex];

		public TwoPlateIDViewModel(IGetPlateIDsService rotationsService) : base()
		{
			PlateIDs = new ObservableCollection<int>(rotationsService.GetPlateIDs);
		}
	}
}
