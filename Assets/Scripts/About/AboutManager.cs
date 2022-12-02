using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendEmail()
     {
        string email = "omnibit2020@gmail.com";
        string subject = MyEscapeURL("Tuile");
        string body = MyEscapeURL("Add your message here...");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

     string MyEscapeURL (string url)
    {
        return WWW.EscapeURL(url).Replace("+","%20");
    }
}
