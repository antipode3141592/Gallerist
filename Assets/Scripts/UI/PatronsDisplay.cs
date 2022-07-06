using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PatronsDisplay : MonoBehaviour
    {
        SchmoozeController _schmoozeController;
        PatronManager _patronManager;

        [SerializeField] PatronCard PatronCard;
        [SerializeField] List<ClickableImage> PatronPortraits;
        [SerializeField] Button ScrollRightButton;
        [SerializeField] Button ScrollLeftButton;
        [SerializeField] TextMeshProUGUI paginationText;

        int currentPage;

        void Awake()
        {
            _schmoozeController = FindObjectOfType<SchmoozeController>();
            _patronManager = FindObjectOfType<PatronManager>();

            foreach (var patron in PatronPortraits)
            {
                patron.OnImageClicked += OnPatronPortraitClicked;
            }
            _schmoozeController.PatronUpdated += UpdatePatronCard;
            
        }

        void OnEnable()
        {
            currentPage = 0;
        }

        void OnPatronPortraitClicked(object sender, string e)
        {
            foreach (var image in PatronPortraits) { image.ResetBackground(); }
            if (e == string.Empty) return;
            Patron patron = _patronManager.Patrons.Find(x => x.Name == e);
            if (patron is null) return;
            //highlight patronportraitbackground
            var portrait = sender as ClickableImage;
            if (portrait is not null) portrait.HighlightBackground();
            _patronManager.SelectedPatron = patron;
            PatronCard.LoadPatronCardData();

        }

        public void SetPatrons()
        {
            foreach (var image in PatronPortraits) { image.ResetBackground(); }
            int pageSize = PatronPortraits.Count;
            for (int i = 0; i < pageSize; i++)
            {
                int patronIndex = i + pageSize * currentPage;
                if (_patronManager.Patrons.Count <= patronIndex) return;
                PatronPortraits[i].SetImage(
                    sprite: _patronManager.Patrons[patronIndex].Portrait, 
                    name: _patronManager.Patrons[patronIndex].Name);
            }
            PatronPortraits[0].HighlightBackground();
            paginationText.text = $"{1 + pageSize*currentPage} to {pageSize * (1 + currentPage)} of {_patronManager.Patrons.Count}";
            _patronManager.SelectedPatron = _patronManager.Patrons.Find(x => x.Name == PatronPortraits[0].Name);
            PatronCard.LoadPatronCardData();
        }

        void UpdatePatronCard(object sender, EventArgs e)
        {
            PatronCard.LoadPatronCardData();
        }

        public void PageRight()
        {
            currentPage++;
            int lastPage = Mathf.CeilToInt(_patronManager.Patrons.Count / PatronPortraits.Count);
            if (currentPage >= lastPage) 
            { 
                currentPage = lastPage;
                return;
            }
            SetPatrons();
        }

        public void PageLeft()
        {
            currentPage--;
            if (currentPage < 0)
            {
                currentPage = 0;
                return;
            }
            SetPatrons();
        }
        
    }
}