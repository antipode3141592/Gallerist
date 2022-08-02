using Gallerist.Data;
using Gallerist.States;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gallerist
{
    public class SchmoozeController : MonoBehaviour
    {
        GameStateMachine _gameStateMachine;
        ArtistManager _artistManager;
        PatronManager _patronManager; 

        public int ElapsedTime { get; set; }
        public int TotalSchmoozingTime => 60;    //Schmooze for sixty minutes
        public int ChatTime => 5;
        public int IntroductionTime => 10;
        public int NudgeTime => 15;

        public event EventHandler PatronUpdated;
        public event EventHandler SchmoozingCompleted;
        public event EventHandler ActionTaken;
        public event EventHandler<bool> EnableChat;
        public event EventHandler<bool> EnableNudge;
        public event EventHandler<bool> EnableIntroduction;

        public event EventHandler<ResultsArgs> ResultsReady;
        public bool ShowResults = false;

        SchmoozeState SchmoozeState;

        void Awake()
        {
            _gameStateMachine = FindObjectOfType<GameStateMachine>();
            SchmoozeState = _gameStateMachine.Schmooze;
            _artistManager = FindObjectOfType<ArtistManager>();
            _patronManager = FindObjectOfType<PatronManager>();
        }

        public void Chat()
        {
            List<ITrait> chatResult = Schmooze.Chat(_patronManager.CurrentObject);
            SchmoozeState.ElapsedTime += ChatTime;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
            string description = "\"";
            string summary = chatResult.Count >= 2 ? $"Revealed {chatResult.Count} traits" : $"Revealed {chatResult.Count} trait";
            ShowResults = true;
            if (ShowResults)
            {

                for (int i = 0; i < chatResult.Count; i++)
                {
                    description += GenerateChatResultText(chatResult[i]);
                }
                description.TrimEnd();
                description += "\"";

                ResultsReady?.Invoke(this, new ResultsArgs(
                    description: description,
                    summary: summary));
            }
            StartCoroutine(AwaitResultsClose());
        }

        string GenerateChatResultText(ITrait trait)
        {
            string description = $"I {TraitLevelDescriptions.GetDescription(trait.Value).ToLower()} art that ";
            if (trait.TraitType == TraitType.Emotive)
                
                description += $"makes me feel {trait.Name.ToLower()}.  ";
            else
                description += $"has {trait.Name.ToLower()} qualities.  ";
            return description;
        }

        static List<string> ChatResultsEmotive = new List<string>()
        {
            "I [trait.Value] makes me feel [trait.Name]",

        };

        static List<string> ChatResultsAesthetic = new List<string>()
        {
            "I [trait.Value] art that has a [trait.Name] quality."
        };

        public void Introduce()
        {
            ResultsArgs results = Schmooze.Introduce(_artistManager.Artist, _patronManager.CurrentObject);
            SchmoozeState.ElapsedTime += IntroductionTime;            
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
            ShowResults = true;
            if (ShowResults)
            {
                ResultsReady?.Invoke(this, results);
            }
            StartCoroutine(AwaitResultsClose());
        }

        IEnumerator AwaitResultsClose()
        {
            while (ShowResults)
                yield return null;
        }

        public void Nudge()
        {
            ResultsArgs results = Schmooze.Nudge(_patronManager.CurrentObject);
            SchmoozeState.ElapsedTime += NudgeTime;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
            ShowResults = true;
            if (ShowResults)
            {
                ResultsReady?.Invoke(this, results);
            }
            StartCoroutine(AwaitResultsClose());
        }

        void CheckActionEnable()
        {
            int remainingTime = SchmoozeState.TotalTime - SchmoozeState.ElapsedTime;
            EnableChat?.Invoke(this, remainingTime >= ChatTime);
            EnableIntroduction?.Invoke(this, remainingTime >= IntroductionTime);
            EnableNudge?.Invoke(this, remainingTime >= NudgeTime);
        }
    }
}