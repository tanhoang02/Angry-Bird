using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Vector3 currentPosition;
    public float force;
    public float maxLenght;
    public float bottomBoundary;
    public GameObject birdPrefab;
    public float birdPositionOffset;
   // public GameObject PointPrafeb;
   // public int numberOfpoints;
   // public GameObject Points;
    public float LaunchForce;


    Rigidbody2D bird;
    Collider2D birdCollider;

    bool isMouseDown;
    // Start is called before the first frame update
    void Start()
    {

        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        CreateBird();
        //CreatePoint();

    }
    /*void CreatePoint()
    {
        Points = new GameObject[numPoint];
        for (int i = 0; i < numPoint; i++)
        {
            Points[i] = Instantiate(PointPrafeb, transform.position, Quaternion.identity);
        }
    }*/
    private void CreateBird()
    {
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;
        bird.isKinematic = true;
        ResetStrip();

    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLenght);
            currentPosition = ClampBoundary(currentPosition);


            SetsStrips(currentPosition);
       
        if (birdCollider)
        {
            birdCollider.enabled = true;
                /*   for (IntInspector = 0;if< Points.Leghth;i++)
                       {
                       Points[i].transform.position = PointPosition(i * 0.1f);

                   }*/
            }
        }
        else
        {
            ResetStrip();
        }
       

    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }
    void Shoot()
    {
        bird.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        bird.velocity = birdForce;
        bird = null;
        birdCollider = null;
        Invoke("CreateBird", 2);

    }
    void ResetStrip()
    {
     
        currentPosition = idlePosition.position;
        SetsStrips(currentPosition);
    }

    void SetsStrips(Vector3 position)

    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);
        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
    }
    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 4000);
        return vector;
    }
   /* Vector2 PointPosition(float t)
    {
        Vector2 currentPointPosition = transform.position + (currentPosition - center.position) * force * -1 * t)+0.5f * Physics2D.gravity * (t * t);
        return currentPointPosition;
    }*/
}

