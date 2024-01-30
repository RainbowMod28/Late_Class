using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnim : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI script;

    public string[] stringArray;

    [SerializeField] float timeBwChars;
    [SerializeField] float timeBwWords;
    public static int letter_count = 6;
    // Start is called before the first frame update

    int i = 0;
    void Start()
    {
    }

    public void EndCheck() {
        CheckLetterCount();
        if (i <= stringArray.Length - 1)
        {
            script.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
        else if(i == stringArray.Length)
        {
            script.text = "";
        }
        if(gameObject.tag == "roomcard")
        {
            gameObject.SetActive(false);
        }
    }

    void CheckLetterCount()
    {
        if (gameObject.tag == "letters")
        {
            if (letter_count > 1)
            {
                stringArray[0] = "ROXANE: GOT ONE...";
                letter_count -= 1;
            }
            else
            {
                stringArray[0] = "ROXANE: MY CARD IS BY THE FOUNTAIN?";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TextVisible()
    {
        script.ForceMeshUpdate();
        int totalVisibleCharacters = script.textInfo.characterCount;
        int counter = 0;

        while(true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            script.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                Invoke("EndCheck", timeBwWords);
                break;
            }
            counter += 1;
            yield return new WaitForSeconds(timeBwChars);
        }
    }
}
