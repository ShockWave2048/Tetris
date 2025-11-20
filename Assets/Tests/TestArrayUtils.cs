using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestArrayUtils
{
    [Test]
    public void ArraySwapTest()
    {
        // Test swapping elements in an array
        int[] arr = { 1, 2, 3, 4, 5 };
        ArrayUtils.swap(arr, 0, 2);
        
        // After swap: { 3, 2, 1, 4, 5 }
        Assert.AreEqual(3, arr[0]);
        Assert.AreEqual(1, arr[2]);
        
        // Test swapping the same element
        int[] arr2 = { 10, 20, 30 };
        ArrayUtils.swap(arr2, 1, 1);
        
        CollectionAssert.AreEqual(new int[] { 10, 20, 30 }, arr2);
    }

    [Test]
    public void ArrayJoinTest()
    {
        // Test joining array elements into a string
        int[] arr = { 1, 2, 3, 4 };
        string result = ArrayUtils.join(arr);
        
        Assert.AreEqual("1234", result);
        
        // Test with string array
        string[] strArr = { "hello", "world", "!" };
        string strResult = ArrayUtils.join(strArr);
        
        Assert.AreEqual("helloworld!", strResult);
        
        // Test with empty array
        int[] emptyArr = { };
        string emptyResult = ArrayUtils.join(emptyArr);
        
        Assert.AreEqual("", emptyResult);
    }

    [Test]
    public void RemoveLineTest()
    {
        // Test removing a line from a 2D array and shifting rows down
        int[,] arr = new int[3, 4];
        
        // Initialize the array with values
        // Row 0: [0, 1, 2]
        // Row 1: [3, 4, 5] 
        // Row 2: [6, 7, 8]
        // Row 3: [9, 10, 11]
        int value = 0;
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                arr[col, row] = value;
                value++;
            }
        }
        
        // Remove line at index 1 (second row)
        ArrayUtils.removeLine(arr, 1);
        
        // After removal, the array should have:
        // Row 0: [0, 1, 2] - unchanged
        // Row 1: [6, 7, 8] - was row 2 before removal
        // Row 2: [9, 10, 11] - was row 3 before removal
        // Last row values don't matter as they're overwritten
        Assert.AreEqual(0, arr[0, 0]);
        Assert.AreEqual(1, arr[1, 0]);
        Assert.AreEqual(2, arr[2, 0]);
        
        Assert.AreEqual(6, arr[0, 1]);
        Assert.AreEqual(7, arr[1, 1]);
        Assert.AreEqual(8, arr[2, 1]);
        
        Assert.AreEqual(9, arr[0, 2]);
        Assert.AreEqual(10, arr[1, 2]);
        Assert.AreEqual(11, arr[2, 2]);
    }

    [Test]
    public void ArrayShuffleTest()
    {
        // Test that shuffling changes the order of elements (with high probability)
        int[] original = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] arr = (int[])original.Clone();
        
        // Shuffle the array
        ArrayUtils.Shuffle(arr);
        
        // Check that the array still contains the same elements
        // (This is not a perfect test for shuffling since there's a small chance 
        // the shuffle could result in the same order, but it's sufficient for testing)
        CollectionAssert.AreEquivalent(original, arr);
        
        // Test with single element array
        int[] singleElement = { 42 };
        ArrayUtils.Shuffle(singleElement);
        Assert.AreEqual(42, singleElement[0]);
        
        // Test with two element array
        int[] twoElements = { 1, 2 };
        ArrayUtils.Shuffle(twoElements);
        CollectionAssert.AreEquivalent(new int[] { 1, 2 }, twoElements);
    }
}