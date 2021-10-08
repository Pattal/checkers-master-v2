using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;

    public int Height;
    public int Width;

    public Grid Grid { get; private set; }
    private Plane plane;

    private Checker.Team currentTeam = Checker.Team.White;
     public Checker[,] CheckersOnChessboard;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        Grid = GetComponent<Grid>();
        plane = new Plane(Vector3.up, Vector3.zero);
        CheckersOnChessboard = new Checker[Width, Height];
    }

    // Update is called once per frame
    private void Update()
    {
        MouseInput();


        Debug.Log(CheckersOnChessboard[7, 7]);
        
    }

    public void Raycast(Ray ray, out Vector3Int field)
    {
        field = Vector3Int.zero;

        if(Input.GetMouseButtonUp(0))
        {
            if (plane.Raycast(ray, out float vector))
            {
                field = Gameplay.Instance.Grid.WorldToCell(ray.GetPoint(vector));
            }
        }

        
    }

    public Vector3Int GetCoodiantesOfCell(Vector3 position)
    {
        return Grid.WorldToCell(position);
    }

    public void SetPlayingChecker(Checker checker)
    {
        var checkerCell = checker.CurrentCell;
        if (!IsCheckerPlaying(checkerCell))
        {
            return;
        }

        
        CheckersOnChessboard[checkerCell.z, checkerCell.x] = checker;
        Debug.Log(checkerCell.y+ " Yes " +  checkerCell.x);
    }
    
    private void MouseInput()
    {
        Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Vector3Int clickedCell);
        //Debug.Log(clickedCell);
    }
    public bool IsCheckerPlaying(Vector3Int cell)
    {
        return cell.x >= 0 && cell.x < Width && cell.y >= 0 && cell.y < Height;
    }
}
