using System;
using System.Windows.Forms;

namespace ACUS.Client
{
    public partial class SplashScreen : Form
    {
        private Timer timer;
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            startTimer();
        }

        void startTimer()
        {
            timer = new Timer();
            //set time interval 3 sec
            timer.Interval = 3000;
            //starts the timer
            timer.Start();
            timer.Tick += stopTimer;
        }

        void stopTimer(object sender, EventArgs e)
        {
            //after 3 sec stop the timer
            timer.Stop();
            //display mainform
            Main mf = new Main();
            mf.Show();
            //hide this form
            this.Hide();
        }
    }
}
