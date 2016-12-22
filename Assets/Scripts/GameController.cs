using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance;


    public bool GameStart = false;
    public bool isTutorial = true;
    public GameObject Master;
    public Canvas tutorialCanvas;
    public GameObject[] tutorial;
    public GameObject[] corns;
    public IceCreamSpawn iceCreamSpawn;
    public CustomerManager customerManager;
    public CustomerManager customerManager1;

    public int Level;
    public bool isPressTrigger = false;
    public bool isCatch = false;
    public bool isDoubleCatch = false;

    public bool isRight = false;
    public bool isFalse = false;

    public bool isEat = false;
    public bool triggerUp = false;
    public bool isFinished = false;

    public int _tutorialStep;
    private float timer;
    private bool _createCustom = true;

    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start ()
    {
        Master.SetActive(false);
        tutorialCanvas.enabled = true;
        _tutorialStep = 0;
        timer = 0;
    }

    void Update()
    {


        if (isTutorial)
        {
            if(corns[0].activeInHierarchy && corns[1].activeInHierarchy)
            {
                isPressTrigger = true;
                triggerUp = false;
            }
            if (!corns[0].activeInHierarchy && !corns[1].activeInHierarchy)
            {
                isPressTrigger = false;
            }
        }

        if (_tutorialStep == 0)
        {
            tutorial[0].SetActive(true);
            if (isPressTrigger)
            {
                tutorial[0].SetActive(false);
                Tutorial1();
            }
        }

        if (_tutorialStep == 1)
        {
            if (!isPressTrigger)
            {
                Tutorial3();
                return;
            }
            if (isCatch)
            {
                isCatch = false;
                Tutorial2();
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    iceCreamSpawn.CreateTutorialIceCream();
                    timer = 0;
                }
            }
        }
        if (_tutorialStep == 2)
        {
            if (!isPressTrigger)
            {            
                Tutorial3();
                return;
            }
            if (isDoubleCatch)
            {
                isDoubleCatch = false;
                Tutorial4();
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    iceCreamSpawn.CreateTutorialIceCream();
                    timer = 0;
                }
            }
        }

        if(_tutorialStep == 4)
        {
            if (isRight)
            {
                isRight = false;
                Tutorial5();
            }
            else if(isFalse)
            {
                isFalse = false;
                tutorial[4].SetActive(false);
                _tutorialStep = 0;
            }
            else if(triggerUp)
            {
                triggerUp = false;
                tutorial[4].SetActive(false);
                _tutorialStep = 0;
            }
        }

        if (isPressTrigger && _tutorialStep == 3)
        {
            tutorial[3].SetActive(false);
            _tutorialStep = 0;
        }

        if (_tutorialStep == 5)
        {
            if (isEat)
            {
                Tutorial6();
            } else if (corns[0].transform.childCount < 2 && corns[1].transform.childCount < 2)
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    iceCreamSpawn.CreateTutorialIceCream();
                    timer = 0;
                }
            }
        }

    }
    private void Tutorial1()
    {

        timer = 0;
        _tutorialStep = 1;
        tutorial[1].SetActive(true);
    }
    private void Tutorial2()
    {
        timer = 0;
        _tutorialStep = 2;
        tutorial[1].SetActive(false);
        tutorial[2].SetActive(true);
        SoundManager.Instance.PlayTutorialAudio(0);
    }

    private void Tutorial3()
    {
        _tutorialStep = 3;
        isCatch = false;
        isDoubleCatch = false;
        timer = 0;
        tutorial[0].SetActive(false);
        tutorial[1].SetActive(false);
        tutorial[2].SetActive(false);
        tutorial[4].SetActive(false);
        
        tutorial[3].SetActive(true);
    }
    private void Tutorial4()
    {
        _tutorialStep = 4;
        isCatch = false;
        isDoubleCatch = false;
        timer = 0;
        tutorial[2].SetActive(false);
        tutorial[4].SetActive(true);
        if (_createCustom)
        {
            customerManager.CreateTutorialCustomer();
            _createCustom = false;
        }
        timer = 0;
        for (int i = 0; i < iceCreamSpawn.transform.childCount; i++)
        {
            Destroy(iceCreamSpawn.transform.GetChild(i).gameObject);
        }
        SoundManager.Instance.PlayTutorialAudio(0);
    }

    private void Tutorial5()
    {
        timer = 0;
        _tutorialStep = 5;
        tutorial[4].SetActive(false);
        tutorial[5].SetActive(true);
        if (customerManager.transform.childCount != 0)
        {
            for (int i = 0; i < customerManager.transform.childCount; i++)
            {
                Destroy(customerManager.transform.GetChild(i).gameObject);
            }
        }
        iceCreamSpawn.CreateTutorialIceCream();
    }
    private void Tutorial6()
    {
        _tutorialStep = 6;
        tutorial[5].SetActive(false);
        tutorial[6].SetActive(true);
        for (int i = 0; i < iceCreamSpawn.transform.childCount; i++)
        {
            Destroy(iceCreamSpawn.transform.GetChild(i).gameObject);
        }
        isFinished = true;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlayStartAudio(0);
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlayStartAudio(1);
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlayStartAudio(2);
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlayStartAudio(3);
        tutorialCanvas.enabled = false;
        isTutorial = false;
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlayStartAudio(4);
        StartCoroutine(customerManager.CreateCustomer());
        StartCoroutine(customerManager1.CreateCustomer());
        IceCornCtrl.Score = 0;
        IceCornCtrl.GameTime = 90;
        if (customerManager.transform.childCount != 0)
        {
            for (int i = 0; i < customerManager.transform.childCount; i++)
            {
                Destroy(customerManager.transform.GetChild(i).gameObject);
            }
        }
        GameStart = true;
    }
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        Master.SetActive(true);
        if (IceCornCtrl.Score < 0)
        {
            SoundManager.Instance.PlayMasterAudio(4);
        }
        else if (IceCornCtrl.Score <= 50)
        {
            SoundManager.Instance.PlayMasterAudio(0);
        }
        else if (IceCornCtrl.Score < 100)
        {
            SoundManager.Instance.PlayMasterAudio(1);
        }
        else if (IceCornCtrl.Score >= 100)
        {
            SoundManager.Instance.PlayMasterAudio(3);
        }
        else
        {
            SoundManager.Instance.PlayMasterAudio(2);
        }
        yield return new WaitForSeconds(3);
        IceCornCtrl.Return = true;
    }

}
