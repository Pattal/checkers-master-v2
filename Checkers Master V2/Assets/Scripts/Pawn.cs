using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Checker
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override int FindPossibleCellsToMove(out Vector3Int[] cells, Vector3Int[] directions)
    {
        
        return base.FindPossibleCellsToMove(out cells, GetDirections());
    }

    protected override Vector3Int[] GetDirections()
    {

        Vector3Int[] possibleDirections = new Vector3Int[2];

        if(Side == Checker.Team.White)
        {
            possibleDirections[0] = CurrentCell + new Vector3Int(0, 0, 1) + Vector3Int.left;
            possibleDirections[1] = CurrentCell + new Vector3Int(0, 0, 1) + Vector3Int.right;
        }
        else
        {
            possibleDirections[0] = CurrentCell + new Vector3Int(0, 0, -1) + Vector3Int.left;
            possibleDirections[1] = CurrentCell + new Vector3Int(0, 0, -1) + Vector3Int.right;
        }
        return possibleDirections;
    }
}
