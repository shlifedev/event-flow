using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
/*
 * 
 */

namespace LD.Framework.EventFlow.Tests
{
    public class EventFlowTest
    {
        [UnityTest]
        public IEnumerator StructedDataZeroGCAndEventReceivTest()
        {
            List<GameObject> gameObjects = new List<GameObject>(100);
            for (int i = 0; i < 100; i++)
            {
                gameObjects.Add(new GameObject());
                gameObjects[^1].AddComponent<EventSubscriber>();
            }

            yield return null;  
            int beforeGC = System.GC.CollectionCount(0); 
            for (int i = 0; i < 100; i++)
            {
                var obj = gameObjects[i];
                EventFlow.Broadcast(new PrimitiveSturctEventMessage(obj, 10));
            }  
            int afterGC = System.GC.CollectionCount(0);  
            Assert.IsTrue(10 * 100 == EventSubscriber.Counter);
            Assert.IsTrue(0 == afterGC - beforeGC);
        }

        // [UnityTearDown]  
        // public IEnumerator TearDown()
        // { 
        //     Debug.Log("counter : " + EventSubscriber.Counter); 
        //     yield return null;
        //     Assert.IsTrue(0 == EventSubscriber.Counter);
        // }
    }
}