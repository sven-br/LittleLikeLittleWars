using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] private float duration = 3.0f;
    [SerializeField] private GameObject background = null;
    [SerializeField] private GameObject text = null;

    private float elapsed_time = 0.0f;

    void Start()
    {
        background.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        elapsed_time += Time.deltaTime;
        float t = Mathf.Min(1.0f, elapsed_time / duration);

        float alphaBackground = Mathf.Sin(Mathf.Pow(t, 5.0f) * Mathf.PI * 0.5f);
        var backgroundMaterial = background.GetComponent<MeshRenderer>().material;
        backgroundMaterial.color = new Color(1, 1, 1, alphaBackground);

        float alphaText = Mathf.Sin(Mathf.Pow(t, 2.0f) * Mathf.PI);
        text.GetComponent<TMPro.TMP_Text>().color = new Color(1, 1, 1, alphaText);

        if (elapsed_time > duration)
        {
            backgroundMaterial.color = Color.white;
            var message = MessageProvider.GetMessage<NextSceneMessage>();
            MessageManager.SendMessage(message);
        }
    }
}
