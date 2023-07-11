using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Transform Load;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadingTo(string scene)
    {
        StartCoroutine(LoadingOut(scene));
    }
    IEnumerator LoadingOut(string scene)
    {
        float time = 0;
        while (time < 0.8f)
        {
            time += Time.deltaTime;
            Load.Rotate(Vector3.back, Time.deltaTime * 200);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
