using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using OVR;

public class UIController : MonoBehaviour
{
    [Header("Public Variables")]
    public bool paused = false;
    public OVRInput.Controller rightController = OVRInput.Controller.None;
    public GameObject pauseMenu = null;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject pointerObject;
    public GameObject canvas;
    public AudioSource beverly;
    public AudioSource music;

    [Header("Menu Components")]
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject returnPanel;
    public GameObject quitPanel;
    public Toggle eyeLevel;
    public Toggle floorLevel;
    public GameObject OVRPlayer;
    public GameObject CameraRig;

    [Header("Volume Controls")]
    public Button masterVol;
    public Slider masVol;
    public Vector2 joystick;

    private bool pressed = false;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitForBeverly());
        pauseMenu.SetActive(false);
        pointerObject.SetActive(false);
        canvas.SetActive(false);
        SetPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrackingLevel();
        if (OVRInput.GetDown(OVRInput.Button.Start, rightController))
        {
            if (paused) // is false
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        if (masterVol.image.color == Color.red)
        {
            masVol.value += (joystick.x * Time.deltaTime);
        }
        AudioListener.volume = masVol.value;
    }

    public void ResumeGame()
    {
        rightHand.SetActive(true);
        leftHand.SetActive(true);
        pointerObject.SetActive(false);
        pauseMenu.SetActive(false);
        canvas.SetActive(false);
        paused = false;
        OVRPlayer.GetComponent<OVRPlayerController>().EnableLinearMovement = true;
    }

    public void PauseGame()
    {
        rightHand.SetActive(false);
        leftHand.SetActive(false);
        pointerObject.SetActive(true);
        pauseMenu.SetActive(true);
        canvas.SetActive(true);
        Transform personTrans = GameObject.FindGameObjectWithTag("MainCamera").transform;
        canvas.transform.position = new Vector3(personTrans.position.x, personTrans.position.y, personTrans.position.z - 2.5f);
        canvas.transform.eulerAngles = new Vector3(canvas.transform.eulerAngles.x, 180, canvas.transform.eulerAngles.z);
        paused = true;
        ChangePause();
        OVRPlayer.GetComponent<OVRPlayerController>().EnableLinearMovement = false;
    }

    public void ChangeSettings()
    {
        if (CameraRig.GetComponent<OVRManager>().trackingOriginType == OVRManager.TrackingOrigin.EyeLevel)
        {
            eyeLevel.isOn = true;
            floorLevel.isOn = false;
        }
        else
        {
            floorLevel.isOn = true;
            eyeLevel.isOn = false;
        }
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
        returnPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void ChangePause()
    {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
        returnPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void ChangeQuit()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        returnPanel.SetActive(false);
        quitPanel.SetActive(true);
    }

    public void ChangeReturn()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        returnPanel.SetActive(true);
        quitPanel.SetActive(false);
    }

    public void CheckTrackingLevel()
    {
        if (eyeLevel.isOn && !floorLevel.isOn)
        {
            CameraRig.GetComponent<OVRManager>().trackingOriginType = OVRManager.TrackingOrigin.EyeLevel;
        }
        else if (floorLevel.isOn && !eyeLevel.isOn)
        {
            CameraRig.GetComponent<OVRManager>().trackingOriginType = OVRManager.TrackingOrigin.FloorLevel;
        }
    }

    public void masterPressStateChange()
    {
        pressed = !pressed;

        if (pressed)
        {
            masterVol.image.color = Color.red;
        }
        else
        {
            masterVol.image.color = Color.white;
        }
    }

    // Button Handler Methods

    public void ReturnTunnel()
    {
        PlayerPrefs.SetFloat("Volume", masVol.value);
        PlayerPrefs.SetString("Tracking", CameraRig.GetComponent<OVRManager>().trackingOriginType.ToString());
        SceneManager.LoadScene("timeMachineRoom");
    }

    public void ReturnMainMenu()
    {
        PlayerPrefs.SetFloat("Volume", masVol.value);
        PlayerPrefs.SetString("Tracking", CameraRig.GetComponent<OVRManager>().trackingOriginType.ToString());
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitForBeverly()
    {
        if (beverly.isPlaying)
        {
            music.volume /= 7;
            yield return new WaitForSeconds(beverly.clip.length);
            music.volume = 0.5f;
        }
    }

    public void SetPlayerPrefs()
    {
        masVol.value = PlayerPrefs.GetFloat("Volume");
        if (PlayerPrefs.GetString("Tracking") == "EyeLevel")
        {
            CameraRig.GetComponent<OVRManager>().trackingOriginType = OVRManager.TrackingOrigin.EyeLevel;
        }
        else if (PlayerPrefs.GetString("Tracking") == "FloorLevel")
        {
            CameraRig.GetComponent<OVRManager>().trackingOriginType = OVRManager.TrackingOrigin.FloorLevel;
        }
    }
}
