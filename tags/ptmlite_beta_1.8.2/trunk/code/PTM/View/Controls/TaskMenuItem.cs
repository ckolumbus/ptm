using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using PTM.Framework;

namespace PTM.View.Controls
{
	/// <summary>
	/// Descripción breve de TaskMenuItem.
	/// </summary>
	public class TaskMenuItem : MenuItem
	{
		public TaskMenuItem() : base()
		{
//			this.OwnerDraw = true;
//			this.DrawItem += new DrawItemEventHandler(TaskMenuItem_DrawItem);
//			this.MeasureItem += new MeasureItemEventHandler(TaskMenuItem_MeasureItem);
			TaskMenuItemHelper.Initialize();
		}

		public override MenuItem CloneMenu()
		{
			TaskMenuItem taskMenuItem = (TaskMenuItem) base.CloneMenu();
			;
			taskMenuItem.taskId = this.taskId;
			taskMenuItem.rectangle = this.rectangle;
			taskMenuItem.Pick = this.Pick;
			return taskMenuItem;
		}

		public override MenuItem MergeMenu()
		{
			TaskMenuItem taskMenuItem = (TaskMenuItem) base.MergeMenu();
			;
			taskMenuItem.taskId = this.taskId;
			taskMenuItem.rectangle = this.rectangle;
			taskMenuItem.Pick = this.Pick;
			return taskMenuItem;
		}


		protected override void OnSelect(EventArgs e)
		{
			TaskMenuItemHelper.selectedItem = this;
			base.OnSelect(e);
		}


		public TaskMenuItem(int defaultTaskId) : this()
		{
			this.taskId = defaultTaskId;
		}

		private int taskId;
		private Rectangle rectangle;

		public int TaskId
		{
			get { return taskId; }
			set { taskId = value; }
		}

		public event EventHandler Pick;

		protected override void OnClick(EventArgs e)
		{
			TaskMenuItemHelper.selectedItem = null;
			if (this.Pick != null)
				this.Pick(this, new EventArgs());
			base.OnClick(e);
		}
/*
		private void TaskMenuItem_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
			MenuItem mi = (MenuItem) sender;

			// Get standard menu font so that the text in this
			// menu rectangle doesn't look funny with a
			// different font
			Font menuFont = SystemInformation.MenuFont;

			// Get a brush to use for painting
			SolidBrush menuBrush;

			// Determine menu brush for painting
			if (mi.Enabled == false)
			{
				// disabled text if menu item not enabled
				menuBrush = new SolidBrush(SystemColors.GrayText);
			}
			else // Normal (enabled) text
			{
				if ((e.State & DrawItemState.Selected) != 0)
				{
					// Text color when selected (highlighted)
					menuBrush =
						new SolidBrush(SystemColors.HighlightText);
				}
				else
				{
					// Text color during normal drawing
					menuBrush = new SolidBrush(SystemColors.MenuText);
				}
			}

			// Center the text portion (out to side of image portion)
			StringFormat strfmt = new StringFormat();
			strfmt.LineAlignment = StringAlignment.Center;

			Icon icon = (Icon) IconsManager.CommonTaskIconsTable[Tasks.FindById(this.taskId).IconId];
			Icon icon16 = new Icon(icon, new Size(16, 16));
			// Rectangle for image portion
			Rectangle rectImage = e.Bounds;

			// Set image rectangle same dimensions as image
			rectImage.Width = icon16.Width;
			rectImage.Height = icon16.Height;

//			// Rectanble for text portion
//			Rectangle rectText = e.Bounds;
//
//			// set wideth to x value of text portion
//			rectText.X += rectImage.Width;

			// Start Drawing the menu rectangle

			// Fill rectangle with proper background color
			// [use this instead of e.DrawBackground() ]
			if ((e.State & DrawItemState.Selected) != 0)
			{
				// Selected color
				e.Graphics.FillRectangle(SystemBrushes.Highlight,
				                         e.Bounds);
			}
			else
			{
				// Normal background color (when not selected)
				e.Graphics.FillRectangle(SystemBrushes.Menu,
				                         e.Bounds);
			}

			// Draw image portion
			e.Graphics.DrawIcon(icon16, rectImage);

			// Draw string/text portion
			//
			// text portion
			// using menu font
			// using brush determined earlier
			// Start at offset of image rect already drawn
			// Total height,divided to be centered
			// Formated string
			e.Graphics.DrawString(mi.Text,
			                      menuFont,
			                      menuBrush,
			                      e.Bounds.Left + icon16.Width,
			                      e.Bounds.Top + ((e.Bounds.Height - menuFont.Height)/2));
		}

		private void TaskMenuItem_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			MenuItem mi = (MenuItem) sender;

			// Get standard menu font so that the text in this
			// menu rectangle doesn't look funny with a
			// different font
			Font menuFont = SystemInformation.MenuFont;

			StringFormat strfmt = new StringFormat();

			SizeF sizef =
				e.Graphics.MeasureString(mi.Text,
				                         menuFont,
				                         1000,
				                         strfmt);


			// Add image height and width  to the text height and width when
			// drawn with selected font (got that from measurestring method)
			// to compute the total height and width needed for the rectangle
			e.ItemWidth =
				(int) Math.Ceiling(sizef.Width) + SystemInformation.SmallIconSize.Width;
			e.ItemHeight = Math.Max((int) Math.Ceiling(sizef.Height), SystemInformation.SmallIconSize.Height + 2);
			//(int)Math.Ceiling(sizef.Height) + 16;
		}

*/
		[DllImport("user32.dll")]
		private static extern bool EndMenu();

		public void PerformPick()
		{
			TaskMenuItemHelper.selectedItem = null;
			if (!this.Visible)
				return;
			if (this.Pick != null)
				this.Pick(this, new EventArgs());
			EndMenu();
			TaskMenuItemHelper.selectedItem = null;
		}
	}

	public class TaskMenuItemHelper : IDisposable
	{
		private static MouseHook hook;
		internal static TaskMenuItem selectedItem;

		private static void hook_MouseDoubleClick(object sender, MouseHookEventArgs e)
		{
			if (selectedItem != null)
			{
				selectedItem.PerformPick();
			}
		}

		public void Dispose()
		{
			hook.Dispose();
		}

		public static void Initialize()
		{
			if (hook == null)
			{
				hook = new MouseHook();
				hook.MouseDoubleClick += new MouseHookEventHandler(hook_MouseDoubleClick);
				hook.Install();
			}
		}
	}
}