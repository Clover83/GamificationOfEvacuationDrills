using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

struct PositionData
{
    public UnityEngine.Vector2 position;
    public int time;
}

public class DatabaseHandler
{
    PositionData[] positionData;

    /// <summary>
    /// Store data internally to be later sent to the server using SendData()
    /// </summary>
    /// <param name="position">Position to be stored</param>
    /// <param name="time">Timestamp to be stored</param>
    void StoreData(UnityEngine.Vector2 position, int time)
    {
        PositionData data;
        data.position = position;
        data.time = time;
        positionData.Append(data);
    }

    /// <summary>
    /// Send all data already recorded using StoreData to the server and clear internal data
    /// </summary>
    bool SendData()
    {
        return false;
    }
}
