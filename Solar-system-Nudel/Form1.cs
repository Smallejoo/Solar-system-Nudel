using Solar_system_Nudel.Classes;
using System;
using System.Windows.Forms;

namespace Solar_system_Nudel
{
    public partial class Form1 : Form
    {
        private OpenGLRenderer _renderer;
        OpenGLRenderer Renderer;
        public Form1()
        {
            InitializeComponent();
            Renderer = new OpenGLRenderer(this.Handle);
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Renderer.Draw();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _renderer?.Cleanup();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Renderer.Draw();
        }
    }
}
