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
    public AudioSource thud;
    public AudioSource pop;
    private bool playedThud = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("wa"))
        {
            return;
        }
        if (collision.gameObject.GetComponent<Fruit>().id == id && id<maxId && priority > collision.gameObject.GetComponent<Fruit>().priority)
        {
            pop.Play();
            par.GetComponent<Gameplay>().score += id + 1;
            GameObject temp;
            Vector3 pos = GetComponent<Transform>().position;
            Destroy(collision.gameObject);
            temp = Instantiate(obj, pos,Quaternion.identity);
            temp.GetComponent<Fruit>().priority = par.GetComponent<Gameplay>().totalPriority++;
            temp.GetComponentInChildren<ParticleSystem>().Play();
            par.GetComponent<Gameplay>().AddObj(temp);
            this.GetComponent<Transform>().position = this.GetComponent<Transform>().position+new Vector3(-1000,0,0);
            Destroy(gameObject);
            return;
        }
        if (!playedThud)
        {
            thud.Play();
            playedThud = true;
        }
    }
}
