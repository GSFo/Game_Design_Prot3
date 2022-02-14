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
    private Tilemap map;
    private Grid grid;
    private Game game;
    public GameObject self;

    private float speedLimit=10;
    private Vector3Int lastCell;
    private Vector3 direction;
    
    void Start()
    {
        speed = 10f;
        damage = 1;
        life = 100;
        map = GameObject.Find("Tilemap").GetComponent<Tilemap>();;
        grid = GameObject.Find("WorldMap").GetComponent<Grid>();;
        game = GameObject.Find("Game").GetComponent<Game>();;
        lastCell = map.WorldToCell(transform.position);

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
        if (isInNewCell())
        {
            lastCell = location;
            refreshDirection();            
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


    public bool isInNewCell()
    {
        Vector3Int currentCell = map.WorldToCell(transform.position);
        //Debug.Log(grid.CellToWorld(map.WorldToCell(transform.position)));
        if (currentCell != lastCell)
        {
            Vector3 distance = transform.position - grid.CellToWorld(currentCell) - grid.cellSize/2;
            
            // Debug.Log("current location" + transform.position.ToString());
            // Debug.Log("current cell location" + grid.CellToWorld(currentCell).ToString());
            // Debug.Log(distance);
            
            return Mathf.Abs(distance.x )< Mathf.Abs(1 - transform.localScale.x)*.5 && Mathf.Abs(distance.y) < Mathf.Abs(1- transform.localScale.y)*.5;
        }
        return false;
    }


    public bool isPath(Vector3Int location)
    {
        return map.HasTile(location + Vector3Int.right)&&map.GetTile<Tile>(location).sprite.name == "Path";
    }
}