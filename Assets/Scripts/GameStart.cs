using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public AudioSource StartAudio;
    public AudioClip StartClip;
    public Image StartButton;
    public Sprite ButtonDownSprite;

    SteamVR_TrackedObject trackedObject;

    private bool _isPressed = false;

    // Use this for initialization
    void Awake ()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject> ();
    }
    void Start()
    {
        _isPressed = false;
    }
    // Update is called once per frame
    void Update ()
    {
		var device = SteamVR_Controller.Input ((int)trackedObject.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && !_isPressed)
        {
            _isPressed = true;
            StartButton.sprite = ButtonDownSprite;
            StartAudio.PlayOneShot(StartClip);
            StartCoroutine(ToMainScene());
        }
    }

    IEnumerator ToMainScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Main");
    }
}
