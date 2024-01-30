using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EyePointer : MonoBehaviour
{
    private const float _maxDistance = 30;
    GameObject _selected = null;
    public GameObject gamemanager;
    public TextMeshProUGUI instruct;
    private GameObject Target;
    public GameObject Player;
    [SerializeField]
    public Vector3 scale;
    public int letter_count = 0;
    public AudioInput micdetect;
    public GameObject room_msg;
    public GameObject talk;
    public GameObject found;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //temp for returning
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _selected.SendMessage("Enable3POV");
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            if (hit.collider.tag == "stalkerarea")
            {
                _selected = hit.transform.gameObject;
                _selected.GetComponent<TextAnim>().EndCheck();
                _selected.SendMessage("Enable3POV");
            }

            if(hit.distance <= 4)
            {
                if(hit.collider.tag == "letters")
                {
                    instruct.text = "[F] to collect";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //pick up animation
                        hit.collider.gameObject.SetActive(false);
                        letter_count += 1;
                    }
                }
                else if(hit.collider.tag == "roomcard")
                {
                    instruct.text = "[F] to collect";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //pick up animation
                        gamemanager.GetComponent<GameManager>().has_card = true;
                        hit.collider.gameObject.SetActive(false);
                        room_msg.GetComponent<TextAnim>().EndCheck();
                    }
                }
                else if(hit.collider.tag == "board")
                {
                    instruct.text = "[F] to read";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //zoom in to read (pop an image on screen)
                        gameObject.GetComponent<PopUpManager>().ShowPopUp();
                    }
                }
                else if(hit.collider.tag == "door")
                {
                    Debug.Log("door hit");
                    //check again before finish
                    if (gamemanager.GetComponent<GameManager>().allowIn == false && gamemanager.GetComponent<GameManager>().has_card == true)
                    {
                        instruct.text = "[F] to open";
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            //spawn inside the room
                            Target = GameObject.FindGameObjectWithTag("spawn");
                            Player.transform.position = Target.transform.position;
                            Player.transform.localScale = scale;
                        }
                    }
                }
                else if (hit.collider.tag == "roomdoor")
                {
                    instruct.text = "[F] to enter";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //spawn inside room
                        Target = GameObject.FindGameObjectWithTag("spawnpoint");
                        Player.transform.position = Target.transform.position;
                        //print dialogue
                        Target.GetComponent<TextAnim>().EndCheck();
                        micdetect.GetLoudness();
                    }
                }
                else if (hit.collider.tag == "closetdoor")
                {
                    instruct.text = "[F] to hide";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Target = GameObject.FindGameObjectWithTag("incloset");
                        Player.transform.position = Target.transform.position;
                        talk.GetComponent<TextAnim>().EndCheck();
                        WinorLose();
                    }
                }
                else if (hit.collider.tag == "bathroom")
                {
                    instruct.text = "[F] to hide";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Target = GameObject.FindGameObjectWithTag("inbath");
                        Player.transform.position = Target.transform.position;
                        talk.GetComponent<TextAnim>().EndCheck();
                        WinorLose();
                    }
                }
                else if (hit.collider.tag == "bed")
                {
                    instruct.text = "[F] to hide";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Target = GameObject.FindGameObjectWithTag("underbed");
                        Player.transform.position = Target.transform.position;
                        talk.GetComponent<TextAnim>().EndCheck();
                        WinorLose();
                    }
                }
                else
                {
                    instruct.text = "";
                }
                
            }
        }

    }

    IEnumerator DialogueFoundCard()
    {
        yield return new WaitForSeconds(4);
        
    }

    public void WinorLose()
    {
        if (Target.tag == "incloset")
        {
            if(micdetect.level > 0.1)
            {
                //die

                //deadscene callin
                found.GetComponent<TextAnim>().EndCheck();
                gamemanager.GetComponent<GameManager>().Lose();
            }
            else
            {
                //stalker caught animations
                gamemanager.GetComponent<GameManager>().Win();
            }
        }
        else
        {
            //do if else to handle animations, or not
            found.GetComponent<TextAnim>().EndCheck();
            gamemanager.GetComponent<GameManager>().Lose();
        }
    }
}
