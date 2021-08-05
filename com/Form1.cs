using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;


namespace com
{
    public partial class Form1 : Form
    {
        string str;
        public Form1()
        {
            InitializeComponent();

            string[] PortNames = SerialPort.GetPortNames();  // 포트 검색.

            foreach (string portnumber in PortNames)
            {
                comboBox1.Items.Add(portnumber);          // 검색한 포트를 콤보박스에 입력. 
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            serialPort1.Write(textBox2.Text + Environment.NewLine);
            Thread.Sleep(100);
            try {
/*                tc0.Text = str.Split('+')[1];
                tc1.Text = str.Split('+')[2];
                tc2.Text = str.Split('+')[3];
                tc3.Text = str.Split('+')[4];
                tc4.Text = str.Split('+')[5];*/
            }
            catch(Exception ee)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {   
                serialPort1.PortName = comboBox1.SelectedItem.ToString();                     //콤보 박스에서 선택.
                serialPort1.BaudRate = int.Parse(comboBox2.SelectedItem.ToString());          //콤보 박스에서 Baud Rate 선택.
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                serialPort1.Open();
                //Timer1.Start();            // Timer Stop
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived); //데이터 받기.
                textBox1.Text += "연결되었습니다." + Environment.NewLine;

                //serialPort1.Write("$01M");                                                    // abcd\r\n Send
            }
            else
            {
                textBox1.Text += "연결되어 있습니다." + Environment.NewLine;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                //Timer1.Stop();             // Timer Stop
                textBox1.Text += "해제되었습니다." + Environment.NewLine;
            }
            else textBox1.Text += "해제되어 있습니다 ." + Environment.NewLine;
        }


        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(MySerialReceived));
        }

        private void MySerialReceived(object s, EventArgs e)  //여기에서 수신 데이타를 사용자의 용도에 따라 처리한다.
        {
            //Byte[] ReceiveData = serialPort1.ReadByte();  //시리얼 버터에 수신된 데이타를 ReceiveData 읽어오기
            str = serialPort1.ReadExisting();
            //string str = Encoding.Default.GetString(ReceiveData);
            //textBox1.Text = textBox1.Text + string.Format("{0:X2}", ReceiveData);  //int 형식을 string형식으로 변환하여 출력
            textBox1.Text += "\n" + str;  //int 형식을 string형식으로 변환하여 출력
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.TextLength;     //스크롤 자동으로 내린다.
            textBox1.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
