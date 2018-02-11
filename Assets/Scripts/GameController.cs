using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {

    public static float CELL_SIZE = 1.0f;
    public static float MOVE_DURATION = 0.05f;

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
        block.transform.SetParent(Zero.transform,false);
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
            c.transform.SetParent(block.transform, true);
            c.transform.localPosition = new Vector3(x-0.5f, y+0.5f, -0.7f);
            var view = c.GetComponent<CellViewController>();
            view.CurrentMatID = field.block.color;
            blockCells[i] = c;
        }
    }

    private void updateField()
    {
        GameObject cell;

        for (int col = 0; col < field.columns; col++)
        {
            for (int row = 0; row < field.rows; row++)
            {
                if (field.grid[col, row] > -1)
                {
                    if (fieldCells[col, row] != null)
                    {
                        cell = fieldCells[col, row];
                        cell.GetComponent<CellViewController>().CurrentMatID = field.grid[col, row];
                    }
                    else
                    {
                        cell = Instantiate(CellPrefab);
                        cell.transform.localPosition = new Vector3((float)col - 0.5f +1, (float)row + 0.5f, -0.7f);
                        cell.GetComponent<CellViewController>().CurrentMatID = field.grid[col, row];
                        cell.transform.SetParent(Zero.transform, false);
                        fieldCells[col, row] = cell;
                    }
                }

                if (field.grid[col, row] < 0 && fieldCells[col, row] != null )
                {
                    DestroyImmediate(fieldCells[col, row]);
                    fieldCells[col, row] = null;
                }
            }
        }
    }

    void Update ()
    {
        tickValue += Time.fixedDeltaTime;

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
            
            block.transform.localPosition = new Vector3(block.transform.localPosition.x, (float)newRow, block.transform.localPosition.z);
            onDownTweenComplete();
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
        field.block.row = newRow;
        field.setBlock();
        field.isRedrawed = true;
    }

    private void blockReset()
    {
        field.block.reset();
    }

    public void moveHandler()
    {
        float newX = (float)field.block.column;
        float newY = (float)field.block.row;
        block.transform.localPosition = new Vector3(newX, newY, 0);
    }

    public void rotateHandler()
    {
        buildBlock();
    }

}
