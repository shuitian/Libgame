using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libgame.Characters;
using Libgame.Components;
using UnityEngine;

namespace Libgame.Bridge
{
    [RequireComponent(typeof(HpComponent))]
    public class HpDieBridge : MonoBehaviour
    {
        private HpComponent _hpComponent;
        public HpComponent hpComponent
        {
            get
            {
                if (_hpComponent == null) 
                {
                    _hpComponent = GetComponent<HpComponent>();
                }
                if(_hpComponent == null)
                {
                    _hpComponent = gameObject.AddComponent<HpComponent>();
                }
                return _hpComponent;
            }
        }

        private FightCharacter _fightCharacter;
        public FightCharacter fightCharacter
        {
            get
            {
                if (_fightCharacter == null)
                {
                    _fightCharacter = GetComponent<FightCharacter>();
                }
                if (_fightCharacter == null)
                {
                    _fightCharacter = gameObject.AddComponent<FightCharacter>();
                }
                return _fightCharacter;
            }
        }

        void OnEnable()
        {
            if (hpComponent && fightCharacter)
            {
                hpComponent.AttachPointLE0CallBack((source, p_hpLost, hp) => fightCharacter.Die(source));
            }
        }

        void OnDisable()
        {
            if (hpComponent && fightCharacter)
            {
                hpComponent.DetachPointLE0CallBack((source, p_hpLost, hp) => fightCharacter.Die(source));
            }
        }
    }
}
