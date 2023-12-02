using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField]
    GameObject activatePlatform;

    Vector3 origPos;
    // Start is called before the first frame update
    void Start()
    {
        activatePlatform.SetActive(false);
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 14)
        {
            transform.position = origPos;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("TriggerPlate"))
        {
            activatePlatform.SetActive(true);
            Debug.Log("Set Active");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("TriggerPlate"))
        {
            activatePlatform.SetActive(false);
        }
    }
}
