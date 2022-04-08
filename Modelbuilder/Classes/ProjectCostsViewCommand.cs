using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder;

#region Remove Group Command
public class RemoveProjectCostsGroupCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.RemoveProjectCostsGroup();
	}
	#endregion ICommand implementation

	private ProjectCostsViewModel _viewModel;

	public RemoveProjectCostsGroupCommand(ProjectCostsViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Remove Group Command

#region Group by Category Command
public class GroupByProjectCostsCategoryCommand : ICommand
{
	#region ICommand implementation
	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByProjectCostsCategory();
	}
	#endregion ICommand implementation

	private ProjectCostsViewModel _viewModel;

	public GroupByProjectCostsCategoryCommand(ProjectCostsViewModel viewModel)
	{
		this._viewModel = viewModel;
	}
}
#endregion Group by Project Command

#region Use 2 ViewModels in Project Tabs
public class ProjectViewModel
{
	public TimeViewModel TimeVM { get; set; }
	public ProjectCostsViewModel ProjectCostsVM { get; set; }
}
#endregion region Use 2 ViewModels in Project Tabs