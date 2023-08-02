using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golf_Ball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    private bool score;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude <= 0.2f)
        {
            Player_Input();
        }
        
    }

    private void Player_Input()
    {
        Vector2 input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, input);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f)
        {
            DragStart();
        }

        if(Input.GetMouseButton(0) && isDragging)
        {
            DragChange(input);
        }

        if(Input.GetMouseButtonUp(0) && isDragging) 
        {
            DragRelease(input);
        }
    }

    private void DragStart()
    {
        isDragging = true;
        lineRenderer.positionCount = 2;
    }

    private void DragChange(Vector2 pos)
    {
        Vector2 dir = (Vector2)transform.position - pos;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));
    }
    
    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lineRenderer.positionCount = 0;
        if(distance <1f)
        {
            return;
        }

        Vector2 dir = (Vector2)transform.position - pos;
        rb.velocity = Vector2.ClampMagnitude(dir * power,maxPower);
    }
}
