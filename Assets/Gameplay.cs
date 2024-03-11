using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{
    public GameObject id1;
    public GameObject id2;
    public GameObject id3;
    public GameObject id4;
    public GameObject id5;
    public GameObject id6;
    public GameObject id7;
    private GameObject preview;
    private GameObject preview2;
    private int objToDrop;
    public int totalPriority = 0;
    private float cooldown = 0.7f;
    private float counter = 0.7f;
    public TextMeshPro text;

    private List<GameObject> list = new List<GameObject>();
    private void genFruit()
    {
        float res = Random.value;
        if (res < 0.33f) objToDrop = 1;
        if (res > 0.33f && res < 0.66f) objToDrop = 2;
        if (res > 0.66f) objToDrop = 3;
    }
    private void Start()
    {
        genFruit();
    }
    private void OnCleeck()
    {
        Destroy(preview);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // mousePos.x -= Screen.width / 2.0f;
       // mousePos.y-= Screen.height / 2.0f;

        float minX = -3.24f+(objToDrop==1?id1.gameObject.GetComponent<Transform>().localScale.x/4.0f
            : objToDrop == 2? id2.gameObject.GetComponent<Transform>().localScale.x / 4.0f
            : id3.gameObject.GetComponent<Transform>().localScale.x / 4.0f);
        float maxX = 3.24f - (objToDrop == 1 ? id1.gameObject.GetComponent<Transform>().localScale.x / 4.0f
            : objToDrop == 2 ? id2.gameObject.GetComponent<Transform>().localScale.x / 4.0f
            : id3.gameObject.GetComponent<Transform>().localScale.x / 4.0f);

        Vector3 pos;
        pos.y = 4f;
        pos.z = 0f;
        pos.x = mousePos.x < minX ? minX : (mousePos.x > maxX ? maxX : mousePos.x);

        if (preview.GetComponent<Fruit>().id == 1)
        {
            list.Add(Instantiate(id1, pos, Quaternion.identity));
            list[list.Count - 1].GetComponent<Fruit>().priority = totalPriority++;
        }
        if (preview.GetComponent<Fruit>().id == 2)
        {
            list.Add(Instantiate(id2, pos, Quaternion.identity));
            list[list.Count - 1].GetComponent<Fruit>().priority = totalPriority++;
        }
        if (preview.GetComponent<Fruit>().id == 3)
        {
            list.Add(Instantiate(id3, pos, Quaternion.identity));
            list[list.Count - 1].GetComponent<Fruit>().priority = totalPriority++;
        }
        genFruit();
        counter = 0f;
    }
    private void ShiftHues()
    {
        float h, s, v;
        Color.RGBToHSV(text.colorGradient.topLeft, out h,out s,out v);
        h = (h + (Time.deltaTime*80 / 360f)) % 1f;
        Color x1 = Color.HSVToRGB(h, s, v);

        Color.RGBToHSV(text.colorGradient.topRight, out h, out s, out v);
        h = (h + (Time.deltaTime * 80 / 360f)) % 1f;
        Color x2 = Color.HSVToRGB(h, s, v);

        Color.RGBToHSV(text.colorGradient.bottomLeft, out h, out s, out v);
        h = (h + (Time.deltaTime * 80 / 360f)) % 1f;
        Color x3 = Color.HSVToRGB(h, s, v);

        Color.RGBToHSV(text.colorGradient.bottomRight, out h, out s, out v);
        h = (h + (Time.deltaTime *80 / 360f)) % 1f;
        Color x4 = Color.HSVToRGB(h, s, v);

        text.colorGradient = new VertexGradient(x1, x2, x3, x4);
    }
    private void Update()
    {
        if(counter<cooldown)counter += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && counter >= cooldown)
        {
            OnCleeck();
        }

        if(preview == null && counter >= cooldown)
        {
            if (totalPriority == 0)
            {
                preview = Instantiate(objToDrop == 1 ? id1 : objToDrop == 2 ? id2 : id3, new Vector3(0, 3.0f, 0f), Quaternion.identity); // FIX positions, scale
                preview.GetComponent<Rigidbody2D>().gravityScale = 0f;
                genFruit();
                preview2 = Instantiate(objToDrop == 1 ? id1 : objToDrop == 2 ? id2 : id3, new Vector3(3f, 4.5f, 0f), Quaternion.identity);
                preview2.GetComponent<Transform>().localScale.Scale(new Vector3(0.25f,0.25f,0.25f));
                preview2.GetComponent<Rigidbody2D>().gravityScale = 0f;
                return;
            }
            preview = preview2;
            preview.GetComponent<Transform>().localScale.Scale(new Vector3(4f, 4f, 4f));
            preview.GetComponent<Transform>().position = new Vector3(0, 3.0f, 0f);

            preview2 = Instantiate(objToDrop==1?id1:objToDrop==2?id2:id3, new Vector3(3,4.5f,0f), Quaternion.identity);
            preview2.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

        ShiftHues();
    }
}
