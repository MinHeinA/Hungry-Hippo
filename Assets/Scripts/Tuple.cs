using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple <T>
{
    public Tuple() {
    }

    public Tuple(T first, int second) {
        this.Item1 = first;
        this.Item2 = second;
    }

    public T Item1 { get; set; }
    public int Item2 { get; set; }

}
