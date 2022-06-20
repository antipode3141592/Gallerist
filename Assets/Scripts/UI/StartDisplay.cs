using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.UI
{
    public class StartDisplay : MonoBehaviour
    {
        GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}