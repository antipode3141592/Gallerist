using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Gallerist.UI
{
    public class PatronsDisplay : MonoBehaviour
    {
        [SerializeField] PatronCard PatronCard;
        [SerializeField] List<ClickableImage> PatronPortraits;
        [SerializeField] Button ScrollRightButton;
        [SerializeField] Button ScrollLeftButton;
        [SerializeField] TextMeshProUGUI paginationText;
        [SerializeField] TextMeshProUGUI searchText;

        int currentPage;
        
        GameManager _gameManager;

        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            foreach (var patron in PatronPortraits)
            {
                patron.OnImageClicked += OnPatronPortraitClicked;
            }
            
        }

        void OnEnable()
        {
            currentPage = 0;
        }

        void OnPatronPortraitClicked(object sender, string e)
        {
            foreach (var image in PatronPortraits) { image.ResetBackground(); }
            if (e == string.Empty) return;
            Patron patron = _gameManager.Patrons.Find(x => x.Name == e);
            if (patron is null) return;
            //highlight patronportraitbackground
            var portrait = sender as ClickableImage;
            if (portrait is not null) portrait.HighlightBackground();
            PatronCard.LoadPatronCardData(patron: patron);

        }

        public void SetPatrons()
        {
            int pageSize = PatronPortraits.Count;
            for (int i = 0; i < pageSize; i++)
            {
                int patronIndex = i + pageSize * currentPage;
                if (_gameManager.Patrons.Count <= patronIndex) return;
                PatronPortraits[i].SetImage(
                    sprite: _gameManager.Patrons[patronIndex].Portrait, 
                    name: _gameManager.Patrons[patronIndex].Name);
            }
            PatronPortraits[0].HighlightBackground();
            paginationText.text = $"{1 + pageSize*currentPage} to {pageSize} of {_gameManager.Patrons.Count}";
            PatronCard.LoadPatronCardData(patron: _gameManager.Patrons[pageSize * currentPage]);
        }

        public void PageRight()
        {
            int _currentPage = currentPage;
            currentPage++;
            int lastPage = Mathf.CeilToInt(_gameManager.PatronPortaits.Count / PatronPortraits.Count);
            if (currentPage > lastPage) currentPage = lastPage;
            if (currentPage != _currentPage)
                SetPatrons();
        }

        public void PageLeft()
        {
            int _currentPage = currentPage;
            currentPage--;
            if (currentPage < 0) currentPage = 0;
            if (currentPage != _currentPage)
                SetPatrons();
        }
        
    }
}