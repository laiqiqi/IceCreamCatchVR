using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OrderController : MonoBehaviour {

    public GameObject[] IceCreamOrders;
    public Sprite[] CustomSpites;

    public List<int> orderList;
    private SpriteRenderer sprite;
    private Animator animator;
	// Use this for initialization
	void Start ()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (GameController.Instance.isTutorial)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(IceCreamOrders[0]);
                go.transform.parent = this.transform;
                go.transform.localPosition = new Vector3(0f, (6.5f + 1.5f * i), 0f);
                go.transform.localRotation = Quaternion.Euler(Vector3.zero);
                go.transform.localScale = new Vector3(10, 10, 10);
                orderList.Add(go.GetComponent<IceCreamInfo>().IceCreamID);
            }
        }
        else
        {
            CreateOrder();
        }

    }

    void CreateOrder()
    {
        //if (IceCornCtrl.GameTime > 20)
        //{
        int IceCreamCount = Random.Range(1, 4);
        for (int i = 0; i < IceCreamCount; i++)
        {
            int type = Random.Range(0, 2);
            GameObject go = Instantiate(IceCreamOrders[type]);
            go.transform.parent = this.transform;
            go.transform.localPosition = new Vector3(0f, (6.5f + 1.5f * i), 0f);
            go.transform.localRotation = Quaternion.Euler(Vector3.zero);
            go.transform.localScale = new Vector3(10, 10, 10);
            orderList.Add(go.GetComponent<IceCreamInfo>().IceCreamID);
        }
        //}
        //else
        //{
        //    StartCoroutine(Hide());
        //    int IceCreamCount = Random.Range(1, 6);
        //    for (int i = 0; i < IceCreamCount; i++)
        //    {
        //        int type = Random.Range(0, IceCreamOrders.Length);
        //        GameObject go = Instantiate(IceCreamOrders[type]);
        //        go.transform.parent = this.transform;
        //        go.transform.localPosition = new Vector3(0f, (6.5f + 1.5f * i), 0f);
        //        go.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //        go.transform.localScale = new Vector3(10, 10, 10);
        //        orderList.Add(go.GetComponent<IceCreamInfo>().IceCreamID);
        //    }
        //}

    }

    public bool CheckOrder(List<int> catchedIceCream)
    {
        if(catchedIceCream.Count != orderList.Count)
        {
            catchedIceCream.Clear();
            return false;
        }
        else
        {
            for (int i = 0; i < catchedIceCream.Count; i++)
            {
                if(catchedIceCream[i] == 5)
                {
                    break;
                }
                else if(catchedIceCream[i] != orderList[i])
                {
                    catchedIceCream.Clear();
                    return false;
                }
            }
        }
        catchedIceCream.Clear();
        return true;
    }
    public void Selected(bool selected)
    {
        if(selected)
        sprite.sprite = CustomSpites[1];
        else
        sprite.sprite = CustomSpites[0];
    }
    public void isRight(bool isright)
    {
        if (isright)
        {
            sprite.sprite = CustomSpites[2];
        }
        else
        {
            sprite.sprite = CustomSpites[3];
        }
    }


    public IEnumerator Hide()
    {
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(1);
        if(this != null)
            Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        orderList.Clear();
    }

}
