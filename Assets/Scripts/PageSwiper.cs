using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    private int currentPage = 1;
    
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 1;

    private void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        var difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        var percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            var newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }

            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    public void MovePage(int dPage)
    {
        if (dPage != 1 & dPage != -1)
            return;

        if (currentPage == 1 && dPage == -1 || currentPage == totalPages && dPage == 1)
            return;

        var newLocation = panelLocation;
        switch (dPage)
        {
            case 1:
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
                break;
            case -1:
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
                break;
        }

        StartCoroutine(SmoothMove(transform.position, newLocation, easing));
        panelLocation = newLocation;
    }

    private IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        var t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}