using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


public class Car_AI3 : Car_AI
{
    protected override void Start()
    {
        base.Start();
        carSpeed = 7f; // misalnya beda speed
    }

    protected override void Move()
    {
        base.Move();
        // Bisa tambah logika unik di sini
    }
}