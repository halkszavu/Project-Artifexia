using RotationModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationEditor.ViewModel
{
	internal class PlateIDViewModel : ViewModelBase
	{
		public ObservableCollection<int> PlateIDs { get; }

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

		public int SelectedPlateID => PlateIDs[SelectedIndex];

		public PlateIDViewModel(IGetPlateIDsService rotationsService) : base()
		{
			PlateIDs = new ObservableCollection<int>(rotationsService.GetPlateIDs);
		}
	}
}