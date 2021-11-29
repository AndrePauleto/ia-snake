using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public List<GameObject> snakes = new List<GameObject>();

    // Start is called before the first frame update
    public GameObject snakePrefab;
    void Start()
    {
        //GameObject.FindGameObjectWithTag("Player").gameObject.bodyObject


       //GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        //GameObject newSnake = Instantiate(snakePrefab,new Vector3(-5.0f,1.0f,0.0f), Quaternion.identity) as GameObject;
        //newSnake.tag = "Bot";
        //snakes.Add(newSnake);

        //newSnake = Instantiate(snakePrefab, new Vector3(5.0f, -1.0f, 0.0f), Quaternion.identity) as GameObject;
        //newSnake.tag = "Bot";
        //snakes.Add(newSnake);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
