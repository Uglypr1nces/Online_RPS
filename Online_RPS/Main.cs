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
using System.Diagnostics;
using NAudio.Wave;

namespace Online_RPS
{
    public partial class Main : Form
    {
        TcpClient client;
        NetworkStream stream;

        string player;
        string otherplayer;
        string playerchoice = null;
        string otherplayerchoice = null;
        string server_address;
        int server_port;

        string paperimg = Path.Combine(Application.StartupPath, "content/paper.jpg");
        string rockimg = Path.Combine(Application.StartupPath, "content/rock.jpg");
        string scissorsimg = Path.Combine(Application.StartupPath, "content/scissors.jpg");
        string youloose = Path.Combine(Application.StartupPath, "content/you loose.mp4");
        string youwin = Path.Combine(Application.StartupPath, "content/you win.mp4");
        string start_sound = Path.Combine(Application.StartupPath, "content/start.mp3");
        string ding_sound = Path.Combine(Application.StartupPath, "content/ding.mp3");
        string game_start_sound = Path.Combine(Application.StartupPath, "content/game_start.mp3");


        int namechanges = 1;

        public Main()
        {
            InitializeComponent();
            Task.Run(() => playSimpleSound(start_sound));
            pictureBox1.Load(paperimg);
            pictureBox2.Load(rockimg);
            pictureBox3.Load(scissorsimg);

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void playSimpleSound(string path)
        {
            try
            {
                using (var audioFile = new AudioFileReader(path))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }
        }

        private void InitializeConnection(string address,int port)
        {
            try
            {
                client = new TcpClient(address, port);
                stream = client.GetStream();
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
        }
        public void send(string message)
        {
            try
            {
                int byteCount = Encoding.ASCII.GetByteCount(message);
                byte[] senddata = Encoding.ASCII.GetBytes(message);

                stream.Write(senddata, 0, senddata.Length);
            }
            catch (Exception ex){MessageBox.Show(ex.Message);}
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

                        this.Invoke((MethodInvoker)(() =>{label1.Text = player;}));
                    }

                    if(response.Contains("you won")){videoplayer(youwin);}
                    else if(response.Contains("you lost")){videoplayer(youloose);}
                    response = "";
                    buffer = new char[1024];
                }
            }
            catch (Exception ex)
            {MessageBox.Show(ex.Message);}
        }
        private void videoplayer(string path)
        {
            try{Process.Start(path);}
            catch{MessageBox.Show("COULD NOT PLAY");}
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (player == "player1") { playerchoice = "paper"; }
            else { otherplayerchoice = "paper"; }
            send(player + "paper");
            Task.Run(() => playSimpleSound(ding_sound));
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            if (player == "player1") { playerchoice = "rock"; }
            else { otherplayerchoice = "rock"; }
            send(player + "rock");
            Task.Run(() => playSimpleSound(ding_sound));
        }
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            if (player == "player1") { playerchoice = "scissors"; }
            else { otherplayerchoice = "scissors"; }
            send(player + "scissors");
            Task.Run(() => playSimpleSound(ding_sound));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server_address = textBox1.Text;
            server_port = Convert.ToInt32(textBox2.Text);
            InitializeConnection(server_address,server_port);
            send("hello world");
            Task.Run(() => playSimpleSound(game_start_sound));
            panel2.Hide();
            Task.Run(() => recieve());
        }
    }
}
