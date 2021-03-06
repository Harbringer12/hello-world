﻿using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.GameLogic.Helper;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using UnityEngine;
using static NitroxModel.Packets.EntityTransformUpdates;

namespace NitroxClient.Communication.Packets.Processors
{
    class EntityTransformUpdatesProcessor : ClientPacketProcessor<EntityTransformUpdates>
    {
        public override void Process(EntityTransformUpdates packet)
        {
            foreach(EntityTransformUpdate entity in packet.Updates)
            {
                Optional<GameObject> opGameObject = GuidHelper.GetObjectFrom(entity.Guid);

                if(opGameObject.IsPresent())
                {
                    GameObject gameObject = opGameObject.Get();

                    float distance = Vector3.Distance(gameObject.transform.position, entity.Position);
                    SwimBehaviour SwimBehaviour = gameObject.GetComponent<SwimBehaviour>();

                    if (distance > 5 || SwimBehaviour == null)
                    {
                        gameObject.transform.position = entity.Position;
                        gameObject.transform.rotation = entity.Rotation;
                    }
                    else
                    {
                        SwimBehaviour.SwimTo(entity.Position, 3f);                        
                    }
                }
            }
        }
    }
}
