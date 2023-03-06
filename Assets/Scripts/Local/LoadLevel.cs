using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Text txtLevel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        txtLevel.text = $"Nivel {PlayerPrefs.GetInt("Nivel")}";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene($"Level 1");
        //SceneManager.LoadScene($"Level {PlayerPrefs.GetInt("Nivel")}");
    }
}
