using Solar_system_Nudel.Classes;
using System;
using System.Windows.Forms;

namespace Solar_system_Nudel
{
    public partial class Form1 : Form
    {
       // private OpenGLRenderer _renderer;
        OpenGLRenderer Renderer;
        public Form1()
        {
            InitializeComponent();
            Renderer = new OpenGLRenderer(this.Handle);
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.FormClosing += Form1_FormClosing;
            timer1.Enabled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch(keyData)
            {
                case Keys.W:
                    {
                        Renderer.OutCommands = 1;
                        break;
                    }
                case Keys.S:
                    {
                        Renderer.OutCommands = 2;
                        break;
                    }
                case Keys.D:
                    {
                        Renderer.OutCommands = 3;
                        break;
                    }
                case Keys.A:
                    {
                        Renderer.OutCommands = 4;
                        break;
                    }
                case Keys.Q:
                    {
                        Renderer.OutCommands = 5;
                        break;
                    }
                case Keys.E:
                    {
                        Renderer.OutCommands = 6;
                        break;
                    }
                case Keys.Z: 
                    {
                        Renderer.OutCommands = 7;
                        break;
                    }
                case Keys.C: 
                    {
                        Renderer.OutCommands = 8;
                        break;
                    }
                    case Keys.R:
                    {
                        Renderer.OutCommands = 9;
                        break;
                    }
                    case Keys.F:
                    {
                        Renderer.OutCommands = 10;
                        break;
                    }


            }
            //Invalidate();
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Renderer.Draw();
            Renderer.DrawSpace();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Renderer.Cleanup();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Renderer.Draw();

            Renderer.DrawSpace();
        }
    }
}
