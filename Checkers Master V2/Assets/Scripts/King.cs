﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Checker
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

    protected override Vector3Int[] GetDirections()
    {
        throw new System.NotImplementedException();
    }
}
