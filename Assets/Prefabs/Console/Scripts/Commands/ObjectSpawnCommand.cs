using System.Linq;
using GameConsole.Exceptions;
using UnityEngine;

namespace GameConsole
{
    public class ObjectSpawnCommand : ConsoleCommandMono
    {
        [SerializeField] private GameObject _spawnObject;
        [SerializeField] private Transform _spawnContainer;

        public override void ExecuteCommand(string command)
        {
            var splitCommand = command.Split(' ').ToList();
            float xPosition = 0f;
            float yPosition = 0f;
            float zPosition = 0f;
            float xRotation = 0f;
            float yRotation = 0f;
            float zRotation = 0f;

            if(splitCommand.Count >= 2)
                xPosition = float.Parse(splitCommand[1]);
            if (splitCommand.Count >= 3)
                yPosition = float.Parse(splitCommand[2]);
            if (splitCommand.Count >= 4)
                zPosition = float.Parse(splitCommand[3]);
            if (splitCommand.Count >= 5)
                xRotation = float.Parse(splitCommand[4]);
            if (splitCommand.Count >= 6)
                yRotation = float.Parse(splitCommand[5]);
            if (splitCommand.Count >= 7)
                zRotation = float.Parse(splitCommand[6]);
            if (splitCommand.Count >= 8)
                throw new CommandNotExistException(command);

            var position = new Vector3(xPosition, yPosition, zPosition);
            var eulerRotation = new Vector3(xRotation, yRotation, zRotation);
            var rotation = Quaternion.Euler(eulerRotation);
            Instantiate(_spawnObject, position, rotation, _spawnContainer);
        }
    }
}