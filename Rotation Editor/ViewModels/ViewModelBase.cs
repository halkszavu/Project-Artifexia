using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RotationEditor.ViewModel
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public ICommand ConfirmCommand;
		public ICommand CancelCommand;

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string property = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
	}
}
