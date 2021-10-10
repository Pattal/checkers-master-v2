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

    public override int FindPossibleCellsToMove(out Vector3Int[] cells)
    {
        return base.FindPossibleCellsToMove(out cells);
    }
}
