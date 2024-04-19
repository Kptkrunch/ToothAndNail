using System;


public interface ISpawnLocationSpawnLoot
 {
  event Action<ISpawnLocationSpawnLoot> requestItemSpawn;
  bool IsActive { get; set; }
 }