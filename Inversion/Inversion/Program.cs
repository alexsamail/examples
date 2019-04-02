using System;
using System.Collections.Generic;

namespace Inversion
{
    class Program
    {
        static long Count = 0; 

        static List<int> Merge(List<int> left, List<int> right)
        {
            int leftIndex = 0, rightIndex = 0;
            List<int> merged = new List<int>(left.Count + right.Count);
            for (int i = 0; i < left.Count + right.Count; i++)
            {
                if (leftIndex < left.Count && rightIndex < right.Count)
                    if (left[leftIndex] > right[rightIndex])
                    {
                        merged.Add(right[rightIndex++]);
                        Count += left.Count - leftIndex;
                    }
                    else merged.Add(left[leftIndex++]);
                else
                {
                    if (rightIndex < right.Count)
                        merged.Add(right[rightIndex++]);
                    else
                        merged.Add(left[leftIndex++]);
                }   
            }
            return merged;
        }

        static List<int> MergeSort(List<int> list)
        {
            if (list.Count == 1)
                return list;
            int midPoint = list.Count / 2;
            List<int> left = new List<int>(midPoint);
            List<int> right = new List<int>(list.Count - midPoint);

            for (int i = 0; i < midPoint; i++)
                left.Add(list[i]);

            for (int i = midPoint, j = 0; i < list.Count; i++, j++)
                right.Add(list[i]);

            return Merge(MergeSort(left), MergeSort(right));
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<int> list = new List<int>(n);
            for (int i = 0; i < n; i++)
                list.Add(int.Parse(Console.ReadLine()));
            MergeSort(list);
            Console.WriteLine(Count);
            Console.ReadKey();
        }
    }
}
