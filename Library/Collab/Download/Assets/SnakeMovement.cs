using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();

    public Transform head;

    // Start is called before the first frame update
    void Start()
    {
        head = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Player")
        {
            MouseRotationSnake();
        }
    }


    private Vector3 direction;
    void MouseRotationSnake()
    {

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0.0f;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    public float speed = 1.5f;
    public float currentRotation = 0.0f;
    public float rotationSensitivity = 300.0f;

    void FixedUpdate()
    {
        if (tag == "Player")
        {
            //Debug.Log(tag);
            MoveForwardMouse();
            CameraFollow();
            SpawnOrbManager();
        }
        else
        {
            MoveForward();
            ChangeDirection();
            SpawnOrbManager();
        }
    }


    void MoveForwardMouse()
    {
        //transform.position += transform.up * speed * Time.deltaTime;
        //transform.position += transform.forward * speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), speed * Time.deltaTime);
    }

    void MoveForward()
    {
        //transform.position += transform.up * speed * Time.deltaTime;
        //transform.position += transform.forward * speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, randomPoint, speed * Time.deltaTime);
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
    //void OnCollisionEnter2D(Collision2D other)
    void OnTriggerEnter2D(Collider2D other)
    {

        //collision.gameObject.name
        if (other.transform.tag == "Orb")
        {

            Destroy(other.gameObject);

            if (bodyParts.Count == 0)
            {
                Vector3 currentPos = transform.position;
                
                Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform;
                //bodyObject.GetComponent<SnakeBody>().InitializeObject(head);
                bodyParts.Add(newBodyPart.transform);
            }
            else
            {
                Vector3 currentPos = bodyParts[bodyParts.Count - 1].position;
               
                Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform;
                //bodyObject.GetComponent<SnakeBody>().beginning = false;
                //bodyObject.GetComponent<SnakeBody>().InitializeObject(head);
                
                this.bodyParts.Add(newBodyPart.transform);

                //var MyInstance = Instantiate(MyPrefab, Vector3.zero, Quaternion.identity);

                //MyScript.test = 1;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("entra aqui - COLLISION");
    }

    //Gerenciamento do spawn dos orbs 
    public float spawnOrbEveryXSeconds = 1;
    public GameObject orbPrefab;
    void SpawnOrbManager()
    {
        StartCoroutine("CallEveryFewSeconds", spawnOrbEveryXSeconds);
    }
    IEnumerator CallEveryFewSeconds(float x)
    {
        yield return new WaitForSeconds(x);
        StopCoroutine("CallEveryFewSeconds");
        Vector3 randomNewOrbPosition = new Vector3(
                Random.Range(
                    Random.Range(transform.position.x - 10, transform.position.x - 5),
                    Random.Range(transform.position.x + 5, transform.position.x + 10)
                ),
                Random.Range(
                    Random.Range(transform.position.y - 10, transform.position.y - 5),
                    Random.Range(transform.position.y + 5, transform.position.y + 10)
                ),
                0
            );
        GameObject newOrb = Instantiate(orbPrefab, randomNewOrbPosition, Quaternion.identity) as GameObject;
        GameObject orbParent = GameObject.Find("Orbs");
        //newOrb.transform.parent = orbParent.transform;
      
    }


    //IA mais burra de todas 
    public float changeDirEveryXSeconds = 1;
    Vector3 randomPoint;
    void ChangeDirection()
    {
        StartCoroutine("UpdateDirEveryXSeconds", changeDirEveryXSeconds);
    }
    IEnumerator UpdateDirEveryXSeconds(float x)
    {
        yield return new WaitForSeconds(x);
        StopCoroutine("UpdateDirEveryXSeconds");
        randomPoint = new Vector3(
                Random.Range(
                    Random.Range(transform.position.x - 10, transform.position.x - 5),
                    Random.Range(transform.position.x + 5, transform.position.x + 10)
                ),
                Random.Range(
                    Random.Range(transform.position.y - 10, transform.position.y - 5),
                    Random.Range(transform.position.y + 5, transform.position.y + 10)
                ),
                0
            );
        direction = randomPoint - transform.position;
        direction.z = 0.0f;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }


}
