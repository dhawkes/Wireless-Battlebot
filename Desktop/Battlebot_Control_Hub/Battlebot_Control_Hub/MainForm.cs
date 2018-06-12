using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO.Ports;

namespace Battlebot_Control_Hub
{
    public partial class MainForm : Form
    {
        // Control variables
        Gamepad joystick;
        UdpClient healthListener;
        IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.1.44"), 2380);
        IPEndPoint local = new IPEndPoint(IPAddress.Parse("192.168.1.44"), 2390);
        IPEndPoint recieve = new IPEndPoint(IPAddress.Any, 0);
        Task<UdpReceiveResult> result;
        SerialPort serial;

        // Robot settings
        bool redTeam = false;
        int robotNum = 1;
        int health = 99;

        // GUI variables
        Image init_img;
        Point left_stick = new Point(132, 97);
        Point right_stick = new Point(390, 196);
        Size stick_size = new Size(68, 68);
        Pen pen;
        Brush brush1, brush2, brush3, brush4;

        public MainForm()
        {
            // Initialize graphic components
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Image, pictureBox1.Size);
            init_img = pictureBox1.Image;

            pen = new Pen(Color.Black);
            brush1 = new SolidBrush(Color.Blue);
            brush2 = new SolidBrush(Color.Red);
            brush3 = new SolidBrush(Color.White);
            brush4 = new SolidBrush(Color.FromArgb(100, Color.Black));

            // Initialize game components
            joystick = new Gamepad(1);                  // Connect to the xbox controller
            healthListener = new UdpClient(2390);       // Begin listening for status packets on WiFi
            result = healthListener.ReceiveAsync();     // Listen for a status packet asynchronosly so that the GUI is not delayed
            serial = new SerialPort("COM5", 115200);    // Connect to the Laptop ESP via serial port COM5
            serial.Open();
        }

        // This is run every 50ms
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the joystick state, if connected
            try
            {
                joystick.LoadState();
            }
            catch
            {
                this.Text = "Joystick - DISCONNECTED";
                return;
            }

            // Send controller data
            int left = (int)((100 * joystick.Thumbsticks.Left.Y) + (100 * joystick.Thumbsticks.Right.X));   // Motors are adjusted for diff drive
            int right = (int)((100 * joystick.Thumbsticks.Left.Y) - (100 * joystick.Thumbsticks.Right.X));
            left = left > 100 ? 100 : left < -100 ? -100 : left;        // Clip the values to accepted bounds
            right = right > 100 ? 100 : right < -100 ? -100 : right;
            left += 100;                                                // Add 100 to make the results unsigned
            right += 100;
            
            // Get the servo direction
            byte servo = 1;
            if(joystick.LeftBumper) {
                servo = 0;
            }
            else if(joystick.RightBumper) {
                servo = 2;
            }

            // Send the computed values to the Laptop ESP via serial
            // Note that the 4th byte is used to communicate button states. This was not untilized in the final version
            // although at one point the buzzer/speaker was controlled in this manner
            serial.Write(new byte[] { (byte)right, (byte)left, (byte)servo, 0x00, (byte)health }, 0, 5);

            // Update the display
            this.Text = "Joystick - Connected";
            pictureBox1.Refresh();
            
            // If a health packet has been processed, update the display
            if (result.IsCompleted)
            {
                // This is a bad way of doing this. If I wasn't writing this under such a time crunch,
                // I would have put the labels into an array and done this in 10 - 15 lines of code
                byte[] healthPacket = result.Result.Buffer;
                StringBuilder sb = new StringBuilder();
                if (redTeam)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        sb.Append((char)healthPacket[i]);
                    }
                    OurNexusLB.Text = sb.ToString();

                    sb.Clear();
                    for (int i = 11; i < 14; i++)
                    {
                        sb.Append((char)healthPacket[i]);
                    }
                    TheirNexusLB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[14]);
                    sb.Append((char)healthPacket[15]);
                    Enemy1LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[16]);
                    sb.Append((char)healthPacket[17]);
                    Enemy2LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[18]);
                    sb.Append((char)healthPacket[19]);
                    Enemy3LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[20]);
                    sb.Append((char)healthPacket[21]);
                    Enemy4LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[3 + (robotNum - 1) * 2]);
                    sb.Append((char)healthPacket[4 + (robotNum - 1) * 2]);
                    HealthLB.Text = sb.ToString();
                    health = int.Parse(HealthLB.Text);

                    Refresh();
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        sb.Append((char)healthPacket[i]);
                    }
                    TheirNexusLB.Text = sb.ToString();

                    sb.Clear();
                    for (int i = 11; i < 14; i++)
                    {
                        sb.Append((char)healthPacket[i]);
                    }
                    OurNexusLB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[3]);
                    sb.Append((char)healthPacket[4]);
                    Enemy1LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[5]);
                    sb.Append((char)healthPacket[6]);
                    Enemy2LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[7]);
                    sb.Append((char)healthPacket[8]);
                    Enemy3LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[9]);
                    sb.Append((char)healthPacket[10]);
                    Enemy4LB.Text = sb.ToString();
                    sb.Clear();

                    sb.Append((char)healthPacket[14 + (robotNum - 1) * 2]);
                    sb.Append((char)healthPacket[15 + (robotNum - 1) * 2]);
                    HealthLB.Text = sb.ToString();
                    health = int.Parse(HealthLB.Text);

                    Refresh();
                }

                // Begin listening for another health packet asynchronously so that the GUI is not delayed
                result = healthListener.ReceiveAsync();
            }
        }

        // This is called when pictureBox1 is refreshed. It repaints the display based on controller inputs
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(init_img, new Point(0, 0));

            float lx = (stick_size.Width / 2.0f) - 4f + (stick_size.Width / 2.0f) * ((float)joystick.Thumbsticks.Left.X);
            float ly = (stick_size.Height / 2.0f) - 4f + (stick_size.Height / 2.0f) * (-(float)joystick.Thumbsticks.Left.Y);
            float rx = (stick_size.Width / 2.0f) - 4f + (stick_size.Width / 2.0f) * ((float)joystick.Thumbsticks.Right.X);
            float ry = (stick_size.Height / 2.0f) - 4f + (stick_size.Height / 2.0f) * (-(float)joystick.Thumbsticks.Right.Y);

            g.FillEllipse(brush3, new Rectangle(left_stick, stick_size));
            g.FillEllipse(brush1, lx + left_stick.X, ly + left_stick.Y, 8.0f, 8.0f);
            g.FillEllipse(brush3, new Rectangle(right_stick, stick_size));
            g.FillEllipse(brush2, rx + right_stick.X, ry + right_stick.Y, 8.0f, 8.0f);

            if (joystick.A)
            {
                g.FillEllipse(brush4, 486, 154, 45, 45);
            }
            if (joystick.Y)
            {
                g.FillEllipse(brush4, 486, 65, 45, 45);
            }
            if (joystick.B)
            {
                g.FillEllipse(brush4, 533, 110, 45, 45);
            }
            if (joystick.X)
            {
                g.FillEllipse(brush4, 439, 110, 45, 45);
            }
        }
    }
}
