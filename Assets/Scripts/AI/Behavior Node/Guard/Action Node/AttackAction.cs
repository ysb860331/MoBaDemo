﻿using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Behavior_Node.Guard.Action_Node {
    [TaskCategory("guard Action")]
    public class AttackAction : Action {

        // 要攻击的敌人的CharacterMono
        public SharedGameObject target;
        private CharacterMono targetCharacterMono;

        private CharacterMono characterMono;
        // 当前攻击是否已经完成
        private bool isAttackFinish = true;

        public override void OnAwake() {
            characterMono = GetComponent<CharacterMono>();
        }

        public override void OnStart() {
            targetCharacterMono = target.Value.GetComponent<CharacterMono>();
        }

        public override TaskStatus OnUpdate() {

            if (target.Value == null) return TaskStatus.Failure;

            if (characterMono.Chasing(target.Value.transform.position, characterMono.characterModel.attackDistance)) {
                if (characterMono.Attack(ref isAttackFinish, target.Value.transform, targetCharacterMono)) {
                    return TaskStatus.Success;
                }
            }
            if (targetCharacterMono.IsCanBeAttack())
                return TaskStatus.Running;
            else
                return TaskStatus.Failure;
        }
    }
}
