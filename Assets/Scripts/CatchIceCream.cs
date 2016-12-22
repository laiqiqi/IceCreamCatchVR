using UnityEngine;
using System.Collections;

public class CatchIceCream : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "IceCreamCorn")
        {
            if (GameController.Instance.isTutorial)
                GameController.Instance.isCatch = true;

            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            gameObject.tag = "IceCream";
            collision.gameObject.tag = "FillingIce";
            IceCornCtrl.CtrlPulse(SteamVR_Controller.Input((int)GetComponentInParent<SteamVR_TrackedObject>().index));
            SoundManager.Instance.PlayCatchAudio();
            Destroy(this);
        }
        if (collision.gameObject.tag == "IceCream")
        {
            if (GameController.Instance.isTutorial)
                GameController.Instance.isDoubleCatch = true;

            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.transform.parent;
            transform.localPosition = new Vector3(0, 0.0838f*(collision.transform.parent.childCount-1), 0);
            transform.localRotation = Quaternion.identity;
            gameObject.tag = "IceCream";
            collision.gameObject.tag = "FillingIce";
            IceCornCtrl.CtrlPulse(SteamVR_Controller.Input((int)GetComponentInParent<SteamVR_TrackedObject>().index));
            SoundManager.Instance.PlayCatchAudio();
            Destroy(this);
        }
        if (collision.gameObject.tag == "Floor")
        {
            if (IceCornCtrl.GameTime>0)
                IceCornCtrl.Score -= 1;
            Destroy(this.gameObject);
        }
    }
}
