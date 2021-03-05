using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
    }
}
