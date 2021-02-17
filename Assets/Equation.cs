using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation : MonoBehaviour
{
    GameObject[] numbersOnTheMap;
    List<Num> eqList = new List<Num>(); //Lista znaków w równaniu

    struct Num //Numer
    {
        readonly public string value;
        readonly public float xPos;

        public Num(string val, float x)
        {
            value = val;
            xPos = x;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)//Dodawanie znaku do równania
    {
        if(collision.CompareTag("Number"))
        {
            eqList.Add(new Num(collision.GetComponent<Number>().value, collision.transform.position.x));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//Usuwanie znaku z równania
    {
        if (collision.CompareTag("Number"))
        {
            eqList.Remove(new Num(collision.GetComponent<Number>().value, collision.transform.position.x));
        }
    }

    void CheckEquation()
    {
        List<float> xList = new List<float>();

        for (int i = 0; i < eqList.Count; i++)//Tworzenie listy pomocniczej
        {
            xList.Add(eqList[i].xPos);
        }

        xList.Sort();//Sortowanie
        List<Num> vList = new List<Num>(); // Lista posortowanych od lewej znaków równania

        for (int i = 0; i < xList.Count; i++)//Sortowanie właściewej listy przy pomocy listy pomocniczej
        {
            for (int j = 0; j < eqList.Count; j++)
            {
                if(eqList[j].xPos == xList[i])
                {
                    vList.Add(eqList[j]);
                    break;
                }
            }
        }

        for (int i = 0; i < vList.Count; i++)//Wypisywanie ciągu
        {
            Debug.Log(vList[i].value);
        }
    }

    void Start()
    {
        numbersOnTheMap = GameObject.FindGameObjectsWithTag("Number");

        for (int i = 0; i < numbersOnTheMap.Length; i++)
        {
            //Debug.Log(numbersOnTheMap[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            CheckEquation();
    }
}
