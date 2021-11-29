using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public int myOrder;
    public Transform head;
    public bool beginning = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < head.GetComponent<SnakeMovement>().bodyParts.Count; i++)
        {
            if (gameObject == head.GetComponent<SnakeMovement>().bodyParts[i].gameObject)
            {
                myOrder = i;
            }
        }
    }


    public void InitializeHead(Transform transform)
    {
        head = transform;
    }

    public void InitializeObject(Transform transform)
    {
        for (int i = 0; i < head.GetComponent<SnakeMovement>().bodyParts.Count; i++)
        {
            if (gameObject == head.GetComponent<SnakeMovement>().bodyParts[i].gameObject)
            {
                myOrder = i;
            }
        }
    }

    private Vector3 movementVelocity;
    [Range(0.0f, 1.0f)]
    public float overTime = 0.05f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;

        if (myOrder == 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.position, ref movementVelocity, overTime);
            transform.LookAt(new Vector3(head.transform.position.x, head.transform.position.y, -90.0f));
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.GetComponent<SnakeMovement>().bodyParts[myOrder-1].position, ref movementVelocity, overTime);
            transform.LookAt(new Vector3(head.transform.position.x, head.transform.position.y, -90.0f));
        }
    }
}
