using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int damage;
    public float life;
    public Tilemap map;
    public Grid grid;
    public Game game;
    public GameObject self;

    private float speedLimit=10;
    private bool refreshTag = false;
    private Vector3 direction;
    
    void Start()
    {
        refreshDirection();
        if (speed > speedLimit)
        {
            speed = speedLimit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int location = map.WorldToCell(transform.position);
        if (isInCell())
        {
            if (refreshTag)
            {
                refreshDirection();
                refreshTag = false;
            }
        }
        else
        {
            refreshTag = true;
        }
            
        transform.position += speed * Time.deltaTime*direction;
        /*
        
        */

    }

    private void refreshDirection()
    {
        //Debug.Log("Refreshing Direction");

        Vector3Int location = map.WorldToCell(transform.position);
        if (isPath(location + Vector3Int.right)&&direction!=Vector3.left)
        {
            direction = Vector3.right;
        }
        else if (isPath(location + Vector3Int.left)&& direction!=Vector3.right)
        {
            direction = Vector3.left;
        }
        else if (isPath(location + Vector3Int.up) && direction!= Vector3.down)
        {
            direction = Vector3.up;
        }
        else if (isPath(location + Vector3Int.down)&& direction!=Vector3.up)
        {
            direction = Vector3.down;
        }
        else
        {
            Debug.Log("End Path");
            game.loseLife(damage);
            Object.Destroy(self);
        }
    }


    //sample code on how to get location and tile type by location of a gameobject
    public string getGridTypeFromWorldCoordinate()
    {
        //get location on tilemap from world coordinate
        Vector3Int location = map.WorldToCell(transform.position);
        //get grid type
        return map.GetTile<Tile>(location).sprite.name;
    }


    public bool isInCell()
    {
        Vector3 location = transform.position;
        location.z = 0;
        float distance = (location - grid.CellToWorld(map.WorldToCell(transform.position))-grid.cellSize/2).magnitude;
        Debug.Log(distance);
        return distance < (2-transform.localScale.x)*.7;
    }


    public bool isPath(Vector3Int location)
    {
        return map.HasTile(location + Vector3Int.right)&&map.GetTile<Tile>(location).sprite.name == "Path";
    }
}
