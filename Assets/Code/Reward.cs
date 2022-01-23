using System;
using UnityEngine;

namespace Rewards
{
    [Serializable]
    public class Reward
    {
        public RewardType RewardType;
        public Sprite Icon;
        public int Value;
    }
}