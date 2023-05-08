using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    public float health;
    public float delay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            var m = collision.gameObject.GetComponent<Rigidbody2D>().mass;
            var v = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;

            StartCoroutine(lowerHealth(m * v));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator lowerHealth(float damage)
    {
        yield return new WaitForSeconds(delay);
        health -= damage;
    }
}
