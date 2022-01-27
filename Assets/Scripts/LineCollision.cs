using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollision : MonoBehaviour
{
    List<Vector2> colliderPoints = new List<Vector2>();
    PolygonCollider2D polygonCollider2D;

    public void GenerateCollider(LineRenderer lr, float xDisplace) //Generate the line collider
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        colliderPoints = CalculateColliderPoints(lr, xDisplace);
        polygonCollider2D.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

    private List<Vector2> CalculateColliderPoints(LineRenderer lr, float xDisplace)
    {
        // Get All positions on the line renderer
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);

        // Get the width of the line
        float width = lr.startWidth;
        
        //Assign every coordinate to a variable
        float x1 = positions[0].x;
        float x2 = positions[1].x;
        float y1 = positions[0].y;
        float y2 = positions[1].y;
        
        float m = (y2 - y1) / (x2 - x1);                                //m = (y2 - y1) / (x2 - x1)
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f)); // w / 2 * m / Square root of m ^ 2 + 1
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f)); // w / 2 * 1 / Square root of 1 + m ^ 2

        //Calculate the Offset from each point to the collision vertex
        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        //Get the full function equation
        float p = y1 - m * x1;

        //Calculate the vertex values after disklacing them
        if (x1 <= x2)     //If the line is growing
        {
            x1 += xDisplace;
            y1 = m * x1 + p;
            x2 -= xDisplace;
            y2 = m * x2 + p;
        }

        else if (x1 > x2) //If the line is decreasing
        {
            x1 -= xDisplace;
            y1 = m * x1 + p;
            x2 += xDisplace;
            y2 = m * x2 + p;
        }

        //Asign values to each vertex
        positions[0].x = x1;
        positions[0].y = y1;
        positions[1].x = x2;
        positions[1].y = y2;


        //Generate the Colliders
        List<Vector2> colliderPositions = new List<Vector2>
        {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colliderPositions;
    }
}
