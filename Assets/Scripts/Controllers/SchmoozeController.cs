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
            StartCoroutine(Chatting(chatResult));
        }

        IEnumerator Chatting(List<ITrait> chatResults)
        {
            string description = "\"";
            ShowResults = true;
            if (ShowResults)
            {

                for (int i = 0; i <  chatResults.Count; i++)
                {
                    description += GenerateChatResultText(chatResults[i]);
                }
                description.Trim();
                description += "\"";
                ResultsReady?.Invoke(this, new ResultsArgs(
                    description: description,
                    summary: $"Reveal {chatResults.Count} traits"));
            }
            while (ShowResults)
                yield return null;
        }

        string GenerateChatResultText(ITrait trait)
        {
            string description = "";
            description = $"I {TraitLevelDescriptions.GetDescription(trait.Value).ToLower()} art that ";
            if (trait.TraitType == TraitType.Emotive)
                description += $"makes me feel {trait.Name.ToLower()}.  ";
            else
                description += $"has {trait.Name.ToLower()} qualities.  ";
            return description;
        }

        public void Introduce()
        {
            string introductionDescription = Schmooze.Introduce(_artistManager.Artist, _patronManager.CurrentObject);
            SchmoozeState.ElapsedTime += IntroductionTime;            
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
            StartCoroutine(Introducing(introductionDescription));
        }

        IEnumerator Introducing(string description)
        {
            ShowResults = true;
            if (ShowResults)
            {
                ResultsReady?.Invoke(this, new ResultsArgs(
                    description: description,
                    summary: $""));
            }
            while (ShowResults)
                yield return null;
        }

        public void Nudge()
        {
            Schmooze.Nudge(_patronManager.CurrentObject);
            SchmoozeState.ElapsedTime += NudgeTime;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
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