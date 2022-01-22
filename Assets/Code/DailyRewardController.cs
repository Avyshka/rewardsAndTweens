using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    public class DailyRewardController
    {
        private readonly DailyRewardView _dailyRewardView;
        private List<SlotRewardContainerView> _slots;

        private bool _isGetReward;

        public DailyRewardController(DailyRewardView dailyRewardView)
        {
            _dailyRewardView = dailyRewardView;
        }

        public void RefreshView()
        {
            InitSlots();
            _dailyRewardView.StartCoroutine(RewardsStateUpdater());
            RefreshUi();
            SubscribeButtons();
        }

        private void InitSlots()
        {
            _slots = new List<SlotRewardContainerView>();
            for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
            {
                var slot = GameObject.Instantiate(
                    _dailyRewardView.SlotRewardViewContainer,
                    _dailyRewardView.SlotRewardsContainer,
                    false
                );
                _slots.Add(slot);
            }
        }

        private IEnumerator RewardsStateUpdater()
        {
            while (true)
            {
                RefreshRewardsState();
                yield return new WaitForSeconds(1);
            }
        }

        private void RefreshRewardsState()
        {
            _isGetReward = true;
            if (_dailyRewardView.TimeGetReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;
                if (timeSpan.TotalSeconds > _dailyRewardView.TimeDeadline)
                {
                    _dailyRewardView.TimeGetReward = null;
                    _dailyRewardView.CurrentSlotInActive = 0;
                }
                else if (timeSpan.TotalSeconds < _dailyRewardView.TimeCooldown)
                {
                    _isGetReward = false;
                }
            }

            RefreshUi();
        }

        private void RefreshUi()
        {
            _dailyRewardView.GetRewardButton.interactable = _isGetReward;
            _dailyRewardView.ProgressBar.gameObject.SetActive(!_isGetReward);
            if (_isGetReward)
            {
                _dailyRewardView.TimerNewReward.text = "The reward is received today";
            }
            else
            {
                if (_dailyRewardView.TimeGetReward != null)
                {
                    var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward =
                        $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                    _dailyRewardView.TimerNewReward.text = $"Time to get the next reward: {timeGetReward}";
                    _dailyRewardView.ProgressBar.value = 1 - (float)currentClaimCooldown.TotalSeconds / _dailyRewardView.TimeCooldown;
                }
            }

            for (var i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i == _dailyRewardView.CurrentSlotInActive);
            }
        }

        private void SubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
            {
                return;
            }

            var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];
            CurrencyView.Instance.SetCurrency(reward.RewardType, reward.Value);

            _dailyRewardView.TimeGetReward = DateTime.UtcNow;
            _dailyRewardView.CurrentSlotInActive =
                (_dailyRewardView.CurrentSlotInActive + 1) % _dailyRewardView.Rewards.Count;
            
            RefreshRewardsState();
        }

        private void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}