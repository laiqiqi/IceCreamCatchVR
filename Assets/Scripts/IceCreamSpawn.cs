using UnityEngine;
using System.Collections;

public class IceCreamSpawn : MonoBehaviour {

    public GameObject[] IceCreamPrefab;
    private float Timer;

    // Use this for initialization
    void Start ()
    {
        Timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameController.Instance.GameStart) return;
        Timer += Time.deltaTime;

        if (IceCornCtrl.GameTime > 0)
        {
            IceCornCtrl.GameTime -= Time.deltaTime;
            //if(IceCornCtrl.GameTime > 20)
            //{
            if (Timer > 2.0f)
            {
                CreateIceCream(0);
                Timer = 0;
            }
            //}
            //else
            //{
            //    if (Timer > 1.0f)
            //    {
            //        CreateIceCream(1);
            //        Timer = 0;
            //    }
            //    //CreateIceCream(1); 
            //}
        }
        else
        {
            GameController.Instance.GameStart = false;
            SoundManager.Instance.PlayOverAudio();
            StartCoroutine(GameController.Instance.GameOver());
        }
        

    }

    public void CreateIceCream(int mode)
    {
        if(mode == 0)
        {
            GameObject go = Instantiate(IceCreamPrefab[Random.Range(0, 2)]);
            go.transform.parent = this.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(-20, 21), 1200, 400);
        }else if(mode == 1)
        {
            GameObject go = Instantiate(IceCreamPrefab[Random.Range(0, IceCreamPrefab.Length)]);
            go.transform.parent = this.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(-20, 21), 1200, 400);
        }

    }
    public void CreateTutorialIceCream()
    {
        GameObject go = Instantiate(IceCreamPrefab[0]);
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(-20, 21), 1200, 400);
    }
}
