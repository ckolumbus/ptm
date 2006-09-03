using System;
using System.Windows.Forms;

namespace PTM.View.Controls.TreeListViewComponents
{
	/// <summary>
	/// Interface ITreeListViewItemComparer
	/// </summary>
	public interface ITreeListViewItemComparer : System.Collections.IComparer
	{
		/// <summary>
		/// Sort order
		/// </summary>
		SortOrder SortOrder
		{
			get;
			set;
		}
		/// <summary>
		/// Column for the comparison
		/// </summary>
		int Column
		{
			get;
			set;
		}
	}
}
