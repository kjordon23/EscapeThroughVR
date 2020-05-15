using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPuzzle : MonoBehaviour
{

    [Header("Puzzle Components")]
    public GameObject keypadCanvas;
    public GameObject canvasPointer;
    public bool canOpen = false;
    public bool isCompleted = false;
    private bool collided = false;

    [Header("Soundtracks")]
    public AudioSource openBeverly;
    public AudioSource beverlyClue;
    public AudioSource correct;
    public AudioSource wrong;
    public AudioSource music;

    [Header("OVRPlayer")]
    public GameObject player;
    public GameObject leftHand;
    public GameObject rightHand;

    private string openCode = "2001";
    private string usersCode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collided == false)
        {
            if (openBeverly.isPlaying)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                return;
                // After Open Beverly Ends, Then activity will begin.
            }
            else
            {
                this.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void checkWinString()
    {
        if (usersCode == openCode)
        {
            if (beverlyClue.isPlaying)
            {
                beverlyClue.Stop();
                music.volume = 0.5f;
            }
            correct.Play();
            canOpen = true;
            isCompleted = true;
            ReturnGame();
        }
        else
        {
            usersCode = "";
            wrong.Play();
        }
    }

    public void GetOne()
    {
        usersCode += '1';
    }

    public void GetTwo()
    {
        usersCode += '2';
    }

    public void GetThree()
    {
        usersCode += '3';
    }

    public void GetFour()
    {
        usersCode += '4';
    }

    public void GetFive()
    {
        usersCode += '5';
    }

    public void GetSix()
    {
        usersCode += '6';
    }

    public void GetSeven()
    {
        usersCode += '7';
    }

    public void GetEight()
    {
        usersCode += '8';
    }

    public void GetNine()
    {
        usersCode += '9';
    }

    public void GetZero()
    {
        usersCode += '0';
    }

    public void ReturnGame()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        keypadCanvas.SetActive(false);
        canvasPointer.SetActive(false);
        player.GetComponent<OVRPlayerController>().EnableLinearMovement = true;
        if (isCompleted == false)
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        collided = true;
        beverlyClue.Stop();
        beverlyClue.Play();
        keypadCanvas.SetActive(true);
        Transform personTrans = GameObject.FindGameObjectWithTag("MainCamera").transform;
        keypadCanvas.transform.position = new Vector3(personTrans.position.x, personTrans.position.y, personTrans.position.z + 1.5f);
        keypadCanvas.transform.eulerAngles = new Vector3(keypadCanvas.transform.eulerAngles.x, 0, keypadCanvas.transform.eulerAngles.z);
        player.GetComponent<OVRPlayerController>().EnableLinearMovement = false;
        this.GetComponent<BoxCollider>().enabled = false;
        canvasPointer.SetActive(true);
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        StartCoroutine(WaitForBeverly());
    }

    IEnumerator WaitForBeverly()
    {
        if (beverlyClue.isPlaying)
        {
            music.volume /= 7;
            yield return new WaitForSeconds(beverlyClue.clip.length);
            music.volume = 0.5f;
        }
    }

    public void playClueAgain()
    {
        beverlyClue.Play();
        StartCoroutine(WaitForBeverly());
    }
}
