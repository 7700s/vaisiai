using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int id;
    private int maxId = 7;
    public int priority;
    public GameObject obj;
    public GameObject par;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("wa"))
        {
            return;
        }
        if (collision.gameObject.GetComponent<Fruit>().id == this.id && id<maxId && priority > collision.gameObject.GetComponent<Fruit>().priority)
        {
            GameObject temp;
            Vector3 pos = GetComponent<Transform>().position;
            Destroy(collision.gameObject);
            temp = Instantiate(obj, pos,Quaternion.identity);
            temp.GetComponent<Fruit>().priority = par.GetComponent<Gameplay>().totalPriority++;
            this.GetComponent<Transform>().position = this.GetComponent<Transform>().position+new Vector3(-1000,0,0);
            Destroy(GetComponent<GameObject>());
        }
    }
}
