using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class NewGameDisplay : MonoBehaviour
    {
        GameManager gameManager;

        [SerializeField] TMP_InputField galleristNameInput;
        [SerializeField] TMP_InputField galleryNameInput;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            galleristNameInput.Select();
        }

        public void StartGame()
        {
            gameManager.GalleristName = galleristNameInput.text;
            gameManager.GalleryName = galleryNameInput.text;
            gameManager.CompleteNewGame();
        }
    }
}