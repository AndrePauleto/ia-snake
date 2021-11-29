using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            currentRotation += rotationSensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentRotation -= rotationSensitivity * Time.deltaTime;
        }
    }

    public float speed = 1.5f;
    public float currentRotation = 0.0f;
    public float rotationSensitivity = 300.0f;   



    void FixedUpdate()
    {
        MoveForward();
        Rotation();
        CameraFollow();
    }

    void MoveForward()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void Rotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, currentRotation));
    }

    [Range(0.0f, 1.0f)]
    public float smoothTime = 0.05f;

    void CameraFollow()
    {
       
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform;
        Vector3 cameraVelocity = Vector3.zero;
        camera.position = Vector3.SmoothDamp(camera.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), ref cameraVelocity, smoothTime);
    }

    public Transform bodyObject;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("entra aqui");
        //collision.gameObject.name
        if (other.transform.tag == "Orb")
        {
           
            Destroy(other.gameObject);

            if (bodyParts.Count == 0)
            {
                Vector3 currentPos = transform.position;
                Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform;
                bodyParts.Add(newBodyPart);
            }
            else 
            {
                Vector3 currentPos = bodyParts[bodyParts.Count - 1].position;
                Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform;
                bodyParts.Add(newBodyPart);
            }
        }
    }
}
