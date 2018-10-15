using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingAlgorithmsSimulation
{
    public partial class frmSortSimulator : Form
    {
        public frmSortSimulator()
        {
            InitializeComponent();
        }

        private void frmSortSimulator_Load(object sender, EventArgs e)
        {
            cboSortAlgo.SelectedIndex = 0;
        }

        #region Variable Global
        Button[] arrButton;
        int[] arrValue;
        int numArr;
        Random rd = new Random();
        int WIDTH = 25;  //kích thước khối
        int GAP = 5;   //khoảng cách giữa các khối
        int speed = 14; //max speed = 4ms, min speed = 14ms
        int sleep = 420;  //thời gian dừng để quan sát các bước duyệt của thuật toán (sleep = 30 * speed)
        bool sorting = false;
        int sortSelected;   //Thuật toán sắp xếp muốn mô phỏng
        #endregion

        private void btnCreateArr_Click(object sender, EventArgs e)
        {
            if (sorting) return;
            numArr = int.Parse(txtNumArr.Text);
            arrValue = new int[numArr];
            arrButton = new Button[numArr];

            pnlSimulator.Controls.Clear();

            for (int i = 0; i < numArr; i++)
            {
                int value = rd.Next(20, 150);
                Button btn = new Button();
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor=Color.LightSkyBlue;
                btn.Size = new Size(WIDTH, value);
                //btn.Text = value.ToString();
                btn.Location = new Point(
                    (pnlSimulator.Width - (WIDTH + GAP) * numArr)/2 + (WIDTH + GAP) * i,
                    pnlSimulator.Height - WIDTH - value);
                btn.BackColor = Color.LightBlue;

                pnlSimulator.Controls.Add(btn);

                arrButton[i] = btn;
                arrValue[i] = value;
            }
            btnSort.Enabled = true;
            //MergeSort(0, numArr - 1);
            //for (int i = 0; i < numArr; i++)
            //{
            //    txtNumArr.Text += arrValue[i] + " ";
            //}
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            sorting = true;
            btnSort.Enabled = false;
            sortSelected = cboSortAlgo.SelectedIndex;
            bgwSimuSort.RunWorkerAsync();          
        }

        //Change speed sort in runtime
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            speed = 14 - trackBar1.Value;
            sleep = 30 * speed;
        }

        //swap p1 p2 of array arrValue
        private void swap(int p1, int p2)
        {
            int Tmp = arrValue[p1];
            arrValue[p1] = arrValue[p2];
            arrValue[p2] = Tmp;
        }

        #region Simulation Functions
        //Highlight color of button
        private void HighLight(int pos, Color color)
        {
            if (pos < numArr)
                arrButton[pos].BackColor = color;
        }

        private void MoveButton(int p1, int p2)
        {
            Status st = new Status();
            st.Pos1 = p1;
            st.Pos2 = p2;
            int x;

            //st.mType = MoveType.LINE_TO_TOP_AND_LINE_TO_BOTTOM;
            //for (x = 0; x < SIZE; x++)
            //{
            //    bgwSimuSort.ReportProgress(0, st);
            //    Thread.Sleep(10);
            //}

            st.mType = MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT;
            int nLoop = ((WIDTH + GAP) * Math.Abs(st.Pos1 - st.Pos2)) / 2;
            for (x = 0; x < nLoop; x++)
            {
                bgwSimuSort.ReportProgress(0, st);
                Thread.Sleep(speed);
            }

            //st.mType = MoveType.TOP_TO_LINE_AND_BOTTOM_TO_LINE;
            //for (x = 0; x < SIZE; x++)
            //{
            //    bgwSimuSort.ReportProgress(0, st);
            //    Thread.Sleep(10);
            //}

            st.mType = MoveType.CHANGED;
            bgwSimuSort.ReportProgress(0, st);
            Thread.Sleep(speed);
        }

        private void bgwSimuSort_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (sortSelected)
            {
                case 0:
                    InterchangeSort();
                    break;
                case 1:
                    InsertionSort();
                    break;
                case 2:
                    SelectionSort();
                    break;
                case 3:
                    BubbleSort();
                    break;
                case 4:
                    HeapSort();
                    break;
                case 5:
                    QuickSort(0, numArr - 1);
                    break;
                case 6:
                    MergeSort(0, numArr - 1);
                    break;
                case 7:
                    ShellSort();
                    break;
                default:
                    break;
            }
        }

        private void bgwSimuSort_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Status st = e.UserState as Status;
            if (st == null) return;
            if (st.mType == MoveType.CHANGED)
            {
                Button btnTmp = arrButton[st.Pos1];
                arrButton[st.Pos1] = arrButton[st.Pos2];
                arrButton[st.Pos2] = btnTmp;
                return;
            }

            Button btn1 = arrButton[st.Pos1];
            Button btn2 = arrButton[st.Pos2];
            //if (st.mType == MoveType.LINE_TO_TOP_AND_LINE_TO_BOTTOM)
            //{
            //    btn1.Top = btn1.Top + 1;
            //    btn2.Top = btn2.Top - 1;
            //}
            //else 
            if (st.mType == MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT)
            {
                btn1.Left = btn1.Left - 2;
                btn2.Left = btn2.Left + 2;
            }
            //else if (st.mType == MoveType.TOP_TO_LINE_AND_BOTTOM_TO_LINE)
            //{
            //    btn1.Top = btn1.Top - 1;
            //    btn2.Top = btn2.Top + 1;
            //}
        }

        private void bgwSimuSort_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < numArr; i++)
            {
                arrButton[i].BackColor = Color.LightBlue;
            }
            sorting = false;
            MessageBox.Show("Đã sắp xếp xong");
        }
        #endregion

        #region Sort Algorithms
        private void BubbleSort()
        {
            bool swapped = true;

            for (int i = 0; i < numArr - 1 && swapped; i++)
            {
                swapped = false;
                for (int j = numArr - 1; j > i; j--)
                {
                    HighLight(j, Color.Green);
                    HighLight(j - 1, Color.Green);
                    Thread.Sleep(sleep);

                    if (arrValue[j] < arrValue[j - 1])
                    {
                        HighLight(j, Color.Orange);
                        HighLight(j - 1, Color.Orange);

                        MoveButton(j, j - 1);
                        swap(j, j - 1);
                        swapped = true;

                    }

                    HighLight(j, Color.LightBlue);
                    HighLight(j - 1, Color.LightBlue);
                }
                HighLight(i, Color.Orange);
            }
        }

        private void InsertionSort()
        {
            for (int i = 1; i < numArr; i++)
            {
                int x = arrValue[i];
                int j = i - 1;

                HighLight(j, Color.Orange);
                HighLight(i, Color.Red);
                Thread.Sleep(sleep);

                while (j >= 0 && arrValue[j] > x)
                {
                    HighLight(j, Color.Green);

                    Thread.Sleep(sleep);
                    arrValue[j  + 1] = arrValue[j];
                    MoveButton(j + 1, j);
                    //sau khi move, index của 2 button đã thay đổi
                    HighLight(j + 1, Color.Orange);
                    Thread.Sleep(sleep);
                    j--;
                }
                HighLight(i, Color.Orange);
                HighLight(j+1, Color.Orange);
                Thread.Sleep(sleep);

                arrValue[j + 1] = x;
            }          
        }

        private void SelectionSort()
        {
            for(int i =0; i< numArr - 1; i++)
            {
                int minPos = i;
                //change color to red
                HighLight(minPos, Color.Red);
                Thread.Sleep(sleep);
                for (int j = i + 1; j < numArr; j++)
                {
                    //change color green
                    HighLight(j, Color.Green);
                    Thread.Sleep(sleep);
                    if (arrValue[j] < arrValue[minPos])
                    {
                        //change color min to lightblue
                        HighLight(minPos, Color.LightBlue);
                        //change color j to red
                        HighLight(j, Color.Red);
                        minPos = j;
                    }
                    else HighLight(j, Color.LightBlue);
                    Thread.Sleep(sleep);
                    //else return color lightblue
                }
                
                MoveButton(minPos, i);
                swap(minPos, i);
                //change color to Orange
                HighLight(i, Color.Orange);
            }
        }

        private void InterchangeSort()
        {
            for(int i = 0; i < numArr - 1; i++)
            {
                HighLight(i, Color.Red);
                Thread.Sleep(sleep);

                for(int j = i + 1; j<numArr; j++)
                {
                    HighLight(j, Color.Green);
                    Thread.Sleep(sleep);

                    if (arrValue[i] > arrValue[j])
                    {
                        HighLight(i, Color.LightBlue);
                        HighLight(j, Color.Red);
                        Thread.Sleep(sleep);

                        MoveButton(j,i);
                        swap(i, j);
                    }
                    HighLight(j, Color.LightBlue);
                    Thread.Sleep(sleep);
                }
                HighLight(i, Color.Orange);
            }
        }

        //Quick Sort with first element as pivot
        private void QuickSort(int low, int high)
        {
            if (low < high)
            {
                int m = Partition(low, high); // O(N)
                                                 // a[low..high] ~> a[low..m–1], pivot, a[m+1..high]
                QuickSort(low, m - 1); // recursively sort left subarray
                                          // a[m] = pivot is already sorted after partition
                QuickSort(m + 1, high); // then sort right subarray
            }
            if (low < numArr)
            {
                HighLight(low, Color.Orange);
                Thread.Sleep(sleep);
            }
        }
        //Parition of Quick Sort Algorithms
        private int Partition(int i, int j)
        {
            int p = arrValue[i]; // p is the pivot
            HighLight(i, Color.Yellow);
            int m = i; 
            for (int k = i + 1; k <= j; k++)
            { 
                HighLight(k, Color.Red);
                Thread.Sleep(sleep);
                if (arrValue[k] < p)
                { 
                    m++;
                    MoveButton(k, m);
                    HighLight(m, Color.LightGreen);
                    Thread.Sleep(sleep);
                    swap(k, m); 
                } 
                else
                {
                    HighLight(k, Color.Purple);
                    Thread.Sleep(sleep);
                }
            }
            MoveButton(m, i);
            swap(i, m); // final step, swap pivot with a[m]
            for (int x = i; x <= j; x++)
            {
                HighLight(x, Color.LightBlue);
            }

            HighLight(m, Color.Orange);
            Thread.Sleep(sleep);
            return m; // return the index of pivot
        }

        void Merge(int low, int mid, int high)
        {
            // subarray1 = a[low..mid], subarray2 = a[mid+1..high], both sorted
            int N = high - low + 1;
            int[] b = new int[N]; // discuss: why do we need a temporary array b?
            int left = low;
            int right = mid + 1;
            int bIdx = 0;
            while (left <= mid && right <= high) // the merging
            {
                //HighLight(left, Color.Red);
                //HighLight(right, Color.Red);
                //Thread.Sleep(sleep);
                if(arrValue[left] <= arrValue[right])
                {
                    b[bIdx++] = arrValue[left++];
                }
                else
                {
                    //MoveButton(right, left);
                    b[bIdx++] = arrValue[right++];
                }
                //HighLight(left, Color.LightBlue);
                //HighLight(right, Color.LightBlue);
                //Thread.Sleep(sleep);
                //b[bIdx++] = (arrValue[left] <= arrValue[right]) ? arrValue[left++] : arrValue[right++];
            } 
            while (left <= mid)
                b[bIdx++] = arrValue[left++]; // leftover, if any
            while (right <= high)
                b[bIdx++] = arrValue[right++]; // leftover, if any
            for (int k = 0; k < N; k++)
                arrValue[low + k] = b[k]; // copy back
        }

        void MergeSort(int low, int high)
        {
            // the array to be sorted is a[low..high]
            if (low < high)
            { // base case: low >= high (0 or 1 item)
                int mid = (low + high) / 2;
                MergeSort(low, mid); // divide into two halves
                MergeSort(mid + 1, high); // then recursively sort them
                Merge(low, mid, high); // conquer: the merge subroutine
            }
        }

        private void HeapSort()
        {
            //Build-Max-Heap
            int heapSize = numArr;
            for (int p = (heapSize - 1) / 2; p >= 0; p--)
                MaxHeapify(heapSize, p);
            for (int i =  heapSize- 1; i > 0; i--)
            {
                HighLight(0, Color.Orange);
                MoveButton(i,0);
                swap(i, 0);
                heapSize--;
                MaxHeapify(heapSize, 0);
            }
        }

        private void MaxHeapify(int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = index;
            
            //Đổi màu 2 giá trị được xét
            // 1. largest vs left
            HighLight(largest, Color.Green);
            if (left < heapSize)
            {
                HighLight(left, Color.Green);
                Thread.Sleep(sleep);
                if (arrValue[left] > arrValue[largest])
                {
                    HighLight(largest, Color.LightBlue);
                    largest = left;
                }
                else HighLight(left, Color.LightBlue);
            }
            
            // 2. largest vs right
            if (right < heapSize)
            {
                HighLight(right, Color.Green);
                Thread.Sleep(sleep);

                if (arrValue[right] > arrValue[largest])
                {
                    HighLight(largest, Color.LightBlue);
                    largest = right;
                }
                else HighLight(right, Color.LightBlue);
            }
            //
            if (largest != index)
            {
                MoveButton(largest, index);
                HighLight(largest, Color.LightBlue);
                HighLight(index, Color.LightBlue);
                swap(index, largest);
                MaxHeapify(heapSize, largest);
            }
            else
            {
                HighLight(largest, Color.LightBlue);
            }
        }

        private void ShellSort()
        {
            
        }
        #endregion
    }
}