using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

    private GameObject[] walls;
    private GameObject[] boxes;  

    void Start()
    {
        if(walls == null) 
        {
            walls = GameObject.FindGameObjectsWithTag("Wall");
            //Debug.Log("Wall added to walls");
            //if(walls == null){Debug.Log("No wall in walls");};
        }
        if(boxes == null)
        {
            boxes = GameObject.FindGameObjectsWithTag("Box");
            //Debug.Log("Box added to boxes");
            //if(walls == null){Debug.Log("No box in boxes");};
        }
    }

    public bool Move(Vector2 direction) {
        //Debug.Log("Player is moving");
        if (Mathf.Abs(direction.x) < 0.5) {
            direction.x = 0;
        }
        else {
            direction.y = 0;
        }
        direction.Normalize();
        if (Blocked(transform.position, direction)) {
            //Debug.Log("Player blocked");
            return false;
        }
        else {
            transform.Translate(direction);
            return true;
        }
    }

    bool Blocked(Vector3 position, Vector2 direction) {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        /*newPos.ToString();
        Debug.Log(newPos);*/
        foreach (GameObject wall in walls) {
            Vector2 wallPos = new Vector2(wall.transform.position.x, wall.transform.position.y);
            /*wallPos.ToString();
            Debug.Log(wallPos);*/
            if (wallPos == newPos) {
                return true;
            }
        }
        foreach (GameObject box in boxes) {
            Vector2 boxPos = new Vector2(box.transform.position.x, box.transform.position.y);
            /*boxPos.ToString();
            Debug.Log(boxPos);*/
            if (boxPos == newPos) {
                Box bx = box.GetComponent<Box>();
                if(bx && bx.Move(direction)) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        return false;
    }
}
