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
            int pivot = arr[high];
            int i = low - 1;
            int buf;
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;

                    buf = arr[i];
                    arr[i] = arr[j];
                    arr[j] = buf;
                }
            }

            buf = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = buf;
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
        private void HeapSort(int[] arr)
        {
            for (int i = arr.Length/2 - 1; i >= 0; i--)
            {
                heapify(arr, arr.Length, i);
            }

            int buf;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                buf = arr[0];
                arr[0] = arr[i];
                arr[i] = buf;

                heapify(arr, i,0);
            }
        }

        private void heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;
            
            if (l < n && arr[l] > arr[largest])
            {
                largest = l;
            }

            if (r < n && arr[r] > arr[largest])
            {
                largest = r;
            }

            int buf;
            if (largest != i)
            {
                buf = arr[i];
                arr[i] = arr[largest];
                arr[largest] = buf;

                heapify(arr, n, largest);
            }
        }

        private void CountingSort(int[] arr)
        {
            int[] count = new int[1001];
            int i,j;
            for (i = 0; i < count.Length;i++) count[i] = 0;

            for (i = 0; i < arr.Length; i++) count[arr[i]]++;

            int b = 0;
            for (i = 0; i < 1001;i++)
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
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            for (i = 0; i < arr.Length; i++)
                arr[i] = output[i];
            
        }

        static void Main(string[] args)
        {
            int[] input = new int[10000];
            Random random = new Random();
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = random.Next(1, 1000);
            }
            Sorting Sort = new Sorting();

            // Sort.WriteArray("initial array: ", input);
            DateTime start = DateTime.Now;
            Sort.QuickSort(input,0,input.Length - 1);
            DateTime end = DateTime.Now;
            // Sort.WriteArray("sorted array: ", input);
            Console.WriteLine("time: {0:F6} ", ((float)Math.Abs(end.Millisecond - start.Millisecond))/1000);
        }
    }
}
