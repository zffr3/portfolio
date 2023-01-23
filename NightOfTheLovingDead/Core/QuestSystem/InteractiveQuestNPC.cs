using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveQuestNPC : MonoBehaviour, IInteractble
{
    [SerializeField]
    private QuestNpcType _type;

    [SerializeField]
    private string _questDescriptions;

    [SerializeField]
    private GameObject _glowZone;
    [SerializeField]
    private string _param;

    private bool _isQuestTaked;

    private SettlementMarker _marker;

    private System.Action<string,string> _startQuestWithParam;

    private void Start()
    {
        this._marker = this.GetComponent<SettlementMarker>();
    }

    private void OnDisable()
    {
        switch (this._type)
        {
            case QuestNpcType.ClearZone:
                ZoneCleanerQuest.Instance.OnQuestEnd -= UpdateQuestState;
                this._startQuestWithParam -= ZoneCleanerQuest.Instance.StartQuest;
                break;
            case QuestNpcType.Delivery:
                DeliveryQuest.Instance.OnQuestEnd -= UpdateQuestState;
                this._startQuestWithParam -= DeliveryQuest.Instance.StartQuest;
                break;
            case QuestNpcType.FindItem:
                FindQuest.Instance.OnQuestEnd -= UpdateQuestState;
                this._startQuestWithParam -= FindQuest.Instance.StartQuest;
                break;
            default:
                break;
        }
    }

    private void SubscribeToEventAndDlg()
    {
        switch (this._type)
        {
            case QuestNpcType.ClearZone:
                ZoneCleanerQuest.Instance.OnQuestEnd += UpdateQuestState;
                this._startQuestWithParam += ZoneCleanerQuest.Instance.StartQuest;
                break;
            case QuestNpcType.Delivery:
                DeliveryQuest.Instance.OnQuestEnd += UpdateQuestState;
                this._startQuestWithParam += DeliveryQuest.Instance.StartQuest;
                break;
            case QuestNpcType.FindItem:
                FindQuest.Instance.OnQuestEnd += UpdateQuestState;
                this._startQuestWithParam += FindQuest.Instance.StartQuest;
                break;
            default:
                break;
        }
    }

    private void TakeQuest(string plrName)
    {
        if (this._startQuestWithParam == null)
            SubscribeToEventAndDlg();
        PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(false);

        this._startQuestWithParam?.Invoke(plrName,this._param);

        this._isQuestTaked = true;
        this._glowZone.SetActive(false);

        PlayerUI.Instance.ShowQuestUi(this._questDescriptions,"Yes","Accept it");
    }

    private void UpdateQuestState()
    {
        this._isQuestTaked = false;
        this._glowZone.SetActive(!this._isQuestTaked);

        if (this._marker != null)
            this._marker.InteractWitgSettlement(this._type);
    }

    public void OnInteract(Collider sender)
    {
        if (!this._isQuestTaked)
        {
            PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(false);
            TakeQuest(sender.GetComponent<NetworkCharacter>().GetPlayerName());
        }
    }
}
public enum QuestNpcType
{
    ClearZone,
    Delivery,
    FindItem
}