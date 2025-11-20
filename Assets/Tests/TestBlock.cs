using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBlock
{
    [Test]
    public void BlockInitializationTest()
    {
        // Test that a block initializes correctly with default values
        Field field = new Field();
        Block block = new Block(field);
        
        Assert.IsNotNull(block.pattern);
        Assert.AreEqual(8, block.pattern.Length);
        Assert.AreEqual(0, block.color);
        Assert.AreEqual(4, block.column);
        Assert.AreEqual(0, block.row);
        Assert.AreEqual(0, block.rotation);
        Assert.IsNotNull(block.field);
    }

    [Test]
    public void BlockWidthHeightTest()
    {
        // Test width and height calculation for different block patterns
        Field field = new Field();
        Block block = new Block(field);
        
        // Set a simple pattern to test width/height calculation
        block.pattern = new int[] { 1, 1, 2, 1, 1, 2, 2, 2 }; // Simple 2x2 block pattern
        
        Assert.AreEqual(2, block.Width);
        Assert.AreEqual(2, block.Height);
    }

    [Test]
    public void BlockMovementTest()
    {
        Field field = new Field();
        Block block = new Block(field);
        
        int initialColumn = block.column;
        int initialRow = block.row;
        
        // Test horizontal movement
        block.move(1, 0); // Move right
        Assert.AreEqual(initialColumn + 1, block.column);
        Assert.AreEqual(initialRow, block.row);
        
        block.move(-1, 0); // Move left
        Assert.AreEqual(initialColumn, block.column);
        Assert.AreEqual(initialRow, block.row);
    }

    [Test]
    public void BlockSetBlockTest()
    {
        Field field = new Field();
        Block block = new Block(field);
        
        // Test setting a specific block type
        block.setBlock(0); // I-block
        
        Assert.AreEqual(0, block.color);
        CollectionAssert.AreEqual(BlocksPatterns.IBlock, block.pattern);
    }

    [Test]
    public void BlockResetTest()
    {
        Field field = new Field();
        Block block = new Block(field);
        
        // Modify block properties
        block.color = 5;
        block.column = 5;
        block.row = 10;
        
        // Reset the block
        block.reset();
        
        // The reset method sets a random block type, so we can only check that
        // the block gets a valid color ID (0-6) and is positioned at the top
        Assert.IsTrue(block.color >= 0 && block.color <= 6);
        Assert.IsTrue(block.row >= field.rows - 1); // Should be near top of field
    }

    [Test]
    public void BlockRotationTest()
    {
        Field field = new Field();
        Block block = new Block(field);
        
        // Set to I-block initially
        block.setBlock(0);
        int[] originalPattern = (int[])block.pattern.Clone();
        
        int initialRotation = block.rotation;
        block.rotate();
        
        // After rotation, the pattern should be different
        Assert.AreNotEqual(initialRotation, block.rotation);
        Assert.IsFalse(ArePatternsEqual(originalPattern, block.pattern));
    }

    [Test]
    public void BlockTickTest()
    {
        Field field = new Field();
        Block block = new Block(field);
        
        int initialRow = block.row;
        
        // Tick should move the block down by 1
        block.tick();
        
        Assert.AreEqual(initialRow - 1, block.row);
    }

    private bool ArePatternsEqual(int[] pattern1, int[] pattern2)
    {
        if (pattern1.Length != pattern2.Length) return false;
        for (int i = 0; i < pattern1.Length; i++)
        {
            if (pattern1[i] != pattern2[i]) return false;
        }
        return true;
    }
}