using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    public List<int> lineConnections = new List<int>();

    //Method which adds a star's id to the line list
    public void AddStarId(Star selectedStar)
    {        
        lineConnections.Add(selectedStar.id);
        lineConnections.Sort();
    }
}
