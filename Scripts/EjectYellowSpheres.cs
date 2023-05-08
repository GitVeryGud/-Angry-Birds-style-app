using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectYellowSpheres : MonoBehaviour
{
    private Rigidbody2D rb;
    public float activationTime;
    public float launchForce;
    private bool once = true;
    private List<Transform> projectiles = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectiles.Add(transform.GetChild(0));
        projectiles.Add(transform.GetChild(1));
        projectiles.Add(transform.GetChild(2));
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.simulated && once)
        {
            once = false;
            StartCoroutine(launchProjectiles());
        }
    }

    private IEnumerator launchProjectiles()
    {
        yield return new WaitForSeconds(activationTime);
        foreach (Transform project in projectiles)
        {
            project.GetComponent<HingeJoint2D>().enabled = false;
            project.parent = null;
            var impulse = project.position - transform.position;
            project.GetComponent<Rigidbody2D>().AddForce(impulse * launchForce, ForceMode2D.Impulse);
        }
    }
}
