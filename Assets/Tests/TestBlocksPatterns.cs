using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBlocksPatterns
{
    [Test]
    public void BlocksPatternsInitializationTest()
    {
        // Test that all block patterns are properly initialized
        Assert.IsNotNull(BlocksPatterns.IBlock);
        Assert.IsNotNull(BlocksPatterns.JBlock);
        Assert.IsNotNull(BlocksPatterns.LBlock);
        Assert.IsNotNull(BlocksPatterns.OBlock);
        Assert.IsNotNull(BlocksPatterns.SBlock);
        Assert.IsNotNull(BlocksPatterns.TBlock);
        Assert.IsNotNull(BlocksPatterns.ZBlock);
        
        // Check that all patterns have the correct length (8 elements: 4 x,y coordinate pairs)
        Assert.AreEqual(8, BlocksPatterns.IBlock.Length);
        Assert.AreEqual(8, BlocksPatterns.JBlock.Length);
        Assert.AreEqual(8, BlocksPatterns.LBlock.Length);
        Assert.AreEqual(8, BlocksPatterns.OBlock.Length);
        Assert.AreEqual(8, BlocksPatterns.SBlock.Length);
        Assert.AreEqual(8, BlocksPatterns.TBlock.Length);
        Assert.AreEqual(8, BlocksPatterns.ZBlock.Length);
    }

    [Test]
    public void BlocksPatternsContentTest()
    {
        // Test specific values for each block pattern
        // I-Block: { 1, 1, 2, 1, 3, 1, 4, 1 } - horizontal line
        int[] expectedIBlock = { 1, 1, 2, 1, 3, 1, 4, 1 };
        CollectionAssert.AreEqual(expectedIBlock, BlocksPatterns.IBlock);
        
        // J-Block: { 1, 1, 1, 2, 2, 2, 3, 2 } - L shape with tail on left
        int[] expectedJBlock = { 1, 1, 1, 2, 2, 2, 3, 2 };
        CollectionAssert.AreEqual(expectedJBlock, BlocksPatterns.JBlock);
        
        // L-Block: { 3, 1, 1, 2, 2, 2, 3, 2 } - L shape with tail on right
        int[] expectedLBlock = { 3, 1, 1, 2, 2, 2, 3, 2 };
        CollectionAssert.AreEqual(expectedLBlock, BlocksPatterns.LBlock);
        
        // O-Block: { 1, 1, 2, 1, 1, 2, 2, 2 } - 2x2 square
        int[] expectedOBlock = { 1, 1, 2, 1, 1, 2, 2, 2 };
        CollectionAssert.AreEqual(expectedOBlock, BlocksPatterns.OBlock);
        
        // S-Block: { 2, 1, 3, 1, 1, 2, 2, 2 } - S shape
        int[] expectedSBlock = { 2, 1, 3, 1, 1, 2, 2, 2 };
        CollectionAssert.AreEqual(expectedSBlock, BlocksPatterns.SBlock);
        
        // T-Block: { 2, 1, 1, 2, 2, 2, 3, 2 } - T shape
        int[] expectedTBlock = { 2, 1, 1, 2, 2, 2, 3, 2 };
        CollectionAssert.AreEqual(expectedTBlock, BlocksPatterns.TBlock);
        
        // Z-Block: { 1, 1, 2, 1, 2, 2, 3, 2 } - Z shape
        int[] expectedZBlock = { 1, 1, 2, 1, 2, 2, 3, 2 };
        CollectionAssert.AreEqual(expectedZBlock, BlocksPatterns.ZBlock);
    }

    [Test]
    public void BlocksPatternsListTest()
    {
        // Test that the patterns list contains all the expected patterns
        Assert.AreEqual(7, BlocksPatterns.patterns.Count);
        
        CollectionAssert.AreEqual(BlocksPatterns.IBlock, BlocksPatterns.patterns[0]);
        CollectionAssert.AreEqual(BlocksPatterns.JBlock, BlocksPatterns.patterns[1]);
        CollectionAssert.AreEqual(BlocksPatterns.LBlock, BlocksPatterns.patterns[2]);
        CollectionAssert.AreEqual(BlocksPatterns.OBlock, BlocksPatterns.patterns[3]);
        CollectionAssert.AreEqual(BlocksPatterns.SBlock, BlocksPatterns.patterns[4]);
        CollectionAssert.AreEqual(BlocksPatterns.TBlock, BlocksPatterns.patterns[5]);
        CollectionAssert.AreEqual(BlocksPatterns.ZBlock, BlocksPatterns.patterns[6]);
    }
}