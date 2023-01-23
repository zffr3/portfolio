using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUi : MonoBehaviour
{
    [SerializeField]
    private List<Image> _starsSprites;

    [SerializeField]
    private Sprite _activatedSprite;

    [SerializeField]
    private GameObject _finalPanel;

    [SerializeField]
    private List<GameObject> _finalStars;

    [SerializeField]
    private GameObject _nextLevelBtn;

    [SerializeField]
    private GameObject _bombBtn;

    [SerializeField]
    private List<GameObject> _nonFinalElements;

    [SerializeField]
    private TMP_Text _levelText;

    private void Start()
    {
        EventBus.SubscribeToEvent(EventType.STAR_TAKED, HandleStarGiven);
        EventBus.SubscribeToEvent(EventType.BULLETS_ENDED, DisplayBombBtn);
        EventBus.SubscribeToEvent(EventType.CAM_ANIMATION_ENDED, DiactivateNonFinalElementsAndStartCorutine);
        EventBus.SubscribeToEvent(EventType.LEVEL_IND_LOADED, SetLevelText);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.STAR_TAKED, HandleStarGiven);
        EventBus.UnsubscribeFromEvent(EventType.BULLETS_ENDED, DisplayBombBtn);
        EventBus.UnsubscribeFromEvent(EventType.CAM_ANIMATION_ENDED, DiactivateNonFinalElementsAndStartCorutine);
        EventBus.UnsubscribeFromEvent(EventType.LEVEL_IND_LOADED, SetLevelText);
    }

    public void CallShotEvent()
    {
        EventBus.Dispath(EventType.SHOT_BTN_PRESSED, this, this);
    }

    private void DisplayNextLevelBtn()
    {
        this._nextLevelBtn.SetActive(true);
    }

    private void DisplayBombBtn(object sender, object param)
    {
        if (this._bombBtn != null)
        {
            this._bombBtn.SetActive(true);
        }
    }

    private void HandleStarGiven(object sender, object param)
    {
        int starIndex = (int)param;

        if (starIndex >= 0 && starIndex < this._starsSprites.Count)
        {
            this._starsSprites[starIndex].sprite = this._activatedSprite;
            DisplayNextLevelBtn();
        }
    }

    private void DiactivateNonFinalElementsAndStartCorutine(object sender, object param)
    {
        for (int i = 0; i < this._nonFinalElements.Count; i++)
        {
            this._nonFinalElements[i].gameObject.SetActive(false);
        }

        StartCoroutine(AnimatedActivateStars());
    }

    IEnumerator AnimatedActivateStars()
    {
        this._finalPanel.gameObject.SetActive(true);
        for (int i = 0; i < this._starsSprites.Count; i++)
        {
            this._finalStars[i].SetActive(this._starsSprites[i].sprite == this._activatedSprite);
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    private void SetLevelText(object sender, object param)
    {
        if (this._levelText != null)
        {
            string levelNumber = ((int)param+1).ToString();
            if (!string.IsNullOrEmpty(levelNumber))
            {
                this._levelText.text = levelNumber;
            }
        }
    }
}
