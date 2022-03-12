using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Input;

namespace Modelbuilder;

#region Remove Group Command
public class RemoveGroupCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.RemoveGroup();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public RemoveGroupCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Remove Group Command

#region Group by Project Command
public class GroupByProjectCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByProject();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public GroupByProjectCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by Project Command

#region Group by Worktype Command
public class GroupByWorktypeCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByWorktype();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public GroupByWorktypeCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by Project Command

#region Group by Year Command
public class GroupByYearCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByYear();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public GroupByYearCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by Year Command

#region Group by Month Command
public class GroupByMonthCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByMonth();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public GroupByMonthCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by Year Command

#region Group by YearMonth Command
public class GroupByYearMonthCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByYearMonth();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public GroupByYearMonthCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by YearMonth Command

#region Group by Day Command
public class GroupByDayCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByDay();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public GroupByDayCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by Day Command

#region Group by YearDay Command
public class GroupByYearDayCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByYearDay();
	}
	#endregion ICommand implementation

	private TimeViewModel _viewModel;

	public GroupByYearDayCommand(TimeViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by YearDay Command