using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    private GameObject firstPuz;
    public GameObject secondPuz;
    public bool isOpen = false;
    private bool labCompleted = false;

    [Header("Audio Files")]
    public AudioSource beverlyLab;
    public AudioSource music;
    public AudioSource beverlyServer;
    public AudioSource beverlyWarning;

    private int labTimesPressed = 0;
    private int serverTimesPressed = 0;

    // Start is called before the first frame update
    void Start()
    {
        firstPuz = GameObject.FindGameObjectWithTag("FirstPuzzle");
        this.GetComponent<Animator>().enabled = false;
    }

    private void Update()
    {

        if (firstPuz.GetComponent<FirstPuzzle>().canOpen == true)
        {
            if (isOpen)
            {
                StartCoroutine(OpenDoor());
            }
            else if (secondPuz.GetComponent<SecondPuzzle>().isCompleted == true)
            {
                labCompleted = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "GateSmall_meshLab" && labTimesPressed == 0)
        {
            beverlyLab.Play();
            StartCoroutine(WaitForBeverlyLab());
            labTimesPressed++;
            isOpen = true;
        }
        else if (this.gameObject.name == "GateSmall_meshServer2" && serverTimesPressed == 0)
        {
            beverlyServer.Play();
            StartCoroutine(WaitForBeverlyServer());
            serverTimesPressed++;
            isOpen = true;
        }
        else if (this.gameObject.name == "GateSmall_meshServer" && labCompleted == false)
        {
            beverlyWarning.Play();
            StartCoroutine(WaitForBeverlyWarning());
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }
        Debug.Log("Collision Occurred");
    }
    
    IEnumerator OpenDoor()
    {
        this.GetComponent<MeshCollider>().enabled = false;
        this.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(7.0f);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<MeshCollider>().enabled = true;
        isOpen = false;
    }

    IEnumerator WaitForBeverlyLab()
    {
        if (beverlyLab.isPlaying)
        {
            music.volume /= 7;
            yield return new WaitForSeconds(beverlyLab.clip.length);
            music.volume = 0.5f;
        }
    }

    IEnumerator WaitForBeverlyWarning()
    {
        if (beverlyWarning.isPlaying)
        {
            this.GetComponent<MeshCollider>().enabled = false;
            music.volume /= 7;
            yield return new WaitForSeconds(beverlyWarning.clip.length);
            music.volume = 0.5f;
            this.GetComponent<MeshCollider>().enabled = true;
        }
    }

    IEnumerator WaitForBeverlyServer()
    {
        if (beverlyServer.isPlaying)
        {
            music.volume /= 7;
            yield return new WaitForSeconds(beverlyServer.clip.length);
            music.volume = 0.5f;
        }
    }
}
