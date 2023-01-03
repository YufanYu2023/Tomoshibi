using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    //clicked
    public GameObject Ldoor;
    public GameObject Rdoor;
    public GameObject logo;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().SetBool("clicked", true);
            Ldoor.GetComponent<Animator>().SetBool("clicked", true);
            Rdoor.GetComponent<Animator>().SetBool("clicked", true);
            logo.GetComponent<Animator>().SetBool("clicked", true);
            StartCoroutine(NextScence());
        }
    }

    IEnumerator NextScence()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
