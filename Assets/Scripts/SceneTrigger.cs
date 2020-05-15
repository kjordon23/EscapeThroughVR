using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTrigger : MonoBehaviour
{
    public GameObject handler;
    public GameObject CameraRig;
    public GameObject UIController;
    public Slider masVol;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetFloat("Volume", masVol.value);
        PlayerPrefs.SetString("Tracking", CameraRig.GetComponent<OVRManager>().trackingOriginType.ToString());
        string sceneTrig = handler.GetComponent<ControlUI>().scene;
        SceneManager.LoadScene(sceneTrig);
        Debug.Log("Collision Occurred");
        PlayerPrefs.SetInt("visit", UIController.GetComponent<ControlUI>().timesVisited);
    }
}
