using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpManager : MonoBehaviour
{
    [SerializeField]
    public GameObject popup;
    public TextMeshProUGUI instruct;
    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(false);
    }
    
    public void ShowPopUp()
    {
        popup.SetActive(true);
        instruct.text = "[X] to close";
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            popup.SetActive(false);
            instruct.text = "";
        }
    }
}
