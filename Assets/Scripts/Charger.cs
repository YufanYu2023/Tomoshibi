using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField] GameObject movingPlatform;
    [SerializeField] GameObject batteryPrefb;
    [SerializeField] GameObject cover;

    public bool isCharging;

    private void Update()
    {
        CheckPower();
    }

    void CheckPower()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isCharging = false;
            cover.GetComponent<Animator>().SetBool("batteryIn", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCharging = true;
        cover.GetComponent<Animator>().SetBool("batteryIn", true);

    }
}
