using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Book))]
public class AutoFlip : MonoBehaviour {
    public FlipMode Mode;
    public float PageFlipTime = 1;
    public float TimeBetweenPages = 1;
    public float DelayBeforeStarting = 0;
    public bool AutoStartFlip=true;
    public Book ControledBook;
    public int AnimationFramesCount = 40;
    bool isFlipping = false;
    // Use this for initialization

    private RectTransform rt;
    private float _point;
    [SerializeField] private float _speedForPoint;
    [SerializeField] private Button _btnRead;
    [SerializeField] private Button _btnClose;
    [SerializeField] private Button _btnRight;
    [SerializeField] private Button _btnLeft;

    [SerializeField] private GameObject _leftPart;
    [SerializeField] private GameObject _rightPart;
    private Vector2 _scalePos;
    [SerializeField] private float _speedForScale;
    [SerializeField] private GameObject _contour;


    void Start()
    {
        rt = transform.GetComponent<RectTransform>();
        _point = -rt.sizeDelta.x / 4;
        rt.anchoredPosition = new Vector2(_point, 0);
        _leftPart.gameObject.SetActive(false);


        _btnClose.gameObject.SetActive(false);
        _btnRight.gameObject.SetActive(false);
        _btnLeft.gameObject.SetActive(false);
        _btnRead.gameObject.SetActive(true);


        if (!ControledBook)
            ControledBook = GetComponent<Book>();
        if (AutoStartFlip)
            StartFlipping();
        ControledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
	}
    private void Update()
    {
        CentredCover();
        ContourCover();
    }
    public void CentredCover()
    {
        float speedPoint = _speedForPoint * Time.deltaTime;
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, new Vector2(_point, 0), speedPoint);
    }
    public void ContourCover()
    {
        if(_contour.GetComponent<RectTransform>().sizeDelta.y <= 850)
            _contour.SetActive(false);
        else
            _contour.SetActive(true);

        float speedPoint = _speedForScale * Time.deltaTime;
        _contour.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(_contour.GetComponent<RectTransform>().sizeDelta, _scalePos, speedPoint);
    }
    public void CloseButton()
    {
        SceneManager.LoadScene(0);
    }
    void PageFlipped()
    {
        isFlipping = false;
    }
	public void StartFlipping()
    {
        StartCoroutine(FlipToEnd());
    }

    public void FlipRightPage()
    {
        if (isFlipping) return;
        if (ControledBook.currentPage >= ControledBook.TotalPageCount)
        {
            return;
        }

        isFlipping = true;
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl)*2 / AnimationFramesCount;
        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
    }
    public void FlipLeftPage()
    {
        if (isFlipping) return;
        if (ControledBook.currentPage <= 0)
        {
            return;
        }

        isFlipping = true;
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl) * 2 / AnimationFramesCount;
        StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
    }
    public void StartRead()
    {
        _btnClose.gameObject.SetActive(true);
        _btnRight.gameObject.SetActive(true);
        _btnLeft.gameObject.SetActive(true);
        _btnRead.gameObject.SetActive(false);

        FlipRightPage();
    }
    IEnumerator FlipToEnd()
    {
        yield return new WaitForSeconds(DelayBeforeStarting);
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2)*0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y)*0.9f;
        //y=-(h/(xl)^2)*(x-xc)^2          
        //               y         
        //               |          
        //               |          
        //               |          
        //_______________|_________________x         
        //              o|o             |
        //           o   |   o          |
        //         o     |     o        | h
        //        o      |      o       |
        //       o------xc-------o      -
        //               |<--xl-->
        //               |
        //               |
        float dx = (xl)*2 / AnimationFramesCount;
        switch (Mode)
        {
            case FlipMode.RightToLeft:
                while (ControledBook.currentPage < ControledBook.TotalPageCount)
                {
                    StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
            case FlipMode.LeftToRight:
                while (ControledBook.currentPage > 0)
                {
                    StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
        }
    }
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        ControledBook.DragRightPageToPoint(new Vector3(x, y, 0));
        if (ControledBook.currentPage == 0)
        {
            _point = 0;
            _btnRead.gameObject.SetActive(false);
            _btnClose.gameObject.SetActive(true);
            _btnRight.gameObject.SetActive(true);
            _btnLeft.gameObject.SetActive(true);
        }
        if (ControledBook.currentPage == ControledBook.TotalPageCount - 2)
        {
            _rightPart.gameObject.SetActive(false);
            _scalePos = new Vector2(1100, 800);
        }

        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookRTLToPoint(new Vector3(x, y, 0));
            
            yield return new WaitForSeconds(frameTime);
            
            x -= dx;
        }

        _leftPart.gameObject.SetActive(true);

        if (ControledBook.currentPage == 0)
        {
            _contour.SetActive(true);
            _scalePos = new Vector2(1270, 890);
        }
        if (ControledBook.currentPage == ControledBook.TotalPageCount - 2)
        {
            _point = rt.sizeDelta.x / 4;
        }

        ControledBook.ReleasePage();
    }
    IEnumerator FlipLTR(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc - xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);
        ControledBook.DragLeftPageToPoint(new Vector3(x, y, 0));
        if (ControledBook.currentPage == ControledBook.TotalPageCount)
        {
            _point = 0;
        }
        if (ControledBook.currentPage == 2)
        {
            _leftPart.gameObject.SetActive(false);
            _scalePos = new Vector2(1100, 800);
        }
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookLTRToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x += dx;
        }
        if (ControledBook.currentPage == ControledBook.TotalPageCount)
        {
            _rightPart.gameObject.SetActive(true);
            _scalePos = new Vector2(1270, 890);
        }
        if (ControledBook.currentPage == 2)
        {
            _point = -rt.sizeDelta.x / 4;
            _btnRead.gameObject.SetActive(true);
            _btnClose.gameObject.SetActive(false);
            _btnRight.gameObject.SetActive(false);
            _btnLeft.gameObject.SetActive(false);
        }
        ControledBook.ReleasePage();
    }
}
