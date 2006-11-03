using System.Windows.Forms;

namespace PTM.View.Controls
{
	/// <summary>
	/// Descripción breve de DefaultTaskMenuItem.
	/// </summary>
	public class DefaultTaskMenuItem : MenuItem
	{
		public DefaultTaskMenuItem()
		{
		}
		public DefaultTaskMenuItem(int defaultTaskId)
		{
			this.defaultTaskId = defaultTaskId;
		}
		
		private int defaultTaskId;

		public int DefaultTaskId
		{
			get { return defaultTaskId; }
			set { defaultTaskId = value; }
		}
	}
}
