using UnityEngine;

public class Block
{
    public int[] pattern = new int[8]; // Rotated pattern.
    public int[] newPattern = new int[8]; // Pre-Rotated pattern.
    public int color = 0;
    public int column = 4;
    public int row = 0;
    public int rotation = 0;
    public Field field;
    public bool isRedrawed = false;

    private System.Random rand = new System.Random();

    public int Width
    {
        get
        {
            return Mathf.Max(pattern[0], pattern[2], pattern[4], pattern[6]);
        }
    }

    public int Height
    {
        get
        {
            return Mathf.Max(pattern[1], pattern[3], pattern[5], pattern[7]);
        }
    }

    public Block(Field field)
    {
        this.field = field;
        reset();
    }

    public void tick()
    {
        move(0,-1);
    }

    public void move(int deltaX=0, int deltaY=0)
    {
        if ((row + deltaY)-Height <= -1) field.checkFloor();

        if (deltaX != 0 && !field.checkIntersec(pattern, column + deltaX, row + deltaY)) // side move
        {
            setPosition(deltaX, deltaY);
            return;
        }

        if (deltaY != 0 && field.checkIntersec(pattern, column + deltaX, row + deltaY)) // down move
        {
            field.setBlock();
            return;
        }

        if (deltaY != 0)
        {
            setPosition(deltaX, deltaY);
        }
    }

    private void setPosition(int deltaX = 0, int deltaY = 0)
    {
        column += deltaX;
        row += deltaY;
        if (column + Width > field.columns - 1) column = field.columns - Width; // Move left.
        column = Mathf.Clamp(column, 0, field.columns - 1);
        isRedrawed = true;
    }

    public void reset()
    {
        setBlock(rand.Next(0,6));
    }

    public void rotate(bool clockWise=true)
    {
        int newRotation = rotation;
        if (clockWise) { newRotation++; } else { newRotation--; };
        if (newRotation > 3 || newRotation < 0 ) newRotation = 0;
        // rotate from source only
        newPattern = (int[])BlocksPatterns.patterns[color].Clone();

        switch (newRotation)
        {
            case 0:
                break;
            case 1:
                swapCoords();
                swapCols();
                break;
            case 2:
                swapRows();
                break;
            case 3:
                swapCoords();
                break;
            default:
                break;
        }

        if ( !field.checkIntersec(newPattern, column, row) ) 
        {
            rotation = newRotation;
            pattern = newPattern;
            // for bounds check
            if (column + Width > field.columns - 1) column = field.columns - Width; // Move left.
            column = Mathf.Clamp(column, 0, field.columns - 1); 

            isRedrawed = true;
        } 
    }

    public void setBlock(int id)
    {
        column = Random.Range(0,field.columns-1); 
        row = field.rows;
        color = id;
        pattern = (int[])BlocksPatterns.patterns[color].Clone();
        isRedrawed = true;
    }

    private void swapCoords()
    {
        for (int i = 0; i <= 3; i++)
            ArrayUtils.swap<int>(newPattern, i*2, i*2+1);
    }

    private void swapCols()
    {
        int w = Mathf.Max(newPattern[0], newPattern[2], newPattern[4], newPattern[6]);
        for (int i = 0; i <= 3; i++)
            newPattern[i * 2] = (w+1) - newPattern[i * 2];
    }

    private void swapRows()
    {
        int h = Mathf.Max(newPattern[1], newPattern[3], newPattern[5], newPattern[7]); 
        for (int i=0;i<=3;i++)
            newPattern[i * 2 + 1] = (h+1) - newPattern[i * 2 + 1];
    }

}

