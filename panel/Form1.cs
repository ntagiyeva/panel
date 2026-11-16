using System.Media;

namespace panel
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        int count = 0;
        Point bombaYeri;
        SoundPlayer ses = new SoundPlayer(Path.Combine(Application.StartupPath, "bomba.wav"));

        public Form1()
        {
            InitializeComponent();
            bombaYeri = pictureBox1.Location;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Size = new Size(60, 60);
            p.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            p.Location = new Point(r.Next(0, ClientSize.Width - p.Width + 1),
                r.Next(0, panel1.Top - p.Height + 1));
            Controls.Add(p);
            pictureBox1.BringToFront();
            count++;
            label2.Text = count.ToString();
            ToqqusmaYoxla();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            pictureBox1.Location = new Point(r.Next(0, ClientSize.Width - pictureBox1.Width + 1),
                r.Next(0, panel1.Top - pictureBox1.Height + 1));
            ToqqusmaYoxla();
        }

        private void button3_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void ToqqusmaYoxla()
        {
            foreach (Control c in Controls)
            {
                if (c is Panel && c != panel1 && c.Bounds.IntersectsWith(pictureBox1.Bounds))
                {
                    count = 0;
                    label2.Text = "0";
                    button1.Enabled = false;
                    button2.Enabled = false;
                    ses.Play();

                    DialogResult cavab = MessageBox.Show(this,
                        "Oyun bitdi! Yeni oyuna başlamaq üçün OK düyməsini basın.",
                        "Oyun bitdi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (cavab == DialogResult.OK)
                    {
                        YeniOyun();
                    }

                    return;
                }
            }
        }

        private void YeniOyun()
        {
            ses.Stop();

            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is Panel && Controls[i] != panel1)
                {
                    Controls[i].Dispose();
                }
            }

            count = 0;
            label2.Text = "0";
            pictureBox1.Location = bombaYeri;
            button1.Enabled = true;
            button2.Enabled = true;
        }
    }
}
