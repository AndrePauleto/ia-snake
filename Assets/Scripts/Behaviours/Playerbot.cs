using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/Playerbot")]
public class Playerbot : AIBehaviour
{
    private bool debug = true;
    private float safeDistance = 10f;

    public override void Init(GameObject own, SnakeMovement ownMove)
    {
        base.Init(own, ownMove);
        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(timeChangeDir));
    }

    //seria interessante ter um controlador com o colisor que define o mundo pra poder gerar pontos dentro desse colisor

    public override void Execute()
    {
        //MoveForward();
        if (!debug)
        {
            MoveForward();
            return;
        }

        if (isSafe())
        {
            GameObject go = getBestOrb();
            if (go != null)
            {
                owner.transform.position = Vector2.MoveTowards(owner.transform.position, go.transform.position, ownerMovement.speed * Time.deltaTime);
            }
        }
    }

    private bool isSafe()
    {
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
        int indexSmallerBot = 0;
        float smallerDistance = safeDistance;

        for (int i = 1; i < bots.Length; i++)
        {
            float distanceBot = Vector3.Distance(bots[i].transform.position, this.owner.transform.position);

            if (distanceBot < smallerDistance)
            {
                indexSmallerBot = i;
                smallerDistance = distanceBot;
            }
        }

        if (smallerDistance < safeDistance)
        {
            safeDistance = 20f;
            Vector3 moveDir = bots[indexSmallerBot].transform.position - this.owner.transform.position;
            owner.transform.Translate(moveDir.normalized * -ownerMovement.speed * Time.deltaTime);
            return false;
        }

        safeDistance = 10f;
        return true;
    }

    private GameObject getBestOrb()
    {
        GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orb");

        if (orbs.Length == 0) {
            return null;
        }

        int indexSmallerOrb = 0;
        float smallerDistance = Mathf.Infinity;

        for (int i = 0; i < orbs.Length; i++)
        {
            orbs[i].GetComponent<SpriteRenderer>().color = Color.green;
            float distanceOrb = Vector3.Distance(orbs[i].transform.position, this.owner.transform.position);

            if (distanceOrb < smallerDistance)
            {
                indexSmallerOrb = i;
                smallerDistance = distanceOrb;
            }
        }

        orbs[indexSmallerOrb].GetComponent<SpriteRenderer>().color = Color.red;
        return orbs[indexSmallerOrb];
    }

    //ia basica, move, muda de direcao e move
    void MoveForward()
    {
        MouseRotationSnake();
        owner.transform.position = Vector2.MoveTowards(owner.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), ownerMovement.speed * Time.deltaTime);
    }

    void MouseRotationSnake()
    {

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - owner.transform.position;
        direction.z = 0.0f;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, rotation, ownerMovement.speed * Time.deltaTime);
    }

    IEnumerator UpdateDirEveryXSeconds(float x)
    {
        yield return new WaitForSeconds(x);
        ownerMovement.StopCoroutine(UpdateDirEveryXSeconds(x));
        randomPoint = new Vector3(
                Random.Range(
                    Random.Range(owner.transform.position.x - 10, owner.transform.position.x - 5),
                    Random.Range(owner.transform.position.x + 5, owner.transform.position.x + 10)
                ),
                Random.Range(
                    Random.Range(owner.transform.position.y - 10, owner.transform.position.y - 5),
                    Random.Range(owner.transform.position.y + 5, owner.transform.position.y + 10)
                ),
                0
            );
        direction = randomPoint - owner.transform.position;
        direction.z = 0.0f;

        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(x));
    }
}
