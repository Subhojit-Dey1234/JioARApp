using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;

public class QuitManager : MonoBehaviour, IBackHandler, IHomeHandler, IMenuHandler
{
    [SerializeField]
    private Vector3 maxScale = Vector3.one * 0.66f;
    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }
    private void Start()
    {
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }
    public void OnBackAction()
    {
        gameObject.transform.localScale = maxScale;
        Time.timeScale = 0f;
    }

    public void OnHomeAction()
    {
        gameObject.transform.localScale = maxScale;
        Time.timeScale = 0f;
    }

    public void OnMenuAction()
    {
        gameObject.transform.localScale = maxScale;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        gameObject.transform.localScale = Vector3.zero;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
