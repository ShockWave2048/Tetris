using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class ArrayUtils
{
    public static void swap<T>(T[] arr, int i, int j)
    {
        T temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    public static string join<T>(T[] arr)
    {
        string s = "";
        foreach (var i in arr) s += i.ToString();
        return s;
    }

    public static void removeLine<T>(T[,] arr, int line)
    {
        for (int x = 0; x < arr.GetLength(0); x++)
        {
            for (int y = line; y < arr.GetLength(1)-1; y++)
            {
                arr[x, y] = arr[x, y + 1];
            }
        }
    }
}

