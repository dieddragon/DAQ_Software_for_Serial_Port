using System.Windows.Forms;
using System.Collections.Generic;

using ZedGraph;

namespace Serial_DAQ_Software
{
    public partial class MainWin : Form
    {
        PointPairList[] data;
        //PointPairList rawDataLC = new PointPairList();

        //array to store serial port names connected to the computer
        string[] namePorts = new string[] { };

        int nData = 0;
        double timeDataOffsetMS = 0;
        int numData = 0;

        int lengthData = 0;
        int iTS = 0;

        bool isJustStarted = true;

        //zed graph stuff
        GraphPane pane;
        LineItem[] curve;

        static readonly object locker = new object();

        private delegate void UpdateAxes();
        private delegate void InvalidatePlot();
        private delegate void RefGraph();

        System.Random rnd = new System.Random();

        public MainWin()
        {
            InitializeComponent();

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
                    double[] tempDataLC = new double[numData];
                    double tempDataTS = 0;

                    nData++;

                    string rawData = serialPortLC.ReadLine();
                    string[] tempMessage = rawData.Split(',');

                    if (iTS == 0)
                        tempDataTS = nData;

                    if (tempMessage.Length == lengthData)
                    {
                        int counter = 0;
                        for (int i = 0; i < lengthData; i++)
                        {
                            if (i == (iTS - 1))
                            {
                                    double.TryParse(tempMessage[i], out tempDataTS);
                            }
                            else
                            {
                                double.TryParse(tempMessage[i], out tempDataLC[counter]);
                                counter++;
                            }
                        }

                        //double.TryParse(tempData[0], out tempDataLC);
                       // double.TryParse(tempData[3], out tempDataTS);

                        // If this is the first set of data read, then set the time offset
                        if (nData == 1)
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
                            for (int i = 0; i < numData; i++)
                            {
                                data[i].Add(timeDataOffsetMS, tempDataLC[i]);
                                curve[i].AddPoint(timeDataOffsetMS, tempDataLC[i]);
                            }
                                

                            //curveLC.AddPoint(timeDataOffsetMS, tempDataLC);
                            if (iTS != 0)
                                timeDataOffsetMS += (tempDataTS / 1000.0);
                            else
                                timeDataOffsetMS += tempDataTS;

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
                
            nData = 0;
            
            try
            {
                

                //serialPortLC.DiscardInBuffer();
                lock (locker)
                {
                    for (int i = 0; i<numData; i++)
                    {
                        data[i].Clear();
                        curve[i].Clear();
                    }

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

        private void InitializeGraph(int numData)
        {
            // Initialize the graph
            pane = zedGraphControl.GraphPane;

            // Set the Titles
            pane.Title = "Data";
            pane.XAxis.Title = (iTS == 0) ? "N" : "Time (ms)";
            pane.YAxis.Title = "Value";

            // Generate curves with random colors
            //int numData = (iTS == 0) ? lengthData : lengthData - 1;
            curve = new LineItem[numData];
            for (int i = 0; i < numData; i++)
            {
                curve[i] = pane.AddCurve("Data: " + (i + 1).ToString(), data[i], System.Drawing.Color.FromArgb(rnd.Next(50, 255), rnd.Next(50, 255), rnd.Next(50, 255)), SymbolType.Diamond);
                curve[i].Line.IsVisible = true;
            }
               
           // curveLC = paneLC.AddCurve("Load Cell", dataLC, System.Drawing.Color.Red, SymbolType.Diamond);
           // curveLC.Line.IsVisible = false;
        }

        private void RefreshGraph()
        {
                zedGraphControl.Invalidate();
                zedGraphControl.AxisChange();                
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
                if (int.TryParse(tbNoData.Text, out lengthData) && int.TryParse(tbIndTS.Text, out iTS) && lengthData > 0)
                {
                    numData = (iTS == 0) ? lengthData : lengthData - 1;
                    if (numData > 0)
                    {
                        if (isJustStarted)
                        {
                            data = new PointPairList[numData];
                            for (int i = 0; i < numData; i++)
                                data[i] = new PointPairList();
                            InitializeGraph(numData);
                            isJustStarted = false;
                        }

                        System.Action actDisc = () => serialPortLC.DiscardInBuffer();
                        this.BeginInvoke(actDisc);
                        System.Action actStop = () => btnStart.Text = "Stop";
                        this.BeginInvoke(actStop);
                    }
                }
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
         //   try
         //   {
                //////////////////////////////// CHANGE!
         //       PointPairList tempData = (PointPairList)dataLC.Clone();

         //       List<string> tempDataString = new List<string>();

         //       string nameFile = System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Hour.ToString() + "_" + System.DateTime.Now.Minute.ToString() + "_" + System.DateTime.Now.Second.ToString() + ".txt";

                // Write to the file
         //       using (System.IO.StreamWriter file =
         //       new System.IO.StreamWriter(nameFile))
         //       {
         //           file.WriteLine("Time (ms) , Load (gr)");
         //           file.WriteLine(tbComment.Text.ToString());
         //           foreach (PointPair pp in tempData)
         //               file.WriteLine(pp.X.ToString() + "," + pp.Y.ToString());

         //       }
         //   }
         //   catch (System.Exception ex)
         //   {
         //       MessageBox.Show(ex.Message);
         //   }
        }
    }

}
