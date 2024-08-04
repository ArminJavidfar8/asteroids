using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
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
    public class MainMenuScreen : IInitializableMono
    {
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject _inputsPanel;
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _levelLabel;
        [SerializeField] private GameObject _wonObject;
        [SerializeField] private GameObject _loseObject;
        private IEventService _eventService;
        private ILevelService _levelService;

        public override void OnInitialized(IServiceProvider serviceProvider)
        {
            _eventService = serviceProvider.GetService<IEventService>();
            _levelService = serviceProvider.GetService<ILevelService>();
        }

        private void Start()
        {
            SetButtonsListener();
            _eventService.RegisterEvent<int>(EventTypes.OnScoreUpdated, ScoreUpdated);
            _eventService.RegisterEvent<bool>(EventTypes.OnLevelFinished, LevelFinished);
        }

        private void ScoreUpdated(int score)
        {
            _scoreLabel.text = score.ToString();
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
            _inputsPanel.SetActive(true);
        }

        private void LevelFinished(bool won)
        {
            _menuPanel.SetActive(true);
            _inputsPanel.SetActive(false);
            _wonObject.SetActive(won);
            _loseObject.SetActive(!won);
            _levelLabel.text = $"Level {_levelService.CurrentLevelNumber}";
            Time.timeScale = 0;
        }

    }
}