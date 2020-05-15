using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using OVR;

public class ControlUI : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject ButtonOne;
    public GameObject ButtonTwo;
    public GameObject ButtonThree;

    [Header("TV Handling")]
    public GameObject TvPanel;
    public Sprite westernImage;
    public Sprite medievalImage;
    public Sprite futureImage;

    [Header("Warp Effect")]
    public GameObject effect;


    [Header("Beverly Audio Soure")]
    public AudioSource beverly;

    [Header("Other Public Variables")]
    public string scene;

    public int timesVisited = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        effect.SetActive(false);
        timesVisited = PlayerPrefs.GetInt("visit") + 1;
        if (timesVisited > 1)
        {
            beverly.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonOne.GetComponent<SVButton>().buttonPressed)
        {
            TvPanel.GetComponent<Image>().sprite = westernImage;
            scene = "Western Scene";
        }
        else if (ButtonTwo.GetComponent<SVButton>().buttonPressed)
        {
            TvPanel.GetComponent<Image>().sprite = medievalImage;
            scene = "MedievalRoom";
        }
        else if (ButtonThree.GetComponent<SVButton>().buttonPressed)
        {
            scene = "FutureEscapeRoom";
            TvPanel.GetComponent<Image>().sprite = futureImage;
        }
    }
}
