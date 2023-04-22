using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using GameConsole.Exceptions;

namespace GameConsole.UI
{
    public class ConsoleUI : Window
    {
        [SerializeField] private Console _console;
        [SerializeField] private TMP_InputField _messageField;
        [SerializeField] private TMP_InputField _commandLine;
        [SerializeField] private int _maxLastCommands;
        private List<string> _messagePool = new();

        private void Awake()
        {
            _messageField.text = "";
        }

        public void ExecuteCommand(string command)
        {
            if (command == "")
                return;
            _commandLine.text = "";
            string message = "";
            try
            {
                _console.TryExecuteCommand(command);
                message = DateTime.Now.ToLongTimeString() + ": " + command;
            }
            catch(Exception exception)
            {
                message = DateTime.Now.ToLongTimeString() + ": " + exception.Message;
            }
            ShowLastMessages(message);
        }

        private void ShowLastMessages(string message)
        {
            _messagePool.Add(message);
            if (_messagePool.Count > _maxLastCommands)
            {
                _messagePool.RemoveAt(0);
            }
            RedrawLastMessages();
        }

        private void RedrawLastMessages()
        {
            _messageField.text = string.Join("\n", _messagePool);
        }
    }
}
