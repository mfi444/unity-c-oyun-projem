using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ikilioyunvu : MonoBehaviour {
    int score1;
    int score2;
    int oyuncu1;
    int oyuncu2;  
    public GUIStyle skoru;
    public GUIStyle oyunsonu;
    Rigidbody rb0;
    public GameObject para0;     //nesneleri ve rigidbody lerini dışarıdan aldık
    Rigidbody rb1;
    public GameObject para1;
    Rigidbody rb2;
    public GameObject para2;
    int hız;
     int atışhakkı1;
    int atışhakı2;
    public GUIStyle hızını;  //dışarıdan değiştirebilmek için public aldık skor ve hızın görüntü ayarları için



    int atışsayısı;

  
    void Start()
    {
        hız = 600;
        atışsayısı = 2;

        oyuncu1 = 1;   //1. oyuncuya hak vererek başlıyoruz
        oyuncu2 = 0;

        score1 = 0;
        score2 = 0;

        atışhakkı1=10;
        atışhakı2=10;
    }
    public void Awake()
    {
        rb0 = para0.GetComponent<Rigidbody>();
        rb1 = para1.GetComponent<Rigidbody>();
        rb2 = para2.GetComponent<Rigidbody>();

    }



    public void FixedUpdate()       //titremeleri egellemek için kullanılıyormş
    {              

        if (Input.GetKeyDown(KeyCode.Z) && hız < 1600)//fazla hızlanırsa kaleye tek seferde  paraların arasından geçirmeye gerek kalmadan gol atabiliyordu
        {
            hız = hız + 100;
            Debug.Log(hız);

        }
        if (Input.GetKeyDown(KeyCode.X) && hız > 100)
        {
            hız = hız - 100;
            Debug.Log(hız);
        }



        para0.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);       //klavyeden ok tışları yardımıyla yönleri alıyorum
        float yön1 = Input.GetAxis("Horizontal");
        float yön2 = Input.GetAxis("Vertical");

        para1.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        float yön3 = Input.GetAxis("Horizontal");
        float yön4 = Input.GetAxis("Vertical");


        para2.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        float yön5 = Input.GetAxis("Horizontal");
        float yön6 = Input.GetAxis("Vertical");


        Vector3 yönler1 = new Vector3(yön3, 0, yön4);       //burada klavyedeki ok tuşlarıyla verilecek yönlerin girişini belirliyorum
        if (Input.GetKeyDown(KeyCode.K) && atışsayısı >= 0)
        {


            rb1.AddForce(yönler1 * hız);      //kuvvet uygulanacak paraya göre paranın rigidbody sine kuvveti uyguluyorum
            atışsayısı--;            //atış hakkının birini kullandığı için azaltıyorum    

        }
        if (Input.GetKey(KeyCode.K))
        {
            arasındamı1();          //arasındamı metodunu kullanarak diğer iki paranın arasında geçip geçmediğini kontrol ediyorum
        }
                
        //bunları diğer paralar için de tanımladım
                                               


        Vector3 yönler0 = new Vector3(yön1, 0, yön2);
        if (Input.GetKeyDown(KeyCode.J) && atışsayısı >= 0)
        {


            rb0.AddForce(yönler0 * hız);
            atışsayısı--;

        }
        if (Input.GetKey(KeyCode.J))
        {
            arasındamı0();
        }


                        

        Vector3 yönler2 = new Vector3(yön5, 0, yön6);
        if (Input.GetKeyDown(KeyCode.L) && atışsayısı >= 0)
        {


            rb2.AddForce(yönler2 * hız);
            atışsayısı--;

        }

        if (Input.GetKey(KeyCode.L))
        {
            arasındamı2();
        }




        if (atışsayısı == -1 && oyuncu1 == 1)    //hangi skoru değiştireceğmi anlamak için oyuncu1 ve oyuncu 2 değişkenleri tuutum
        {
            oyuncu1 = 0;                          //OYUNCU DEĞİŞİNCE BAŞTAN BAŞLAMASI İÇİN KORDİNATLARINI YENİLEDİM
            oyuncu2 = 1;
           pozisyonbelirle();
            atışhakkı1--;//sıradaki oyuncu için atış hakkını güncelledim
            atışsayısı = 2;
        }
        if (atışsayısı == -1 && oyuncu2 == 1)  //burada  1 oyuncunun aktif olduğunu gösteriyor
            //o oyuncu sırası olmadığını gösterior
        {
            oyuncu1 = 1;
            oyuncu2 = 0;      
            pozisyonbelirle();
            atışhakı2--;
            atışsayısı = 2;

        }
        if ( atışhakkı1 == 0 && atışhakı2 == 0 )
        {
            Invoke("araüze", 2.0f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
           araüze();       //zamanla çağırdım  oyun sonu kazanaı görebilmek için
        }




    }
    void pozisyonbelirle()         //nesneyi temas ettiğinde paraların belirli aralıklarda konumunu değiştiriyorum
        //metot olarak almamın nedeni iki kişi oynadığı için eğer atış hakkı olan oyuncu atış hakkını bitirince tekrar kordinatlarını yenileyerek sırayı diğer oyunvuya veriyorum 
    {
        para0.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));
        para1.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));
        para2.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(0, 35, 25, 50), "Oyuncu1 " + score1, skoru);
        GUI.Label(new Rect(0, 25, 25, 50), "Oyuncu2 " + score2, skoru);//ilk iki kordinat sonraki ikisi ise boyut 
        GUI.Label(new Rect(0, 15, 25, 25), "Atış Hakkın  " + atışsayısı, hızını);

        if (oyuncu1 == 1)  //ekrana oyuncu 1 ve oyuncu 2 nin sıralarını yazdırıyorum
        {
           GUI.Label(new Rect(140, 15, 25, 25), "Oyuncu 1 ", oyunsonu); //middle left yaptım dışarıdan
            GUI.Label(new Rect(140, 35, 25, 25), "Atış Hakkın  " +atışhakkı1, skoru);
                                                                        //oyuncu sırasını if else kullanarak sahnede gösteriyorum
        }
        else
        {
            GUI.Label(new Rect(140, 15, 25, 25), "Oyuncu 2 " , oyunsonu);
            GUI.Label(new Rect(140, 35, 25, 25), "Atış Hakkın  " + atışhakı2, skoru);
        }


        if (hız == 1600)    //MAX VE MİN HIZLARI EKRANDA GÖSTERİYORUM
        {
            GUI.Label(new Rect(0, 0, 25, 25), "MAX  HIZ " + hız, hızını);

        }
        if (hız == 100)
        {
            GUI.Label(new Rect(0, 0, 25, 25), "MİN HIZ " + hız, hızını);
        }
       if(hız != 100 && hız != 1600)
        {
            GUI.Label(new Rect(0, 0, 25, 25), "Hızı " + hız, hızını);
        }





        if (atışhakkı1 == 0 && atışhakı2 == 0)    //oyun sonu kazananı gösteriyorum
        {
            if (score1 > score2)
            {
               GUI.Label(new Rect(20, 60, 25, 50), "Kazanan Oyuncu 1  " + score1 , oyunsonu);
            }
            else if (score2 > score1)
            {
               GUI.Label(new Rect(20, 60, 25, 50), "Kazanan Oyuncu 2 " + score1, oyunsonu);
            }
            else
            {
                GUI.Label(new Rect(20, 60, 25, 50), "DURUM BERABERE " , oyunsonu);
            }
          
        }
    }


    public void araüze()
    {
        SceneManager.LoadScene(3);//arayüz sahnesini file setting den görebilriz 3. sıraya koydum
    }

    //burada paraların birbirleri arasından geçerek hareket ettirilip ettirilmediklerini kontrol ediyorum
    //bunu her para için ayrı ayrı kontrol ettim
    //iki oyuncu olduğundan karmaşıklığı azaltmak içim metotlar kullanarak kontrol ettim
    void arasındamı2()
    {

       


        if (para0.transform.position.x <= para2.transform.position.x && para2.transform.position.x <= para1.transform.position.x &&       //x para1 küçükken oara 2 den
            para0.transform.position.z <= para2.transform.position.z && para2.transform.position.z <= para1.transform.position.z          //z para1 küçükken para 2 den
        || para0.transform.position.x >= para2.transform.position.x && para2.transform.position.x >= para1.transform.position.x &&       //x para1 büyükken para 2 den 
            para0.transform.position.z >= para2.transform.position.z && para2.transform.position.z >= para1.transform.position.z ||       //z para1 büyükken para2 den

            para0.transform.position.x <= para2.transform.position.x && para2.transform.position.x <= para1.transform.position.x &&       //x para 1 küçükken para2 den
            para0.transform.position.z >= para2.transform.position.z && para2.transform.position.z >= para1.transform.position.z           //z  para 1 büyükken paa 2 den
        || para0.transform.position.x >= para2.transform.position.x && para2.transform.position.x >= para1.transform.position.x &&        //x para1 büyükken para2 den
            para0.transform.position.z <= para2.transform.position.z && para2.transform.position.z <= para1.transform.position.z)          //z para1 küçükken para2 den

        {
            atışsayısı = 2;//geçirmişse atış sayısını tekrar kazanıyyor      
        }

    }
    void arasındamı1()
    {
       


        if (para0.transform.position.x <= para1.transform.position.x && para1.transform.position.x <= para2.transform.position.x &&        //x para1 küçükken oara 2 den
            para0.transform.position.z <= para1.transform.position.z && para1.transform.position.z <= para2.transform.position.z           //z para1 küçükken para 2 den 
        || para0.transform.position.x >= para1.transform.position.x && para1.transform.position.x >= para2.transform.position.x &&        //x para1 büyükken para 2 den
            para0.transform.position.z >= para1.transform.position.z && para1.transform.position.z >= para2.transform.position.z ||        //z para1 büyükken para2 den

            para0.transform.position.x <= para1.transform.position.x && para1.transform.position.x <= para2.transform.position.x &&        //x para 1 küçükken para2 den
            para0.transform.position.z >= para1.transform.position.z && para1.transform.position.z >= para2.transform.position.z           //z  para 1 büyükken para 2 den
        || para0.transform.position.x >= para1.transform.position.x && para1.transform.position.x >= para2.transform.position.x &&        //x para1 büyükken para2 den
            para0.transform.position.z <= para1.transform.position.z && para1.transform.position.z <= para2.transform.position.z)          //z para1 küçükken para2 den 

        {
            atışsayısı = 2;//geçirmişse atış sayısını tekrar kazanıyyor

        }

    }
    void arasındamı0()
    {
       


        if (para1.transform.position.x <= para0.transform.position.x && para0.transform.position.x <= para2.transform.position.x &&       //x para1 küçükken oara 2 den
            para1.transform.position.z <= para0.transform.position.z && para0.transform.position.z <= para2.transform.position.z           //z para1 küçükken para 2 den 
        || para1.transform.position.x >= para0.transform.position.x && para0.transform.position.x >= para2.transform.position.x &&       //x para1 büyükken para 2 den 
            para1.transform.position.z >= para0.transform.position.z && para0.transform.position.z >= para2.transform.position.z ||        //z para1 büyükken para2 den

            para1.transform.position.x <= para0.transform.position.x && para0.transform.position.x <= para2.transform.position.x &&       //x para 1 küçükken para2 den
            para1.transform.position.z >= para0.transform.position.z && para0.transform.position.z >= para2.transform.position.z           //z  para 1 büyükken paa 2 den 
        || para1.transform.position.x >= para0.transform.position.x && para0.transform.position.x >= para2.transform.position.x &&       //x para1 büyükken para2 den
            para1.transform.position.z <= para0.transform.position.z && para0.transform.position.z <= para2.transform.position.z)          //z para1 küçükken para2 den 

        {
            atışsayısı = 2;//geçirmişse atış sayısını tekrar kazanıyyor

        }

    }

    void OnCollisionEnter(Collision obje)//nesneyi temas ettiğinde çalışıyor
    {

        if (obje.gameObject.name == "para1" || obje.gameObject.name == "para2" || obje.gameObject.name == "para3")
        {      
            Debug.Log("değdi1");     //hangi oyuncu gol atmışsa skorunu arttırıyoru
            if (oyuncu1 == 1)    
            {
                score1 = score1 + 1;
            }
            if (oyuncu2 == 1)
            {
                score2 = score2 + 1;
            }

            //hepsinin konumlarını rastgeele atayarak oyuna devam edecem

            pozisyonbelirle();

            atışsayısı = 2;      //geçirdikten sonra atış sayısını da geri kazanıyor


        }


    }


}

