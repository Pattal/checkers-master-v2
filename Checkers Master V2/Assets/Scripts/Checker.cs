using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Checker : MonoBehaviour
{
   public enum Team 
    { 
        White,
        Black
    }

    public Vector3Int CurrentCell;
    public Team Side;

    virtual protected void Start()
    {
        CurrentCell = Gameplay.Instance.GetCoodiantesOfCell(transform.position);
        Gameplay.Instance.SetPlayingChecker(this);
        // delete after pototype
        transform.position = Gameplay.Instance.Grid.GetCellCenterWorld(CurrentCell);
    }


    public virtual int FindPossibleCellsToMove(out Vector3Int[] cells)
    {
        cells = new Vector3Int[4];
        int count = 0;
        Vector3Int[] directions = { CurrentCell + new Vector3Int(0,0,1) + Vector3Int.left,
                                    CurrentCell + new Vector3Int(0,0,1) + Vector3Int.right,
                                    CurrentCell + new Vector3Int(0,0,-1) + Vector3Int.left,
                                    CurrentCell + new Vector3Int(0,0,-1) + Vector3Int.right
                                 };
        
        foreach(Vector3Int cell in directions)
        {
            
            if(!Gameplay.Instance.IsCheckerPlaying(cell))
            {
                continue;
            }

            Debug.Log(cell);
            if (Gameplay.Instance.GetChecker(cell) == null)
            {
                cells[count] = cell;
                count++;
            }
        }

        return count;
    }
}
