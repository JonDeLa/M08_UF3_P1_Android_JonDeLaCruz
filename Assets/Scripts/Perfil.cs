using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Perfil : MonoBehaviour
{
    
     GameObject fotoPrefab;
  
    private void Start()
    {
        //fotoPrefab = GameObject.Find("perfil").GetComponent<Image>();
    
    }
    private void Update()
    {
       // getImage();
    }

    // Update is called once per frame
    void getImage()
    {
        //NO SE PORQUE NO ME FUNCIONA ASIQUE SE LO PASO POR PARAMETRO

        //Sprite perfil = Resources.Load<Sprite>("Foto_01");

        //fotoPrefab.sprite = perfil;

    }
}
