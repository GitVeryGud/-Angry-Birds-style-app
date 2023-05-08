using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    private float angle;
    private Vector2 worldMousePos;
    public Transform birdSpawnPos;
    public GameObject redBird;
    public GameObject yellowBird;
    public GameObject blueBird;
    private GameObject selectedBird;
    private GameObject bird;
    private bool holding;
    private float dist;
    private bool change;

    private float maxRadius;
    private float k;

    [Header("Slingshot 1")]
    public float maxRadius1;
    public float k1;
    [Header("Slingshot 2")]
    public float maxRadius2;
    public float k2;
    [Header("Slingshot 3")]
    public float maxRadius3;
    public float k3;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        maxRadius = maxRadius1;
        k = k1;
        sr.color = Color.blue;

        selectedBird = redBird;
    }

    // Update is called once per frame
    void Update()
    {
        createBird();
        chooseBird();
        SlingShotChange();
        BirdShooting();
    }

    private void createBird()
    {
        if (bird == null)
        {
            bird = Instantiate(selectedBird, birdSpawnPos.position, Quaternion.identity);
        }

        if (change)
        {
            change = false;
            var temp = bird;
            bird = Instantiate(selectedBird, birdSpawnPos.position, Quaternion.identity);
            Destroy(temp);
        }
    }

    private void chooseBird()
    {
        if (Input.GetKeyDown("q"))
        {
            selectedBird = redBird;
            change = true;
        }

        else if (Input.GetKeyDown("w"))
        {
            selectedBird = yellowBird;
            change = true;
        }

        else if (Input.GetKeyDown("e"))
        {
            selectedBird = blueBird;
            change = true;
        }
    }

    private void SlingShotChange()
    {
        if (Input.GetKeyDown("1"))
        {
            maxRadius = maxRadius1;
            k = k1;
            sr.color = Color.blue;
        }

        else if (Input.GetKeyDown("2"))
        {
            maxRadius = maxRadius2;
            k = k2;
            sr.color = Color.magenta;
        }

        else if (Input.GetKeyDown("3"))
        {
            maxRadius = maxRadius3;
            k = k3;
            sr.color = Color.yellow;
        }
    }

    private void BirdShooting()
    {
        if (Input.GetMouseButton(0))
        {
            holding = true;

            Vector2 mousePos = Input.mousePosition;
            worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            angle = -Vector2.SignedAngle(worldMousePos - (Vector2)transform.position, transform.up);

            //Debug.Log(angle);

            dist = Vector2.Distance(worldMousePos, birdSpawnPos.position);

            // Ensures only positive angles can be used
            if (angle > 0)
            {
                bird.transform.position = (Vector2)birdSpawnPos.position + (worldMousePos - (Vector2)birdSpawnPos.position).normalized * Mathf.Min(dist, maxRadius);
            }

        }

        // Triggers when letting go of the mouse button
        else if (holding == true && angle > 0)
        {
            holding = false;
            var birdParts = bird.GetComponentsInChildren<Rigidbody2D>();

            foreach (Rigidbody2D rb in birdParts)
            {
                rb.simulated = true;
            }

            // Impulse given by slingshot
            var elasticForce = -(worldMousePos - (Vector2)birdSpawnPos.position).normalized * k * Mathf.Min(dist, maxRadius);

            birdParts[0].AddForce(elasticForce, ForceMode2D.Impulse);

            if (birdParts[0].GetComponent<BirdDisappear>() != null)
            {
                birdParts[0].GetComponent<BirdDisappear>().enabled = true;
            }

            bird = null;
        }

        else
        {
            bird.transform.position = birdSpawnPos.position;
        }
    }
}
