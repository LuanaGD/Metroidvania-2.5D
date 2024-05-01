using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionExample : MonoBehaviour
{

    public GameObject Step1;
    public GameObject Step2;

    public bool IsAtStep;

    Vector2 moveToStep;

    void Awake()
    {
        Step1 = GetComponent<GameObject>();
        Step2 = GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!IsAtStep)
        {
            transform.position = moveToStep;
                if (transform.position == Step2.transform.position)
            {
                IsAtStep = true;
            }
        }
    }
}
