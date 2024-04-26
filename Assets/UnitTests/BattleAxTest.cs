// using JAssets.Scripts_SC.Items.Weapons;
// using JAssets.Scripts_SC.Spawners;
// using Photon.Pun;
// using UnityEngine;
// using UnityEngine.Assertions;
//
// namespace Tests
// {
//     public class BattleAxTests
//     {
//         private BattleAx _battleAx;
//         private Mock<PhotonView> _mockPhotonView;
//         private Mock<HitParticleSpawner> _mockHitParticleSpawner;
//
//         [SetUp]
//         public void SetUp()
//         {
//             GameObject battleAxObj = new GameObject();
//             _battleAx = battleAxObj.AddComponent<BattleAx>();
//             
//             GameObject photonViewObj = new GameObject();
//             _mockPhotonView = photonViewObj.AddComponent<PhotonView>();
//             
//             _battleAx.gameObject.AddComponent(_mockPhotonView.Object);
//             
//             // Assign spawner singleton instance to the mocked instance
//             _mockHitParticleSpawner = new Mock<HitParticleSpawner>();
//             HitParticleSpawner.instance = _mockHitParticleSpawner.Object;
//         }
//
//         [Test]
//         public void BattleAx_AttackWithoutSpecialTrigger_SetsAttackAnimTrueAndSpecAnimFalse()
//         {
//             // Arrange
//             _battleAx._specialTrigger = false;
//             
//             // Act
//             _battleAx.Attack();
//
//             // Assert
//             Assert.IsTrue(_battleAx.animator.GetBool(rtso.attackAnimString));
//             Assert.IsFalse(_battleAx.animator.GetBool(rtso.specAnimString));
//         }
//
//         [Test]
//         public void BattleAx_AttackWithSpecialTrigger_SetsMegaChopTrue()
//         {
//             // Arrange
//             _battleAx._specialTrigger = true;
//             
//             // Act
//             _battleAx.Attack();
//
//             // Assert
//             Assert.IsTrue(_battleAx.animator.GetBool(_battleAx.MegaChop));
//             Assert.IsFalse(_battleAx._specialTrigger);
//         }
//         
//         [Test]
//         public void BattleAx_SpecialWithoutSpecialTrigger_SetsSpecAnimTrue()
//         {
//             // Arrange
//             _battleAx._specialTrigger = false;
//             
//             // Act
//             _battleAx.Special();
//
//             // Assert
//             Assert.IsTrue(_battleAx.animator.GetBool(rtso.specAnimString));
//         }
//         
//         // Tests for the OnCollisionEnter2D function would be more complex and depend 
//         // largely on the results of the calls to other functions not defined in the BattleAx class. 
//         // It is recommended to use concepts such as mocking to create unit tests for such functions.
//     }
// }