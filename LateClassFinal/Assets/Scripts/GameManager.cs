using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI clock;
    public GameObject startmenu;
    public GameObject deadmenu;
    public GameObject pausemenu;
    public GameObject winmenu;

    public static bool playing = false;
    public static bool dead = false;
    public bool allowIn = false;
    //15mins * 60s = 900s (real time)
    public static float timeRemaining = 900;
    public static bool timerIsRunning = false;
    public static float minPassed = 0;
    public static int minutes = 0;
    public static int interval = 15;
    public bool has_card = false;
    private void Awake()
    {
        Time.timeScale = 1;
        startmenu.SetActive(true);
        deadmenu.SetActive(false);
        pausemenu.SetActive(false);
        winmenu.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (playing)
        {
            startmenu.SetActive(false);
            deadmenu.SetActive(false);
            pausemenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // 15 ingame mins = 15x15 = 225

                timeRemaining -= Time.deltaTime;
                //Debug.Log(timeRemaining);
                minPassed += Time.deltaTime;
                //Debug.Log(minPassed);
                DisplayTime(minPassed);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Lose();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        //1 in game min = 15 sec irl
        if (timeToDisplay >= interval)
        {
            minutes += 1;
            interval += 15;
            Debug.Log(minutes);
        }
        if(minutes == 50)
        {
            allowIn = true;
        }
        clock.text = "09:" + minutes.ToString("00");
    }

    public void StartGame()
    {
        playing = true;
        timerIsRunning = true;
        startmenu.SetActive(false);
        deadmenu.SetActive(false);
        gameObject.GetComponent<TextAnim>().EndCheck();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausemenu.SetActive(false);
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(4);
        QuitGame();
    }

    public void Win()
    {
        playing = false;
        dead = false;
        winmenu.SetActive(true);
        StartCoroutine(Quit());
    }

    //trigger when lose
    public void Lose()
    {
        playing = false;
        dead = true;
        deadmenu.SetActive(true);
        StartCoroutine(Quit());
    }
}
