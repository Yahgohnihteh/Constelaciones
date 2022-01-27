/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;

    void Update()
    {
        // Detectar touch
        Touch touch = Input.GetTouch(0);
        Vector2 touchOrigin = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(touchOrigin, touch.position);
            if (hit.collider && hit.collider.tag == "Star")
            {
                sp.color = Color.red;
            }
            else
            {
                sp.color = Color.white;
            }
        }
    }
}
*/
