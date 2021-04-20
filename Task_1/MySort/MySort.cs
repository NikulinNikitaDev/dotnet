using System;

namespace MySort
{
    public static class Program
    {
        private static void MySort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i;
                while (j > 0 && array[j - 1] > key)
                {
                    array[j] = array[j - 1];
                    j--;
                }

                array[j] = key;
            }
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Example 1:");
            int[] arr1 = {9, 8, 7, 6, 5, 4, 3, 2, 1};
            foreach (var element in arr1)
                Console.Write(element + " ");
            Console.WriteLine();
            MySort(arr1);
            foreach (var element in arr1)
                Console.Write(element + " ");
            Console.WriteLine();
            
            Console.WriteLine("Example 2:");
            int[] arr2 = {5, 4, 5, 4, 5, 4, 5, 4};
            foreach (var element in arr2)
                Console.Write(element + " ");
            Console.WriteLine();
            MySort(arr2);
            foreach (var element in arr2)
                Console.Write(element + " ");
            Console.WriteLine();
        }
    }
}