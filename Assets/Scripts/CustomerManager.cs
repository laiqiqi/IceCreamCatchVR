using UnityEngine;
using System.Collections;

public class CustomerManager : MonoBehaviour {
    public GameObject[] Customers;

	public IEnumerator CreateCustomer()
    {
        yield return new WaitForSeconds(1);
        GameObject customer = Instantiate(Customers[Random.Range(0, Customers.Length)]);
        customer.transform.parent = this.transform;
        customer.transform.localPosition = Vector3.zero;
        customer.transform.localRotation = Quaternion.identity;
        customer.transform.localScale = Vector3.one;
    }

    public void CreateTutorialCustomer()
    {
        GameObject customer = Instantiate(Customers[Random.Range(0, Customers.Length)]);
        customer.transform.parent = this.transform;
        customer.transform.localPosition = Vector3.zero;
        customer.transform.localRotation = Quaternion.identity;
        customer.transform.localScale = Vector3.one;
    }

    void Update()
    {
        if (GameController.Instance.GameStart && IceCornCtrl.GameTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
