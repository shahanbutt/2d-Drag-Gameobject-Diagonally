// Email me for enquires shahanbutt0@gmail.com
// Please follow this link for any confusion
// https://www.youtube.com/watch?v=l-_DbsFjz_s&fbclid=IwAR1k5X5nSetjrBy23GDRii0ryigYXpLz72ui1N-SrTbkqY3LO6feG0BWFsk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject_Mouse : MonoBehaviour
{
    public bool moveToStartPos = false;
    public Vector3 startPos; // minimum limit for movoment , aslo the start postion
    public Vector3 endPos; // maximum position for movemnet
    Vector3 mousePos; // mouse position for moving gameobject
    float distance; // distance between start and end position
    Vector3 directionVectorFromStartToEnd;

    private void Awake()
    {
        if (moveToStartPos)
            transform.localPosition = startPos; // move gameobject to start poistion
        distance = Vector3.Distance(startPos, endPos); // caluclate distance between start and end poisition        
        directionVectorFromStartToEnd = (endPos - startPos).normalized; // get direction vector from start and end vector
    }

    // only move object when object is draged by mouse
    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)); // get mouse position with respect to screen to world point
        
        transform.position = PointOnALinePerpendicularToAPoint(startPos, endPos, mousePos);
    }

    // this function will get a vector position in between start and end position that will be perpendicular to the mouse position
    // this vector position will change according to the mouse position
    public Vector3 PointOnALinePerpendicularToAPoint(Vector3 pointAOnLine, Vector3 pointBOnLine, Vector3 pointInSpace)
    {        
        // finding lambda value accroding to direction from start and end position with respect to mouse position
        float lambda = FindLambdaForPointOnLine(pointAOnLine, pointInSpace, directionVectorFromStartToEnd);

        // limit lambda value to min = 0 and max being the distance between the start and end position
        if (lambda < 0)
        {
            lambda = 0;
        }
        else if (lambda > distance)
        {
            lambda = distance;
        }

        float xOfPointXOnLine = pointAOnLine.x + lambda * directionVectorFromStartToEnd.x;
        float yOfPointXOnLine = pointAOnLine.y + lambda * directionVectorFromStartToEnd.y;
        float zOfPointXOnLine = pointAOnLine.z + lambda * directionVectorFromStartToEnd.z;
        Vector3 pointXOnLine = new Vector3(xOfPointXOnLine, yOfPointXOnLine, zOfPointXOnLine);

        return pointXOnLine;
    }

    public float FindLambdaForPointOnLine(Vector3 pointA, Vector3 pointInSpace, Vector3 directionVector)
    {
        float lambda = ((directionVector.x * pointInSpace.x) + (directionVector.y * pointInSpace.y) + (directionVector.z * pointInSpace.z)
            - (pointA.x * directionVector.x) - (pointA.y * directionVector.y) - (pointA.z * directionVector.z)) / 
            ((directionVector.x * directionVector.x) + (directionVector.y * directionVector.y) + (directionVector.z * directionVector.z));

        return lambda;
    }

}