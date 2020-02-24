using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StateMachineTester
{
    public partial class SimRate : Form
    {
        float[] temperatures = { 10, 20, 30, 40 };
        float[] masses = { 1, 3, .1f, .1f };
        float simSpeed = 1;
        Thread t;
        bool run;
        public SimRate()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Thread(mainLoop);
            t.Start();
        }
        
        public void mainLoop()
        {
            while (true)
            {
                if (run)
                {
                    temperatures[0] = (masses[0] * temperatures[0] + masses[3] * temperatures[3])/ (masses[0] + masses[3]);
                    temperatures[1] = (masses[1] * temperatures[1] + masses[2] * temperatures[2]) / (masses[1] + masses[2]);
                    temperatures[2] = temperatures[0];
                    temperatures[3] = temperatures[1];
                    MethodInvoker mi = delegate ()
                    {
                        ATemp.Text = temperatures[0] + "";
                        BTemp.Text = temperatures[1] + "";
                        CTemp.Text = temperatures[2] + "";
                        DTemp.Text = temperatures[3] + "";
                    };
                    this.Invoke(mi);

                    Thread.Sleep((int)(1000 * 1 / simSpeed));
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }

        private void playClick(object sender, EventArgs e)
        {
            simSpeed = (int)SimRateVal.Value;
            run = true;
        }

        private void pauseClick(object sender, EventArgs e)
        {
            run = false;
        }
    }
}
