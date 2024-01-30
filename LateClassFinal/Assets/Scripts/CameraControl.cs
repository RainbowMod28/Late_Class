using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void Enable3POV()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

    public void Disable3POV()
    {
        cam2.SetActive(false);
        cam1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disable3POV();
        }
    }
}
