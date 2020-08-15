using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThrough : MonoBehaviour
{
    private Transform target;
    private SpriteRenderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PassThroughObject();
    }

    private void PassThroughObject()
    {
        if (transform.position.y < target.position.y)
        {
            myRenderer.sortingOrder = 1;
        }
        else if (transform.position.y > target.position.y)
        {
            myRenderer.sortingOrder = -1;
        }
    }
}
