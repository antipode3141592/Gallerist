using Gallerist.Data;
using Gallerist.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class SchmoozeController : MonoBehaviour
    {
        ArtistManager _artistManager;
        PatronManager _patronManager; 
        GameStatsController _gameStatsController;

        public event EventHandler PatronUpdated;
        public event EventHandler SchmoozingCompleted;

        public event EventHandler<string> ActionComplete;

        public event EventHandler<ResultsArgs> ResultsReady;
        public bool ShowResults = false;

        void Awake()
        {
            _artistManager = FindObjectOfType<ArtistManager>();
            _patronManager = FindObjectOfType<PatronManager>();
            _gameStatsController = FindObjectOfType<GameStatsController>();
        }

        public void Chat()
        {
            List<ITrait> chatResult = Schmooze.Chat(_patronManager.CurrentObject, bonus: _gameStatsController.Stats.TotalRenown);
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            
            string description = "\"";
            string summary = $"Revealed {chatResult.Count} trait{PluralHelpers.PluralS(chatResult.Count)}";
            if (chatResult.Count == 0)
                description += $"I think I'd rather look at the art for a while, thank you.";
            for (int i = 0; i < chatResult.Count; i++)
            {
                description += GenerateChatResultText(chatResult[i]);
            }
            description.TrimEnd(' ');
            description += "\"";

            ResultsReady?.Invoke(this, new ResultsArgs(
                description: description,
                summary: summary));
            ShowResults = true;
            StartCoroutine(AwaitResultsClose("Chat"));
        }

        string GenerateChatResultText(ITrait trait)
        {
            string description = "";
            if (trait.TraitType == TraitType.Emotive)
                description = ChatResultsEmotive[Random.Range(0, ChatResultsEmotive.Count)];
            else
                description = ChatResultsAesthetic[Random.Range(0, ChatResultsAesthetic.Count)];
            description = description.Replace("[trait]", $"{trait.Name.ToLower()}");
            description = description.Replace("[traitLevel]", $"{TraitLevelDescriptions.GetDescription(trait.Value).ToLower()}");
            return description;
        }

        static List<string> ChatResultsEmotive = new List<string>()
        {
            "I [traitLevel] art that expresses [trait].",
            "Art with [trait] feelings, I [traitLevel]."
        };

        static List<string> ChatResultsAesthetic = new List<string>()
        {
            "I [traitLevel] art that has a [trait] quality.",
            "I [traitLevel] art that expresses [trait].",
        };

        public void Introduce()
        {
            ResultsArgs results = Schmooze.Introduce(_artistManager.Artist, _patronManager.CurrentObject);
            _patronManager.CurrentObject.HasMetArtist = true;

            PatronUpdated?.Invoke(this, EventArgs.Empty);

            ShowResults = true;
            ResultsReady?.Invoke(this, results);

            StartCoroutine(AwaitResultsClose("Introduce"));
        }

        IEnumerator AwaitResultsClose(string actionName)
        {
            while (ShowResults)
                yield return null;
            ActionComplete?.Invoke(this, actionName);
        }

        public void Nudge()
        {
            ResultsArgs results = Schmooze.Nudge(_patronManager.CurrentObject);

            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ShowResults = true;
            ResultsReady?.Invoke(this, results);
            StartCoroutine(AwaitResultsClose("Nudge"));
        }
    }
}