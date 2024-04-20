using System;


public interface ISpawnLocationItems
 {
  event Action<ISpawnLocationItems> requestItemSpawn;
  bool IsActive { get; set; }
 }