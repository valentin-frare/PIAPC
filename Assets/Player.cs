using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float axisHorizontal => Input.GetAxis("Horizontal");
    private float axisVertical => Input.GetAxis("Vertical");

    [SerializeField] private float speed;

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.Translate(new Vector3(axisHorizontal, 0, axisVertical) * Time.deltaTime * speed);
    }
}
