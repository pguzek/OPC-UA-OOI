using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UAOOI.SemanticData.TypeEditors
{
	public class TreeNode
	{
		public ModelNode PropertyModel { get; private set; }
		public ObservableCollection<TreeNode> Children { get; private set; }
		public object Value
		{
			get
			{
				return PropertyModel.Instance;
			}
			set
			{
				try
				{
					PropertyModel.Instance = value;
				}
				catch(FormatException e)
				{
					MessageBox.Show(e.Message, e.HelpLink);
				}
			}
		}

		private bool isExpanded;

		public bool IsExpanded
		{
			get { return isExpanded; }
			set
			{
				isExpanded = value;
				if(value)
				{
					BuildChildrenBranch();
				}
			}
		}

		public ICommand ModifyCommand { get; private set; }

		public TreeNode(ModelNode propertyModel)
		{
			Children = new ObservableCollection<TreeNode>();

			PropertyModel = propertyModel;
			if(PropertyModel != null && PropertyModel.Children.Count() > 0)
			{
				Children.Add(new TreeNode(null));
			}

			ModifyCommand = new CommandWrapper(param =>
			{
				TreeView tree = param as TreeView;
				if(tree != null)
				{
					tree.Items.Refresh();
				}
			}, param => true);
		}

		public void BuildChildrenBranch()
		{
			Children.Clear();
			foreach(ModelNode item in PropertyModel.Children)
			{
				Children.Add(new TreeNode(item));
			}
		}
	}
}
