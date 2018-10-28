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
        Column[] arrColumn;
        int[] arrValue;
        int numArr;
        Random rd = new Random();
        int WIDTH = 30;  //kích thước khối
        int GAP = 5;   //khoảng cách giữa các khối
        int speed = 14; //max speed = 4ms, min speed = 14ms
        int sleep = 420;  //thời gian dừng để quan sát các bước duyệt của thuật toán (sleep = 30 * speed)
        bool sorting = false;
        int sortSelected;   //Thuật toán sắp xếp muốn mô phỏng
        Column[] arrIndex;
        #endregion

        private void btnCreateArr_Click(object sender, EventArgs e)
        {

            if (sorting) return;
            numArr = (int)nudNumArr.Value;
            arrValue = new int[numArr];
            arrColumn = new Column[numArr];
            lblSortInfo.Text = "";
            pnlSimulator.Controls.Clear();

            for (int i = 0; i < numArr; i++)
            {
                int value = rd.Next(1, 150);
                Column col = new Column();
                col.Size = new Size(WIDTH, value + 20);
                col.Text = value + "";
                col.Location = new Point(
                    (pnlSimulator.Width - (WIDTH + GAP) * numArr) / 2 + (WIDTH + GAP) * i,
                    pnlSimulator.Height - WIDTH - value - 200);
                col.BackColor = Color.LightBlue;

                pnlSimulator.Controls.Add(col);

                arrColumn[i] = col;
                arrValue[i] = value;
            }
            btnSort.Enabled = true;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (!bgwSimuSort.IsBusy)
            {
                sorting = true;
                btnSort.Enabled = false;
                sortSelected = cboSortAlgo.SelectedIndex;
                bgwSimuSort.RunWorkerAsync();
            }
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
            if (pos < numArr && pos >= 0)
                arrColumn[pos].BackColor = color;
        }

        private void MoveCol(int p1, int p2, MoveType type)
        {
            int x;
            Status st = new Status();
            st.Pos1 = p1;
            st.Pos2 = p2;
            st.mType = type;

            if (st.mType == MoveType.LINE_TO_BOTTOM)
            {
                if (sortSelected == 8) arrIndex[p1] = arrColumn[p2]; //merge sort is running
                for (x = 0; x < (WIDTH + 140) / 5; x++)
                {
                    bgwSimuSort.ReportProgress(0, st);
                    Thread.Sleep(speed);
                }
            }
            if (st.mType == MoveType.LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT)
            {
                if (p1 == p2) return;
                st.mType = (p1 > p2) ? MoveType.LEFT_TO_RIGHT : MoveType.RIGHT_TO_LEFT;
                int nLoop = ((WIDTH + GAP) * Math.Abs(st.Pos1 - st.Pos2)) / 5;
                for (x = 0; x < nLoop; x++)
                {
                    bgwSimuSort.ReportProgress(0, st);
                    Thread.Sleep(speed);
                }
            }

            else if (st.mType == MoveType.BOTTOM_TO_LINE)
            {
                for (x = 0; x < (WIDTH + 140) / 5; x++)
                {
                    bgwSimuSort.ReportProgress(0, st);
                    Thread.Sleep(speed);
                }
            }
            else if (st.mType == MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT)
            {
                if (p1 == p2) return;
                int nLoop = ((WIDTH + GAP) * Math.Abs(st.Pos1 - st.Pos2)) / 5;
                for (x = 0; x < nLoop; x++)
                {
                    bgwSimuSort.ReportProgress(0, st);
                    Thread.Sleep(speed);
                }

                st.mType = MoveType.CHANGED;
                bgwSimuSort.ReportProgress(0, st);
                Thread.Sleep(speed);
            }
            else if (st.mType == MoveType.CHANGED)
            {
                bgwSimuSort.ReportProgress(0, st);
                Thread.Sleep(speed);
            }
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
                    BinaryInsertionSort();
                    break;
                case 3:
                    SelectionSort();
                    break;
                case 4:
                    BubbleSort();
                    break;
                case 5:
                    ShakerSort();
                    break;
                case 6:
                    HeapSort();
                    break;
                case 7:
                    QuickSort(0, numArr - 1);
                    break;
                case 8:
                    MergeSort(0, numArr - 1);
                    break;
                case 9:
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
                if (sortSelected == 8)    //merge sort is running
                {
                    arrColumn[st.Pos1] = arrIndex[st.Pos2];
                }
                else
                {
                    Column colTmp = arrColumn[st.Pos1];
                    arrColumn[st.Pos1] = arrColumn[st.Pos2];
                    arrColumn[st.Pos2] = colTmp;
                }
                return;
            }

            Column col1 = arrColumn[st.Pos1];
            Column col2 = arrColumn[st.Pos2];
            if (st.mType == MoveType.LINE_TO_BOTTOM)
            {
                col2.Top += 5;
            }
            else if (st.mType == MoveType.LEFT_TO_RIGHT)
            {
                col2.Left += 5;
            }
            else if (st.mType == MoveType.RIGHT_TO_LEFT)
            {
                col2.Left -= 5;
            }
            else if (st.mType == MoveType.BOTTOM_TO_LINE)
            {
                col2.Top -= 5;
            }
            else if (st.mType == MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT)
            {
                col1.Left = col1.Left - 5;
                col2.Left = col2.Left + 5;
            }
        }

        private void bgwSimuSort_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Đã ngừng mô phỏng!");
                return;
            }
            for (int i = 0; i < numArr; i++)
            {
                arrColumn[i].BackColor = Color.LightBlue;
            }
            sorting = false;
            MessageBox.Show("Đã sắp xếp xong");
        }
        #endregion

        #region Sort Algorithms

        private void InterchangeSort()
        {
            for (int i = 0; i < numArr - 1; i++)
            {
                HighLight(i, Color.Red);
                Thread.Sleep(sleep);

                for (int j = i + 1; j < numArr; j++)
                {
                    HighLight(j, Color.Green);
                    Thread.Sleep(sleep);

                    if (arrValue[i] > arrValue[j])
                    {
                        HighLight(i, Color.LightBlue);
                        HighLight(j, Color.Red);
                        Thread.Sleep(sleep);

                        MoveCol(j, i, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                        swap(i, j);
                    }
                    HighLight(j, Color.LightBlue);
                    Thread.Sleep(sleep);
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
                MoveCol(i, i, MoveType.LINE_TO_BOTTOM);
                Thread.Sleep(sleep);

                while (j >= 0 && arrValue[j] > x)
                {
                    HighLight(j, Color.Green);

                    Thread.Sleep(sleep);
                    arrValue[j + 1] = arrValue[j];
                    MoveCol(j + 1, j, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                    //sau khi move, index của 2 button đã thay đổi
                    HighLight(j + 1, Color.Orange);
                    Thread.Sleep(sleep);
                    j--;
                }

                MoveCol(j + 1, j + 1, MoveType.BOTTOM_TO_LINE);
                HighLight(i, Color.Orange);
                HighLight(j + 1, Color.Orange);
                Thread.Sleep(sleep);

                arrValue[j + 1] = x;
            }
        }

        private int BinarySearch(int item, int low, int high)
        {
            if (high <= low)
                return (item > arrValue[low]) ? (low + 1) : low;

            int mid = (low + high) / 2;

            if (item == arrValue[mid])
                return mid + 1;

            if (item > arrValue[mid])
                return BinarySearch(item, mid + 1, high);
            return BinarySearch(item, low, mid - 1);
        }

        private void BinaryInsertionSort()
        {
            int i, location, j, selected;
            for (i = 1; i < numArr; i++)
            {
                selected = arrValue[i];
                j = i - 1;
                HighLight(j, Color.Orange);
                HighLight(i, Color.Red);
                MoveCol(i, i, MoveType.LINE_TO_BOTTOM);
                Thread.Sleep(sleep);

                location = Math.Abs(Array.BinarySearch(arrValue, 0, i, selected) + 1);

                while (j >= location)
                {
                    HighLight(j, Color.Green);
                    Thread.Sleep(sleep);
                    MoveCol(j + 1, j, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                    HighLight(j + 1, Color.Orange);
                    Thread.Sleep(sleep);

                    arrValue[j + 1] = arrValue[j];
                    j--;
                }
                MoveCol(j + 1, j + 1, MoveType.BOTTOM_TO_LINE);
                HighLight(i, Color.Orange);
                HighLight(j + 1, Color.Orange);
                Thread.Sleep(sleep);

                arrValue[j + 1] = selected;
            }
        }

        private void SelectionSort()
        {
            for (int i = 0; i < numArr - 1; i++)
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

                MoveCol(minPos, i, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                swap(minPos, i);
                //change color to Orange
                HighLight(i, Color.Orange);
            }
        }

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
                        Thread.Sleep(sleep);

                        MoveCol(j, j - 1, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                        swap(j, j - 1);
                        swapped = true;

                    }

                    HighLight(j, Color.LightBlue);
                    HighLight(j - 1, Color.LightBlue);
                }
                HighLight(i, Color.Orange);
            }
        }

        private void ShakerSort()
        {
            bool swapped = true;
            int start = 0;
            int end = numArr;

            while (swapped == true)
            {
                swapped = false;

                for (int i = start; i < end - 1; i++)
                {
                    HighLight(i, Color.Green);
                    HighLight(i + 1, Color.Green);
                    Thread.Sleep(sleep);

                    if (arrValue[i] > arrValue[i + 1])
                    {
                        HighLight(i, Color.Orange);
                        HighLight(i + 1, Color.Orange);
                        Thread.Sleep(sleep);

                        MoveCol(i + 1, i, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                        swap(i, i + 1);
                        swapped = true;
                    }

                    HighLight(i, Color.LightBlue);
                    HighLight(i + 1, Color.LightBlue);
                }
                HighLight(end - 1, Color.Orange);
                if (swapped == false) break;

                swapped = false;
                end = end - 1;
                for (int i = end - 1; i > start; i--)
                {
                    HighLight(i, Color.Green);
                    HighLight(i - 1, Color.Green);
                    Thread.Sleep(sleep);

                    if (arrValue[i - 1] > arrValue[i])
                    {
                        HighLight(i - 1, Color.Orange);
                        HighLight(i, Color.Orange);
                        Thread.Sleep(sleep);

                        MoveCol(i, i - 1, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                        swap(i - 1, i);
                        swapped = true;
                    }
                    HighLight(i - 1, Color.LightBlue);
                    HighLight(i, Color.LightBlue);
                }
                HighLight(start, Color.Orange);

                start = start + 1;
            }
        }

        private void HeapSort()
        {
            //Build-Max-Heap
            int heapSize = numArr;
            for (int p = (heapSize - 1) / 2; p >= 0; p--)
                MaxHeapify(heapSize, p);
            for (int i = heapSize - 1; i > 0; i--)
            {
                HighLight(0, Color.Orange);
                MoveCol(i, 0, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
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
                MoveCol(largest, index, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
                HighLight(largest, Color.LightBlue);
                HighLight(index, Color.LightBlue);
                swap(index, largest);
                MaxHeapify(heapSize, largest);
            }
            else HighLight(largest, Color.LightBlue);
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
                    MoveCol(k, m, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
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
            MoveCol(m, i, MoveType.LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT);
            swap(i, m); // final step, swap pivot with a[m]
            for (int x = i; x <= j; x++)
            {
                HighLight(x, Color.LightBlue);
            }

            HighLight(m, Color.Orange);
            Thread.Sleep(sleep);
            return m; // return the index of pivot
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

        void Merge(int low, int mid, int high)
        {
            // subarray1 = a[low..mid], subarray2 = a[mid+1..high], both sorted
            int N = high - low + 1;
            int[] b = new int[N];
            arrIndex = new Column[numArr];
            int left = low;
            int leftArrCol = left;
            int right = mid + 1;
            int bIdx = 0;
            int k;
            for (k = low; k <= high; k++)
            {
                HighLight(k, Color.LightGreen);
            }
            Thread.Sleep(sleep);

            while (left <= mid && right <= high) // the merging
            {
                if (arrValue[left] <= arrValue[right])
                {
                    MoveCol(leftArrCol, left, MoveType.LINE_TO_BOTTOM);
                    MoveCol(leftArrCol, left, MoveType.LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT);
                    b[bIdx++] = arrValue[left++];
                }
                else
                {
                    MoveCol(leftArrCol, right, MoveType.LINE_TO_BOTTOM);
                    MoveCol(leftArrCol, right, MoveType.LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT);
                    b[bIdx++] = arrValue[right++];
                }
                leftArrCol++;
            }

            while (left <= mid)
            {
                MoveCol(leftArrCol, left, MoveType.LINE_TO_BOTTOM);
                MoveCol(leftArrCol, left, MoveType.LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT);
                leftArrCol++;
                b[bIdx++] = arrValue[left++]; // leftover, if any
            }
            while (right <= high)
            {
                MoveCol(leftArrCol, right, MoveType.LINE_TO_BOTTOM);
                MoveCol(leftArrCol, right, MoveType.LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT);
                leftArrCol++;
                b[bIdx++] = arrValue[right++]; // leftover, if any
            }

            for (k = 0; k < N; k++)
            {
                MoveCol(low + k, low + k, MoveType.CHANGED);
                arrValue[low + k] = b[k]; // copy back
            }

            for (k = low; k < leftArrCol; k++)
            {
                MoveCol(k, k, MoveType.BOTTOM_TO_LINE);
            }
            for (k = low; k <= high; k++)
            {
                HighLight(k, Color.LightBlue);
            }
        }

        private void ShellSort()
        {
            for (int gap = numArr / 2; gap > 0; gap /= 2)
            {
                lblSortInfo.Invoke((Action)delegate
                {
                    lblSortInfo.Text = "GAP = " + gap;
                });

                for (int i = gap; i < numArr; i++)
                {
                    int temp = arrValue[i];
                    int j;
                    HighLight(i, Color.Red);
                    if (i >= gap)
                    {
                        HighLight(i - gap, Color.Green);
                        Thread.Sleep(sleep);
                    }
                    int x = i;
                    int count = 0;
                    for (j = i; j >= gap && arrValue[j - gap] > temp; j -= gap)
                    {
                        count++;
                        if (count == 1) MoveCol(j - gap, x, MoveType.LINE_TO_BOTTOM);

                        HighLight(j - gap, Color.Green);
                        MoveCol(j, j - gap, MoveType.LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT);
                        Thread.Sleep(sleep);
                        HighLight(j - gap, Color.LightBlue);

                        if (count > 1)
                        {
                            MoveCol(j, j - gap, MoveType.CHANGED);
                        }
                        arrValue[j] = arrValue[j - gap];
                        x = j - gap;
                    }
                    if (x != i)
                    {
                        MoveCol(x, i, MoveType.LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT);
                        MoveCol(i, i, MoveType.BOTTOM_TO_LINE);
                        MoveCol(i, x, MoveType.CHANGED);
                    }
                    HighLight(j, Color.LightBlue);
                    HighLight(i - gap, Color.LightBlue);
                    arrValue[j] = temp;
                }
            }
        }

        #endregion
    }
}