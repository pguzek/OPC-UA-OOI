using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UAOOI.SemanticData.TypeEditors
{
	public class StructureEditorModel
	{
		public static object ReflectedObject { get; set; }

		public static StructureEditorModel GetInstance()
		{
			return new StructureEditorModel(ReflectedObject);
		}

		public ModelNode Root { get; private set; }

		private StructureEditorModel(object reflectedObject)
		{
			List<ModelNode> instanceList = new List<ModelNode>();
			Root = new ModelNode(reflectedObject, reflectedObject.GetType().Name);
			instanceList.Add(Root);
			Root.Children = FindProperties(Root.Instance, instanceList, 256);
		}

		private static IEnumerable<ModelNode> FindProperties(object obj, IList<ModelNode> instances, int level)
		{
			if(level < 1 || obj == null)
			{
				return new List<ModelNode>();
			}

			Type type = obj.GetType();
			PropertyInfo[] properties = type.GetProperties();
			List<ModelNode> reflected = new List<ModelNode>();

			foreach(PropertyInfo prop in properties)
			{
				if(prop.GetIndexParameters().Length == 0)
				{
					ModelNode node = instances.FirstOrDefault(item => Object.ReferenceEquals(item.Instance, obj));

					if(node == null || node.TargetProp == null || !node.TargetProp.Equals(prop))
					{
						bool readOnly = !prop.CanWrite || prop.GetSetMethod() == null;
						node = new ModelNode(obj, prop, readOnly);
						instances.Add(node);
						node.Children = FindProperties(node.Instance, instances, level - 1);
					}

					reflected.Add(node);
				}
			}

			return reflected;
		}
	}
}
