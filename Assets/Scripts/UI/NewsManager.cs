using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.UI
{
    public class NewsManager : MonoBehaviour
    {


        public Queue<News> NewsQueue { get; } = new();

        SalesController evaluationController;

        void Awake()
        {
            evaluationController = FindObjectOfType<SalesController>();
        }

    }

    public class News
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}