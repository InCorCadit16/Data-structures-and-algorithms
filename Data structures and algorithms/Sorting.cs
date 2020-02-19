using System;
using System.IO;
using System.Collections;

namespace Data_structures_and_algorithms
{
    class Sorting
    {
        private void WriteArray(string prefix, int[] arr)
        {
            Console.Write(prefix);
            foreach(int val in arr) {
                Console.Write(val + " ");
            }
            Console.WriteLine();
        }

        private void WriteArray(string prefix, int[] arr, int start, int end)
        {
            Console.Write(prefix);
           for (int i = start; i < end; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }


        private void BinarySearch(int n)
        {
            int[] range = new int[1024];
            for (int i = 0; i < range.Length; i++)
            {
                range[i] = i;
            }

            int min = range[0], max = range[range.Length - 1], center;
            while (min <= max)
            {
                Console.WriteLine("max = {0}, min = {1}", max, min);
                center = (min + max) / 2;
                if (range[center] < n)
                    min = center + 1;
                else if (range[center] > n)
                    max = center - 1;
                else 
                {
                    Console.WriteLine("{0} found", n);
                    return;
                }
            }

            Console.WriteLine("{0} not found", n);
        }

        // second cycle goes till length - i - 1;
        // second cycle replaced with while (if there where 0 swaps in inner cycle, then array is sorted)
        private void OptimizedBubbleSort(int[] arr)
        {
            int buf;
            int i = 0;
            bool needCycle = true;
            while (needCycle)
            {
                needCycle = false;
                for (int n = 0; n < arr.Length - i - 1; n++)
                {
                    if (arr[n] > arr[n + 1])
                    {
                        buf = arr[n];
                        arr[n] = arr[n + 1];
                        arr[n + 1] = buf;
                        needCycle = true;
                    }
                }
                i++;
            }
        }

        private void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int min = arr[i], min_i = i;
                for (int n = i + 1; n < arr.Length; n++)
                {
                    if (min > arr[n])
                    {
                        min = arr[n];
                        min_i = n;
                    }
                }

                arr[min_i] = arr[i];
                arr[i] = min;
            }
        }

        private void RecursiveSelectionSort(int[] arr, int i)
        {
            if (i == arr.Length) return;
            int min = arr[i], min_i = i;
            for (int n = i+1; n < arr.Length; n++)
            {
                if (min > arr[n])
                {
                    min = arr[n];
                    min_i = n;
                }
            }

            arr[min_i] = arr[i];
            arr[i] = min;
            RecursiveSelectionSort(arr, i + 1);
        }

        private void InsertionSort(int[] arr)
        {
            int key, j;
            for (int i = 0; i < arr.Length; i++)
            {
                key = arr[i];
                j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        private void RecursiveInsertionSort(int[] arr,int i) 
        {
            if (i == arr.Length) return;
            int key, j;
            key = arr[i];
            j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
            RecursiveInsertionSort(arr, i + 1);
        }

        private void BinaryInsertionSort(int[] arr)
        {
            int key, j, loc;
            for (int i = 1; i < arr.Length; i++)
            {
                key = arr[i];
                j = i - 1;

                loc = BinaryToInsert(arr, key, j,0);

                while(j >= loc)
                {
                    arr[j + 1] = arr[j--];
                }
                arr[j + 1] = key;
            }
        }

        private int BinaryToInsert(int[] arr,int val, int max, int min)
        {
            if (max <= min)
            {
                return ((val > arr[min]) ? (min + 1) : min);
            }

            int mid = (max + min) / 2;
            if (val == arr[mid]) return mid+1;
            if (val > arr[mid]) return BinaryToInsert(arr, val, max, mid + 1);
            return BinaryToInsert(arr, val, mid - 1, min);
        }

        private void TreeSort(int[] arr) 
        {
            Node head = new Node(arr[0]);
            for (int i = 1;i < arr.Length;i++)
            {
                head.Insert(arr[i]);
            }

            ArrayList result = new ArrayList();
            head.InorderTraversal(result);

            for (int i = 0; i < arr.Length; i++)
                arr[i] = (int) result.ToArray()[i];
        }

        private class Node
        {
            public int val;
            public Node left;
            public Node right;

            public Node(int val)
            {
                this.val = val;
            }

            public void Insert(int new_val)
            {
                if (new_val >= val)
                {
                    if (right == null)
                    {
                        right = new Node(new_val);
                        return;
                    } else
                        right.Insert(new_val);
                } else
                {
                    if (left == null)
                    {
                        left = new Node(new_val);
                        return;
                    } else
                        left.Insert(new_val);
                }
            }

            public void InorderTraversal(ArrayList list)
            {
                if (left != null)
                    left.InorderTraversal(list);

                list.Add(val);

                if (right != null)
                    right.InorderTraversal(list);

                
            }
        }

        // В массиве выбирается опорный элемент
        // Все элементы больше либо равные ему ставятся после него, а остальные до него
        // Затем это части сортируются рекурсивно по этому же принципу
        private void QuickSort(int[] arr,int low,int high)
        {
            if (low < high)
            {
                int p = partition(arr, low, high);

                QuickSort(arr, low, p - 1);
                QuickSort(arr, p + 1, high);
            }
        }

        private int partition(int[] arr, int low, int high)
        {
            var p = arr[new Random().Next(low, high)];
            Swap(ref arr[p], ref arr[high]);
            p = arr[high];
            var i = p - 1;
            for (int j = p; j < high; j++)
            {
                if (arr[j] < p)
                {
                    i++;
                    Swap(ref arr[i], ref arr[j]);
                }
            }

            Swap(ref arr[i + 1], ref arr[high]);
            return i + 1;
        }

        // Массив рекурсивно делится на 2 равные части и эти части сортируются отдельно 
        // Затем отсортированные части массива объединяются (они так же сортируются между собой)
        private void MergeSort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int mid = (l + r) / 2;
                MergeSort(arr, l, mid);

                MergeSort(arr, mid + 1, r);

                Merge(arr, l, mid, r);
            }
        }

        private void Merge(int[] arr, int l, int mid, int r)
        {
            int i, j, k, n1 = mid - l + 1, n2 = r - mid;
            int[] L = new int[n1];
            int[] R = new int[n2];
            
            for (i = 0; i < n1; i++) L[i] = arr[i + l];
            for (j = 0; j < n2; j++) R[j] = arr[j + mid + 1];

            i = 0; j = 0; k = l;
            while (i < n1 && j < n2) 
            {
               if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                } else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while(i < n1)
            {
                arr[k] = L[i];
                k++; i++;
            }

            while(j < n2)
            {
                arr[k] = R[j];
                k++; j++;
            }
        }

        // Элементы представляются в массиве как в хипе (дерево в котором у каждого узла
        // не больше 2 потомков, каждый из которых меньше самого узла).
        // Главное, это расставить все элементы в массиве по правилам хипа
        // (процедура идёт с самых нижних узлов к верхним)
        // Затем каждый элемент последовательно ставиться в верх хипа и процедура вызывается рекурсивно.
        public void HeapSort(int[] arr)
        {
            int HeapSize = arr.Length;
            BuilMaxHeap(arr, HeapSize);
            
            for (int i = arr.Length - 1; i > 0; i--)
            {
                Swap(ref arr[0], ref arr[i]);
                HeapSize--;
                MaxHeapify(arr, 0, HeapSize);
            }
        }

        private void BuilMaxHeap(int[] arr, int HeapSize)
        {
            for (int i = arr.Length / 2; i >= 0; i--)
                MaxHeapify(arr, i, HeapSize);
        }

        private void Swap(ref int a, ref int b)
        {
            a += b;
            b = a - b;
            a -= b;
        }

        private void MaxHeapify(int[] arr, int i, int HeapSize)
        {
            int left = 2*i + 1;
            int right = 2*i + 2;
            var largest = i;
            if (left < HeapSize && arr[i] < arr[left])
                largest = left;

            if (right < HeapSize && arr[largest] < arr[right])
                largest = right;

            if (largest != i)
            {
                Swap(ref arr[largest], ref arr[i]);
                MaxHeapify(arr, largest, HeapSize);
            }
        }


        private void CountingSort(int[] arr)
        {
            int[] count = new int[getMax(arr) + 1];
            int i,j;
            for (i = 0; i < count.Length;i++) count[i] = 0;

            for (i = 0; i < arr.Length; i++) count[arr[i]]++;

            int b = 0;
            for (i = 0; i < count.Length; i++)
            {
                for (j = 0; j < count[i]; j++)
                {
                    arr[b] = i;
                    b++;
                }
            }
        }

        // Сортировка по разрядам (сначала по единицам, потом по десяткам и так далее (сами разряды сортируются CountingSort))
        private void RadixSort(int[] arr)
        {
            int m = getMax(arr);

            for (int exp = 1; m/exp > 0; exp *= 10)
            {
                countSort(arr, exp);
            }
        }

        private int getMax(int[] arr)
        {
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max) max = arr[i];
            }
            return max;
        }

        private void countSort(int[] arr, int exp)
        {
            int[] output = new int[arr.Length];
            int i;
            int[] count = new int[10];

            for (i = 0; i < arr.Length; i++)
                count[(arr[i] / exp) % 10]++;

            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (i = arr.Length - 1; i >= 0; i--)
            {
                int pos = count[(arr[i] / exp) % 10];
                output[pos - 1] = arr[i];
                count[pos]--;
            }

            for (i = 0; i < arr.Length; i++)
                arr[i] = output[i];
            
        }

        static void Main(string[] args)
        {
            int[] input = new int[10];
            int[] input1 = new int[10];
            int[] input2 = new int[6];
            Random random = new Random();
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = random.Next(1, 3000);
                input1[i] = input[i];
                //input2[i] = input[i];
            }
            

            Sorting Sort = new Sorting();
            /*Sort.WriteArray("input : ", input);
            Sort.WriteArray("input1: ", input1);
            Sort.WriteArray("input2: ", input2);*/

            PrintTime(Sort.MergeSort, input, 0, input.Length - 1);

            /*Sort.WriteArray("input sorted : ", input);
            Sort.WriteArray("input1 sorted: ", input1);
            Sort.WriteArray("input2 sorted: ", input2);*/
        }

        static void PrintTime(Action<int[]> Function, int[] input)
        {
            DateTime start = DateTime.Now;
            Function(input);
            DateTime end = DateTime.Now;
            int SpacePlace = Function.Method.ToString().IndexOf(" ") + 1;
            int Length = Function.Method.ToString().IndexOf("(") - SpacePlace;
            string MethodName = Function.Method.ToString().Substring(SpacePlace, Length);
            Console.WriteLine("{0} time: {1:F6} ",  MethodName ,(Math.Abs(end.Ticks - start.Ticks) / 10000000f));
        }

        static void PrintTime(Action<int[], int> Function, int[] input, int i)
        {
            DateTime start = DateTime.Now;
            Function(input, i);
            DateTime end = DateTime.Now;
            int SpacePlace = Function.Method.ToString().IndexOf(" ") + 1;
            int Length = Function.Method.ToString().IndexOf("(") - SpacePlace;
            string MethodName = Function.Method.ToString().Substring(SpacePlace, Length);
            Console.WriteLine("{0} time: {1:F6} ", MethodName, (Math.Abs(end.Ticks - start.Ticks) / 10000000f));
        }

        static void PrintTime(Action<int[], int, int> Function, int[] input, int l, int r)
        {
            DateTime start = DateTime.Now;
            Function(input, l, r);
            DateTime end = DateTime.Now;
            int SpacePlace = Function.Method.ToString().IndexOf(" ") + 1;
            int Length = Function.Method.ToString().IndexOf("(") - SpacePlace;
            string MethodName = Function.Method.ToString().Substring(SpacePlace, Length);
            Console.WriteLine("{0} time: {1:F6} ", MethodName, (Math.Abs(end.Ticks - start.Ticks) / 10000000f));
        }
    }
}
