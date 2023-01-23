using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemToDelivery item = other.GetComponent<ItemToDelivery>();
        if (item != null)
        {
            DeliveryQuest.Instance.EndQuest(item);
            this.gameObject.SetActive(false);
        }
        else
        {
            PlayerUI.Instance.ShowQuestUi("Find target", "Get it", "Ok");
        }    
    }
}
