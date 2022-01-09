using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public bool onGoal;

    private GameObject[] walls;
    private GameObject[] boxes;
    private GameObject[] goals;

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
        if(goals == null)
        {
            goals = GameObject.FindGameObjectsWithTag("Goal");
            //Debug.Log("Goal added to goals");
            //if(goals == null){Debug.Log("No goal in goals");};
        }
    }

    public bool Move(Vector2 direction) {
        if (BoxBlocked(transform.position, direction)) {
            return false;
        }
        else {
            transform.Translate(direction);
            TransformWhenOnGoal();
            return true;
        }
    }

    private void TransformWhenOnGoal() {
        foreach (GameObject goal in goals) {
            if (transform.position.x == goal.transform.position.x && transform.position.y == goal.transform.position.y) {
                GetComponent<SpriteRenderer>().color = Color.red;
                onGoal = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        onGoal = false;
    }

    private bool BoxBlocked(Vector3 position, Vector2 direction) {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        /*newPos.ToString();
        Debug.Log(newPos);*/
        foreach (GameObject wall in walls) {
            Vector2 wallPos = new Vector2(wall.transform.position.x, wall.transform.position.y);
            if (wallPos == newPos) {
                return true;
            }
        }
        foreach (GameObject box in boxes) {
            Vector2 boxPos = new Vector2(box.transform.position.x, box.transform.position.y);
            if (boxPos == newPos) {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction)) {
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