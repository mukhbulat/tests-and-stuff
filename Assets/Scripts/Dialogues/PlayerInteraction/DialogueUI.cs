﻿using System.Collections.Generic;
using Dialogues.Data;
using Dialogues.PlayerInteraction.GameState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.PlayerInteraction
{
    public class DialogueUI : MonoBehaviour
    {
        // There are few ways to do this, I'll use just a list filled in inspector.
        [SerializeField] private List<HasDialogue> charactersWithDialogue;
        
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private TextMeshProUGUI lineText;
        [SerializeField] private List<Button> buttons;
        [SerializeField] private GameBehaviour gameBehaviour;

        private HasDialogue _currentDialogue;
        private List<DialogueLine> _currentChoices;
        private List<TextMeshProUGUI> _buttonTexts;

        private void OnEnable()
        {
            foreach (var dialogueCharacter in charactersWithDialogue)
            {
                dialogueCharacter.DialogueEnabled += OnDialogueEnabled;
                dialogueCharacter.LineChanged += OnDialogueLineChanged;
            }

            _buttonTexts = new List<TextMeshProUGUI>(buttons.Count);
            foreach (var button in buttons)
            {
                var tmp = button.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                {
                    _buttonTexts.Add(tmp);
                }

                button.gameObject.SetActive(false);
            }
            
        }

        private void OnNextLineRequested()
        {
            AnyButtonClicked();
        }

        private void OnDisable()
        {
            foreach (var dialogueCharacter in charactersWithDialogue)
            {
                dialogueCharacter.DialogueEnabled -= OnDialogueEnabled;
                dialogueCharacter.LineChanged -= OnDialogueLineChanged;
            }
            
        }

        private void OnDialogueLineChanged()
        {
            speakerText.text = _currentDialogue.CurrentLine.Speaker.Name;
            lineText.text = _currentDialogue.CurrentLine.Line;
            CreateChoices();
        }

        private void ChoiceButtonClicked(int choiceNum)
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }
            Debug.Log($"{choiceNum} is clicked");
            _currentDialogue.ChangeLine(_currentChoices[choiceNum]);
        }

        private void AnyButtonClicked()
        {
            if (_currentChoices.Count == 1)
            {
                _currentDialogue.ChangeLine(_currentChoices[0]);
            }
            else if (_currentChoices.Count == 0)
            {
                _currentDialogue.ChangeLine(null);
            }
        }

        private void OnDialogueEnabled(bool isActive, HasDialogue character)
        {
            gameBehaviour.EnableDialogue(isActive);
            _currentDialogue = character;
            if (!isActive && _currentDialogue.IsUnique)
            {
                _currentDialogue.DialogueEnabled -= OnDialogueEnabled;
                _currentDialogue.LineChanged -= OnDialogueLineChanged;
            }
            if (!isActive)
            {
                _currentDialogue = null;
                gameBehaviour.NextLineRequested -= OnNextLineRequested;
            }
            else
            {
                gameBehaviour.NextLineRequested += OnNextLineRequested;
            }
        }

        private void CreateChoices()
        {
            var choices = _currentDialogue.CurrentLine.Choices;
            if (choices.Count == 0)
            {
                _currentChoices = new List<DialogueLine>(0);
            }
            else if (choices.Count == 1)
            {
                _currentChoices = new List<DialogueLine>(1);
                foreach (var pair in choices)
                {
                    _currentChoices.Add(pair.Key);
                }
            }
            else
            {
                _currentChoices = new List<DialogueLine>(choices.Count);
                int i = 0;
                foreach (var pair in choices)
                {
                    _currentChoices.Add(pair.Key);
                    buttons[i].gameObject.SetActive(true);
                    _buttonTexts[i].text = pair.Value;
                    var choiceNum = i;
                    buttons[i].onClick.AddListener(() => ChoiceButtonClicked(choiceNum));
                    Debug.Log($"{pair.Value} for {i}");
                    i += 1;
                }
            }
        }

    }
}