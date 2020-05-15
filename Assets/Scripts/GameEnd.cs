using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{

    [Header("End Game Components")]
    public GameObject firstPuz;
    public GameObject secondPuz;
    public GameObject thirdPuz;

    public GameObject forceField;
    public AudioSource endBeverly;
    public AudioSource music;
    // Update is called once per frame
    void Update()
    {
        if (firstPuz.GetComponent<FirstPuzzle>().isCompleted && secondPuz.GetComponent<SecondPuzzle>().isCompleted && thirdPuz.GetComponent<ThirdPuzzle>().isCompleted)
        {
            forceField.SetActive(false);
            endBeverly.Play();
            if (endBeverly.isPlaying)
            {
                music.volume = 0.1f;
            }
            else
            {
                music.volume = 0.5f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("timeMachineRoom");
    }
}
