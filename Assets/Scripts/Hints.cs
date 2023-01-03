using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    public GameObject hints;
    public GameObject tutorial;


    public void ButtonDown()
    {
        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        hints.SetActive(true);
        tutorial.SetActive(true);
        yield return new WaitForSeconds(2);
        hints.SetActive(false);
        tutorial.SetActive(false);
    }
}
