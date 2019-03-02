using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class algıla : MonoBehaviour {
    int score1;
    int high_score;       
    public GUIStyle skoru;
    public GUIStyle oyunsonu;   
    Rigidbody rb0;              //nesneleri ve rigdbodylerini dışarıdan alıyorum
    public GameObject para0;
    Rigidbody rb1;
    public GameObject para1;
    Rigidbody rb2;
    public GameObject para2;
    int hız;  
    public GUIStyle hızını;  //dışarıdan değiştirebilmek için public aldık skor ve hızın görüntü ayarları için aldım 



    int atışsayısı;


    void Start()
    {    
        hız = 600;  
        atışsayısı = 2;

        high_score = PlayerPrefs.GetInt("enyüksek");
        score1 = 0;  
    }
    public void Awake()
    {
        rb0 = para0.GetComponent<Rigidbody>();
        rb1 = para1.GetComponent<Rigidbody>();
        rb2 = para2.GetComponent<Rigidbody>();

    }
           


    public void FixedUpdate()    
    {
        if (score1 > PlayerPrefs.GetInt("enyüksek")){   //en yüksek skoru tutum bunu start kısmında tekrar çağırarak sağladım
            PlayerPrefs.SetInt("enyüksek", score1);
        }



        if (Input.GetKeyDown(KeyCode.Z)&& hız<1600)//fazla hızlanırsa kaleye tek seferde  paraların arasından geçirmeye gerek kalmadan sayı alabilir
        {
            hız = hız + 100;
            Debug.Log(hız);

        }
        if (Input.GetKeyDown(KeyCode.X)&& hız>100)
        {
            hız = hız - 100;
            Debug.Log(hız);
        }



        para0.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);       //klavyeden yön tuşlarıyla yönleri alıyorum
        float yön1 = Input.GetAxis("Horizontal");
        float yön2 = Input.GetAxis("Vertical");

        para1.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        float yön3 = Input.GetAxis("Horizontal");
        float yön4 = Input.GetAxis("Vertical");


        para2.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        float yön5 = Input.GetAxis("Horizontal");
        float yön6 = Input.GetAxis("Vertical");



        Vector3 yönler0 = new Vector3(yön1, 0, yön2);  //burada klavyedeki ok tuşlarıyla verilecek yönlerin girişini belirliyorum
        if (Input.GetKeyDown(KeyCode.J) && atışsayısı >= 0)
        {                                                               


            rb0.AddForce(yönler0 * hız);          //kuvvet uygulanacak paraya göre paranın rigidbody sine kuvveti uyguluyorum
            atışsayısı--;                         //atış hakkının birini kullandığı için azaltıyorum       

        }
        if (Input.GetKey(KeyCode.J))
        {                                                
            arasındamı0();             //arasındamı metodunu kullanarak diğer iki paranın arasında geçip geçmediğini kontrol ediyorum
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




        if (atışsayısı == -1)  //paraları birbirleri arasından geçirme hakkı kalmaadığında oyunu sonlandırıyorum
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
        GUI.Label(new Rect(0, 35,25, 50), "Skor " + score1, skoru);       //ilk iki kordinat sonraki ikisi ise boyutbelirlemede kulanılıyor
        GUI.Label(new Rect(0, 45, 25, 50), "En Yüksek Skor " + high_score, skoru);
       

        GUI.Label(new Rect(0, 15, 25, 25), "Atış Hakkın  " + atışsayısı, hızını);
        
                //paralarıj alabileceği hızlar sınrlayarak bunu ekranda da belirttim
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
         if (Input.GetKeyDown(KeyCode.Q)||atışsayısı==-1)  
            {
            GUI.Label(new Rect(20, 40, 25, 50), "SKORUN= " + score1, oyunsonu);
            GUI.Label(new Rect(20, 60, 25, 50), "EN YÜKSEK SKOR= " + high_score, oyunsonu);

            }
    }
       
   
    public void araüze()
    {
        SceneManager.LoadScene(3);//arayüz sahnesini file setting den görebilriz 3. sıraya koydum çağrıldığında giriş ekranına gidiyor
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
            atışsayısı = 2;//diğer iki paranın arasından geçirmişse atış sayısını tekrar kazanıyyor

        }  

    }

    void OnCollisionEnter(Collision obje)//nesneyi temas ettiğinde paraların belirli aralıklarda konumunu değiştiriyorum
    {
       
        if (obje.gameObject.name == "para1" || obje.gameObject.name == "para2" || obje.gameObject.name == "para3")
        {
            Debug.Log("değdi1");
            score1 = score1 + 1;
            //hepsinin konumlarını rastgeele atayarak oyuna devam edecem

            para0.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));
            para1.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));
            para2.transform.position = new Vector3(Random.Range(-25, 25), 5, Random.Range(45, 60));

            atışsayısı = 2;      //kaleye değdikten sonra atış sayısını yenilemiş oluyor


        }


    }


}
