using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDisappear : MonoBehaviour
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
            StartCoroutine(DestroyBird());
        }
    }

    private IEnumerator DestroyBird()
    {
        yield return new WaitForSeconds(deactivationTime);
        Destroy(gameObject);
    }
}
