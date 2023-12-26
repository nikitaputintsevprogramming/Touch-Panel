using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{

    public Book ControledBook;
    public AutoFlip autoFlip;

    private RectTransform rt;

    [SerializeField] private GameObject _leftLogo;
    [SerializeField] private GameObject _rightLogo;

    void Start()
    {
        rt = transform.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (ControledBook.currentPage <= 0)
        {
            CentredCover(-rt.sizeDelta.x / 4);
            _leftLogo.SetActive(true);
            _rightLogo.SetActive(true);
        }
        else if (ControledBook.currentPage >= ControledBook.bookPages.Length)
        {
            CentredCover(rt.sizeDelta.x / 4);
            _leftLogo.SetActive(true);
            _rightLogo.SetActive(true);
        }
        else
        {
            CentredCover(0);
            _leftLogo.SetActive(false);
            _rightLogo.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            autoFlip.FlipRightPage();
        }
    }

    //private void CentredMainCover()
    //{
    //    float point = rt.sizeDelta.x / 4;
    //    rt.anchoredPosition = new Vector2(-point, 0);
    //}
    private void CentredCover(float point)
    {
        rt.anchoredPosition = new Vector2(point, 0);
    }
}
