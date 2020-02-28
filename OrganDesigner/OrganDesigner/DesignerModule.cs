using System;
using System.Threading;
using System.Windows.Forms;

namespace OrganDesigner
{
    /// <summary>
    /// </summary>
    /// IDX ORDERING: WCMSBPV
    public partial class DesignerModule : Form
    {
        Thread t;
        public bool stop, pause;
        public Structure s;
        int lastIdx = -1;
        float[] startHealths = { 8, 3, 3, 200, 6, 3, 3 };
        float[] metabolisms = { .1f, .1f, .1f, .1f, .1f, .1f, .1f };
        float[] maxMs = { 7, 1, 1, 1, 1, 1, 1 };
        float[] powerConsumptions = { 2, .01f, 3, .01f, .01f, .01f, .01f };
        float[] maxCharges = { 100, 100, 100 };
        int maxBoostCount = 2;
        float betaRate = 1.7f;
        float drainRate = 1;
        float fatGrowth = 1;
        float fatBreakdown = 1;
        float baseBps = 1;
        float homeostasis = 1;
        ProgressBar[] cores, powers, temps, charges;
        bool isFlat = false;
        public DesignerModule()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            //create and start a new thread in the load event.
            cores = new ProgressBar[] { WriterCore, CapacitorCore, MotorCore, StructureCore, BetaCore, PumpCore, VisionCore };
            temps = new ProgressBar[] { WriterTemp, CapacitorTemp, MotorTemp, StructureTemp, BetaTemp, PumpTemp, VisionTemp };
            powers = new ProgressBar[] { WriterPower, CapacitorPower, MotorPower, StructurePower, BetaPower, PumpPower, VisionPower };
            charges = new ProgressBar[] { WriterCharge, CapacitorCharge, MotorCharge };
            //passing it a method to be run on the new thread.
        }

        public void mainLoopFlat(float simRate)
        {
            FlatOrganSystem s = new FlatOrganSystem(startHealths, metabolisms, powerConsumptions, maxMs, maxCharges, maxBoostCount, betaRate, drainRate, fatGrowth, fatBreakdown, baseBps, homeostasis);

            foreach (var number in s.Discrete())
            {
                while (pause)
                {
                    Thread.Sleep(50);
                }
                Console.WriteLine(s.coreM[FlatOrganSystem.sI] + " " + s.dynamicM[FlatOrganSystem.sI]);
                if (stop)
                    break;
                MethodInvoker mi = delegate ()
                {
                    HeartRate.Text = s.curBps + "";
                    for (int i = 0; i < s.coreM.Length; i++)
                    {
                        if (s.coreM[i] + s.dynamicM[i] > 0)
                        {
                            cores[i].Value = (int)(100 * s.coreM[i] / s.startHealth[i]);
                            powers[i].Value = (int)(100 * s.currentPower[i]);
                            temps[i].Value = Math.Min(100, (int)(s.getTemperature(i)));
                            if (i <= Organ.lastChargeableOrgan)
                            {
                                charges[i].Value = (int)(100 * s.charge[i] / s.maxCharge[i]);
                            }
                        }
                        else
                        {
                            cores[i].Value = 0;
                            powers[i].Value = 0;
                            temps[i].Value = 0;
                            if (i <= Organ.lastChargeableOrgan)
                            {
                                charges[i].Value = 0;
                            }

                        }

                    }


                };
                this.Invoke(mi);
                Thread.Sleep((int)(number * 1000 * 1 / simRate));

                //To keep stuff from jamming up
            }

        }
        public void mainLoop(float simRate)
        {
            //you need to use Invoke because the new thread can't access the UI elements directly
            s = new Structure(startHealths, metabolisms, powerConsumptions, maxMs, maxCharges, maxBoostCount, betaRate, drainRate, fatGrowth, fatBreakdown, baseBps, homeostasis);
            s.startSimulation();
            foreach (var number in s.Discrete())
            {
                while (pause)
                {
                    Thread.Sleep(50);
                }
                Console.WriteLine(s.coreM + " " + s.dynamicM);
                if (stop)
                    break;
                MethodInvoker mi = delegate ()
                {
                    HeartRate.Text = s.curBps + "";
                    for (int i = 0; i < s.organs.Length; i++)
                    {
                        if (s.organs[i].coreM + s.organs[i].dynamicM > 0)
                        {
                            cores[i].Value = (int)(100 * s.organs[i].coreM / s.organs[i].startHealth);
                            powers[i].Value = (int)(100 * s.organs[i].currentPower);
                            temps[i].Value = Math.Min(100, (int)(s.organs[i].getTemperature()));
                            if (i <= Organ.lastChargeableOrgan)
                            {
                                charges[i].Value = (int)(100 * ((ChargeableOrgan)s.organs[i]).charge / ((ChargeableOrgan)s.organs[i]).maxCharge);
                            }
                        }
                        else
                        {
                            cores[i].Value = 0;
                            powers[i].Value = 0;
                            temps[i].Value = 0;
                            if (i <= Organ.lastChargeableOrgan)
                            {
                                charges[i].Value = 0;
                            }

                        }

                    }


                };
                this.Invoke(mi);
                Thread.Sleep((int)(number * 1000 * 1 / simRate));

                //To keep stuff from jamming up
            }

        }

        private void killMainLoop(object sender, EventArgs e)
        {
            //stop the thread.
            pause = false;
            stop = true;
            PauseButton.Enabled = PlayButton.Enabled = false;
            for (int i = 0; i < 7; i++)
            {
                cores[i].Value = 0;
                powers[i].Value = 0;
                temps[i].Value = 0;
                if (i <= Organ.lastChargeableOrgan)
                {
                    charges[i].Value = 0;
                }
            }
        }

        private void startLoop(object sender, EventArgs e)
        {
            if (t != null)
            {
                pause = false;
                stop = true;
                t.Join();
            }
            pause = false;
            stop = false;
            if (isFlat)
            {
                t = new Thread(() => mainLoopFlat((float)SimRate.Value));
            }
            else
            {
                t = new Thread(() => mainLoop((float)SimRate.Value));
            }
            t.Start();
            PauseButton.Enabled = true;
            PlayButton.Enabled = false;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            pause = true;
            PauseButton.Enabled = false;
            PlayButton.Enabled = true;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            pause = false;
            PauseButton.Enabled = true;
            PlayButton.Enabled = false;
        }

        private void OrganSelection(object sender, EventArgs e)
        {
            int idx = OrganSelector.SelectedIndex;

            if (lastIdx != -1)
            {
                startHealths[lastIdx] = float.Parse(StartHealth.Text);
                maxMs[lastIdx] = float.Parse(MaxM.Text);
                powerConsumptions[lastIdx] = float.Parse(PowerConsumption.Text);
                metabolisms[lastIdx] = float.Parse(Metabolism.Text);
            }
            StartHealth.Text = startHealths[idx] + "";
            MaxM.Text = maxMs[idx] + "";
            PowerConsumption.Text = powerConsumptions[idx] + "";
            Metabolism.Text = metabolisms[idx] + "";

            if (idx == Organ.MotorI || idx == Organ.WriterI || idx == Organ.CapacitorI)
            {
                MaxCharge.Visible = true;
                MaxCharge.Text = maxCharges[idx] + "";
            }
            else
            {
                if (lastIdx == Organ.MotorI || lastIdx == Organ.WriterI || lastIdx == Organ.CapacitorI)
                {
                    maxCharges[lastIdx] = int.Parse(MaxCharge.Text);
                }
                MaxCharge.Visible = false;
                MaxCharge.Clear();
            }

            if (idx == Organ.StructureI)
            {
                FatBreakdown.Text = fatBreakdown + "";
                FatGrowth.Text = fatGrowth + "";
                Homeostasis.Text = homeostasis + "";
                BaseBps.Text = baseBps + "";
                FatBreakdown.Visible = true;
                FatGrowth.Visible = true;
                Homeostasis.Visible = true;
                BaseBps.Visible = true;
            }
            else
            {
                if (lastIdx == Organ.StructureI)
                {
                    fatBreakdown = float.Parse(FatBreakdown.Text);
                    fatGrowth = float.Parse(FatGrowth.Text);
                    homeostasis = float.Parse(Homeostasis.Text);
                    baseBps = float.Parse(BaseBps.Text);
                }
                FatBreakdown.Clear();
                FatGrowth.Clear();
                Homeostasis.Clear();
                BaseBps.Clear();
                FatBreakdown.Visible = false;
                FatGrowth.Visible = false;
                Homeostasis.Visible = false;
                BaseBps.Visible = false;
            }

            if (idx == Organ.BetaI)
            {
                BetaRate.Text = betaRate + "";
                BetaRate.Visible = true;
            }
            else
            {
                if (lastIdx == Organ.BetaI)
                {
                    betaRate = float.Parse(BetaRate.Text);
                }
                BetaRate.Visible = false;
                BetaRate.Clear();
            }

            if (idx == Organ.MotorI)
            {
                MaxBoostCount.Text = maxBoostCount + "";
                MaxBoostCount.Visible = true;
            }
            else
            {
                if (lastIdx == Organ.MotorI)
                {
                    maxBoostCount = int.Parse(MaxBoostCount.Text);
                }
                MaxBoostCount.Visible = false;
                MaxBoostCount.Clear();
            }

            if (idx == Organ.PumpI)
            {
                DrainRate.Text = drainRate + "";
                DrainRate.Visible = true;
            }
            else
            {
                if (lastIdx == Organ.PumpI)
                {
                    drainRate = float.Parse(DrainRate.Text);
                }
                DrainRate.Visible = false;
                DrainRate.Clear();
            }

            lastIdx = idx;

        }


    }


}
