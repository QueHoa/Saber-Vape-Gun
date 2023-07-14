using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Transform Load;
    public AnimationCurve curve;
    public Image img;
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
            if (time >= 0.4f)
            {
                float a = curve.Evaluate(time - 0.4f) * 2.5f;
                img.color = new Color(0f, 0f, 0f, a);
            }
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
