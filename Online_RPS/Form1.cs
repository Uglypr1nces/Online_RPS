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

        string paperimg = Path.Combine(Application.StartupPath, "images/paper.jpg");
        string rockimg = Path.Combine(Application.StartupPath, "images/rock.jpg");
        string scissorsimg = Path.Combine(Application.StartupPath, "images/scissors.jpg");

        int namechanges = 1;

        public Form1()
        {
            InitializeComponent();
            InitializeConnection();
            Task.Run(() => recieve());
            send("hello world");

            pictureBox1.Load(paperimg);
            pictureBox2.Load(rockimg);
            pictureBox3.Load(scissorsimg);

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
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

                    if (response.Substring(0, 4) == "name" && namechanges != 0)
                    {
                        player = response.Substring(5);
                        namechanges = 0;

                        if (player == "player1") { otherplayer = "player2"; }
                        else { otherplayer = "player1"; }
                    }

                    if (response == "you won" ^ response == "you lost") { MessageBox.Show(response); }

                    buffer = new char[1024];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (player == "player1") { playerchoice = "paper"; }
            else { otherplayerchoice = "paper"; }
            send(player + "paper");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (player == "player1") { playerchoice = "rock"; }
            else { otherplayerchoice = "rock"; }
            send(player + "rock");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (player == "player1") { playerchoice = "scissors"; }
            else { otherplayerchoice = "scissors"; }
            send(player + "scissors");
        }
    }
}
