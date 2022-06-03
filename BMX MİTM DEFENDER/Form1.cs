using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace BMX_MİTM_DEFENDER
{
    
    public partial class Form1 : Form
    {
        DataTable tablo = new DataTable();
        int click;
        int a;
        int sayac;
        static CountdownEvent countdown;
        static int upCount = 0;
        static object lockObj = new object();
        const bool resolveNames = true;

        public string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a " + ipAddress;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string strOutput = pProcess.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                    + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                    + "-" + substrings[7] + "-"
                    + substrings[8].Substring(0, 2);
                return macAddress;
            }
            else
            {
                return "not found";
            }
        }
        static List<string> WriteAllIpAddresses()
        {
            List<string> ips = new List<string>();
            string tempIp = string.Empty;

            for (int i = 0; i <= 255; i++)
            {
                for (int j = 0; j <= 255; j++)
                {
                    for (int k = 0; k <= 255; k++)
                    {
                        for (int l = 0; l <= 255; l++)
                        {
                            tempIp = String.Format($"{i}.{j}.{k}.{l}");
                            ips.Add(tempIp);
                        }
                    }
                }
            }
            return ips;
        }



        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



            
            tablo.Columns.Add("MAC ADRESS", typeof(string));
            tablo.Columns.Add("İP", typeof(string));
            tablo.Rows.Add("1c-44-19-7e-b4-98", "192.168.1.56");
      
            pictureBox2.Hide();
            pictureBox3.Hide();
            pictureBox4.Hide();
      
            
            var psi = new ProcessStartInfo
            {
                FileName = "https://linktr.ee/mcozcan",
                UseShellExecute = true,
            };
            Process.Start(psi);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (click > 0)
            {

                dataGridView2.DataSource = null;
                progressBar1.Value = 0;
                pictureBox3.Hide();
                pictureBox4.Hide();
                label2.Text = "Taranıyor";
            }
            timer1.Enabled = true;
            timer1.Interval = 685;
            backgroundWorker1.RunWorkerAsync();          

            
          


            
        }
  
        private void button2_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("Man-in-the-middle attack Saldırgan ve Modem MAC adresleri birebir aynı olur, saldırganın birbiri ile doğrudan iletişim kuran iki taraf arasındaki iletişimi gizlice ilettiği veya değiştirdiği saldırı türüdür. İletişim ağı üzerinde veri paketleri serbestçe dolaşır. Özellikle broadcast olarak salınan paketler, aynı ağa bağlı tüm cihazlar tarafından görülebilir.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "https://linktr.ee/mcozcan",
                UseShellExecute = true,
            };
            Process.Start(psi);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
          
            if (progressBar1.Value == 100)
            {//eğer progressbarın değeri 100e eşitlenirse
                timer1.Stop();//timerı durduruyoruz.

            }
            sayac++;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            pictureBox2.Show();

            dataGridView2.DataSource = tablo;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.Black;
            style.ForeColor = Color.Green;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ;

            

            
                    string ip = "192.168.1.";
                    for (int i = 0; i < 255; i++)
                    {
                        string nf = "not found";
                        if (GetMacAddress(ip + i) == nf)
                        {
                            //ip boş
                        }

                        else
                        {
                            tablo.Rows.Add(GetMacAddress(ip + i), ip + i);
                        }
                    }
                    string c_ip;
                    for (int x = 0; x < 255; x++)
                    {
                        string nf = "not found";
                    bas1:
                        c_ip = ip + x;
                        if (GetMacAddress(c_ip) == nf)
                        {
                            x++;
                            goto bas1;
                        }
                        else
                        {
                            int j = 1;
                            while (j < 255)
                            {
                            b2:
                                if (c_ip == ip + j)
                                {
                                    j++;
                                    goto b2;
                                }
                                else
                                {
                                    if (GetMacAddress(c_ip) == GetMacAddress(ip + j))
                                    {
                                        a = 1;

                                        j++;

                                    }
                                    else
                                    {
                                        j++;

                                    }
                                }
                            }
                        }
                    }

                    if (a == 1)
                    {
                pictureBox2.Hide();
                pictureBox3.Show();
                label2.Text = "Saldırı Yok Ağınız Güvenli";
                label2.ForeColor = Color.Black;
                label2.BackColor = Color.Green;
                click++;
                //clean

            }

            else
                    {
                pictureBox2.Hide();
                pictureBox4.Show();
                label2.ForeColor = Color.White;
                label2.BackColor = Color.Red;
                label2.Text = "Saldırı Var Ağdan Derhal Çıkın!";
                MessageBox.Show("Saldırı Var Ağdan Derhal Çıkın! , Eğer ağın yöneticisi iseniz ağ şifresini değiştirin , Güvenlik türünün WPA-2 PSK olduğundan emin olun , Halka açık bir ağa bağlı iseniz ağı kullanmayın!");
                click++;
                
                    }

                    
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
