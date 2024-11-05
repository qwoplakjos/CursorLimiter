using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CursorLimitApp
{
    public partial class Form1 : Form
    {
        private Rectangle _clippedRectangle;
        private Timer _clipTimer;
        private bool _isClipActive;

        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID = 1;

        public Form1()
        {
            InitializeComponent();


            _clipTimer = new Timer
            {
                Interval = 100
            };

            _clipTimer.Tick += (s, e) =>
            {
                if (_isClipActive && !_clippedRectangle.IsEmpty && !Cursor.Clip.Equals(_clippedRectangle))
                {
                    Cursor.Clip = _clippedRectangle;
                }
            };

            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT, Keys.C.GetHashCode());
        }

        private void SelectAreaButton_Click(object sender, EventArgs e)
        {
            using (var selectionForm = new SelectionForm())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    _clippedRectangle = selectionForm.SelectedRectangle;
                    _isClipActive = true;
                    Cursor.Clip = _clippedRectangle;
                    _clipTimer.Start();
                    statusLabel.Text = "Mouse clip: on";
                    statusLabel.ForeColor = Color.Green;
                    this.Activated += (s, _) => { if (_isClipActive) Cursor.Clip = _clippedRectangle; };
                    this.Deactivate += (s, _) => Cursor.Clip = Rectangle.Empty;
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ToggleCursorClip(false);
        }

        private void ToggleCursorClip(bool activate)
        {


            _isClipActive = activate;
            if (_isClipActive && !_clippedRectangle.IsEmpty)
            {
                statusLabel.Text = "Mouse clip: on";
                statusLabel.ForeColor = Color.Green;
                Cursor.Clip = _clippedRectangle;
                _clipTimer.Start();
            }
            else
            {
                statusLabel.Text = "Mouse clip: off";
                statusLabel.ForeColor = Color.Red;
                Cursor.Clip = Rectangle.Empty;
                _clipTimer.Stop();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                ToggleCursorClip(!_isClipActive);
            }
            base.WndProc(ref m);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            UnregisterHotKey(this.Handle, HOTKEY_ID);
        }


        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;
    }


    public partial class SelectionForm : Form
    {
        private bool _isSelecting;
        private Point _startPoint;
        private Rectangle _selectedRectangle;

        public Rectangle SelectedRectangle { get; private set; }

        public SelectionForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Gray;
            this.Opacity = 0.3;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Cursor = Cursors.Cross;
            this.DoubleBuffered = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isSelecting = true;
            _startPoint = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isSelecting)
            {

                var endPoint = e.Location;
                _selectedRectangle = new Rectangle(
                    Math.Min(_startPoint.X, endPoint.X),
                    Math.Min(_startPoint.Y, endPoint.Y),
                    Math.Abs(_startPoint.X - endPoint.X),
                    Math.Abs(_startPoint.Y - endPoint.Y));

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isSelecting = false;


            SelectedRectangle = new Rectangle(
                this.PointToScreen(_selectedRectangle.Location),
                _selectedRectangle.Size);


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_isSelecting)
            {
                using (Pen selectPen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawRectangle(selectPen, _selectedRectangle);
                }
            }
        }
    }

}


