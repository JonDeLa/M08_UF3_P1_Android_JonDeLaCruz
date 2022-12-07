using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{

    int currentCamIndex = 0;
    WebCamTexture tex;
    public RawImage display;
   public void takePhoto()
    {
        Texture2D Foto = new Texture2D(tex.width, tex.height);
        Foto.SetPixels(tex.GetPixels());
        Foto.Apply();
        byte[] bytes = Foto.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/Foto_01.png", bytes);
        StartStopCam_Clicked();
    }
    public void SwapCam_Clicked()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            currentCamIndex += 1;
            currentCamIndex %= WebCamTexture.devices.Length;

            if (tex != null)
            {
                StopWebCam();
                StartStopCam_Clicked();
            }
        }
    }

    public void StartStopCam_Clicked()
    {
        if (tex != null) // Paramos la camara si esta encendida
        {
            StopWebCam();
            
        }
        else // Encendemos la camara si esta apagada
        {
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;

            tex.Play();
           
        }
    }

    private void StopWebCam()//Funcion para apagar la camara
    {
        display.texture = null;
        tex.Stop();
        tex = null;
    }
    public void goGame()
    {
        SceneManager.LoadScene(1);
    }
    public void exitGame()
    {
        Application.Quit();
    }

}
