using UnityEngine;
using System.Collections;

public class CustomCheck : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Customer" && transform.childCount > 0)
        {
            GetComponentInParent<IceCornCtrl>().IsCustomer = true;
            //Save the customer
            GetComponentInParent<IceCornCtrl>().customer = other.GetComponent<OrderController>();
            //Save the customerSpawn
            GetComponentInParent<IceCornCtrl>().customerSpawn = other.GetComponentInParent<CustomerManager>();
            other.GetComponentInParent<OrderController>().Selected(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Customer")
        {
            GetComponentInParent<IceCornCtrl>().IsCustomer = false;
            other.GetComponentInParent<OrderController>().Selected(false);
        }
    }
}
