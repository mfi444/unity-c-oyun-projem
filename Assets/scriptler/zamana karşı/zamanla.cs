using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class zamanla : MonoBehaviour {
    int score1;
    int high_score;
    public GUIStyle renkelndir;
    public GUIStyle oyunsonu;//dışarıdan değiştirebilmek için public aldık skor ve hızın görüntü ayarları için
    Rigidbody rb0;
    public GameObject para0;
    Rigidbody rb1;
    public GameObject para1;
    Rigidbody rb2;
    public GameObject para2;
    int hız;
    float başlangışsüresi;
    public GUIStyle hızını;  //dışarıdan değiştirebilmek için public aldık skor ve hızın görüntü ayarları için
    int atışsayısı;



    // Use this for initialization
    void Start()
    {
        hız = 600;
        atışsayısı = 2;

        başlangışsüresi = 20;

        high_score = PlayerPrefs.GetInt("enyüksekzamanlı");
        score1 = 0;
    }
    public void Awake()
    {
        rb0 = para0.GetComponent<Rigidbody>();
        rb1 = para1.GetComponent<Rigidbody>();
        rb2 = para2.GetComponent<Rigidbody>();

    }



    public void FixedUpdate()       //titremeleri egellemek için kullanılıyormş
    {
        zamanlayıcı();//zamanı azaltam işlemini tekrarı burada oluyor
        if (score1 > PlayerPrefs.GetInt("enyüksekzamanlı"))
        {
            PlayerPrefs.SetInt("enyüksekzamanlı", score1);
        }



        if (Input.GetKeyDown(KeyCode.Z) && hız < 1600)//fazla hızlanırsa kaleye tek seferde sokuyor paraların arasından geçirmeye gerek kalmıyor
        {
            hız = hız + 100;
            Debug.Log(hız);

        }
        if (Input.GetKeyDown(KeyCode.X) && hız > 100)
        {
            hız = hız - 100;
            Debug.Log(hız);
        }



        para0.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);       //klavyeden yönleri alıyorum
        float yön1 = Input.GetAxis("Horizontal");
        float yön2 = Input.GetAxis("Vertical");

        para1.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        float yön3 = Input.GetAxis("Horizontal");
        float yön4 = Input.GetAxis("Vertical");


        para2.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        float yön5 = Input.GetAxis("Horizontal");
        float yön6 = Input.GetAxis("Vertical");



        Vector3 yönler0 = new Vector3(yön1, 0, yön2);    //burada klavyedeki ok tuşlarıyla verilecek yönlerin girişini belirliyorum
        if (Input.GetKeyDown(KeyCode.J) && atışsayısı >= 0)
        {


            rb0.AddForce(yönler0 * hız);     //kuvvet uygulanacak paraya göre paranın rigidbody sine kuvveti uyguluyorum
            atışsayısı--;                  //atış hakkının birini kullandığı için azaltıyorum      

        }
        if (Input.GetKey(KeyCode.J))          //arasındamı metodunu kullanarak diğer iki paranın arasında geçip geçmediğini kontrol ediyorum
        {
            arasındamı0();
        }


            //bunları diğer paralar için de tanımladım

        Vector3 yönler1 = new Vector3(yön3, 0, yön4);
        if (Input.GetKeyDown(KeyCode.K) && atışsayısı >= 0)
        {


            rb1.AddForce(yönler1 * hız);
            atışsayısı--;

        }
        if (Input.GetKey(KeyCode.K))
        {
            arasındamı1();
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




        if (atışsayısı == -1 || başlangışsüresi <= 0)
        {
            Invoke("araüze", 2.0f); //zamanla çağırdım
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            araüze();
        }



    }
  
    public void OnGUI()
    {
        GUI.Label(new Rect(0, 35, 25, 50), "Skor " + score1, hızını);       //ilk iki kordinat sonraki ikisi ise boyut
        GUI.Label(new Rect(0, 45, 25, 50), "En Yüksek Skor " + high_score, hızını);


        GUI.Label(new Rect(0, 15, 25, 25), "Atış Hakkın  " + atışsayısı, renkelndir);
        GUI.Label(new Rect(0, 25, 25, 25), "kalan süren  " + başlangışsüresi, renkelndir);

        //fazla hızlanırsa kaleye tek seferde  paraların arasından geçirmeye gerek kalmadan sayı alabilir
        if (hız == 1600)
        {
            GUI.Label(new Rect(15, 0, 25, 25), "MAX  HIZ " + hız, hızını);

        }
        if (hız == 100)
        {
            GUI.Label(new Rect(15, 0, 25, 25), "MİN HIZ " + hız, hızını);
        }
        if (hız != 100 && hız != 1600)
        {
            GUI.Label(new Rect(0, 0, 25, 25), "Hızı " + hız, hızını);
        }
        if (Input.GetKeyDown(KeyCode.Q) || atışsayısı == -1 || başlangışsüresi <= 0)         //sonlanma şartları (eğer süre bitmişsa veya atış hakkı kalmamışsa veya oyundan çıkılmak isteniyorsa(Q))
        {
            GUI.Label(new Rect(20, 40, 25, 50), "SKORUN= " + score1, oyunsonu);
            GUI.Label(new Rect(20, 60, 25, 50), "EN YÜKSEK SKOR= " + high_score, oyunsonu);

        }
    }
    void zamanlayıcı()     //zamanlamayı tuttuğum süreyi azalttığım yer burası
    {
        if (başlangışsüresi >= 0f)
        {
            başlangışsüresi -= Time.deltaTime;
        }
    }

    public void araüze()
    {
        SceneManager.LoadScene(3);//arayüz sahnesini file setting den görebilriz 3. sıraya koydum , giriş sayfasına geliyor
    }

    //burada paraların birbirleri arasından geçerek hareket ettirilip ettirilmediklerini kontrol ediyorum
    //bunu her para için ayrı ayrı kontrol ettim

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

    void OnCollisionEnter(Collision obje)//nesneye temas ettiğinde çalışıyor
    {

        if (obje.gameObject.name == "para1" || obje.gameObject.name == "para2" || obje.gameObject.name == "para3")
        {
            atışsayısı = 2;      //geçirdikten sonra atış sayısını da geri kazanıyor 
            Debug.Log("değdi 1");
            score1 = score1 + 1;
            //hepsinin konumlarını belirli aralıklarda rastgeele atayarak oyuna devam edecem

            para0.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));
            para1.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));
            para2.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));




        }


    }


}

