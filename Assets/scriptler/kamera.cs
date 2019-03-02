using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera : MonoBehaviour
{
    public GameObject[] kameralar;

    void Start()
    {         
           for(int i = 0; i < kameralar.Length; i++)
        {
            kameralar[i].SetActive(false);               //bütün kameraları kaapatıyoruz
        }
        kameralar[3].SetActive(true);//birini aktif etttik
    }

           
    void Update()
    {
        kamerabelirle();
        

    }
    void kamerabelirle()
    {
             if (Input.GetKeyDown(KeyCode.I))
        {
            kameralar[4].SetActive(false);
            kameralar[3].SetActive(false);
            kameralar[2].SetActive(false);
            kameralar[1].SetActive(false);
            kameralar[0].SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            kameralar[4].SetActive(false);
            kameralar[3].SetActive(false);
            kameralar[2].SetActive(false);
            kameralar[1].SetActive(true);
            kameralar[0].SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            kameralar[4].SetActive(false);
            kameralar[3].SetActive(false);
            kameralar[2].SetActive(true);
            kameralar[1].SetActive(false);
            kameralar[0].SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            kameralar[4].SetActive(false);
            kameralar[3].SetActive(true);
            kameralar[2].SetActive(false);
            kameralar[1].SetActive(false);
            kameralar[0].SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            kameralar[4].SetActive(true);
            kameralar[3].SetActive(false);
            kameralar[2].SetActive(false);
            kameralar[1].SetActive(false);
            kameralar[0].SetActive(false);

        }
    }
}
