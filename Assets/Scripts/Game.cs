using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    public int life;

    void Start()
    {
        life = 100;
    }
    
    public void loseLife(int dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
