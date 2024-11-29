using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Solar_system_Nudel.Classes;
using System;
using System.Windows.Forms;


namespace Solar_system_Nudel
{
    public partial class Form1 : Form
    {
        private OpenGLRenderer _renderer;
        private System.Windows.Forms.Timer _timer_RUN;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.FormClosing += Form1_FormClosing;

            _renderer = new OpenGLRenderer(this.Handle);
            _timer_RUN = new System.Windows.Forms.Timer();
            _timer_RUN.Interval = 100; // Approx. 60 frames per second
            _timer_RUN.Tick += Timer_RUN_Tick;
            _timer_RUN.Start();

        }
       
        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!_renderer.InitializeOpenGL())
            {
                MessageBox.Show("Failed to initialize OpenGL");
                Close();
            }
           

        }
        private void Timer_RUN_Tick(object sender, EventArgs e)
        {
            //this.Invalidate(); // Triggers the Paint event for rendering
            _renderer.Render();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           _renderer.Render();
            //this.Invalidate();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        //    _renderer?.Cleanup();
          //  _timer?.Stop(); // Stop
        }
    }
}
