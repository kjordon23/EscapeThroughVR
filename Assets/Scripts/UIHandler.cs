using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject playPanel;
    public GameObject quitPanel;
    public Button masterVol;
    public Slider masVol;
    public Vector2 joystick;
    [Header("Oculus Prefabs")]
    public GameObject OVRPlayer;
    public GameObject CameraRig;
    public GameObject Avatar;
    public Toggle eyeLevel;
    public Toggle floorLevel;

    [Header("Set Dyncamically")]
    private bool pressed = false;

    private void Start()
    {
        PlayerPrefs.SetInt("visit", 0);
        Time.timeScale = 1;
        ChangeMain();
    }

    private void Update()
    {
        CheckTrackingLevel();
        joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        if (masterVol.image.color == Color.red)
        {
            masVol.value += (joystick.x * Time.deltaTime);
        }
        AudioListener.volume = masVol.value;
    }

    public void ChangeMain()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        playPanel.SetActive(false);
        quitPanel.SetActive(false);
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
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
        playPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void ChangePlay()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        playPanel.SetActive(true);
        quitPanel.SetActive(false);
    }

    public void ChangeQuit()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        playPanel.SetActive(false);
        quitPanel.SetActive(true);
    }

    public void PlayGame()
    {
        PlayerPrefs.SetFloat("Volume", masVol.value);
        PlayerPrefs.SetString("Tracking", CameraRig.GetComponent<OVRManager>().trackingOriginType.ToString());
        Debug.Log(CameraRig.GetComponent<OVRManager>().trackingOriginType.ToString());
        SceneManager.LoadScene("timeMachineRoom");
    }

    public void QuitGame()
    {
        Application.Quit();
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
}
