using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //for letters and roomcard
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            gameObject.GetComponent<TextAnim>().EndCheck();
        }
    }

    //for messages checkpoints
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gameObject.tag == "messages")
            {
                gameObject.GetComponent<PopUpManager>().ShowPopUp();
            }
            gameObject.GetComponent<TextAnim>().EndCheck();
        }
    }
}
