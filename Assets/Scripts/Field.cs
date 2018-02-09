using System;
using UnityEngine;

class Field
{
    public int[,] grid;
    public int columns;
    public int rows;
    public int score = 0;
    public Block block;
    public bool isRedrawed = false;
    //public int[] removedLines = new int[4]();

    public Field()
    {
        reset();
    }

    private void reset()
    {
        columns = 10;
        rows = 20;
        grid = new int[columns,rows];
        block = new Block(this);

        for (int m = 0; m < columns; m++)
        {
            for (int n = 0; n < rows; n++)
            {
                grid[m, n] = -1;
            }
        }

        isRedrawed = true; 
    }

    public void checkFloor() 
    {
        if ((block.row - block.Height) <= 0 ) setBlock();
    }

    public void setBlock()
    {
        for (int i=0;i<=3;i++)
        {
            int c = block.column + block.pattern[i * 2]-1;
            int r = block.row - block.pattern[i * 2 + 1]; 
            if (r < 0 || r > rows-1) continue;
            if (c < 0 || c > columns-1) continue;
            grid[c, r] = block.color;
        }

        block.reset();
        linesCheck();
        isRedrawed = true;
    }

    private void linesCheck()
    {
        bool isCheck = true;

        while (isCheck)
        {
            isCheck = false;

            for (int r = 0; r < rows; r++)
            {
                int rowFill = 0;

                for (int c = 0; c < columns; c++)
                {
                    if (grid[c, r] > -1)
                    {
                        rowFill++;
                    }

                }

                if (rowFill >= columns)
                {
                    score++;
                    ArrayUtils.removeLine(grid, r);
                    isCheck = true;
                    break;
                }
            }
        }
    }

    public bool checkIntersec(int[] pat, int column=0, int row=0)
    {
        for (int i = 0; i <= 3; i++)
        {
            int c = column + pat[i * 2] - 1;
            int r = row - pat[i * 2 + 1];
            if (r < 0 || r >= rows-1) continue;
            if (c < 0 || c >= columns) continue;
            if (grid[c, r] >= 0) return true;
        }

        return false;
    }
    // TODO: move to Block class.
    // Return stop row.
    public int blockDown()
    {
        int row = block.row;

        int h = block.Height;

        while ( row > h-1 )
        {
            if (checkIntersec(block.pattern, block.column, row)) return row+1;
            row--;
        }
       
        return block.Height; 
    }
}

