using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPuzzle : MonoBehaviour
{
    [Header("Puzzle Components")]
    public GameObject keyboardCanvas;
    public GameObject canvasPointer;
    public bool passwordFound = false;
    public bool isCompleted = false;
    private bool collided = false;
    public Text curText;

    [Header("OVRPlayer Controls")]
    public GameObject player;
    public GameObject leftHand;
    public GameObject rightHand;

    [Header("Soundtracks")]
    public AudioSource openBeverly;
    public AudioSource beverlyClue;
    public AudioSource wrong;
    public AudioSource music;

    private string openCode = "DELOREAN";
    private string usersCode;

    // Start is called before the first frame update
    void Start()
    {
        curText.text = "";
        usersCode = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (collided == false)
        {
            if (openBeverly.isPlaying)
            {
                this.GetComponent<MeshCollider>().enabled = false;
                return;
                // After Open Beverly Ends, Then activity will begin.
            }
            else
            {
                this.GetComponent<MeshCollider>().enabled = true;
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
            passwordFound = true;
            isCompleted = true;
            ReturnGame();
        }
        else
        {
            usersCode = "";
            StartCoroutine(displayTryAgain());
            wrong.Play();
        }
    }

    public void ReturnGame()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        keyboardCanvas.SetActive(false);
        canvasPointer.SetActive(false);
        player.GetComponent<OVRPlayerController>().EnableLinearMovement = true;
        if (isCompleted == false)
        {
            this.GetComponent<MeshCollider>().enabled = true;
        }
        else
        {
            this.GetComponent<MeshCollider>().enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        collided = true;
        if (openBeverly.isPlaying)
        {
            openBeverly.Stop();
        }
        keyboardCanvas.SetActive(true);
        Transform personTrans = GameObject.FindGameObjectWithTag("MainCamera").transform;
        keyboardCanvas.transform.position = new Vector3(personTrans.position.x + 1.5f, personTrans.position.y + 0.5f, personTrans.position.z);
        keyboardCanvas.transform.eulerAngles = new Vector3(-20, 90, keyboardCanvas.transform.eulerAngles.z);
        player.GetComponent<OVRPlayerController>().EnableLinearMovement = false;
        canvasPointer.SetActive(true);
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        this.GetComponent<MeshCollider>().enabled = false;
        beverlyClue.Stop();
        beverlyClue.Play();
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

    IEnumerator displayTryAgain()
    {
        curText.text = "Try Again";
        yield return new WaitForSeconds(1.0f);
        curText.text = "";
    }

    public void playClueAgain()
    {
        beverlyClue.Play();
        StartCoroutine(WaitForBeverly());
    }

    // Methods for Buttons

    public void GetA()
    {
        usersCode += 'A';
        curText.text = usersCode;
    }

    public void GetB()
    {
        usersCode += 'B';
        curText.text = usersCode;
    }

    public void GetC()
    {
        usersCode += 'C';
        curText.text = usersCode;
    }

    public void GetD()
    {
        usersCode += 'D';
        curText.text = usersCode;
    }

    public void GetE()
    {
        usersCode += 'E';
        curText.text = usersCode;
    }

    public void GetF()
    {
        usersCode += 'F';
        curText.text = usersCode;
    }

    public void GetG()
    {
        usersCode += 'G';
        curText.text = usersCode;
    }

    public void GetH()
    {
        usersCode += 'H';
        curText.text = usersCode;
    }

    public void GetI()
    {
        usersCode += 'I';
        curText.text = usersCode;
    }

    public void GetJ()
    {
        usersCode += 'J';
        curText.text = usersCode;
    }

    public void GetK()
    {
        usersCode += 'K';
        curText.text = usersCode;
    }

    public void GetL()
    {
        usersCode += 'L';
        curText.text = usersCode;
    }

    public void GetM()
    {
        usersCode += 'M';
        curText.text = usersCode;
    }

    public void GetN()
    {
        usersCode += 'N';
        curText.text = usersCode;
    }

    public void GetO()
    {
        usersCode += 'O';
        curText.text = usersCode;
    }

    public void GetP()
    {
        usersCode += 'P';
        curText.text = usersCode;
    }

    public void GetQ()
    {
        usersCode += 'Q';
        curText.text = usersCode;
    }

    public void GetR()
    {
        usersCode += 'R';
        curText.text = usersCode;
    }

    public void GetS()
    {
        usersCode += 'S';
        curText.text = usersCode;
    }

    public void GetT()
    {
        usersCode += 'T';
        curText.text = usersCode;
    }

    public void GetU()
    {
        usersCode += 'U';
        curText.text = usersCode;
    }

    public void GetV()
    {
        usersCode += 'V';
        curText.text = usersCode;
    }

    public void GetW()
    {
        usersCode += 'W';
        curText.text = usersCode;
    }

    public void GetX()
    {
        usersCode += 'X';
        curText.text = usersCode;
    }

    public void GetY()
    {
        usersCode += 'Y';
        curText.text = usersCode;
    }

    public void GetZ()
    {
        usersCode += 'Z';
        curText.text = usersCode;
    }

    
}
