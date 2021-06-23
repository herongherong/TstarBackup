using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoNewStage : MonoBehaviour
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
        SceneManager.LoadScene("newStage");
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("newStage");
    }
}