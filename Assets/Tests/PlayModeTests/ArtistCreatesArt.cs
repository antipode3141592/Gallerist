using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Gallerist;

public class ArtistCreatesArt: IPrebuildSetup, IPostBuildCleanup
{
    GameManager gameManager;
    ArtistManager artistManager;
    ArtManager artManager;

    public void Setup()
    {

    }

    public void Cleanup()
    {
        
    }

    
    [Test]
    public void ArtistCreatesArtSuccess()
    {
        
    }

    [Test]
    public void ArtistCreatesArtFail()
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ArtistCreatesArtWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    
}
