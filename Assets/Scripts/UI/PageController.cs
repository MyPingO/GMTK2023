using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [Tooltip("This is the page that the controller will always push on start up. Leave empty for none.")]
    [SerializeField] protected Page firstDefaultPage;

    protected Stack<Page> pages;

    public Stack<Page> Pages => pages;

    protected virtual void Awake()
    {
        pages = new Stack<Page>();

        if (firstDefaultPage != null)
        {
            OpenPage(firstDefaultPage);
        }
    }

    public void OpenDefaultPage()
    {
        OpenPage(firstDefaultPage);
    }

    public void CloseDefaultPage()
    {
        CloseTopPage();
    }

    public virtual void OpenPage(Page newPage)
    {
        if (pages.Count > 0)
        {
            //Don't open the same page
            if (pages.Peek() == newPage)
                return;

            //If the new page requires the hiding of the previous page
            if (newPage.HidesPageBelow)
            {
                CloseWithoutPop();
            }
        }

        newPage.Initialize();

        //Add page to the stack
        newPage.OpenPage(this);
        pages.Push(newPage);
    }

    public virtual Page CloseTopPage()
    {
        Page poppedPage = pages.Peek();
        poppedPage.ClosePage(this);
        pages.Pop();

        if (poppedPage.HidesPageBelow)
        {
            pages.Peek().OpenPage(this);
        }

        return poppedPage;
    }

    public virtual Page CloseWithoutPop()
    {
        Page topPage = pages.Peek();
        topPage.ClosePage(this);
        return topPage;
    }
}
