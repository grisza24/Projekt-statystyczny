using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace transport
{

    
    public partial class Form1 : Form
    {
        Random gen;//generator liczb losowych
        TransportDataClassesDataContext db;//obiekt do komunikacji z baza
        Form Form2;
        DataGridView table;

        public Form1()
        {
            InitializeComponent();
        }

        private void startStop_Click(object sender, EventArgs e)
        {
            if (startStop.Text == "Start")
            {
                startStop.Text = "Stop";
                showTab.Enabled = false;
                timer1.Start();
            }
            else
            {
                startStop.Text = "Start";
                showTab.Enabled = true;
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bus.Left >= (panel1.Left + panel1.Width))
            {
                bus.Left = 0;
                inBus.Left = bus.Width / 2;
            }
            else
            {
                bus.Left += 1;
                inBus.Left += 1;
            }

            if ((bus.Left + bus.Width / 2) == (pictureBox2.Left + pictureBox2.Width / 2))
            {
                int busOut = gen.Next(0,int.Parse(inBus.Text));//ile wysiadlo
                int busIn = int.Parse(label1.Text);//ile wsiadlo

                startStop_Click(sender, e);
                inBus.Text = (int.Parse(inBus.Text) - busOut).ToString();
                inBus.Text = (int.Parse(inBus.Text) + busIn).ToString();

                Thread.Sleep(1500);

                Ruch tmp = new Ruch
                {
                    nr_przystanku = 1,
                    wsiadlo = busIn,
                    wysiadlo = busOut,
                    stan_po = int.Parse(inBus.Text),
                    czas = DateTime.Now
                };
                db.Ruches.InsertOnSubmit(tmp);
                db.SubmitChanges();

                label1.Text = (gen.Next(0, 10)).ToString();
                startStop_Click(sender, e);
            }

            if ((bus.Left + bus.Width / 2) == (pictureBox3.Left + pictureBox3.Width / 2))
            {
                int busOut = gen.Next(0, int.Parse(inBus.Text));
                int busIn = int.Parse(label2.Text);

                startStop_Click(sender, e);
                inBus.Text = (int.Parse(inBus.Text) - busOut).ToString();
                inBus.Text = (int.Parse(inBus.Text) + busIn).ToString();
                Thread.Sleep(1500);

                Ruch tmp = new Ruch
                {
                    nr_przystanku = 2,
                    wsiadlo = busIn,
                    wysiadlo = busOut,
                    stan_po = int.Parse(inBus.Text),
                    czas = DateTime.Now.AddMinutes(gen.Next(5,7))
                };
                db.Ruches.InsertOnSubmit(tmp);
                db.SubmitChanges();

                label2.Text = (gen.Next(0, 10)).ToString();
                startStop_Click(sender, e);
            }

            if ((bus.Left + bus.Width / 2) == (pictureBox4.Left + pictureBox4.Width / 2))
            {
                int busOut = gen.Next(0, int.Parse(inBus.Text));
                int busIn = int.Parse(label3.Text);

                startStop_Click(sender, e);
                inBus.Text = (int.Parse(inBus.Text) - busOut).ToString();
                inBus.Text = (int.Parse(inBus.Text) + busIn).ToString();
                Thread.Sleep(1500);

                Ruch tmp = new Ruch
                {
                    nr_przystanku = 3,
                    wsiadlo = busIn,
                    wysiadlo = busOut,
                    stan_po = int.Parse(inBus.Text),
                    czas = DateTime.Now.AddMinutes(gen.Next(8, 11))
                };
                db.Ruches.InsertOnSubmit(tmp);
                db.SubmitChanges();

                label3.Text = (gen.Next(0, 10)).ToString();
                startStop_Click(sender, e);
            }

            if ((bus.Left + bus.Width / 2) == (pictureBox5.Left + pictureBox5.Width / 2))
            {
                int busOut = gen.Next(0, int.Parse(inBus.Text));
                int busIn = int.Parse(label4.Text);

                startStop_Click(sender, e);
                inBus.Text = (int.Parse(inBus.Text) - busOut).ToString();
                inBus.Text = (int.Parse(inBus.Text) + busIn).ToString();
                Thread.Sleep(1500);

                Ruch tmp = new Ruch
                {
                    nr_przystanku = 4,
                    wsiadlo = busIn,
                    wysiadlo = busOut,
                    stan_po = int.Parse(inBus.Text),
                    czas = DateTime.Now.AddMinutes(gen.Next(12, 18))
                };
                db.Ruches.InsertOnSubmit(tmp);
                db.SubmitChanges();

                label4.Text = (gen.Next(0, 10)).ToString();
                startStop_Click(sender, e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gen = new Random();
            db = new TransportDataClassesDataContext();
            Form2 = new Form();
            table = new DataGridView();

            Form2.Size = new Size(670, 300);
            table.Size = new Size(670, 300);
            Form2.Controls.Add(table);

            label1.Text = gen.Next(0, 10).ToString();
            label2.Text = gen.Next(0, 10).ToString();
            label3.Text = gen.Next(0, 10).ToString();
            label4.Text = gen.Next(0, 10).ToString();
        }

        private void showTab_Click(object sender, EventArgs e)
        {
            var query = from r in db.Ruches select r;

            table.DataSource = query;
            Form2.ShowDialog();
        }
    }
}
