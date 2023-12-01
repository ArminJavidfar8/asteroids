using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private GameObject _menuPanel;
        private IEventService _eventService;

        private void Awake()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void Start()
        {
            SetButtonsListener();
            _eventService.RegisterEvent(EventTypes.OnPlayerDied, PlayerDied);
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