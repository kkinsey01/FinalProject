using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    GameObject platform;

    const float platformTimeMax = 7.5f;
    float timeActive;
    bool activate;
    // Start is called before the first frame update
    void Start()
    {
        platform = GameObject.Find("Timed Platform");
        platform.SetActive(false);
        timeActive = 0;
        activate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            timeActive += Time.deltaTime;
            if (timeActive >= platformTimeMax)
            {
                timeActive = 0;
                platform.SetActive(false);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            platform.SetActive(true);
            timeActive = 0;
            activate = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            activate = true;
        }
    }
}
