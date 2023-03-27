using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RotationEditor
{
	public class MainViewModel : ViewModelBase
	{
		public ICommand TestingCommand { get; }
		public ICommand ExitCommand { get; }
		public ICommand SaveCommand { get; }
		public ICommand SaveAsCommand { get; }
		public ICommand OpenCommand { get; }
		public ICommand NewCommand { get; }
		public ICommand AboutCommand { get; }
		public ICommand RefreshCommand { get; }

		public ObservableCollection<RotationViewModel> Rotations { get; set; }

		public MainViewModel()
		{
			ExitCommand = new ExitCommand();
			TestingCommand = new TestingCommand();
		}
	}
}