using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {

    public static float CELL_SIZE = 1.0f;
    public static float MOVE_DURATION = 0.0f;

    public GameObject CellPrefab;
    public GameObject BlockPrefab;
    public GameObject Field;
    public GameObject Zero;
    public Text scoreText;

    private Field field;
    private GameObject block;
    private GameObject[] blockCells = new GameObject[4];
    private GameObject[,] fieldCells = new GameObject[10,20];
    private bool isKeyUpPressed = false;
    private bool isKeyDownPressed = false;
    private bool isKeyLeftPressed = false;
    private bool isKeyRightPressed = false;
    private Tweener tween = null;
    private int newRow;
    private float tickValue = 0.0f;
    private float maxTick = 1.650f;
    private AudioSource audioMove;

    void Start ()
    {
        audioMove = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        field = new Field();
        block = Instantiate(BlockPrefab);
        block.transform.SetParent(Zero.transform,true);
        buildBlock();
    }

    private void buildBlock()
    {
        for (int i=0;i <= 3;i++) // Allways four cells.
        {
            if ( blockCells[i] != null )
            {
                blockCells[i].transform.parent = null;
                DestroyImmediate(blockCells[i]);
            }
            float x = (float)field.block.pattern[i*2];
            float y = -(float)field.block.pattern[i*2+1];
            GameObject c = Instantiate(CellPrefab);
            c.transform.SetParent(block.transform);
            c.transform.localPosition = new Vector3(x-0.5f, y+0.5f, -0.7f);
            var view = c.GetComponent<CellViewController>();
            view.CurrentMatID = field.block.color;
            blockCells[i] = c;
        }
    }

    private void updateField()
    {
        GameObject cell;
        CellViewController view;

        for (int m = 0; m < field.columns; m++)
        {
            for (int n = 0; n < field.rows; n++)
            {
                if (field.grid[m, n] > -1)
                {
                    if (fieldCells[m, n] != null)
                    {
                        cell = fieldCells[m, n];
                        view = cell.GetComponent<CellViewController>();
                        view.CurrentMatID = field.grid[m, n];
                    }
                    else
                    {
                        float x = (float)m;
                        float y = (float)n;
                        cell = Instantiate(CellPrefab);
                        cell.transform.localPosition = new Vector3(x + 0.5f, y + 0.5f+1.5f, -0.7f);
                        view = cell.GetComponent<CellViewController>();
                        view.CurrentMatID = field.grid[m, n];
                        cell.transform.SetParent(block.transform);
                        fieldCells[m, n] = cell;
                    }
                }

                if (field.grid[m, n] < 0 && fieldCells[m, n] != null )
                {
                    DestroyImmediate(fieldCells[m, n]);
                    fieldCells[m, n] = null;
                }
            }
        }
    }

    void Update ()
    {
        tickValue += Time.fixedDeltaTime;

        if (tween != null && !tween.IsComplete())
        {
            return;
        }

        if ( tickValue >= maxTick)
        {
            tickValue = 0;
            field.block.tick();
        }

        if ( field.block.isRedrawed )
        {
            scoreText.text = "Score: "+field.score;
            rotateHandler();
            moveHandler();
            field.block.isRedrawed = false;
        }

        if ( field.isRedrawed)
        {
            updateField();
            field.isRedrawed = false;
        }

        // Rotating.
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isKeyUpPressed)
        {
            isKeyUpPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && isKeyUpPressed)
        {
            isKeyUpPressed = false;
            field.block.rotate();
        }

        // Left press.
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isKeyLeftPressed)
        {
            isKeyLeftPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && isKeyLeftPressed)
        {
            isKeyLeftPressed = false;
            field.block.move(-1);
            audioMove.Play();
        }

        // Right press.
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isKeyRightPressed)
        {
            isKeyRightPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && isKeyRightPressed)
        {
            isKeyRightPressed = false;
            field.block.move(1);
            audioMove.Play();
        }

        // Down press.
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isKeyDownPressed)
        {
            isKeyDownPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && isKeyDownPressed)
        {
            isKeyDownPressed = false;
            newRow = field.blockDown();
            
            tween = block.transform.DOMoveY(newRow, 0.25f);
            tween.OnComplete(onDownTweenComplete);
        }

        if (Input.GetKeyDown(KeyCode.I)) field.block.setBlock(0);
        if (Input.GetKeyDown(KeyCode.J)) field.block.setBlock(1);
        if (Input.GetKeyDown(KeyCode.L)) field.block.setBlock(2);
        if (Input.GetKeyDown(KeyCode.O)) field.block.setBlock(3);
        if (Input.GetKeyDown(KeyCode.S)) field.block.setBlock(4);
        if (Input.GetKeyDown(KeyCode.T)) field.block.setBlock(5);
        if (Input.GetKeyDown(KeyCode.Z)) field.block.setBlock(6);
    }
   
    private void onDownTweenComplete()
    {
        tween = null;
        field.block.row = newRow;
        field.setBlock();
        field.isRedrawed = true;
    }

    private string printGrid()
    {
        string grid = "";
        for (int r = field.rows-1; r >= 0; r--)
        {
            for (int c = 0; c < field.columns; c++)
            {
                if (field.grid[c, r] >= 0) { grid += " " + field.grid[c, r]; }
                else { grid += " " + "-"; }
            }

            grid += "\n";
        }
        return grid;
    }

    private void blockReset()
    {
        field.block.reset();
    }

    public void moveHandler()
    {
        float newX = (float)field.block.column;
        float newY = (float)field.block.row+2+0.3f;
        block.transform.DOMoveX(newX, MOVE_DURATION);
        block.transform.DOMoveY(newY, MOVE_DURATION);
    }

    public void rotateHandler()
    {
        buildBlock();
    }

}
