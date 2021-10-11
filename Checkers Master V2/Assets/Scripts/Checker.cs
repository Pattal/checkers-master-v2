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


    public virtual int FindPossibleCellsToMove(out Vector3Int[] cells, Vector3Int[] directions)
    {
        cells = new Vector3Int[4];
        int count = 0;
        
        
        foreach(Vector3Int cell in directions)
        {
            
            if(!Gameplay.Instance.IsCheckerPlaying(cell))
            {
                continue;
            }

            if (Gameplay.Instance.GetChecker(cell) == null)
            {
                cells[count] = cell;
                count++;
            }
            else if(Gameplay.Instance.GetChecker(cell) != null && Gameplay.Instance.GetChecker(cell).Side != Side)
            {
                Vector3Int possibleDirection = GetAttackDirections(cell);

                cells[count] = possibleDirection;
                count++;

            }
        }

        return count;
    }

    protected abstract Vector3Int[] GetDirections();
    protected Vector3Int GetAttackDirections(Vector3Int cell)
    {
        Vector3Int diff = cell - CurrentCell;

        
        return cell + diff;
    }

    private Vector3Int GetAbsoluteFromVector(Vector3Int vector)
    {
        Vector3Int abs = new Vector3Int(Mathf.Abs(vector.x), 0, Mathf.Abs(vector.z));
        return abs;
    }

}
