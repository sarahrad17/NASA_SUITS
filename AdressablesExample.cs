using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;


public class AddressablesExample : MonoBehaviour {

    GameObject myGameObject; //myGameObject is the placeholder object for whatever script is attached to 

    Acess_Database database;
    database = myGameObject.getComponent<Access_Database>();
    
    database.Start();
    

    String AssetAddress = database.getAddress();//smth like that
    

    
    Addressables.LoadAssetAsync<GameObject>(AssetAddress).Completed += OnLoadDone;
    
    }

    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        // In a production environment, you should add exception handling to catch scenarios such as a null result.
        myGameObject = obj.Result;
    }
}
