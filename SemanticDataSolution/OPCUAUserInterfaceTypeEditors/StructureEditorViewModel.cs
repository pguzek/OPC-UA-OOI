using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UAOOI.SemanticData.TypeEditors
{
	public class StructureEditorViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
		{
			if(PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		//-------------------------------------------------------------------------------

		public ObservableCollection<TreeNode> TreeItemsSource { get; private set; }

		//-------------------------------------------------------------------------------

		public StructureEditorModel Model { get; private set; }

		//-------------------------------------------------------------------------------

		public StructureEditorViewModel()
		{
			Model = StructureEditorModel.GetInstance();
			TreeItemsSource = new ObservableCollection<TreeNode>();
			TreeItemsSource.Add(new TreeNode(Model.Root));
			NotifyPropertyChanged("TreeItemsSource");
		}
	}
}
