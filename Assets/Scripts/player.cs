using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    //public GameObject storage;
    public GameObject movingPlatform;

    Rigidbody2D myRigidBody;
    Collider2D myCollider;
    Color transparent = new Color32(225, 225, 225, 50);
    Spawner spawner;
    Animator myAnimator;

    public List<Vector3> positons;

    public bool isRewinding = false;
    public bool isInControl = false;
    public bool hasControled = false;
    public bool hasRewind = false;
    public float power = 1f;
    //int positionNum = 0;
    Vector3 originalPosition;

    public List<float> controlThrows;

    [SerializeField] float powerLostSpeed = 0.005f;



    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        spawner = FindObjectOfType<Spawner>();
        //positons = new List<Vector3>();
        controlThrows = new List<float>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        originalPosition = transform.position;
        isInControl = false;
    }
    // Update is called once per frame
    void Update()
    {
        CheckLayers();
        KeyPressed();
        FlipSprite();
    }

    private void FlipSprite()
    {
        bool playerRuning = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerRuning)
        {
            transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void KeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Return) &&hasControled)
        {
            myAnimator.SetBool("isCharging", false);
            StartRewind();
            transform.position = originalPosition;
            isInControl = false;
            hasRewind = true;
            power = 1;
            transform.SetParent(null);
            //storage.transform.localScale = new Vector3(storage.transform.localScale.x, power, storage.transform.localScale.z);
            GetComponent<SpriteRenderer>().color = transparent;
           // storage.GetComponent<SpriteRenderer>().color = transparent;
        }
        if (!isInControl&&!hasControled)
        {
            GetComponent<SpriteRenderer>().color = transparent;
           // storage.GetComponent<SpriteRenderer>().color = transparent;
        }
        if(isInControl&&!hasRewind)
        {
            GetComponent<SpriteRenderer>().color = new Color32(225, 225, 225, 255);
           // storage.GetComponent<SpriteRenderer>().color = new Color32(225, 225, 225, 255);
        }
    }

    void StartRewind()
    {
        if (hasControled)
        {
            GetComponent<SpriteRenderer>().color = transparent;
            //storage.GetComponent<SpriteRenderer>().color = transparent;
            isRewinding = true;
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

    void Recrod()
    {
        positons.Add(transform.position);//将等待第一次回溯的时间也算了进去
    }

    void CheckLayers()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Charger"))) //&& storage.transform.localScale.y >=0)
        {
            power -= powerLostSpeed;
            myAnimator.SetBool("isCharging",true);
            //storage.transform.localScale = new Vector3(storage.transform.localScale.x, power, storage.transform.localScale.z);
            isInControl = false;
        }
        else
        {
            myAnimator.SetBool("isCharging", false);
        }
    }
}


