using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoBDC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onclick()
    {
        SceneManager.LoadScene("breakDownCathedral");
    }

    private void OnTriggerEnter(Collider col)
    {
        SceneManager.LoadScene("breakDownCathedral");
    }
}
