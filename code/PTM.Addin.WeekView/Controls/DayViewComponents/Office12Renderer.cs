using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Calendar
{
    public class Office12Renderer : AbstractRenderer
    {
        Font baseFont;

        public override Font BaseFont
        {
            get
            {
                if (baseFont == null)
                {
                    baseFont = new Font("Segoe UI", 8, FontStyle.Regular);
                }

                return baseFont;
            }
        }

        public override Color HourColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(230, 237, 247);
            }
        }

        public override Color HalfHourSeperatorColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(165, 191, 225);
            }
        }

        public override Color HourSeperatorColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(213, 215, 241);
            }
        }

        public override Color WorkingHourColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(255, 255, 255);
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return Color.FromArgb(213, 228, 242);
            }
        }

        public override Color SelectionColor
        {
            get
            {
                return System.Drawing.Color.FromArgb(41, 76, 122);
            }
        }

        public override void DrawHourLabel(System.Drawing.Graphics g, System.Drawing.Rectangle rect, int hour)
        {
            Color color = Color.FromArgb(101, 147, 207);

            using (Pen pen = new Pen(color))
                g.DrawLine(pen, rect.Left, rect.Y, rect.Width, rect.Y);

            using (SolidBrush brush = new SolidBrush(color))
            {
                g.DrawString(hour.ToString("##00"), HourFont, brush, rect);

                rect.X += 27;

                g.DrawString("00", MinuteFont, brush, rect);
            }
        }

        public override void DrawDayHeader(System.Drawing.Graphics g, System.Drawing.Rectangle rect, DateTime date)
        {

            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Center;
            m_Format.FormatFlags = StringFormatFlags.NoWrap;
            m_Format.LineAlignment = StringAlignment.Center;

            StringFormat m_Formatdd = new StringFormat();
            m_Formatdd.Alignment = StringAlignment.Near;
            m_Formatdd.FormatFlags = StringFormatFlags.NoWrap;
            m_Formatdd.LineAlignment = StringAlignment.Center;

            using (SolidBrush brush = new SolidBrush(this.BackColor))
                g.FillRectangle(brush, rect);

            using (Pen aPen = new Pen(Color.FromArgb(205, 219, 238)))
                g.DrawLine(aPen, rect.Left, rect.Top + (int)rect.Height / 2, rect.Right, rect.Top + (int)rect.Height / 2);

            using (Pen aPen = new Pen(Color.FromArgb(141, 174, 217)))
                g.DrawRectangle(aPen, rect);

            Rectangle topPart = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, (int)(rect.Height / 2) - 1);
            Rectangle lowPart = new Rectangle(rect.Left + 1, rect.Top + (int)(rect.Height / 2) + 1, rect.Width - 1, (int)(rect.Height / 2) - 1);

            using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(228, 236, 246), Color.FromArgb(214, 226, 241), LinearGradientMode.Vertical))
                g.FillRectangle(aGB, topPart);

            using (LinearGradientBrush aGB = new LinearGradientBrush(lowPart, Color.FromArgb(194, 212, 235), Color.FromArgb(208, 222, 239), LinearGradientMode.Vertical))
                g.FillRectangle(aGB, lowPart);

            if (date.Date.Equals(DateTime.Now.Date))
            {
                topPart.Inflate((int)(-topPart.Width / 4 + 1), 1); //top left orange area
                topPart.Offset(rect.Left - topPart.Left + 1, 1);
                topPart.Inflate(1, 0);
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(247, 207, 114), Color.FromArgb(251, 230, 148), LinearGradientMode.Horizontal))
                {
                    topPart.Inflate(-1, 0);
                    g.FillRectangle(aGB, topPart);
                }

                topPart.Offset(rect.Right - topPart.Right, 0);        //top right orange
                topPart.Inflate(1, 0);
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(251, 230, 148), Color.FromArgb(247, 207, 114), LinearGradientMode.Horizontal))
                {
                    topPart.Inflate(-1, 0);
                    g.FillRectangle(aGB, topPart);
                }

                using (Pen aPen = new Pen(Color.FromArgb(128, 240, 154, 30))) //center line
                    g.DrawLine(aPen, rect.Left, topPart.Bottom - 1, rect.Right, topPart.Bottom - 1);

                topPart.Inflate(0, -1);
                topPart.Offset(0, topPart.Height + 1); //lower right
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(240, 157, 33), Color.FromArgb(250, 226, 142), LinearGradientMode.BackwardDiagonal))
                    g.FillRectangle(aGB, topPart);

                topPart.Offset(rect.Left - topPart.Left + 1, 0); //lower left
                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(240, 157, 33), Color.FromArgb(250, 226, 142), LinearGradientMode.ForwardDiagonal))
                    g.FillRectangle(aGB, topPart);
                using (Pen aPen = new Pen(Color.FromArgb(238, 147, 17)))
                    g.DrawRectangle(aPen, rect);
            }

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            //get short dayabbr. if narrow dayrect
            string sTodaysName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
            if (rect.Width < 105)
                sTodaysName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(date.DayOfWeek);

            rect.Offset(2, 1);

            using (Font fntDay = new Font("Segoe UI", 8))
                g.DrawString(sTodaysName, fntDay, SystemBrushes.WindowText, rect, m_Format);

            rect.Offset(-2, -1);

            using (Font fntDayDate = new Font("Segoe UI", 9, FontStyle.Bold))
                g.DrawString(date.ToString(" d"), fntDayDate, SystemBrushes.WindowText, rect, m_Formatdd);
        }

        public override void DrawDayBackground(System.Drawing.Graphics g, System.Drawing.Rectangle rect)
        {

        }

        public override void DrawAppointment(System.Drawing.Graphics g, System.Drawing.Rectangle rect, Appointment appointment, bool isSelected, int gripWidth)
        {
            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Near;
            m_Format.LineAlignment = StringAlignment.Near;

            Color start = InterpolateColors(appointment.Color, Color.White, 0.4f);
            Color end = InterpolateColors(appointment.Color, Color.FromArgb(191, 210, 234), 0.7f);

            if ((appointment.Locked))
            {
                // Draw back
                using (Brush m_Brush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.LargeConfetti, Color.Blue, appointment.Color))
                    g.FillRectangle(m_Brush, rect);

                // little transparent
                start = Color.FromArgb(230, start);
                end = Color.FromArgb(180, end);

                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(rect);

                using (LinearGradientBrush aGB = new LinearGradientBrush(rect, start, end, LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, rect);
            }
            else
            {
                // Draw back
                using (LinearGradientBrush aGB = new LinearGradientBrush(rect, start, end, LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, rect);
            }

            if (isSelected)
            {
                Rectangle m_BorderRectangle = rect;

                using (Pen m_Pen = new Pen(appointment.BorderColor, 4))
                    g.DrawRectangle(m_Pen, rect);

                m_BorderRectangle.Inflate(2, 2);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, 1))
                    g.DrawRectangle(m_Pen, m_BorderRectangle);

                m_BorderRectangle.Inflate(-4, -4);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, 1))
                    g.DrawRectangle(m_Pen, m_BorderRectangle);
            }
            else
            {
                // Draw gripper
                Rectangle m_GripRectangle = rect;

                m_GripRectangle.Width = gripWidth + 1;

                start = InterpolateColors(appointment.BorderColor, appointment.Color, 0.2f);
                end = InterpolateColors(appointment.BorderColor, Color.White, 0.6f);

                using (LinearGradientBrush aGB = new LinearGradientBrush(rect, start, end, LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, m_GripRectangle);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, 1))
                    g.DrawRectangle(m_Pen, rect);

                // Draw shadow lines
                int xLeft = rect.X + 6;
                int xRight = rect.Right + 1;
                int yTop = rect.Y + 1;
                int yButton = rect.Bottom + 1;

                for (int i = 0; i < 5; i++)
                {
                    using (Pen shadow_Pen = new Pen(Color.FromArgb(70 - 12 * i, Color.Black)))
                    {
                        g.DrawLine(shadow_Pen, xLeft + i, yButton + i, xRight + i - 1, yButton + i); //horisontal lines
                        g.DrawLine(shadow_Pen, xRight + i, yTop + i, xRight + i, yButton + i); //vertical
                    }
                }

            }

            rect.X += gripWidth;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.DrawString(appointment.Title, this.BaseFont, SystemBrushes.WindowText, rect, m_Format);
            g.TextRenderingHint = TextRenderingHint.SystemDefault;

        }
    }
}
