using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PTM.View.Controls
{
	internal class IndicatorControl : UserControl
	{
		private Int32 _Maximum;
		private Int32 _Minimum;
		private Int32 _Value;
		private Int32 _GraphWidth;

		public IndicatorControl()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.DoubleBuffer |
				ControlStyles.ResizeRedraw,
				true
				);
			_Maximum = Int32.MaxValue;
			_Minimum = 0;
			_Value = 0;
			_GraphWidth = 33;
		}

		private void InitializeComponent()
		{
			// 
			// Indicator
			// 
			this.BackColor = Color.Black;
			this.ForeColor = Color.Lime;
			this.Name = "Indicator";
			this.Size = new Size(72, 72);
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00000200; // WS_EX_CLIENTEDGE
				return cp;
			}
		}

		public Int32 Maximum
		{
			get { return _Maximum; }
			set
			{
				if (value < this.Minimum)
					throw new ArgumentException();
				_Maximum = value;
				Invalidate();
			}
		}

		public Int32 Minimum
		{
			get { return _Minimum; }
			set
			{
				if (value < 0)
					throw new ArgumentException();
				_Minimum = value;
				Invalidate();
			}
		}

		public Int32 Value
		{
			get { return _Value; }
			set
			{
				if (value < this.Minimum || value > this.Maximum)
					throw new ArgumentOutOfRangeException();
				_Value = value;
				Invalidate();
			}
		}

		public Int32 GraphWidth
		{
			get { return _GraphWidth; }
			set
			{
				_GraphWidth = value;
				Invalidate();
			}
		}

		public string TextValue
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			SolidBrush b = new SolidBrush(this.ForeColor);
			Int32 w = this.ClientSize.Width;
			Int32 h = this.ClientSize.Height;
			Int32 y = h;
			// draw text
			y -= this.Font.Height + 4;
			RectangleF r = new RectangleF(0, y, w, h);
			StringFormat f = new StringFormat(StringFormatFlags.NoWrap);
			f.Alignment = StringAlignment.Center;
			e.Graphics.DrawString(this.Text, this.Font, b, r, f);
			// draw graph
			Int32 graphheight = h - 6 - 4 - this.Font.Height - 4;
			graphheight += 1;
			Int32 lines = graphheight/3;
			Byte red = (Byte) (this.ForeColor.R/2);
			Byte green = (Byte) (this.ForeColor.G/2);
			Byte blue = (Byte) (this.ForeColor.B/2);
			Color dimmed = Color.FromArgb(red, green, blue);
			Pen dpen = new Pen(dimmed);
			Pen hpen = new Pen(this.ForeColor);
			dpen.DashStyle = DashStyle.Dot;
			Int32 lx = (w - this.GraphWidth)/2;
			Int32 ly = h - 4 - this.Font.Height - 7;
			Int32 lw = this.GraphWidth/2;
			Int32 x0 = lx;
			Int32 y0 = 0;
			Int32 x1 = 0;
			Int32 y1 = 0;
			Int32 linestohighlite = (Int32) Math.Ceiling(
			                                	(this.Value - this.Minimum)*1.0/
			                                	((this.Maximum - this.Minimum*1.0)/lines)
			                                	);
			for (Int32 i = 0; i < lines; i++)
			{
				x0 = lx;
				y0 = ly - (i*3) - 1;
				x1 = x0 + lw;
				y1 = y0;
				if (i < linestohighlite)
				{
					e.Graphics.FillRectangle(b, x0, y0, lw, 2);
					e.Graphics.FillRectangle(b, x1 + 1, y0, lw, 2);
				}
				else
				{
					// left two lines
					e.Graphics.DrawLine(dpen, x0, y0, x1, y1);
					e.Graphics.DrawLine(dpen, x0 + 1, y0 + 1, x1, y1 + 1);
					// right two lines
					x0 = x1 + 1;
					x1 = x0 + lw;
					e.Graphics.DrawLine(dpen, x0, y0, x1, y1);
					e.Graphics.DrawLine(dpen, x0 + 1, y0 + 1, x1, y1 + 1);
				}
			}
			hpen.Dispose();
			dpen.Dispose();
			b.Dispose();
		}
	}
}