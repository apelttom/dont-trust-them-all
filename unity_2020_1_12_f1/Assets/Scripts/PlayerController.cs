using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 originalPosition, targetPosition;
    public float timeToMove = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving){
            if (Input.GetKey(KeyCode.W))
                StartCoroutine(MovePlayer(Vector3.up));
            
            if (Input.GetKey(KeyCode.S))
                StartCoroutine(MovePlayer(Vector3.down));
            
            if (Input.GetKey(KeyCode.A))
                StartCoroutine(MovePlayer(Vector3.left));

            if (Input.GetKey(KeyCode.D))
                StartCoroutine(MovePlayer(Vector3.right));
        }
    }

    // taken from https://www.youtube.com/watch?v=AiZ4z4qKy44&ab_channel=Comp-3Interactive
    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        originalPosition = transform.position;
        targetPosition = originalPosition + direction;
        
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }
}
