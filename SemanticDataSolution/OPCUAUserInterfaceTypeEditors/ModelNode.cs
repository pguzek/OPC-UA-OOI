using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UAOOI.SemanticData.TypeEditors
{
	public class ModelNode
	{
		public IEnumerable<ModelNode> Children { get; set; }
		public string Name { get; private set; }
		public bool ReadOnly { get; private set; }
		public PropertyInfo TargetProp { get; private set; }
		private object instance;

		public object Instance
		{
			get
			{
				if(TargetProp != null)
				{
					return TargetProp.GetValue(instance);
				}
				else
				{
					return instance;
				}
			}
			set
			{
				TargetProp.SetValue(instance, Convert.ChangeType(value, TargetProp.PropertyType));
			}
		}

		public ModelNode(object instance, PropertyInfo targetProp, bool readOnly)
		{
			this.instance = instance;
			Name = targetProp.Name;
			ReadOnly = readOnly;
			TargetProp = targetProp;
		}

		public ModelNode(object instance, string name)
		{
			this.instance = instance;
			Name = name;
			ReadOnly = true;
			TargetProp = null;
		}
	}
}
