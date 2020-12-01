﻿using NeoServer.Game.Common;
using NeoServer.Game.Contracts.Creatures;
using NeoServer.Game.Contracts.Items;
using NeoServer.Game.Contracts.World;
using NeoServer.Game.Creatures.Model.Monsters;
using NeoServer.Server.Events.Combat;
using NeoServer.Server.Events.Creature;
using System;
using System.Collections.Generic;

namespace NeoServer.Game.Creatures
{
    public class MonsterFactory : IMonsterFactory
    {
        private readonly IMonsterDataManager _monsterManager;
        private readonly CreatureWasBornEventHandler _creatureWasBornEventHandler;
        private readonly CreatureAttackEventHandler _creatureAttackEventHandler;
        private readonly MonsterDefendEventHandler _monsterDefendEventHandler;
        private readonly IItemFactory itemFactory;
        private readonly IPathFinder _pathFinder;

        public MonsterFactory(IMonsterDataManager monsterManager,
            CreatureWasBornEventHandler creatureWasBornEventHandler, IPathFinder pathFinder, CreatureAttackEventHandler creatureAttackEventHandler, MonsterDefendEventHandler monsterDefendEventHandler, IItemFactory itemFactory)
        {
            _monsterManager = monsterManager;
            _creatureWasBornEventHandler = creatureWasBornEventHandler;
            _pathFinder = pathFinder;
            _creatureAttackEventHandler = creatureAttackEventHandler;
            _monsterDefendEventHandler = monsterDefendEventHandler;
            this.itemFactory = itemFactory;
        }
        public IMonster Create(string name, ISpawnPoint spawn = null)
        {
            var result = _monsterManager.TryGetMonster(name, out IMonsterType monsterType);
            if (result == false)
            {
                throw new KeyNotFoundException($"Given monster name: {name} is not loaded");
            }
            var monster = new Monster(monsterType, _pathFinder.Find, spawn);

            monster.OnWasBorn += _creatureWasBornEventHandler.Execute;
            monster.OnAttackEnemy += _creatureAttackEventHandler.Execute;
            monster.OnDefende += _monsterDefendEventHandler.Execute;
            monster.OnDropLoot += AttachLootEvent;

            return monster;
        }
        public void AttachLootEvent(ICombatActor creature, ILoot loot)
        {
            if (creature is not IMonster monster) return;


            foreach (var item in loot.Items)
            {
                var attributes = new Dictionary<ItemAttribute, IConvertible>();

                if (item.Amount > 1)
                {
                    attributes.TryAdd(ItemAttribute.Count, item.Amount);
                }

                var itemToDrop = itemFactory.Create(item.ItemId, monster.Location, attributes);

                monster.Corpse?.TryAddItem(itemToDrop);
            }
        }

    }
}
