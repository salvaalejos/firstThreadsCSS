using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAP_U2P1_B
{
    public partial class Form1 : Form
    {
        private Thread thread;
        private List<int> proyectos = new List<int>();
        
        private int intentos = 100;
        private int tiempo = 0;
        private int minutos = 0;

        public Form1()
        {
            proyectos.Add(1);
            proyectos.Add(2);
            proyectos.Add(3);
            proyectos.Add(4);
            proyectos.Add(5);
            proyectos.Add(6);
            proyectos.Add(7);
            proyectos.Add(8);
            proyectos.Add(9);
            proyectos.Add(10);
            proyectos.Add(11);
            proyectos.Add(12);
            proyectos.Add(13);
            proyectos.Add(14);
            proyectos.Add(15);
            thread = new Thread(hiloTipo1);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread.Start();
            backgroundWorker1.RunWorkerAsync();
        }

        private void hiloTipo1()
        {
            while(true)
            {
                Console.WriteLine("Soy el hilo tipo 1");
                new Log().WriteEntry("Hilo tipo 1");
                Thread.Sleep(1000);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
            backgroundWorker1.CancelAsync();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            int indice = random.Next(proyectos.Count);

            label1.Text = "Proyecto: " + proyectos[indice];

            intentos--;
            if(intentos <= 0)
            {
                timer1.Stop();
                proyectos.RemoveAt(indice);

                label1.ForeColor = Color.Teal;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            intentos = 100;
            label1.ForeColor = Color.Salmon;

            timer1.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                tiempo++;
                if(tiempo == 60)
                {
                    tiempo = 0;
                    minutos++;
                }
                backgroundWorker1.ReportProgress(1);

                Thread.Sleep(1000);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (minutos == 0)
                label2.Text = "Tiempo: " + tiempo + " segundos";
            else if (tiempo == 0)
                label2.Text = "Tiempo: " + minutos + " minutos";
            else if ( minutos == 1 && tiempo == 1)
                label2.Text = "Tiempo: " + minutos + " minuto, " + tiempo + " segundo";
            else if ( minutos == 1 && tiempo > 1)
                label2.Text = "Tiempo: " + minutos + " minuto, " + tiempo + " segundos";
            else if ( minutos > 1 && tiempo == 1)
                label2.Text = "Tiempo: " + minutos + " minutos, " + tiempo + " segundo";
            else if ( minutos > 0 && tiempo > 0) 
                label2.Text = "Tiempo: " + minutos + " minutos, " + tiempo + " segundos";
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Fin del hilo 3");
        }
    }
}
