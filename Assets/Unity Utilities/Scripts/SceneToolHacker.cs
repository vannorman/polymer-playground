using System.Reflection;


using UnityEngine;
using UnityEditor;


// SceneToolHacker uses diabolical reflection technique to set the
// active tool. Note that this may well break with future Unity releases!!
public static class SceneToolHacker
{
	public enum Tool
	{
		DragCamera = 0,
		Translate = 1,
		Rotate = 2,
		Scale = 3
	}


	public static Tool CurrentTool
	{
		get { return (Tool)mTools_current.GetValue(null, null); }
		set { mTools_current.SetValue(null, (int)value, null); }
	}
	// "Sorry Virginia, there is no private."
	private static PropertyInfo mTools_current = typeof(Tools).GetProperty("current", BindingFlags.Static | BindingFlags.NonPublic);

}