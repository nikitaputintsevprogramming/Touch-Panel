using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ResetTime : MonoBehaviour
{
    [SerializeField] private float _the—lockIsTicking;
    [SerializeField] private float _OnShitTheTimeIsReset = 0;

    [SerializeField] private float _timeForAlahAkbar;

    private void Update()
    {
        _the—lockIsTicking = Time.timeSinceLevelLoad - _OnShitTheTimeIsReset;
        if (Input.GetMouseButtonDown(0))
        {
            Reset();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Reset();
            }
        }

        if (_the—lockIsTicking > _timeForAlahAkbar)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Reset()
    {
        _OnShitTheTimeIsReset += _the—lockIsTicking;
        _the—lockIsTicking = 0f;
    }
}