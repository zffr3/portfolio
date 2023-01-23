using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorQuest : MonoBehaviour, IInteractble
{
    [SerializeField]
    private DoorQuestStates _currentState;

    [SerializeField]
    private string _questDescriptions;

    private InteractionObject _interactionObj;

    private void Awake()
    {
        this._currentState = DoorQuestStates.QusetAwaitingOpening;
        this._interactionObj = this.GetComponent<InteractionObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this._currentState == DoorQuestStates.QuestInProgress)
        {
            if (other.GetComponent<ItemToDelivery>() != null)
            {
                this._currentState++;
                Destroy(other.gameObject);
                CloseQuest();
            }
        }
    }

    private void StartDoorQuest()
    {
        BunkerController.instance.OpenDoorQuest();
        PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(false);
        PlayerUI.Instance.ShowQuestUi(this._questDescriptions, "Yes", "Accept it");
    }

    private void CloseQuest()
    {
        BunkerController.instance.CloseDoorQuest();    
    }

    public void OnInteract(Collider sender)
    {
        if (this._currentState == DoorQuestStates.QusetAwaitingOpening)
        {
            this._currentState++;
            StartDoorQuest();
            this._interactionObj.StopInteraction();
        }
        else
        {
            return;
        }
    }
}
public enum DoorQuestStates
{
    QusetAwaitingOpening = 0,
    QuestInProgress = 1,
    QuestCompleted = 2
}
