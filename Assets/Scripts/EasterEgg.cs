using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{

    public Image taco;
    public float RotationStrenght;

    // Update is called once per frame
    void Update()
    {
        Color newColor = new Color (Random.value, Random.value, Random.value );
        taco.color = newColor;
        taco.transform.Rotate(RotationStrenght, RotationStrenght, RotationStrenght);
    }
}