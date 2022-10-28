using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool[,] blocks = new bool[4, 4] {
        { true, true, true, true },
        { true, true, true, true },
        { true, true, true, true },
        { true, true, true, true },
    };
    public bool[,] I = new bool[4, 4] {
        { false, false, false, false },
        { true, true, true, true},
        { false, false, false, false },
        { false, false, false, false },
    };
    public bool[,] O = new bool[4, 4] {
        { false, true, true, false },
        { false, true, true, false },
        { false, false, false, false },
        { false, false, false, false },
    };

    public int Column;
    public int Row;
    public bool[,] Shape;
}
