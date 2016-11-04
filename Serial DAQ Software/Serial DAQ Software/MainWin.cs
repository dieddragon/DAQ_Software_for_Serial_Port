using System.Windows.Forms;
using System.Collections.Generic;

using ZedGraph;

namespace Serial_DAQ_Software
{
    public partial class MainWin : Form
    {
        PointPairList dataLC = new PointPairList();
        PointPairList rawDataLC = new PointPairList();

        //array to store serial port names connected to the computer
        string[] namePorts = new string[] { };

        int nDataLC = 0;
        double timeDataOffsetMS = 0;

        int lengthData = 4;

        //zed graph stuff
        GraphPane paneLC;
        LineItem curveLC;

        static readonly object locker = new object();

        private delegate void UpdateAxes();
        private delegate void InvalidatePlot();
        private delegate void RefGraph();

        public MainWin()
        {
            InitializeComponent();

            InitializeGraph();

            namePorts = System.IO.Ports.SerialPort.GetPortNames();

            SetPortNames();
        }

        private void btnConnectSP_Click(object sender, System.EventArgs e)
        {
            if (btnConnectSP.Text == "Connect")
            {
                if (cbCOM.SelectedItem != null)
                {
                    serialPortLC.PortName = cbCOM.SelectedItem.ToString();

                    if (serialPortLC.IsOpen)
                    {
                        serialPortLC.DiscardInBuffer();
                        serialPortLC.Close();
                    }                       

                    if (!serialPortLC.IsOpen)
                    {
                        serialPortLC.Open();
                        serialPortLC.DiscardInBuffer();
                    }

                    btnConnectSP.Text = "Stop";
                }
            }
            else
            {
                if (serialPortLC.IsOpen)
                {
                    serialPortLC.DiscardInBuffer();
                    serialPortLC.Close();
                }
                    

                btnConnectSP.Text = "Connect";

                HandleStartButton();
            }
                
        }

        private void serialPortLC_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (btnStart.Text == "Stop")
                {
                    double tempDataLC;
                    double tempDataTS;

                    nDataLC++;
                    string rawData = serialPortLC.ReadLine();
                    string[] tempData = rawData.Split(',');

                    if (tempData.Length == lengthData)
                    {

                        double.TryParse(tempData[0], out tempDataLC);
                        double.TryParse(tempData[3], out tempDataTS);

                        // If this is the first set of data read, then set the time offset
                        if (nDataLC == 1)
                        {
                            // change
                            timeDataOffsetMS = 0;
                        }

                        // Record the raw signal
                        //rawDataLC.Add(timeDataOffsetMS, tempDataLC);

                        // Tell ZedGraph to refigure the
                        // axes since the data have changed
                        lock (locker)
                        {

                            dataLC.Add(timeDataOffsetMS, tempDataLC);
                            curveLC.AddPoint(timeDataOffsetMS, tempDataLC);
                            timeDataOffsetMS += (tempDataTS / 1000.0);
                            RefreshGraph();

                        }
                        
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Serial Data Read Fail ");
            }

        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            HandleStartButton();                
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            if (serialPortLC.IsOpen)
            {
                System.Action actDisc = () => serialPortLC.DiscardInBuffer();
                this.BeginInvoke(actDisc);
            }
                
            nDataLC = 0;
            
            try
            {
                

                //serialPortLC.DiscardInBuffer();
                lock (locker)
                {
                    dataLC.Clear();
                    curveLC.Clear();
                    timeDataOffsetMS = 0;
                }
                    
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSetFreq_Click(object sender, System.EventArgs e)
        {
            if (serialPortLC.IsOpen && cbSetFreqLC.SelectedItem != null)
                serialPortLC.WriteLine("f " + cbSetFreqLC.SelectedItem.ToString());
        }



        #region Funs

        private void InitializeGraph()
        {
            // Initialize the graph
            paneLC = zedGraphControlLC.GraphPane;

            // Set the Titles
            paneLC.Title = "Load vs Time";
            paneLC.XAxis.Title = "Time (ms)";
            paneLC.YAxis.Title = "Load (digits)";

            // Generate a red curve with diamond symbols
            curveLC = paneLC.AddCurve("Load Cell", dataLC, System.Drawing.Color.Red, SymbolType.Diamond);
            curveLC.Line.IsVisible = false;
        }

        private void RefreshGraph()
        {
                zedGraphControlLC.Invalidate();
                zedGraphControlLC.AxisChange();                
        }

        private void SetPortNames()
        {
            if (namePorts.Length != 0)
            {
                for (int i = 0; i < namePorts.Length; i++)
                    cbCOM.Items.Add(namePorts[i]);
                cbCOM.SelectedItem = cbCOM.Items[0];
            }
        }

        private void HandleStartButton()
        {
            if (btnStart.Text == "Start" && btnConnectSP.Text == "Stop")
            {
                System.Action actDisc = () => serialPortLC.DiscardInBuffer();
                System.Action actStop = () => btnStart.Text = "Stop";
                this.BeginInvoke(actDisc);
                this.BeginInvoke(actStop);

            }
            else
            {
                System.Action actStart = () => btnStart.Text = "Start";
                this.BeginInvoke(actStart);
            }
        }

        #endregion

        private void btnWritetoFile_Click(object sender, System.EventArgs e)
        {
            try
            {
                //////////////////////////////// CHANGE!
                PointPairList tempData = (PointPairList)dataLC.Clone();

                List<string> tempDataString = new List<string>();

                string nameFile = System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Hour.ToString() + "_" + System.DateTime.Now.Minute.ToString() + "_" + System.DateTime.Now.Second.ToString() + ".txt";

                // Write to the file
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(nameFile))
                {
                    file.WriteLine("Time (ms) , Load (gr)");
                    file.WriteLine(tbComment.Text.ToString());
                    foreach (PointPair pp in tempData)
                        file.WriteLine(pp.X.ToString() + "," + pp.Y.ToString());

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
