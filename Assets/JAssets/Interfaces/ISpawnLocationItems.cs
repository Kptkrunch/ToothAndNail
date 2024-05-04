using System;

namespace JAssets.Interfaces
{
 public interface ISpawnLocationItems
 {
  event Action<ISpawnLocationItems> requestItemSpawn;
  bool IsActive { get; set; }
 }
}