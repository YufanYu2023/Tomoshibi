using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] List<Transform> waypoints;
    public int waypoinIndex = 0;

    Rigidbody2D myRigidBody;

    public Vector3 originalPosition;
    public Vector3 endPosition;
    public GameObject charger;
    //public player playerS;


    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (charger.GetComponent<Charger>().isCharging)
        {
            Move();
        }
        else
        {
            transform.position = originalPosition;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            waypoinIndex = 0;
        }
        //Debug.Log(waypoinIndex);
    }

    public void Move()
    {
        if (waypoinIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypoinIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypoinIndex++;
            }
        }
        else
        {         
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      collision.transform.SetParent(transform);  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
    }



}
