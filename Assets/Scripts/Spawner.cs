using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject batteryPrefb;
    public List<GameObject> batteries;
    public GameObject hints;
    public GameObject tutorial;
    //public GameObject storage;

    int currentBatteryIndex = 0;

    Vector3 originalPosition;
    player player;
    public List<List<Vector3>> batterries;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = batteryPrefb.transform.position;
        player = batteryPrefb.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        Manager();
        LevelControl();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hints.SetActive(true);
            tutorial.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            hints.SetActive(false);
            tutorial.SetActive(false);
        }
    }

    void LevelControl()
    {
        if (Input.GetKeyDown(KeyCode.RightShift)|| Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentBatteryIndex<batteries.Count-1)
            {
                batteries[currentBatteryIndex].GetComponent<player>().isInControl = false;
                batteries[currentBatteryIndex].GetComponent<Collider2D>().isTrigger = false;
                currentBatteryIndex++;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //batteryPrefb.transform.position = originalPosition;
            if (currentBatteryIndex+1<batteries.Count)
            {
                batteries[currentBatteryIndex + 1].GetComponent<player>().controlThrows.Clear();
            }
        }
    }

    private void Manager()
    {
        batteries[currentBatteryIndex].GetComponent<player>().isInControl = true;
        batteries[currentBatteryIndex].GetComponent<player>().hasControled = true;
    }


}
