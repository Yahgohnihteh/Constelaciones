using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int id;
    public bool isValid;

    [SerializeField]
    private List<int> validator = new List<int>();
    [SerializeField]
    public List<int> connectedStars = new List<int>();

    public void Evaluate()
    {
        
        // Check if a star is properly connected
        connectedStars.Sort();

        if (connectedStars.Count != validator.Count)
        {
            isValid = false;
            return;
        }

        for (int i = 0; i < connectedStars.Count; i++)
        {
            if (connectedStars[i] != validator[i])
            {
                isValid = false;
                return;
            }
        }

        isValid = true;
    }
}

