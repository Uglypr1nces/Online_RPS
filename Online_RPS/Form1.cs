using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_RPS
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;

        string player;
        string otherplayer;

        string playerchoice = null;
        string otherplayerchoice = null;

        int time = 10;

        int buttonpresses = 1;

        bool gamestart = false;

        public Form1()
        {
            InitializeConnection();
            Task.Run(() => recieve());
            send("hello world");
            InitializeComponent();

        }

        private void InitializeConnection()
        {
            try
            {
                client = new TcpClient("192.168.1.53", 5555);
                stream = client.GetStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gamestarter()
        {
            timer1.Interval = 1000;
            timer1.Start();
        }
        public void send(string message)
        {
            try
            {
                int byteCount = Encoding.ASCII.GetByteCount(message);
                byte[] senddata = Encoding.ASCII.GetBytes(message);

                stream.Write(senddata, 0, senddata.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void recieve()
        {
            try
            {
                StreamReader sr = new StreamReader(stream);
                char[] buffer = new char[1024];
                int bytesRead;

                while ((bytesRead = sr.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string response = new string(buffer, 0, bytesRead);

                    if (response.Substring(0, 4) == "name")
                    {
                        player = response.Substring(5);

                        if (player == "player1") { otherplayer = "player2"; }
                        else { otherplayer = "player1"; }

                    }

                    if (response.Contains("gamestart"))
                    {
                        gamestart = true;
                        gamestarter();

                        MessageBox.Show("game is starting");
                    }

                    if (response == otherplayer + "scissors")
                    {
                        MessageBox.Show("other player picked scissors");
                    }

                    buffer = new char[1024];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (buttonpresses != 0 && gamestart)
            {
                if (player == "player1") { playerchoice = "paper"; }
                else { otherplayerchoice = "paper"; }
                send(player + "paper");

                buttonpresses = 0;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (buttonpresses != 0 && gamestart)
            {
                if (player == "player1") { playerchoice = "rock"; }
                else { otherplayerchoice = "rock"; }
                send(player + "rock");

                buttonpresses = 0;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (buttonpresses != 0 && gamestart)
            {
                if (player == "player1") { playerchoice = "scissors"; }
                else { otherplayerchoice = "scissors"; }
                send(player + "scissors");

                buttonpresses = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                label1.Text = Convert.ToString(time);
            }));

            time--;

            if (time < 0)
            {
                buttonpresses++;
                time = 10;
                //Thread.Sleep(5000);
            }
        }

    }
}