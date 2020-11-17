﻿using Autofac;
using NeoServer.Game.Contracts;
using NeoServer.Game.Contracts.Spells;
using NeoServer.Game.Creatures.Spells;

namespace NeoServer.Server.Events
{
    public class EventSubscriber
    {
        private readonly IMap map;
        private IComponentContext container;

        public EventSubscriber(IMap map, IComponentContext container)
        {
            this.map = map;
            this.container = container;
        }

        public void AttachEvents()
        {
            map.OnCreatureAddedOnMap += (creature, cylinder) => container.Resolve<PlayerAddedOnMapEventHandler>().Execute(creature, cylinder);
            map.OnThingRemovedFromTile += container.Resolve<ThingRemovedFromTileEventHandler>().Execute;
            map.OnThingMoved += container.Resolve<CreatureMovedOnFloorEventHandler>().Execute;
            map.OnThingMovedFailed += container.Resolve<InvalidOperationEventHandler>().Execute;
            map.OnThingAddedToTile += container.Resolve<ThingAddedToTileEventHandler>().Execute;
            map.OnThingUpdatedOnTile += container.Resolve<ThingUpdatedOnTileEventHandler>().Execute;
            BaseSpell.OnSpellInvoked += container.Resolve<SpellInvokedEventHandler>().Execute;
        }
    }
}
