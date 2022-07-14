using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class Pagination : MonoBehaviour
    {
        [SerializeField] List<ClickableImage> thumbnails;
        [SerializeField] Button pageRightButton;
        [SerializeField] Button pageRightEndButton;
        [SerializeField] Button pageLeftButton;
        [SerializeField] Button pageLeftEndButton;

        [SerializeField] PageButton pageButtonPrefab;
        [SerializeField] List<PageButton> pageButtons;

        int currentPage;
        int totalPages;
        int itemCount;

        public List<ClickableImage> Thumbnails => thumbnails;
        public int PageSize => Thumbnails.Count;

        public event EventHandler<string> ThumbnailSelected;
        public event EventHandler<int> PageSelected;
        void Awake()
        {
            foreach(var thumbnail in Thumbnails)
            {
                thumbnail.OnImageClicked += OnThumbnailSelected;
            }
            foreach(var button in pageButtons)
            {
                button.OnClick += OnPageSelected;
            }
        }

        void OnEnable()
        {
            SelectPage(0);
        }

        void OnPageSelected(object sender, int e)
        {
            SelectPage(e);
        }

        void OnThumbnailSelected(object sender, string e)
        {
            //reset highlight for each thumbnail
            foreach (var thumbnail in Thumbnails) { thumbnail.ResetBackground(); }
            if (e == string.Empty) return;
            //highlight patronportraitbackground
            var clickableImage = sender as ClickableImage;
            if (clickableImage is not null) 
                clickableImage.HighlightBackground();
            ThumbnailSelected?.Invoke(this, e);
        }

        public void SetThumbnails(IEnumerable<IThumbnail> thumbnailItems)
        {
            
            totalPages = Mathf.CeilToInt((float)thumbnailItems.Count() / (float)Thumbnails.Count);
            if (Debug.isDebugBuild)
                Debug.Log($"Sprites: {thumbnailItems.Count()} / Thumbnails: {Thumbnails.Count} = Total Pages: {totalPages}");
            for (int i = 0; i < Thumbnails.Count; i++)
            {
                int index = i + PageSize * currentPage;

                if (Debug.isDebugBuild)
                    Debug.Log($"i: {i}, index: {index}");
                if (thumbnailItems.Count() <= index)
                {
                    Thumbnails[i].gameObject.SetActive(false);
                }
                else
                {
                    Thumbnails[i].gameObject.SetActive(true);
                    Thumbnails[i].SetImage(
                        sprite: thumbnailItems.ElementAt(index).Image,
                        name: thumbnailItems.ElementAt(index).Name);
                }
            }
            Thumbnails[0].ImageClicked();
            SetPages();
        }

        public void SetPages()
        {
            for (int i = 0; i < totalPages; i++)
            {
                pageButtons[i].gameObject.SetActive(true);
                pageButtons[i].SetButton(i);
            }
            for (int j = totalPages; j < pageButtons.Count; j++)
            {
                pageButtons[j].gameObject.SetActive(false);
            }
            
        }

        public void SelectPage(int page)
        {
            currentPage = page;
            foreach(PageButton button in pageButtons)
            {
                if (button.gameObject.activeInHierarchy)
                {
                    button.HighlightBackground(button.Index == page);
                }
            }
            PageSelected?.Invoke(this, page);
            
        }

        public void PageRight()
        {
            var newpage = currentPage + 1;
            
            if (newpage >= totalPages)
            {
                newpage = totalPages - 1;
            }
            SelectPage(newpage);
        }

        public void PageLeft()
        {
            var newpage = currentPage - 1;
            if (newpage < 0)
            {
                newpage = 0;
            }
            //SetPatrons();
            SelectPage(newpage);
        }

        public void PageRightEnd()
        {
            SelectPage(totalPages - 1);
        }

        public void PageLeftEnd()
        {
            SelectPage(0);
        }
    }
}