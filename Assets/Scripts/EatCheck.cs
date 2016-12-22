using UnityEngine;
using System.Collections;

public class EatCheck : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (GameController.Instance.isTutorial)
        {
            if (collider.name == "Eat" && GameController.Instance._tutorialStep == 5)
            {
                if (transform.childCount > 0)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                    SoundManager.Instance.PlayEatAudio();
                    transform.tag = "IceCreamCorn";
                    GameController.Instance.isEat = true;
                }
            }
        }
        else
        {
            if (collider.name == "Eat")
            {
                if (transform.childCount > 0)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                    SoundManager.Instance.PlayEatAudio();
                    transform.tag = "IceCreamCorn";
                    GameController.Instance.isEat = true;
                }
            }
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (transform.childCount == 0)
        {
            transform.tag = "IceCreamCorn";
        }
    }
}
