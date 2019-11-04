/* Filename: HeroAnimState.cs
 * Author: Vladislav Ostrikov
 * Last modified by: Vladislav Ostrikov
 * Last modified: Nov 3, 2019
 * This script is used to simplify hero animation control.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    [System.Serializable]
    public enum HeroAnimState
    {
        IDLE, WALK, JUMP
    }
}
