using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPuzzle : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject OVRPlayer;
    public GameObject objective;


    public GameObject cubeStartPos;
    private Vector3 lastPos;

    [Header("Conditions")]
    public bool isCompleted = false;
    public bool inMaze = false;

    [Header("Audio")]
    public AudioSource nextObjective;
    public AudioSource music;
    public AudioClip mazeMusic;
    public AudioClip origMusic;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMaze == false)
        {
            return;
        }

        if (objective.GetComponent<CoinScript>().isCollected == true)
        {
            music.clip = origMusic;
            music.Play();
            OVRPlayer.GetComponent<CharacterController>().enabled = false;
            OVRPlayer.transform.position = lastPos;
            OVRPlayer.GetComponent<CharacterController>().enabled = true;
            isCompleted = true;
            nextObjective.Play();
            StartCoroutine(WaitForBeverly());
            inMaze = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        music.clip = mazeMusic;
        music.Play();
        lastPos = OVRPlayer.transform.position;
        OVRPlayer.GetComponent<CharacterController>().enabled = false;
        OVRPlayer.transform.position = cubeStartPos.transform.position;
        OVRPlayer.GetComponent<CharacterController>().enabled = true;
        cubeStartPos.SetActive(false);
        this.GetComponent<BoxCollider>().enabled = false;
        inMaze = true;
    }

    IEnumerator WaitForBeverly()
    {
        if (nextObjective.isPlaying)
        {
            music.volume /= 7;
            yield return new WaitForSeconds(nextObjective.clip.length);
            music.volume = 0.5f;
        }
    }
}
