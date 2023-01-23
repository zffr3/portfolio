using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedRooms : MonoBehaviour
{
    [SerializeField]
    private List<CursedRoom> _rooms;

    [SerializeField]
    private float _curseTimeMin;
    [SerializeField]
    private float _curseTimeMax;

    [SerializeField]
    private int _curseCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CurseRoom());
    }

    IEnumerator CurseRoom()
    {
        yield return new WaitForSecondsRealtime(Random.Range(this._curseTimeMin, this._curseTimeMax));

        CursedRoom room = this._rooms[Random.Range(0, this._rooms.Count)];
        this._rooms.Remove(room);

        if (this._rooms.Count > this._curseCount)
        {
            int seed = Random.Range(0, 100);
            if (seed <= 50)
            {
                room.CurseRoom();
            }
            else
            {
                room.FakeCurse();
            }
        }
        else
        {
            room.CurseRoom();
        }
        this._curseCount--;

        if (this._curseCount != 0)
        {
            StartCoroutine(CurseRoom());
        }
    }
}
