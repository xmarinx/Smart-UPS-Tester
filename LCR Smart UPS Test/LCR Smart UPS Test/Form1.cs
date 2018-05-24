using System;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LCR_Smart_UPS_Test
{
    public partial class Form1 : Form
    {
        public double VV_IN, VV_OUT, VV_ACCU, VV_UPS, CELL1, CELL2, CELL3, CS_OUT, CS_IN;
        public int BL;
        public string value, buffer, TEST_BATTERY, UPS;
        bool con1, con2, con3, con4, con5, con6, con7, con8, con9, con10, con11, con12, con13,con14, con15, con16, com1;
	   
	    DataTable dt = new DataTable();
            
       
        SerialPort comPort = new SerialPort();
        
        public Form1()
        {
            InitializeComponent();
          
           
            
        }
        public void drawTable()
        {
            dt.Columns.Add("VAR", typeof(string));
            dt.Columns.Add("VALUE", typeof(double));
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows[0][0] = "VV_IN";
            dt.Rows[1][0] = "VV_OUT";
            dt.Rows[2][0] = "VV_ACCU";
            dt.Rows[3][0] = "VV_UPS";
            dt.Rows[4][0] = "CELL1";
            dt.Rows[5][0] = "CELL2";
            dt.Rows[6][0] = "CELL3";
            dt.Rows[7][0] = "CS_IN";
            dt.Rows[8][0] = "CS_OUT";

            dataGridView1.DataSource = dt;
        }
        public void tabela()
        {
            if (comPort.IsOpen == true)
            {
                dt.Rows[0][1] = VV_IN;
                dt.Rows[1][1] = VV_OUT;
                dt.Rows[2][1] = VV_ACCU;
                dt.Rows[3][1] = VV_UPS;
                dt.Rows[4][1] = CELL1;
                dt.Rows[5][1] = CELL2;
                dt.Rows[6][1] = CELL3;
                dt.Rows[7][1] = CS_IN;
                dt.Rows[8][1] = CS_OUT;

               
            }
            else
            {
                ReloadForm();
            }
        }

        private void ReloadForm()
        {
            rtbBalancer.BackColor = DefaultBackColor;
            rtbBreme.BackColor = DefaultBackColor;
            rtbNapetosti.BackColor = DefaultBackColor;
           
            btnCharge.Text = "Charge OFF";
           
        }

        private void SetDefault()
        {
            comPort.BaudRate = 9600;
            comPort.DataBits = 8;
            comPort.StopBits = StopBits.One;
            comPort.Parity = Parity.None;
            comPort.PortName = listBox1.Text;
        }

       
        #region test

        private void Test()
        {            
            if (comPort.IsOpen == true)
            {
                richTextBox1.Text = string.Empty;
                if (VV_IN > 13 | VV_IN < 10)
                {
                    con1 = false;
                    richTextBox1.AppendText("Error1: Vhodna napetost je izven limita" + Environment.NewLine);
                }
                else con1 = true;
                if (VV_UPS < VV_OUT)
                {
                    con2 = false;
                    richTextBox1.AppendText("Error2: Napetost v UPS je manja od izhodne napetosti" + Environment.NewLine);
                }
                else con2 = true;
                if (VV_ACCU > VV_UPS)
                {
                    con3 = false;
                    richTextBox1.AppendText("Error3: Napetost v akumulatoru je vecja od napetosti v UPSu" + Environment.NewLine);
                }
                else con3 = true;
             /*   if ((VV_UPS - VV_OUT) < 0.1)
                {
                    con4 = false;
                    richTextBox1.AppendText("Error4" + Environment.NewLine);
                }
                else con4 = true;
                if ((VV_UPS - VV_OUT) > 0.7)
                {
                    con5 = false;
                    richTextBox1.AppendText("Error5" + Environment.NewLine);
                }
                else con5 = true;
                */
              
                if (CELL1 < 1 | CELL1 > 6)
                {
                    con6 = false;
                    richTextBox1.AppendText("Error6: Napetost v prvi celici je izvan limita" + Environment.NewLine);
                }
                else con6 = true;
                if (CELL2 < 1 | CELL2 > 6)
                {
                    con7 = false;
                    richTextBox1.AppendText("Error7: Napetost v drugi celici je izvan limita" + Environment.NewLine);
                }
                else con7 = true;
                if (CELL3 < 1 | CELL3 > 6)
                {
                    con8 = false;
                    richTextBox1.AppendText("Error8: Napetost v tretji celici je izvan limita" + Environment.NewLine);
                }
                else con8 = true;
            
                if (CS_OUT > 250)
                {
                    con9 = false;
                    richTextBox1.AppendText("Error9" + Environment.NewLine);
                }
                else con9 = true;
                if (CS_IN > 100)
                {
                    con10 = false;
                    richTextBox1.AppendText("Error10" + Environment.NewLine);
                }
                else con10 = true;
           

                if (con1 == true && con2 == true && con3 == true && /*con4 == true && con5 == true &&*/ con6 == true && con7 == true && con8 == true && con9 == true && con10 == true)
                {
                    rtbNapetosti.BackColor = Color.Green;
                }
                else rtbNapetosti.BackColor = Color.Red;
            }
        }
        private void TestBat()
        {
            if (comPort.IsOpen == true)
            {
                richTextBox1.Text = string.Empty;
                comPort.WriteLine("SERVICE CHARGE OFF");
                Wait(100);
                comPort.WriteLine("SERVICE TEST_BATTERY START");
                Wait(2222);
                if (CS_OUT >= 1000)
                {
                    rtbBreme.BackColor = Color.Green;
                    if (rtbBreme.BackColor == Color.Green)
                    {
                        comPort.WriteLine("SERVICE RESET_EEPROM START");
                    }          
     
                }
            }
        }
       
        #endregion

       
        
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            drawTable();
        }
   
  

        private void btnTest1_MouseClick(object sender, MouseEventArgs e)
        {
            Test();       
        }

        private void btnTest2_MouseClick(object sender, MouseEventArgs e)
        {
            TestBat();
        }


        private void btnCharge_MouseClick(object sender, MouseEventArgs e)
        {
            if (comPort.IsOpen)
            {
                if (btnCharge.Text == "Charge OFF")
                {
                    comPort.WriteLine("SERVICE CHARGE OFF");
                    btnCharge.Text = "Charge ON";
                }
                else if (btnCharge.Text == "Charge ON")
                {
                    comPort.WriteLine("SERVICE CHARGE ON");
                    btnCharge.Text = "Charge OFF";
                }
            }            
        }

        private void btnReset_MouseClick(object sender, MouseEventArgs e)
        {
            if (comPort.IsOpen == true)
            {
                comPort.WriteLine("SERVICE RESET START");
            }
        }

        private void btnTest3_MouseClick(object sender, MouseEventArgs e)
        {
           // TestBat();
        }



        public void balancerTest()
        {
            if (comPort.IsOpen == true)
            {

               
                richTextBox1.Text = string.Empty;
                comPort.WriteLine("SERVICE CHARGE OFF");
                timer2.Stop();               
                Balancer1();
                Thread.Sleep(55);
                timer2.Start();
                Wait(2222);
                timer2.Stop();
                if (CELL1 < 1)
                {
                    con14 = true;
                    comPort.WriteLine("SERVICE BALANCER 000");
                    Balancer2();
                    Thread.Sleep(55);
                    timer2.Start();
                    Wait(2222);
                    timer2.Stop();
                }
                else { richTextBox1.AppendText("Error Balancer 1" + Environment.NewLine); con14 = false; Balancer2(); Thread.Sleep(55); timer2.Start(); Wait(2222); timer2.Stop(); comPort.WriteLine("SERVICE BALANCER 000"); }
                
                if (CELL2 < 1)
                {
                    con15 = true;
                    comPort.WriteLine("SERVICE BALANCER 000");
                    Balancer3();
                    Thread.Sleep(55);
                    timer2.Start();
                    Wait(2222);
                    timer2.Stop();
                 
                }
                else
                {
                    con15 = false;
                    richTextBox1.AppendText("Error Balancer 2" + Environment.NewLine);
                    Balancer3();
                    Thread.Sleep(55);
                    timer2.Start();
                    Wait(2222);
                    timer2.Stop();
                   
                }
                if (CELL3 < 2)
                {

                    con16 = true;
                    comPort.WriteLine("SERVICE BALANCER 000");

                }
                
                else
                {
                    richTextBox1.AppendText("Error Balancer 3" + Environment.NewLine);
                    con16 = false;
                    comPort.WriteLine("SERVICE BALANCER 000");
                    
                }
                if (con14 == true && con15 == true && con16 == true)
                {
                    rtbBalancer.BackColor = Color.Green;
                    comPort.WriteLine("SERVICE CHARGE ON");
                    timer2.Start();
                   
                }
                else
                {
                    rtbBalancer.BackColor = Color.Red;
                    comPort.WriteLine("SERVICE CHARGE ON");
                    timer2.Start();
                   
                }
                
            }

    }

        private void button1_Click(object sender, EventArgs e)
        {
            balancerTest();
        }
            
        private void BalancerClear()
        {
            comPort.WriteLine("SERVICE BALANCER 000");
        }

        private void Balancer1()
        {
            
                comPort.WriteLine("SERVICE BALANCER 001");
                          
        }

        private void Balancer2()
        {
           
            comPort.WriteLine("SERVICE BALANCER 010");
          
        }
            
        
        private void Balancer3()
        {
           
                comPort.WriteLine("SERVICE BALANCER 100");
           
        }
            
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (comPort.IsOpen == true)
            {
                comPort.WriteLine("SERVICE BALANCER 4");

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (comPort.IsOpen == true)
            {
                comPort.WriteLine("SERVICE BALANCER 010");

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (comPort.IsOpen == true)
            {
                comPort.WriteLine("SERVICE BALANCER 001");

            }
        }


        private void button2_Click_2(object sender, EventArgs e)
        {
            if (comPort.IsOpen == true)
            {
                comPort.WriteLine("SERVICE BALANCER 001");

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comPort.WriteLine("SERVICE BALANCER 1");
            }
            else
            {
                comPort.WriteLine("SERVICE BALANCER 0");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                comPort.WriteLine("SERVICE BALANCER 2");
            }
            else
            {
                comPort.WriteLine("SERVICE BALANCER 0");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comPort.WriteLine("SERVICE BALANCER 4");
            }
            else
            {
                comPort.WriteLine("SERVICE BALANCER 0");
            }
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            getPorts();
           
            if (listBox1.Items.Count == 2)
            {
                timer1.Stop();
            }
            else timer1.Start();
        }
        private void FlushPort()
        {
            comPort.DiscardInBuffer();
            comPort.DiscardOutBuffer();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {

                if (comPort.IsOpen == true)
                {
                    GetValue();
                    tabela();  
                }
                else ReloadForm();
            }
            catch (System.IO.IOException)
            {
                ReloadForm();
            }                 
        }

        private void getPorts()
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                if (!listBox1.Items.Contains(s))
                    listBox1.Items.Add(s);                            
            }            
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comPort.IsOpen == true) comPort.Close();
            if (listBox1.Text == string.Empty)
            {
                com1 = false;
                comPort.Close();
            }
            else com1 = true;
            if (com1 == true)
            {
                try
                {
                    ReloadForm();
                    SetDefault();

                    timer2.Start();
                    if (comPort.IsOpen == false)
                    {
                        comPort.Open();

                    }
                }
                catch (System.IO.IOException)
                {
                    ReloadForm();

                }
            }      
        }


        private void btnTestBattery_Click(object sender, EventArgs e)
        {

                comPort.WriteLine("SERVICE TEST_BATTERY START");

        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            try
            {
                comPort.WriteLine("SERVICE RESET_EEPROM START");
            }
            catch (System.InvalidOperationException) { }
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            try
            {
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {


                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    return strSource.Substring(Start, End - Start);
                }
                else
                {

                    return "";
                }

            }
            catch (System.ArgumentOutOfRangeException) { return ""; }
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            TestBat();
        }

        public void Wait(int ms)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < ms)
                Application.DoEvents();

        }
        
        public void GetValue()
        {

            if (comPort.IsOpen == true)
            {
                try
                {
                    comPort.WriteLine("VA");
                    Wait(800);
                    buffer = comPort.ReadExisting();
                }
                catch (System.InvalidOperationException)
                {
                    ReloadForm();
                }

                try
                {
                    value = getBetween(buffer, "VV_IN", "\n");
                    VV_IN = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "VV_OUT", "\n");
                    VV_OUT = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "VV_ACCU", "\n");
                    VV_ACCU = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "VV_UPS", "\n");
                    VV_UPS = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "VV_CELL1", "\n");
                    CELL1 = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "VV_CELL2", "\n");
                    CELL2 = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "VV_CELL3", "\n");
                    CELL3 = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "CS_BATTERY_IN", "\n");
                    CS_IN = double.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "CS_BATTERY_OUT", "\n");
                    CS_OUT = double.Parse(value);
                    value = string.Empty;
            /*        value = getBetween(buffer, "BL", "\n");
                    BL = int.Parse(value);
                    value = string.Empty;
                    value = getBetween(buffer, "VA TEST_BATTERY", "\n");
                    TEST_BATTERY = value;
                    value = string.Empty;
                    value = getBetween(buffer, "VA UPS", "\n");
                    UPS = value;
                    value = string.Empty;
                    */
                }
                catch (System.FormatException)
                {

                }
            }
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
