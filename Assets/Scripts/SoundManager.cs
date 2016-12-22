using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip[] StartClips;
    public AudioClip[] RightClips;
    public AudioClip[] WrongClips;
    public AudioClip[] TimeUpClips;
    public AudioClip EatClip;
    public AudioClip CatchClip;
    public AudioClip[] MasterClips;
    public AudioClip[] TutorialClips;


    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayStartAudio(int index)
    {
        audioSource.PlayOneShot(StartClips[index]);
    }
    public void PlayRightAudio()
    {
        audioSource.PlayOneShot(RightClips[Random.Range(0, RightClips.Length)]);
    }
    public void PlayWrongAudio()
    {
        audioSource.PlayOneShot(WrongClips[Random.Range(0, WrongClips.Length)]);
    }
    public void PlayEatAudio()
    {
        audioSource.PlayOneShot(EatClip);
    }
    public void PlayCatchAudio()
    {
        audioSource.PlayOneShot(CatchClip);
    }
    public void PlayOverAudio()
    {
        audioSource.PlayOneShot(TimeUpClips[Random.Range(0, TimeUpClips.Length)]);
    }
    public void PlayMasterAudio(int index)
    {
        audioSource.PlayOneShot(MasterClips[index]);
    }
    public void PlayTutorialAudio(int index)
    {
        audioSource.PlayOneShot(TutorialClips[index]);
    }
}
