/* Developed by Ertan Tike (ertan.tike@moreum.com) */

using System;
using System.Collections;
using System.Text;
using System.Drawing;

namespace Calendar
{
    public class SelectionTool : ITool
    {
        private DayView dayView;

        public DayView DayView
        {
            get
            {
                return dayView;
            }
            set
            {
                dayView = value;
            }
        }

        private DateTime startDate;
        private TimeSpan length;
        private Mode mode;
        private TimeSpan delta;

        public void Reset()
        {
            length = TimeSpan.Zero;
            delta = TimeSpan.Zero;
        }

        public void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            Appointment selection = dayView.SelectedAppointment;

            if ((selection != null) && (!selection.Locked))
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:

                        // Get time at mouse position
                        DateTime m_Date = dayView.GetTimeAt(e.X, e.Y);

                        switch (mode)
                        {
                            case Mode.Move:

                                // add delta value
                                m_Date = m_Date.Add(delta);

                                if (length == TimeSpan.Zero)
                                {
                                    startDate = selection.StartDate;
                                    length = selection.EndDate - startDate;
                                }
                                else
                                {
                                    DateTime m_EndDate = m_Date.Add(length);

                                    if (m_EndDate.Day == m_Date.Day)
                                    {
                                        selection.StartDate = m_Date;
                                        selection.EndDate = m_EndDate;
                                        dayView.Invalidate();
                                        dayView.RaiseAppointmentMove(new AppointmentEventArgs(selection));
                                    }
                                }

                                break;

                            case Mode.ResizeBottom:

                                if (m_Date > selection.StartDate)
                                {
                                    if (selection.EndDate.Day == m_Date.Day)
                                    {
                                        selection.EndDate = m_Date;
                                        dayView.Invalidate();
                                        dayView.RaiseAppointmentMove(new AppointmentEventArgs(selection));
                                    }
                                }

                                break;

                            case Mode.ResizeTop:

                                if (m_Date < selection.EndDate)
                                {
                                    if (selection.StartDate.Day == m_Date.Day)
                                    {
                                        selection.StartDate = m_Date;
                                        dayView.Invalidate();
                                        dayView.RaiseAppointmentMove(new AppointmentEventArgs(selection));
                                    }
                                }

                                break;
                        }

                        break;

                    default:

                        Mode tmpNode = GetMode(e);

                        switch (tmpNode)
                        {
                            case Mode.Move:
                                dayView.Cursor = System.Windows.Forms.Cursors.Default;
                                break;
                            case Mode.ResizeBottom:
                            case Mode.ResizeTop:
                                dayView.Cursor = System.Windows.Forms.Cursors.SizeNS;
                                break;
                        }

                        break;
                }
            }
        }
    	
    	

        private Mode GetMode(System.Windows.Forms.MouseEventArgs e)
        {
            if (dayView.SelectedAppointment == null)
                return Mode.None;

            if (dayView.appointmentViews.Contains(dayView.SelectedAppointment))
            {
                //DayView.AppointmentView view = dayView.appointmentViews[dayView.SelectedAppointment];
            	Rectangle view = dayView.SelectedAppointment.Rectangle;

                Rectangle topRect = view;
                Rectangle bottomRect = view;

                bottomRect.Y = bottomRect.Bottom - 5;
                bottomRect.Height = 5;
                topRect.Height = 5;

                //if (topRect.Contains(e.Location))
            	if (topRect.Contains(new Point(e.X, e.Y)))
                    return Mode.ResizeTop;
               // else if (bottomRect.Contains(e.Location))
            	 else if (bottomRect.Contains(new Point(e.X, e.Y)))
                    return Mode.ResizeBottom;
                else
                    return Mode.Move;
            }

            return Mode.None;
        }

        public void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Complete != null)
                    Complete(this, EventArgs.Empty);
            }

            dayView.RaiseSelectionChanged(EventArgs.Empty);

            mode = Mode.Move;

            delta = TimeSpan.Zero;
        }

        public void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (dayView.SelectedAppointmentIsNew)
            {
                dayView.RaiseNewAppointment();
            }

            if (dayView.CurrentlyEditing)
                dayView.FinishEditing(false);

            mode = GetMode(e);

            if (dayView.SelectedAppointment != null)
            {
                DateTime downPos = dayView.GetTimeAt(e.X, e.Y);
                // Calculate delta time between selection and clicked point
                delta = dayView.SelectedAppointment.StartDate - downPos;
            }
            else
            {
                delta = TimeSpan.Zero;
            }

            length = TimeSpan.Zero;
        }

        public event EventHandler Complete;

        enum Mode
        {
            ResizeTop,
            ResizeBottom,
            Move,
            None
        }
    }
}
