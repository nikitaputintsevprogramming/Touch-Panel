using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    public Book ControledBook;

    [SerializeField] private UnityEngine.Video.VideoClip[] clips;

    [SerializeField] private UnityEngine.Video.VideoPlayer _video1;
    [SerializeField] private UnityEngine.Video.VideoPlayer _video2;
    [SerializeField] private UnityEngine.Video.VideoPlayer _video3;
    [SerializeField] private UnityEngine.Video.VideoPlayer _video4;



    public void Video1Enable(bool enable)
    {
        _video1.gameObject.SetActive(enable);

        if (ControledBook.currentPage == 58)
            _video1.clip = clips[0];
        //if (ControledBook.currentPage == 492)
        //    _video1.clip = clips[7];
        //if (ControledBook.currentPage == 494)
        //    _video1.clip = clips[11];
    }

    public void Video2Enable(bool enable)
    {
        _video2.gameObject.SetActive(enable);
        if (ControledBook.currentPage == 58)
            _video2.clip = clips[1];
        //if (ControledBook.currentPage == 490)
        //    _video2.clip = clips[4];
        //if (ControledBook.currentPage == 492)
        //    _video2.clip = clips[8];
        //if (ControledBook.currentPage == 494)
        //    _video2.clip = clips[12];
    }
    public void Video3Enable(bool enable)
    {
        _video3.gameObject.SetActive(enable);
        if (ControledBook.currentPage == 58)
            _video3.clip = clips[2];
        //if (ControledBook.currentPage == 492)
        //    _video3.clip = clips[9];
        //if (ControledBook.currentPage == 494)
        //    _video3.clip = clips[13];
    }
    public void Video4Enable(bool enable)
    {
        //_video4.gameObject.SetActive(enable);
        //if (ControledBook.currentPage == 488)
        //    _video4.clip = clips[2];
        //if (ControledBook.currentPage == 490)
        //    _video4.clip = clips[6];
        //if (ControledBook.currentPage == 492)
        //    _video4.clip = clips[10];
        //if (ControledBook.currentPage == 494)
        //    _video4.clip = clips[14];
    }

}