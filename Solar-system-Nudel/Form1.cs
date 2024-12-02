using Solar_system_Nudel.Classes;
using System;
using System.Windows.Forms;

namespace Solar_system_Nudel
{
    public partial class Form1 : Form
    {
        private OpenGLRenderer _renderer;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _renderer = new OpenGLRenderer(this.Handle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing OpenGL: {ex.Message}");
                Close();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _renderer.InitRender();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _renderer?.Cleanup();
        }
    }
}
