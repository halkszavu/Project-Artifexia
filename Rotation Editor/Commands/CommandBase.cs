using System;
using System.Windows.Input;

namespace RotationEditor.Commands
{
	/// <summary>
	/// Base class for implemented commands
	/// </summary>
	public abstract class CommandBase : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public virtual bool CanExecute(object? parameter) => true;

		public abstract void Execute(object? parameter);

		protected void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
