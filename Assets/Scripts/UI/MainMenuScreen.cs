using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _scoreLabel;
        private IEventService _eventService;

        private void Awake()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void Start()
        {
            SetButtonsListener();
            _eventService.RegisterEvent(EventTypes.OnPlayerDied, PlayerDied);
            _eventService.RegisterEvent<int>(EventTypes.OnScoreUpdated, ScoreUpdated);
        }

        private void ScoreUpdated(int score)
        {
            _scoreLabel.text = score.ToString();
        }

        private void PlayerDied()
        {
            Time.timeScale = 0;
            _menuPanel.SetActive(true);
            _eventService.BroadcastEvent(EventTypes.OnLevelFinished);
        }

        private void SetButtonsListener()
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(PlayButtonClicked);
        }

        private void PlayButtonClicked()
        {
            Time.timeScale = 1;
            _eventService.BroadcastEvent(EventTypes.OnLevelStarted);
            _menuPanel.SetActive(false);
        }
    }
}