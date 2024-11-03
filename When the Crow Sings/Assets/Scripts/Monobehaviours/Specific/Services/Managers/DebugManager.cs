using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Should be where all debug functionality can be controlled from.

    // Change this to toggle debug behavior easily.
    public static bool debugEnabled = true;

    // Invisible walls, dialogue triggers, loading zones, QTE triggers, etc.
    public static bool showCollidersAndTriggers = true;
}
