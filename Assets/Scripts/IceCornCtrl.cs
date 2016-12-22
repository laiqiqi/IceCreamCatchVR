using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IceCornCtrl : MonoBehaviour {

    /// <summary>
    /// Controller Model
    /// </summary>
    public GameObject Model;
    /// <summary>
    /// IceCornModel
    /// </summary>
    public GameObject IceCornModel;

    public GameObject ControllerTooltips;

    /// <summary>
    /// CatchedIceCream List
    /// </summary>
    public List<int> CatchedIceCream;
    /// <summary>
    /// Is Customer or not
    /// </summary>
    public bool IsCustomer = false;
    /// <summary>
    /// customer temp
    /// </summary>
    public OrderController customer;
    /// <summary>
    /// customerSpawn temp
    /// </summary>
    public CustomerManager customerSpawn;
    /// <summary>
    /// Score
    /// </summary>
    public static int Score;
    /// <summary>
    /// Game Time
    /// </summary>
    public static float GameTime;

    public static bool Return = false;


    /// <summary>
    /// Controller
    /// </summary>
    SteamVR_TrackedObject trackedObject;
    /// <summary>
    /// Raycast check
    /// </summary>
    //RaycastHit hit;

    void Awake(){
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
    }
	// Use this for initialization
	void Start () {
        Model.SetActive(true);
        IceCornModel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () 
	{
		var device = SteamVR_Controller.Input ((int)trackedObject.index);
        //Trigger Down
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Model.SetActive(false);
            IceCornModel.SetActive(true);
            
        }
        if (GameController.Instance.isTutorial)
        {
            if (GameController.Instance._tutorialStep == 0 || GameController.Instance._tutorialStep == 3)
            {
                ControllerTooltips.SetActive(true);

                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    ControllerTooltips.transform.FindChild("TriggerTooltip").gameObject.SetActive(false);
                }
                if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    ControllerTooltips.transform.FindChild("TriggerTooltip").gameObject.SetActive(true);
                }
            }
            else
            {
                ControllerTooltips.SetActive(false);
                ControllerTooltips.transform.FindChild("TriggerTooltip").gameObject.SetActive(true);
            }
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (Return)
            {
                Return = false;
                Score = 0;
                SceneManager.LoadSceneAsync("Start");
            }
        }

        if (IsCustomer && customer != null)
        {
            CatchedIceCream.Clear();
            //Update the CatchedIceCream List
            for (int i = 0; i < IceCornModel.transform.childCount; i++)
            {
                CatchedIceCream.Add(IceCornModel.transform.GetChild(i).GetComponent<IceCreamInfo>().IceCreamID);
            }
            int index = CatchedIceCream.Count;
            //if Trigger Up
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                //Check isRight
                bool isRight = customer.CheckOrder(CatchedIceCream);
                
                if (isRight)
                {
                    if (GameController.Instance.isTutorial)
                    {
                        GameController.Instance.isRight = true;
                    }
                    Score += 10 * index;                  
                    customer.isRight(isRight);
                    SoundManager.Instance.PlayRightAudio();
                }
                else
                {
                    if (GameController.Instance.isTutorial)
                    {
                        GameController.Instance.isFalse = true;
                    }
                    Score -= 10;
                    customer.isRight(isRight);
                    SoundManager.Instance.PlayWrongAudio();
                }
                //Delete Catched IceCream
                for (int i = 0; i < IceCornModel.transform.childCount; i++)
                {
                    Destroy(IceCornModel.transform.GetChild(i).gameObject);
                }
                IceCornModel.tag = "IceCreamCorn";
                Model.SetActive(true);
                IceCornModel.SetActive(false);
                //Hide the customer
                StartCoroutine(customer.Hide());
                if (GameTime > 0 || (GameController.Instance.isTutorial && GameController.Instance.isFalse))
                {
                    //Create the new customer
                    StartCoroutine(customerSpawn.CreateCustomer());
                }
                //Delete the customer temp
                customer = null;
            }
        }
        else
        {
            //Trigger Up(IsCustomer is false)
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) || (GameController.Instance.isTutorial && GameController.Instance.isFinished))
            {
                if (GameController.Instance.isTutorial)
                {
                    GameController.Instance.triggerUp = true;
                }
                //Find All of IceCream
                Component[] rigidbodys = GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rigidbody in rigidbodys)
                {
                    IceCornModel.tag = "IceCreamCorn";
                    rigidbody.gameObject.tag = "FillingIce";
                    rigidbody.transform.SetParent(null);
                    rigidbody.isKinematic = false;
                    rigidbody.gameObject.AddComponent<CatchIceCream>();

                    // We should probably apply the offset between trackedObj.transform.position
                    // and device.transform.pos to insert into the physics sim at the correct
                    // location, however, we would then want to predict ahead the visual representation
                    // by the same amount we are predicting our render poses.
                    var origin = trackedObject.origin ? trackedObject.origin : trackedObject.transform.parent;
                    if (origin != null)
                    {
                        rigidbody.velocity = origin.TransformVector(device.velocity);
                        rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
                    }
                    else
                    {
                        rigidbody.velocity = device.velocity;
                        rigidbody.angularVelocity = device.angularVelocity;
                    }

                    rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
                }
                //Hide the IceCornModel Show the Controller Model
                Model.SetActive(true);
                IceCornModel.SetActive(false);
                //Clear the CatchedIceCream List
                CatchedIceCream.Clear();
            }
        }
       
    }

    //Controller Pulse
    public static void CtrlPulse(SteamVR_Controller.Device device)
    {
        device.TriggerHapticPulse(2000);
    }

}
