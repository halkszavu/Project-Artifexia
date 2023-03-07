using RotationHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rotation_Editor.ViewModel
{
	public class RotationModel
	{
		public int PlateID { get; set; }
		public double TimeStamp { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public double Angle { get; set; }
		public int ConjugateID { get; set; }
		public string Comment { get; set; }

		public static explicit operator RotationModel(RotationEvent rotEvent) => new RotationModel()
		{
			PlateID = rotEvent.PlateID,
			TimeStamp = rotEvent.TimeStamp,
			ConjugateID = rotEvent.ConjugatePlateID,
			Comment = rotEvent.Comment,
			Longitude = rotEvent.Coordinates.Longitude,
			Latitude = rotEvent.Coordinates.Latitude,
			Angle = rotEvent.Coordinates.Angle,
		};
	}

	public class ReconstructionModel : INotifyPropertyChanged
	{
		private ObservableCollection<RotationModel> _rotations;
		public ObservableCollection<RotationModel> Rotations
		{
			get => this._rotations;
			set
			{
				if (value != this._rotations)
				{
					this._rotations = value;
					OnPropertyChanged();
				}
			}
		}

		HashSet<int> PlateIds;

		public ReconstructionModel()
		{
			Rotations = new();
			PlateIds = new();
		}

		internal void AddRotation(RotationModel model)
		{
			if (!PlateIds.Contains(model.PlateID))
				PlateIds.Add(model.PlateID);
			Rotations.Add(model);

			OnPropertyChanged(nameof(Rotations));
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string  propertyName = "")=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
