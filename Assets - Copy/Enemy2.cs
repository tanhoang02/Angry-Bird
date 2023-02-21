using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{

    public float defend;
    public GameObject effect;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > defend)
        {

            Destroy(gameObject, 0.1f);
            Instantiate(effect, transform.position, Quaternion.identity);

        }
        else
        {
            defend -= collision.relativeVelocity.magnitude;
        }




    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
