using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] healthBar;
    
    public Text dialouge;
     
    //Static Singleton Instance
    public static UIManager _Instance=null;
    
    //property to get instance
    public static UIManager Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (UIManager)FindObjectOfType(typeof(UIManager));
            }
            return _Instance;
        }

    }

    public void SetBarAmount(float amount)
    {
        healthBar[0].fillAmount = amount;
    }
    
    public IEnumerator CountdownToGame()
    {
        Debug.Log("Inside coroutine");
        UIManager.Instance.dialouge.text = "I GLOVE YOU";
        yield return new WaitForSeconds(1f);
        UIManager.Instance.dialouge.text = "3";
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.dialouge.text = "2";
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.dialouge.text = "1";
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.dialouge.text = "Fight";
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.dialouge.text = "";
    }

}
