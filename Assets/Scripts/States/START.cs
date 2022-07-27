using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class START : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;

        ArtManager _artManager;
        ArtistManager _artistManager;
        PatronManager _patronManager;

        public START(ArtManager artManager, ArtistManager artistManager, PatronManager patronManager)
        {
            _artManager = artManager;
            _artManager.ObjectsGenerated += OnArtPiecesGenerated;
            _artistManager = artistManager;
            _artistManager.ArtistGenerated += OnArtistGenerated;
            _patronManager = patronManager;
        }

        void OnArtPiecesGenerated(object sender, EventArgs e)
        {
            _patronManager.NewPatrons(20);
        }

        void OnArtistGenerated(object sender, EventArgs e)
        {
            _artManager.GenerateArtpieces(10);
        }

        public void OnEnter()
        {
            StateEntered?.Invoke(this, EventArgs.Empty);
            IsComplete = false;

            _artistManager.NewArtist();
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

    }
}