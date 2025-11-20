using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestField
{
    [Test]
    public void FieldInitializationTest()
    {
        // Test that a field initializes correctly with default values
        Field field = new Field();
        
        Assert.AreEqual(10, field.columns);
        Assert.AreEqual(20, field.rows);
        Assert.IsNotNull(field.grid);
        Assert.AreEqual(0, field.score);
        Assert.IsNotNull(field.block);
        Assert.IsTrue(field.isRedrawed);
    }

    [Test]
    public void FieldGridInitializationTest()
    {
        // Test that the grid is properly initialized with -1 values
        Field field = new Field();
        
        for (int col = 0; col < field.columns; col++)
        {
            for (int row = 0; row < field.rows; row++)
            {
                Assert.AreEqual(-1, field.grid[col, row]);
            }
        }
    }

    [Test]
    public void FieldSetBlockTest()
    {
        Field field = new Field();
        
        // Modify the current block's position and pattern
        field.block.column = 0;
        field.block.row = 3;
        field.block.pattern = new int[] { 1, 1, 2, 1, 1, 2, 2, 2 }; // Simple 2x2 pattern
        field.block.color = 5;
        
        int initialScore = field.score;
        
        // Set the block to the field
        field.setBlock();
        
        // Check that the block cells were placed in the grid
        Assert.AreEqual(5, field.grid[0, 2]); // block.column + pattern[0] - 1, block.row - pattern[1]
        Assert.AreEqual(5, field.grid[1, 2]); // block.column + pattern[2] - 1, block.row - pattern[3]
        Assert.AreEqual(5, field.grid[0, 1]); // block.column + pattern[4] - 1, block.row - pattern[5]
        Assert.AreEqual(5, field.grid[1, 1]); // block.column + pattern[6] - 1, block.row - pattern[7]
        
        // Check that the block was reset
        Assert.IsTrue(field.block.color >= 0 && field.block.color <= 6);
        
        // Check that redraw flag was set
        Assert.IsTrue(field.isRedrawed);
    }

    [Test]
    public void FieldCheckIntersectionTest()
    {
        Field field = new Field();
        
        // Test with empty field - should not intersect
        int[] pattern = { 1, 1, 2, 1, 1, 2, 2, 2 };
        bool intersects = field.checkIntersec(pattern, 0, 5);
        Assert.IsFalse(intersects);
        
        // Place a block in the grid
        field.grid[0, 4] = 3; // Place a block at position (0, 4)
        
        // Now test intersection - should intersect at position where there's a block
        intersects = field.checkIntersec(pattern, 0, 5); // This would place a cell at (0, 4)
        Assert.IsTrue(intersects);
    }

    [Test]
    public void FieldLinesCheckTest()
    {
        Field field = new Field();
        
        int initialScore = field.score;
        
        // Fill the bottom row to test line clearing
        for (int col = 0; col < field.columns; col++)
        {
            field.grid[col, 0] = col; // Fill bottom row
        }
        
        // Run line check
        field.linesCheck();
        
        // Score should have increased
        Assert.Greater(field.score, initialScore);
        
        // The filled row should now be empty (all -1 values)
        for (int col = 0; col < field.columns; col++)
        {
            Assert.AreEqual(-1, field.grid[col, 0]);
        }
    }

    [Test]
    public void FieldCheckFloorTest()
    {
        Field field = new Field();
        
        // Set up a block that touches the floor
        field.block.row = 1;
        field.block.pattern = new int[] { 1, 1, 2, 1, 1, 2, 2, 2 }; // Height of 2
        // With row=1 and height=2, (row - Height) = -1, which is <= 0, so floor is hit
        
        // Create a mock block that will call setBlock when floor is hit
        int initialScore = field.score;
        
        // This should trigger setBlock which increases score after line check
        field.checkFloor();
        
        // The block should have been set, which resets the block and may run line checks
        Assert.IsTrue(field.block.color >= 0 && field.block.color <= 6);
    }
}