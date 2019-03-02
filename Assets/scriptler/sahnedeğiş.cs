using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sahnedeğiş : MonoBehaviour {

	public void sahnedeğiştirme(int numarası)
    {
        SceneManager.LoadScene(numarası);

    }
}
