using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Calendar
{
    public class Office11Renderer : AbstractRenderer
    {
        private Font minuteFont;

        public override Font MinuteFont
        {
            get
            {
                if (minuteFont == null)
                    minuteFont = new Font(BaseFont, FontStyle.Italic);

                return minuteFont;
            }
        }

        public override void DrawHourLabel(Graphics g, Rectangle rect, int hour)
        {
            Color m_Color = ControlPaint.LightLight(SystemColors.WindowFrame);
            m_Color = ControlPaint.Light(m_Color);

            using (Pen m_Pen = new Pen(m_Color))
                g.DrawLine(m_Pen, rect.Left, rect.Y, rect.Width, rect.Y);

            g.DrawString(hour.ToString("##00"), HourFont, SystemBrushes.ControlText, rect);

            rect.X += 27;

            g.DrawString("00", MinuteFont, SystemBrushes.ControlText, rect);
        }

        public override void DrawDayHeader(Graphics g, Rectangle rect, DateTime date)
        {
            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Center;
            m_Format.FormatFlags = StringFormatFlags.NoWrap;
            m_Format.LineAlignment = StringAlignment.Center;

            ControlPaint.DrawButton(g, rect, ButtonState.Inactive);
            ControlPaint.DrawBorder3D(g, rect, Border3DStyle.Etched);

            g.DrawString(
                System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek),
                BaseFont,
                SystemBrushes.WindowText,
                rect,
                m_Format
                );
        }

        public override void DrawDayBackground(Graphics g, Rectangle rect)
        {
            using (Brush m_Brush = new SolidBrush(this.HourColor))
                g.FillRectangle(m_Brush, rect);
        }

        public override void DrawAppointment(Graphics g, Rectangle rect, Appointment appointment, bool isSelected, int gripWidth)
        {
            StringFormat m_Format = new StringFormat();
            m_Format.Alignment = StringAlignment.Near;
            m_Format.LineAlignment = StringAlignment.Near;

            if ((appointment.Locked) && isSelected)
            {
                // Draw back
                using (Brush m_Brush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Wave, Color.LightGray, appointment.Color))
                    g.FillRectangle(m_Brush, rect);
            }
            else
            {
                // Draw back
                using (SolidBrush m_Brush = new SolidBrush(appointment.Color))
                    g.FillRectangle(m_Brush, rect);
            }

            if (isSelected)
            {
                using (Pen m_Pen = new Pen(appointment.BorderColor, 4))
                    g.DrawRectangle(m_Pen, rect);

                Rectangle m_BorderRectangle = rect;

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

                using (SolidBrush m_Brush = new SolidBrush(appointment.BorderColor))
                    g.FillRectangle(m_Brush, m_GripRectangle);

                using (Pen m_Pen = new Pen(SystemColors.WindowFrame, 1))
                    g.DrawRectangle(m_Pen, rect);
            }

            rect.X += gripWidth;
            g.DrawString(appointment.Title, this.BaseFont, SystemBrushes.WindowText, rect, m_Format);
        }
    }

}
