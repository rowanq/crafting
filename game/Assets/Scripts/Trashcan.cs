using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    public Storage storage;
    void OnMouseDown()
    {
        storage.TrashOn();
    }
}
