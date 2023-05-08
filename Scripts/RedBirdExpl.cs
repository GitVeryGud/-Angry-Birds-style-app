using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBirdExpl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float deactivationTime;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.simulated && once)
        {
            once = false;
            StartCoroutine(explosionTimer());
        }
    }

    private IEnumerator explosionTimer()
    {
        yield return new WaitForSeconds(5f);
        transform.GetChild(0).GetComponent<PointEffector2D>().enabled = true;

        yield return new WaitForSeconds(deactivationTime);
        Destroy(gameObject);
    }
}
