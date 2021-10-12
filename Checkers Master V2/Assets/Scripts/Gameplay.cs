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

    public Checker.Team CurrentTeam { get; private set; } = Checker.Team.White;
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

        
    }

    public bool Raycast(Ray ray, out Vector3Int field)
    {
        field = Vector3Int.zero;

            if (plane.Raycast(ray, out float vector))
            {
                field = Gameplay.Instance.Grid.WorldToCell(ray.GetPoint(vector));
                return true;
            }
        return false;
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
        CheckersOnChessboard[checkerCell.x, checkerCell.z] = checker;
    }
    
    public Checker GetChecker(Vector3Int cell)
    {
        if(!IsCheckerPlaying(cell))
        {
            return null;
        }

        return CheckersOnChessboard[cell.x, cell.z];

    }
    public bool IsCheckerPlaying(Vector3Int cell)
    {
        return cell.x >= 0 && cell.x < Width && cell.z >= 0 && cell.z < Height;
    }

    public void MoveChecker(Checker checker, Vector3Int to)
    {
        CheckersOnChessboard[checker.CurrentCell.x, checker.CurrentCell.z] = null;
        CheckersOnChessboard[to.x, to.z] = checker;
        checker.CurrentCell = to;
        checker.transform.position = Gameplay.Instance.Grid.GetCellCenterWorld((new Vector3Int(to.x, 0, to.z)));
        Debug.Log(Gameplay.Instance.Grid.CellToWorld(new Vector3Int(to.x, 0, to.z)));


    }
    
    public void SwitchTeam()
    {
        CurrentTeam = CurrentTeam == Checker.Team.White ? Checker.Team.Black : Checker.Team.White;
    }
}
