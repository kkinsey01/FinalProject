using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingLogScript : MonoBehaviour
{
    [SerializeField]
    GameObject logPrefab;

    [SerializeField]
    GameObject spawnPoint;

    [SerializeField]
    GameObject destroyPoint;

    float speed;
    Rigidbody rb;
    IEnumerator SpawnLogs(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 pos = spawnPoint.transform.position;
        Instantiate(logPrefab, pos, Quaternion.identity);
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLogs(5.0f));
        rb = GetComponent<Rigidbody>();
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (spawnPoint.transform.position - destroyPoint.transform.position).normalized;
        rb.velocity = -direction * speed;
        if (transform.position.z <= 180)
        {
            Destroy(gameObject);
        }
    }
}
