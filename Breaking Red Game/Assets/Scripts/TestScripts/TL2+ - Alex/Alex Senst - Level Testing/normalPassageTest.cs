using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class normalPassageTest
{
    private GameObject passageObject;
    private GameObject fadePanelPrefab;

    [SetUp]
    public IEnumerator SetUp()
    {
        // Create dummy fade panel prefab with CanvasGroup
        fadePanelPrefab = new GameObject("FadePanel");
        fadePanelPrefab.AddComponent<CanvasGroup>();

        // Create the passage object and add the NormalPassage script
        passageObject = new GameObject("NormalPassage");
        var normalPassage = passageObject.AddComponent<NormalPassage>();

        // Manually assign the fadePanel prefab
        var fadePanelField = typeof(NormalPassage).GetField("fadePanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        fadePanelField.SetValue(normalPassage, fadePanelPrefab);

        yield return null; // Let Unity initialize the component
    }

    [Test]
    public IEnumerator FadePanel_IsInstantiated_WithCanvasGroup_AndAlphaZero()
    {
        // Manually trigger Start
        passageObject.GetComponent<NormalPassage>().Invoke("Start", 0f);
        yield return null; // Wait a frame for Start to finish

        // Check that a new fade panel was instantiated as a child
        Assert.AreEqual(1, passageObject.transform.childCount, "Fade panel was not instantiated as a child.");

        GameObject instantiatedFadePanel = passageObject.transform.GetChild(0).gameObject;

        Assert.IsNotNull(instantiatedFadePanel.GetComponent<CanvasGroup>(), "Instantiated fade panel is missing CanvasGroup.");

        var canvasGroup = instantiatedFadePanel.GetComponent<CanvasGroup>();
        Assert.AreEqual(0f, canvasGroup.alpha, 0.01f, "CanvasGroup alpha was not set to 0.");
    }

    [TearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(passageObject);
        GameObject.Destroy(fadePanelPrefab);
        yield return null;
    }
}
